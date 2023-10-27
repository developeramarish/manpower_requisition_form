import React, {useEffect,useState } from 'react';
import axios from "axios"; 
import './MyRequisitions.css';
import DataTableComponents from './Components/DataTableComponent';
 

import DashboardHeader from './Pages/Header';
import LeftPanel from './Pages/LeftPanel';
import SearchText from './Pages/SearchText';



 
    const MyRequisitions = () => {
      
      const [data, setData] = useState([{}]);
       
      React.useEffect(() => {
        const url = "https://localhost:7128/api/Mrfdetail/GetMrfDetails/0";
        fetch(url)
          .then((response) => {
            return response.json()
          })
          .then((json) => {
            setData(json['result'])
          })
          
          .catch((error) => console.log(error));
      }, []);
      console.log(data)
         const columns = [
        {columnName : 'MRF ID', field : 'referenceNo'},
        {columnName : 'Created By', field : 'name'},
        {columnName : 'CreatedOn', field : 'createdOnUtc'},
        {columnName : 'LastUpdated', field : 'updatedOnUtc'},
        {columnName : 'RequisionType', field : 'requisitionType'},
        {columnName : 'NoOfPositions', field : 'vacancyNo'},
        {columnName : 'ExpRequired', field : 'experience'},
        {columnName : 'SalaryRange', field : 'salary'},
        {columnName : 'Status', field : 'mrfStatus'}
        
      ]
   
    return (
      <div >
      <DashboardHeader />
      <div style={{ display: 'flex' }}>
        <LeftPanel />
       
        <div className = "bar">
          <div class="containerH">
              <label class="box" >My Requisitions</label>
              <div class="SearchText"><SearchText/></div>
          </div>
          
           
    <div className = "bar"><DataTableComponents data= {data} columns={columns} rows={5}/></div>
    </div> 
    </div>
  </div>
    );
    
     
    }
    export default MyRequisitions ;
