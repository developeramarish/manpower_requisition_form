import React, {useEffect,useState } from 'react';
import DashboardHeader from "./Header";
import LeftPanel from "./LeftPanel";
import DataTableComponents from '../Components/DataTableComponent';
import SearchText from './SearchText';
import ButtonC  from "../Components/Button";
import { Toolbar } from 'primereact/toolbar';
import { useNavigate } from 'react-router-dom';
import EmployeeDtailsEdit from './EmployeeDtailsEdit';
export default function  EmployeDetails() {
    const [data, setData] = useState([{}]);
    const [editMode, setEditMode] = useState(false);
    const [editData, setEditData] = useState()
    const navigate= useNavigate();
    //if we pass id 0 then ge get all the data otherwise we get specific data like id=1 means  
     React.useEffect(() => {
       const url = "https://localhost:7128/api/Employeedetails/GetEmployee/0";
       fetch(url)
         .then((response) => {
           return response.json()
         })
         .then((json) => {
           setData(json['result'])
         })
         
         .catch((error) => console.log(error));
     }, []);
     const [name, namechange] = useState("");
     const [email, emailchange] = useState("");
     const [roleId, setRole] = useState(null);
     const [contactNo,phonechange] = useState("");
     const [allowedByEmployeeId] = useState("1");
     const [createdByEmployeeId] = useState("1");
     const [createdOnUtc] = useState(new Date().toISOString());
     const [updatedByEmployeeId] = useState("1");
     const [isAllowed] = useState(true);
     const [updatedOnUtc] = useState(new Date().toISOString());
    const columns = [
        {columnName : 'Name', field : 'name'},
        {columnName : 'Email', field : 'email'},
        {columnName : 'contactNo', field : 'contactNo'},
        {columnName : 'Role', field : 'roleName'},
          
      ]
       
      const leftToolbarTemplate = () => {
        return (
            <div className="flex flex-wrap gap-2">
                <ButtonC label="New" icon="pi pi-plus" severity="success" onClick={()=> navigate("/EmployeDetailsCreate")} />
            </div>
        );
    };
    const [isDeleted] = useState(true); 
    const Removefunction = (id) => {
     const url = "https://localhost:7128/api/Employeedetails/GetEmployee/"+id;
      fetch(url)
        .then((response) => {
          return response.json()
        }).then((result) => {
           namechange(result.result[0].name);
          emailchange(result.result[0].email);
            phonechange(result.result[0].contactNo);
        })
      if (window.confirm('Do you want to remove?')) {
      const empdata = {name,email,contactNo,allowedByEmployeeId,createdByEmployeeId,createdOnUtc,updatedByEmployeeId,
        isAllowed,updatedOnUtc,isDeleted};
      fetch("https://localhost:7128/api/Employeedetails/Put/"+id, {
          method: "Put",
          headers: { "content-type": "application/json" },
          body: JSON.stringify(empdata)
           
        }).then((res) => {
          alert('Deleted successfully.')
    
        }).catch((err) => {
          console.log(err.message)
        })
      }
  }
  const updateData = (p_BVal) =>{
    setEditMode(p_BVal);
  }
  const LoadEdit = (id) => {
    setEditData(id);
    setEditMode(true);
}
 
    const actionBodyTemplate = (rowData) => {
      return (
          <React.Fragment>
              <ButtonC icon="pi pi-pencil" rounded outlined className="mr-2" onClick={()=>{LoadEdit(rowData.id)}} />
              <ButtonC icon="pi pi-trash" rounded outlined severity="danger" onClick={()=>{Removefunction(rowData.id) }} />
            
          </React.Fragment>
      );
  };
    
  return (
    <div >
      <DashboardHeader />
      <div style={{ display: 'flex' }}>
        <LeftPanel />
       {!editMode && 
        <div className = "bar">
          <div class="containerH">
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
    </div>
  </div>
  );
}