// DashboardContent.js
import React from 'react';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';

const MRFSummaryTable = () => {
  // MRF Summary Data
  const mrfSummaryData = [
    // Your data goes here
  ];

  return (
    <div>
      <h2>MRF Summary</h2>
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
      <h2>Resume Summary</h2>
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
      <h2>Interview Summary</h2>
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
    <div>
      <MRFSummaryTable />
      <ResumeSummaryTable />
      <InterviewSummaryTable />
    </div>
  );
};

export default DashboardContent;
