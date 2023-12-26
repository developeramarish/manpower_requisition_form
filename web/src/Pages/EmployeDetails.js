import React, { useEffect, useState } from "react";
import DashboardHeader from "./Header";
import LeftPanel from "./LeftPanel";
import DataTableComponents from "./../components/DataTableComponent";
import SearchText from "./SearchText";
import ButtonC from "./../components/Button";
import { Toolbar } from "primereact/toolbar";
// import { useNavigate } from "react-router-dom";
import EmployeeDtailsEdit from "./EmployeeDtailsEdit";
import "../styles/layout/MyRequisitions.css";
export default function EmployeDetails() {
  const [data, setData] = useState([{}]);
  const [editMode, setEditMode] = useState(false);
  const [editData, setEditData] = useState();
  // const navigate = useNavigate();
  //if we pass id 0 then ge get all the data otherwise we get specific data like id=1 means
  React.useEffect(() => {
    const url = "https://localhost:7128/api/Employeedetails/GetEmployee/0";
    fetch(url)
      .then((response) => {
        return response.json();
      })
      .then((json) => {
        setData(json["result"]);
      })

      .catch((error) => console.log(error));
  }, []);

  const columns = [
    { columnName: "Sr No.", field: "id" },
    { columnName: "Name", field: "name" },
    { columnName: "Email", field: "email" },
    { columnName: "contactNo", field: "contactNo" },
    { columnName: "Role", field: "roleName" },
  ];
  const leftToolbarTemplate = () => {
    return (
      <div className="flex flex-wrap gap-2">
        <ButtonC
          label="New"
          icon="pi pi-plus"
          severity="success"
          onClick={() => {
            /* navigate("/EmployeDetailsCreate") */}}
        />
      </div>
    );
  };
  const [isDeleted] = useState(true);

  const Removefunction = (
    id,
    name,
    email,
    contactNo,
    employeeCode,
    isAllowed,
    allowedByEmployeeId,
    createdByEmployeeId,
    updatedByEmployeeId,
    roleId
  ) => {
    const empdata = {
      isDeleted,
      name,
      email,
      contactNo,
      employeeCode,
      isAllowed,
      allowedByEmployeeId,
      createdByEmployeeId,
      updatedByEmployeeId,
      roleId,
    };

    if (window.confirm("Do you want to remove?")) {
      fetch("https://localhost:7128/api/Employeedetails/Put/" + id, {
        method: "Put",
        headers: { "content-type": "application/json" },
        body: JSON.stringify(empdata),
      })
        .then((res) => {
          alert("Deleted successfully.");
          var oData = data.filter((row) => {
            return row.id !== id;
          });
          setData(oData);
        })
        .catch((err) => {
          console.log(err.message);
        });
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
            Removefunction(
              rowData.id,
              rowData.name,
              rowData.email,
              rowData.contactNo,
              rowData.employeeCode,
              rowData.isAllowed,
              rowData.allowedByEmployeeId,
              rowData.createdByEmployeeId,
              rowData.updatedByEmployeeId,
              rowData.roleId
            );
          }}
        />
      </React.Fragment>
    );
  };

  return (
    <div>
      <div>
        <DashboardHeader />
      </div>
      <div style={{ display: "flex" }}>
        <div className=" ">
          <LeftPanel />
        </div>
        <div className="MyResume">
          {!editMode && (
            <div >
              <div >
                <h3 className="text-black-alpha-90  text-2xl font-bold  m-4">
                  Employee Details
                </h3>
                {/* <label class="box" >Employee Details</label> */}
                {/* <div class="SearchText"><SearchText/></div> */}
              </div>
              <Toolbar className="m-4" left={leftToolbarTemplate}></Toolbar>
              <div className="m-4">
                <DataTableComponents
                  data={data}
                  scrollable            
                 columns={columns}
                  body={actionBodyTemplate}
                  rows={10}
                />
              </div>
            </div>
          )}
          {editMode && (
            <EmployeeDtailsEdit id={editData} updateData={updateData} />
          )}
        </div>
      </div>

      {/* <DashboardHeader />
      <div style={{ display: 'flex' }}>
        <LeftPanel />
       {!editMode && 
        <div className = "bar">
          <div className="containerH">
              <label class="box" >Employee Details</label>
              <div class="SearchText"><SearchText/></div>
          </div>
        <Toolbar className="mb-4" left={leftToolbarTemplate} ></Toolbar>         
          <div className = "bar"><DataTableComponents data= {data}  columns={columns} body={actionBodyTemplate} rows={5} />
    
         </div>
      </div>  
      }
      {editMode && <EmployeeDtailsEdit id={editData} updateData = {updateData}/>

      }
    </div> */}
    </div>
  );
}
