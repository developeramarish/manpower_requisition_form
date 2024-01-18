import React, { useEffect, useState,useRef } from "react";
import ButtonC from "../components/Button";
import "../styles/layout/MyRequisitionsBody.css";
import { Toolbar } from "primereact/toolbar";
import { Column } from "primereact/column";
import { DataTable } from "primereact/datatable";
import { API_URL } from "../constants/config";
import { getData, navigateTo } from "../constants/Utils";
import ToastMessages from "../components/ToastMessages";
export default function AllEmployees() {
  const [data, setData] = useState([{}]);
  const [editMode, setEditMode] = useState(false);
  const [editData, setEditData] = useState();
  const toastRef = useRef(null);
  //if we pass id 0 then ge get all the data otherwise we get specific data like id=1 means
  
  useEffect(() => {
		async function getReqData() {   
			const apiUrl = API_URL.ALL_EMPLOYEE;
			const data = await getData(apiUrl);
			setData(data);
		}
    getReqData();
	});

  console.log(data);
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
            LoadEdit(rowData.employeeId);
          }}
        />       
         <ToastMessages ref={toastRef} />
      </React.Fragment>
    );
  };
  const columns = [
		{
			field: "employeeId",
			header: "Emp ID",
		},
		{
			field: "userName",
			header: "Name",
			sortable: true,
		},
		{
			field: "email",
			header: "Email",
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
			<h3 className="my-req-title">All Employees</h3>
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
    
		</div>
	);
   
}
