import React, { useEffect, useState,useRef } from "react";
import ButtonC from "./../components/Button";
import "../css/MyRequistionsBody.css";
import { Toolbar } from "primereact/toolbar";
import EmployeeDtailsEdit from "./EmployeeDtailsEdit";
import { Column } from "primereact/column";
import { DataTable } from "primereact/datatable";
import { API_URL } from "../constants/config";
import { getData, navigateTo } from "../constants/Utils";
import ToastMessages from "./../components/ToastMessages";
export default function EmployeDetails() {
  const [data, setData] = useState([{}]);
  const [editMode, setEditMode] = useState(false);
  const [editData, setEditData] = useState();
  const toastRef = useRef(null);
  //if we pass id 0 then ge get all the data otherwise we get specific data like id=1 means
  useEffect(() => {
    getEmployeeData();
  }, []);
  async function getEmployeeData() {
    const empData=await getData(API_URL.GET_EMPLOYEE_DETAILS+"/0");
      setData(empData.result);
    }
  // const leftToolbarTemplate = () => {
  //   return (
  //     <div className="flex flex-wrap gap-2">
  //       <ButtonC
  //         label="New"
  //         icon="pi pi-plus"
  //         severity="success"
  //         onClick={() => {
  //           /* navigate("/EmployeDetailsCreate") */}}
  //       />
  //     </div>
  //   );
  // };
  const Removefunction = async (rowData) => {
    
    if(rowData.isDeleted){
      rowData.isDeleted=false;
      getDetails();
      if (window.confirm("Do you want to Enabled?")) {
        toastRef.current.showSuccessMessage("Enbled successfully!");
       window.location.reload();
      }
    }
    else{
      rowData.isDeleted=true;
      getDetails();
       if (window.confirm("Do you want to remove?")) {
        toastRef.current.showSuccessMessage("Delete successfully!");
       window.location.reload();
      }
    }
     function getDetails(){
       fetch(API_URL.UPDATE_EMPLOYEE + rowData.id, {
        method: "Put",
        headers: { "content-type": "application/json" },
        body: JSON.stringify(rowData),
      })
    }
   
   
  
    // if (window.confirm("Do you want to remove?")) {
    // if (response.ok) {
    //   const responseData = await response.json();
    //   console.log("Response Data:", responseData);
    //   toastRef.current.showSuccessMessage("Delete successfully!");
    //   window.location.reload();
    //   setTimeout(() => {
    //      navigateTo("employee");
    //   }, 1000);
    //   }
    //   else {
    //     console.error("Request failed with status:", response.status);
    //     if (response.status === 400) {
    //       toastRef.current.showBadRequestMessage(
    //         "Bad request: " + response.url
    //       );
    //     }
    //   }
    // }

  };
  const updateData = (p_BVal) => {
    setEditMode(p_BVal);
  };
  const LoadEdit = (id) => {
    setEditData(id);
    setEditMode(true);
  };
  const actionBodyTemplate = (rowData,option) => {
    //console.log(option);
  
    return (
      <>
      {(!rowData.isDeleted ) &&(
        <>
        <React.Fragment>
       
        <ButtonC
          icon="pi pi-pencil"
          rounded
          outlined
          className="mr-2 text-white"
          onClick={() => {
            LoadEdit(rowData.id);
          }}
        />
        <ButtonC
          icon="pi pi-times"
          rounded
          outlined
          className="mr-2 text-white"
          severity="danger"
          onClick={() => {
            Removefunction(rowData);
          }}
        />
         <ToastMessages ref={toastRef} />
        
      </React.Fragment>
        </>
      )

      }
      {(rowData.isDeleted)  && (
             <ButtonC
             style={{ marginLeft:'0px'}}
             icon="pi pi-check"
             className="mr-2 text-white w-2.2"
             //label=" " 
             severity="success"
             onClick={() => {
               Removefunction(rowData);
             }}
           />
          )}
     
      </>
      
  
    );
    
    
  };
  const columns = [
		{
			field: "id",
			header: "Sr No.",
		},
		{
			field: "name",
			header: "Name",
			sortable: true,
		},
		{
			field: "email",
			header: "Email",
			sortable: true,
		},
		{
			field: "contactNo",
			header: "contactNo",
			sortable: true,
		},
		{
			field: "roleName",
			header: "Role",
			sortable: true,
		},
    {
      header: "Action",
      body:actionBodyTemplate,
			sortable: true,
		},
	];
  return (
    
		<div className="my-req">
    {(!editMode) && (
      <>
			<h3 className="my-req-title">Employee Details</h3>
			<div className="req-table">
				<DataTable
					header=""
					value={data}
					paginator={data.length > 10}
					removableSort
					rows={10}
          rowsPerPageOptions={[5, 10, 25, 50,100]} 
					scrollable
					scrollHeight="flex"
				>
					{columns.map((col,index) => (
						<Column
						key={index}
							field={col.field}
							header={col.header}
							body={col.body}
							bodyClassName={"req-col " + col.bodyClassName}
							sortable={col.sortable}
						/>
					))}
          
				</DataTable>
			</div>
      </>
      )}
      {(editMode)  && (
            <EmployeeDtailsEdit id={editData} updateData={updateData} />
          )}
		</div>
	);
   
}
