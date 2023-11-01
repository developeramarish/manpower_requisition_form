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
export default function  EmployeDetails() {
    const [data, setData] = useState([{}]);
    const [value, setValue] = useState([{}]);
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
     useEffect(() => {
        deleteData();
        }, []);
    const deleteData = () => {
        const apiUrl = `https://localhost:7128/api/Role`;
        fetch(apiUrl)
                .then(response => response.json())
                .then(responseData => {
                  if (Array.isArray(responseData.result)) {
                    const data = responseData.result;
                    
                    setValue(data);
                  } else {
                    console.error('API response result is not an array:', responseData);
                  }
                })
                .catch(error => {
                  console.error('Fetch error:', error);
                });
       
      };
    const columns = [
        {columnName : 'Name', field : 'name'},
        {columnName : 'Email', field : 'email'},
        {columnName : 'contactNo', field : 'contactNo'},
        {columnName : 'Role', field : 'roleName'},
          
      ]
      const openNew = () => {
        EmployeDetailsBody();
    };
      const leftToolbarTemplate = () => {
        return (
            <div className="flex flex-wrap gap-2">
                <ButtonC label="New" icon="pi pi-plus" severity="success" onClick={openNew} />
            </div>
        );
    };
    
  return (
    // <>
    //   <DashboardHeader />
    //   <div className="flex bg-gray-200">
    //     <LeftPanel />
    //     <div className="flex flex-column gap-2 w-full p-3 py-2 h-full ">
    //     <div className="flex flex-row align-items-center h-3rem w-full px-2">
    //   <h3 className="text-black-alpha-90 mr-auto text-xl ">Employee Details</h3>
    //        </div>
    //         <MyRequisitions/>
    //     </div>
    //   </div>
    // </>
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
    <div className = "bar"><DataTableComponents data= {data}  columns={columns} rows={5} />
    
    </div>
    </div> 
    </div>
  </div>
  );
}