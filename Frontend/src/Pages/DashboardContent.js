// DashboardContent.js
import React from 'react';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import SearchText from './SearchText';
const MRFSummaryTable = () => {
  // MRF Summary Data
  const mrfSummaryData = [
    // Your data goes here
  ];

  return (
    <div>
      <h5><b>MRF Summary</b></h5>
      <DataTable value={mrfSummaryData}>
        {/* Define your columns */}
        <Column field="id" header="ID"></Column>
        <Column field="title" header="Title"></Column>
        {/* Add more columns as needed */}
      </DataTable>
    </div>
  );
};

const ResumeSummaryTable = () => {
  // Resume Summary Data
  const resumeSummaryData = [
    // Your data goes here
  ];

  return (
    <div>
      <h5><b>Resume Summary</b></h5>
      <DataTable value={resumeSummaryData}>
        {/* Define your columns */}
        <Column field="id" header="ID"></Column>
        <Column field="name" header="Name"></Column>
        {/* Add more columns as needed */}
      </DataTable>
    </div>
  );
};

const InterviewSummaryTable = () => {
  // Interview Summary Data
  const interviewSummaryData = [
    // Your data goes here
  ];

  return (
    <div>
      <h5><b>Interview Summary</b></h5>
      <DataTable value={interviewSummaryData}>
        {/* Define your columns */}
        <Column field="id" header="ID"></Column>
        <Column field="candidate" header="Candidate"></Column>
        {/* Add more columns as needed */}
      </DataTable>
    </div>
  );
};

const DashboardContent = () => {
  return (
    <div class="mydashboard">
       <label>My Dashboard</label>
       <div className='search'>
       <SearchText/>
       </div>
     <div class="left-div">
      <MRFSummaryTable />
      </div>
      <div class="right-div">
      <ResumeSummaryTable />
      <InterviewSummaryTable />
      </div>
    </div>
  );
};

export default DashboardContent;
