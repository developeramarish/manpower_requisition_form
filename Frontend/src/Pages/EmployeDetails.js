import React, {useEffect,useState } from 'react';
import RequisitionBody from "../Components/RequisitionBody";
import SearchHeader from "../Components/SearchHeader";
import DashboardHeader from "./Header";
import LeftPanel from "./LeftPanel";
import DataTableComponents from '../Components/DataTableComponent';
import SearchText from './SearchText';
import ButtonC  from "../Components/Button";
import { Toolbar } from 'primereact/toolbar';
import EmployeDetailsBody from './EmployeDetailsBody';
import { Button } from 'primereact/button';
import { Column } from 'primereact/column';
import { Dialog } from 'primereact/dialog';
import { useNavigate } from 'react-router-dom';
export default function  EmployeDetails() {
    const [data, setData] = useState([{}]);
    const [value, setValue] = useState([{}]);
    const navigate= useNavigate();
    //if we pass id 0 then ge get all the data otherwise we get specific data like id=1 means  
     React.useEffect(() => {
       const url = "https://localhost:7128/api/Employeedetails/GetEmployee";
       fetch(url)
         .then((response) => {
           return response.json()
         })
         .then((json) => {
           setData(json['result'])
         })
         
         .catch((error) => console.log(error));
     }, []);
  
    const columns = [
        {columnName : 'Name', field : 'name'},
        {columnName : 'Email', field : 'email'},
        {columnName : 'contactNo', field : 'contactNo'},
        {columnName : 'Role', field : 'roleName'},
          
      ]
       
      const leftToolbarTemplate = () => {
        return (
            <div className="flex flex-wrap gap-2">
                <ButtonC label="New" icon="pi pi-plus" severity="success" onClick={()=> navigate("/EmployeDetailsBody")} />
            </div>
        );
    };
    const Removefunction = (id) => {
      if (window.confirm('Do you want to remove?')) {
          fetch("https://localhost:7128/api/Employeedetails/" + id, {
              method: "DELETE"
          }).then((res) => {
              alert('Removed successfully.')
              window.location.reload();
          }).catch((err) => {
              console.log(err.message)
          })
      }
  }
    const actionBodyTemplate = (rowData) => {
      return (
          <React.Fragment>
              <ButtonC icon="pi pi-pencil" rounded outlined className="mr-2" onClick={()=> navigate("/EmployeDetailsBody")} />
              <ButtonC icon="pi pi-trash" rounded outlined severity="danger" onClick={()=>{Removefunction(rowData.id) }} />
             
          </React.Fragment>
      );
  };
    
  return (
    <div >
      <DashboardHeader />
      <div style={{ display: 'flex' }}>
        <LeftPanel />
       
        <div className = "bar">
          <div class="containerH">
              <label class="box" >Employee Details</label>
              <div class="SearchText"><SearchText/></div>
          </div>
          
          
          <Toolbar className="mb-4" left={leftToolbarTemplate} ></Toolbar>         
    <div className = "bar"><DataTableComponents data= {data}  columns={columns} body={actionBodyTemplate} rows={5} />
    
    </div>
    </div> 
    </div>
  </div>
  );
}