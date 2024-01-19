import React, { useEffect, useState,useRef } from "react";
import ButtonC from "./../components/Button";
import "../css/MyRequistionsBody.css";
import { Toolbar } from "primereact/toolbar";
import EmployeeDtailsEdit from "./EmployeeDtailsEdit";
import { Column } from "primereact/column";
import { DataTable } from "primereact/datatable";
import { API_URL } from "../constants/config";
import { navigateTo } from "../constants/Utils";
import ToastMessages from "./../components/ToastMessages";
export default function EmployeDetails() {
  const [data, setData] = useState([{}]);
  const [editMode, setEditMode] = useState(false);
  const [editData, setEditData] = useState();
  const toastRef = useRef(null);
  //if we pass id 0 then ge get all the data otherwise we get specific data like id=1 means
  React.useEffect(() => {
    const url = API_URL.GET_EMPLOYEE_DETAILS+"/0";
    fetch(url)
      .then((response) => {
        return response.json();
      })
      .then((json) => {
        setData(json["result"]);
      })

      .catch((error) => console.log(error));
  }, []);

 
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
    rowData.isDeleted=true;
    if (window.confirm("Do you want to remove?")) {
    const response = await fetch(API_URL.UPDATE_EMPLOYEE + rowData.id, {
      method: "Put",
      headers: { "content-type": "application/json" },
      body: JSON.stringify(rowData),
    })
    
    if (response.ok) {
      const responseData = await response.json();
      console.log("Response Data:", responseData);
      toastRef.current.showSuccessMessage("Delete successfully!");
      var oData = data.filter((row) => {
                  return row.id !== rowData.id;
                });
                setData(oData);
      setTimeout(() => {
         navigateTo("employee");
      }, 1000);
      }
      else {
        console.error("Request failed with status:", response.status);
        if (response.status === 400) {
          toastRef.current.showBadRequestMessage(
            "Bad request: " + response.url
          );
        }
      }
    }

  };
  const updateData = (p_BVal) => {
    setEditMode(p_BVal);
  };
  const LoadEdit = (id) => {
    setEditData(id);
    setEditMode(true);
  };
  const actionBodyTemplate = (rowData) => {
    return (
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
          icon="pi pi-trash"
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
    );
  };
  const columns = [
		{
			field: "id",
			header: "Sr No.",
			//bodyClassName: "ref-col",
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
