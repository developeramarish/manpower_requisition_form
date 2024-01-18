import React, { useEffect, useState,useRef } from "react";
import ButtonC from "../components/Button";
import "../styles/layout/MyRequisitionsBody.css";
import { Toolbar } from "primereact/toolbar";
import { Column } from "primereact/column";
import { DataTable } from "primereact/datatable";
import { API_URL } from "../constants/config";
import { getData, navigateTo } from "../constants/Utils";
import ToastMessages from "../components/ToastMessages";
import DropdownComponent from "../components/Dropdown";
export default function AllEmployees() {
  const [data, setData] = useState([{}]);
  const [editMode, setEditMode] = useState(false);
  const [roleId, setRole] = useState({});
  const [roleOptions, roleOptionchange] = useState([]);
  const fetchData = () => {
    const apiUrl = API_URL.GET_ROLE;
    fetch(apiUrl)
      .then(response => response.json())
      .then(responseData => {
        if (Array.isArray(responseData.result)) {
          const data = responseData.result;
          console.log(data);
          const options = data.map(x => { return { value: x.id,  roleName: x.name } })
          roleOptionchange(options);
        } else {
          console.error('API response result is not an array:', responseData);
        }
      })
      .catch(error => {
        console.error('Fetch error:', error);
      });

  };
  const [editData, setEditData] = useState();
  const toastRef = useRef(null);
  //if we pass id 0 then ge get all the data otherwise we get specific data like id=1 means
  
  useEffect(() => {
		async function getReqData() {   
			const apiUrl = API_URL.ALL_EMPLOYEE;
			const data = await getData(apiUrl);
			setData(data);
		}
    fetchData();
    getReqData();
	});
  
  const updateData = (p_BVal) => {
    setEditMode(p_BVal);
  };
  const LoadEdit = (id) => {
    setEditData(id);
    setEditMode(true);
  };
  const actionBodyTemplate = () => {
    return (
        <DropdownComponent
             optionLabel="roleName"
             optionValue="value"
             value={roleId}
             type="roleId"
             options={roleOptions}
              onChange={e => {
                console.log(e.target)
                setRole(e.target.value)
              }}
            />
    
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
      header: "Role",
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
