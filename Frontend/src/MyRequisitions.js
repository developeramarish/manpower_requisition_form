import React, { useState } from 'react';
import './MyRequisitions.css';
import { ReactDOM } from 'react';
import { variables } from './Variables.js';
import {AddDepModal} from './AddDepModal';
import {EditDepModal} from './EditDepModal';

// import "primereact/resources/themes/bootstrap4-light-blue/themes.css"
// import "primereact/resources/primereact.min.css"
// import "primeicons/primeicons.css"
// import "primeflex/primeflex.css"


import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { DataViewLayoutOptions } from 'primereact/dataview';

 
    const MyRequisitions = () => {
        // Dummy data
        const [data, setData] = useState([
          { MRFID: 1, CreatedBy: '03-05-2023',CreatedOn:'03-05-2023',LastUpdated: '03-05-2023',RequisionType:
        'New',NoOfPositions:2,ExpRequired:2,SalaryRange:'2-4 LPA',Status:'Draft'},
        { MRFID: 1, CreatedBy: '03-05-2023',CreatedOn:'03-05-2023',LastUpdated: '03-05-2023',RequisionType:
        'New',NoOfPositions:2,ExpRequired:2,SalaryRange:'2-4 LPA',Status:'Draft'},
        { MRFID: 1, CreatedBy: '03-05-2023',CreatedOn:'03-05-2023',LastUpdated: '03-05-2023',RequisionType:
        'New',NoOfPositions:2,ExpRequired:2,SalaryRange:'2-4 LPA',Status:'Draft'},
        { MRFID: 1, CreatedBy: '03-05-2023',CreatedOn:'03-05-2023',LastUpdated: '03-05-2023',RequisionType:
        'New',NoOfPositions:2,ExpRequired:2,SalaryRange:'2-4 LPA',Status:'Draft'},
        { MRFID: 1, CreatedBy: '03-05-2023',CreatedOn:'03-05-2023',LastUpdated: '03-05-2023',RequisionType:
        'New',NoOfPositions:2,ExpRequired:2,SalaryRange:'2-4 LPA',Status:'Draft'},
        { MRFID: 1, CreatedBy: '03-05-2023',CreatedOn:'03-05-2023',LastUpdated: '03-05-2023',RequisionType:
        'New',NoOfPositions:2,ExpRequired:2,SalaryRange:'2-4 LPA',Status:'Draft'},
        { MRFID: 1, CreatedBy: '03-05-2023',CreatedOn:'03-05-2023',LastUpdated: '03-05-2023',RequisionType:
        'New',NoOfPositions:2,ExpRequired:2,SalaryRange:'2-4 LPA',Status:'Draft'},
        { MRFID: 1, CreatedBy: '03-05-2023',CreatedOn:'03-05-2023',LastUpdated: '03-05-2023',RequisionType:
        'New',NoOfPositions:2,ExpRequired:2,SalaryRange:'2-4 LPA',Status:'Draft'},
        { MRFID: 1, CreatedBy: '03-05-2023',CreatedOn:'03-05-2023',LastUpdated: '03-05-2023',RequisionType:
        'New',NoOfPositions:2,ExpRequired:2,SalaryRange:'2-4 LPA',Status:'Draft'},
        { MRFID: 1, CreatedBy: '03-05-2023',CreatedOn:'03-05-2023',LastUpdated: '03-05-2023',RequisionType:
        'New',NoOfPositions:2,ExpRequired:2,SalaryRange:'2-4 LPA',Status:'Draft'}
          // Add more dummy data as needed
        ]);


   
    
    
    return(
         
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
        
    );
    }
    export default MyRequisitions ;

