import React, { useEffect, useState,useRef } from "react";
import ButtonC from "../components/Button";
import { Button } from "primereact/button";
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
  const [roleId, setRoleId] = useState([]);
  const [saveBttn, setSaveBttn] = useState([]);
  
  const [roleOptions, roleOptionchange] = useState([]);
 
  const [editData, setEditData] = useState();
  const toastRef = useRef(null);
  //if we pass id 0 then ge get all the data otherwise we get specific data like id=1 means
  
  useEffect(() => {
    const fetchData = () => {
      const apiUrl = API_URL.GET_ROLE;
      fetch(apiUrl)
        .then(response => response.json())
        .then(responseData => {
          if (Array.isArray(responseData.result)) {
            const data = responseData.result;
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
		async function getReqData() {   
			const apiUrl = API_URL.ALL_EMPLOYEE;
			const data = await getData(apiUrl);
			setData(data);

      let arr = new Array(data.length).fill(false);
      let roleArr = new Array(data.length).fill(0);
      setSaveBttn(arr);
      setRoleId(roleArr);
		}
    fetchData();
    getReqData();
	}, []);
  
  const updateData = (p_BVal) => {
    setEditMode(p_BVal);
  };
  const LoadEdit = (id) => {
    setEditData(id);
    setEditMode(true);
  };
  const roleBodyTemplate = (rowData, options) => {
    const handleDropdownChange = (e) => {
      let roleIdCopy = [...roleId];
      let sv = [...saveBttn];
      sv[options.rowIndex] = true;
      setSaveBttn(sv);
      roleIdCopy[options.rowIndex] = e.target.value;
      setRoleId(roleIdCopy);
  };

    return (
        <DropdownComponent
             optionLabel="roleName"
             optionValue="value"
             value={roleId[options.rowIndex]}
             type="roleId"
             options={roleOptions}
              onChange={handleDropdownChange}
            />
    
    );
  };

  const actionBodyTemplate = (interview, options) => {
    const onClickHandleSave = () => {
      //  update(interview);
        let sv = [...saveBttn];
        sv[options.rowIndex] = false;
        setSaveBttn(sv);
      };
      if (saveBttn[options.rowIndex]) {
        return <Button icon="pi pi-save "   className="action_btn" onClick={onClickHandleSave} />;
      }
      return <Button icon="pi pi-save" className="action_btn" disabled />;
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
      body:roleBodyTemplate,
			sortable: true,
		},
    {
      header: "Action",
      body: actionBodyTemplate,
      bodyClassName: "int-edit-col",
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
