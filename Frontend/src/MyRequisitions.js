import React, { useState } from 'react';
import './MyRequisitions.css';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { DataViewLayoutOptions } from 'primereact/dataview';
import DashboardHeader from './Pages/Header';
import LeftPanel from './Pages/LeftPanel';
import SearchText from './Pages/SearchText';
    const MyRequisitions = () => {
        // Dummy data
        const [data, setData] = useState([
          { MRFID: '01/MUM/CFR/JAN/15/003', CreatedBy: '03-05-2023',CreatedOn:'03-05-2023',LastUpdated: '01-05-2023',RequisionType:
        'New',NoOfPositions:2,ExpRequired:2,SalaryRange:'2-4 LPA',Status:'Sent'},
        { MRFID:  '02/MUM/CFR/JAN/15/003', CreatedBy: '01-05-2023',CreatedOn:'03-05-2023',LastUpdated: '03-03-2023',RequisionType:
        'ReNew',NoOfPositions:2,ExpRequired:2,SalaryRange:'2-4 LPA',Status:'Draft'},
        { MRFID:  '02/MUM/CFR/JAN/15/005', CreatedBy: '03-05-2023',CreatedOn:'03-05-2023',LastUpdated: '03-05-2023',RequisionType:
        'ReNew',NoOfPositions:2,ExpRequired:2,SalaryRange:'2-4 LPA',Status:'Draft'},
        { MRFID:  '05/MUM/CFR/JAN/15/003', CreatedBy: '03-05-2023',CreatedOn:'03-05-2023',LastUpdated: '03-04-2023',RequisionType:
        'New',NoOfPositions:2,ExpRequired:2,SalaryRange:'2-4 LPA',Status:'Draft'},
        { MRFID:  '02/MUM/CFR/JAN/15/003', CreatedBy: '03-05-2023',CreatedOn:'03-05-2023',LastUpdated: '03-05-2023',RequisionType:
        'ReNew',NoOfPositions:2,ExpRequired:2,SalaryRange:'2-4 LPA',Status:'Draft'},
        { MRFID:  '06/MUM/CFR/JAN/15/003', CreatedBy: '03-05-2023',CreatedOn:'03-05-2023',LastUpdated: '03-05-2023',RequisionType:
        'ReNew',NoOfPositions:2,ExpRequired:2,SalaryRange:'2-4 LPA',Status:'Draft'},
        { MRFID:  '02/MUM/CFR/JAN/15/003', CreatedBy: '03-05-2023',CreatedOn:'03-05-2023',LastUpdated: '03-05-2023',RequisionType:
        'ReNew',NoOfPositions:2,ExpRequired:2,SalaryRange:'2-4 LPA',Status:'Draft'},
        { MRFID:  '02/MUM/CFR/JAN/15/003', CreatedBy: '03-05-2023',CreatedOn:'03-05-2023',LastUpdated: '03-05-2023',RequisionType:
        'New',NoOfPositions:2,ExpRequired:2,SalaryRange:'2-4 LPA',Status:'Draft'},
        { MRFID:  '02/MUM/CFR/JAN/15/003', CreatedBy: '03-05-2023',CreatedOn:'03-05-2023',LastUpdated: '03-05-2023',RequisionType:
        'New',NoOfPositions:2,ExpRequired:2,SalaryRange:'2-4 LPA',Status:'Draft'},
        { MRFID:  '02/MUM/CFR/JAN/15/003', CreatedBy: '03-05-2023',CreatedOn:'03-05-2023',LastUpdated: '03-05-2023',RequisionType:
        'New',NoOfPositions:2,ExpRequired:2,SalaryRange:'2-4 LPA',Status:'Draft'}
          // Add more dummy data as needed
        ]);


   
    
    
    return(
      <div>
      <DashboardHeader />
      <div style={{ display: 'flex' }}>
        <LeftPanel />
        <div className="MyDashboard">
        <div className="containerH">
        <div className="box">
       <label>MY REQUISITIONS</label>
       </div>
       <SearchText/>
       </div>
        <div className = "bar">
            
            <DataTable value={data} paginator rows={5} rowsPerPageOptions={[5, 10, 25, 50]} tableStyle={{ minWidth: '80rem' }} >
    <Column field="MRFID" header="MRF ID" style={{ width: '20%'}} ></Column>
    <Column field="CreatedBy" header="Created By"sortable style={{ width: '25%' }}></Column>
    <Column field="CreatedOn" header="Created On"sortable style={{ width: '25%' }}></Column>
    <Column field="LastUpdated" header="Last Updated"sortable style={{ width: '25%' }}></Column>
    <Column field="RequisionType" header="Requision Type"sortable style={{ width: '25%' }}></Column>
    <Column field="NoOfPositions" header="No.of positions"sortable style={{ width: '25%' }}></Column>
    <Column field="ExpRequired" header="Exp Required"sortable style={{ width: '25%' }}></Column>
    <Column field="SalaryRange" header="Salary Range"sortable style={{ width: '25%' }}></Column>
    <Column field="Status" header="Status"sortable style={{ width: '25  %' }}></Column>
    </DataTable>
    </div>
      </div>
    </div>
 
        </div>
        
    );
    }
    export default MyRequisitions ;

