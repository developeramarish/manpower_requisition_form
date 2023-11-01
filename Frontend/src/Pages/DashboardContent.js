// DashboardContent.js
import React, { useEffect, useState } from "react";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import SearchText from "./SearchText";
import SearchHeader from "../Components/SearchHeader";
import { fetchData } from "../Service/GetDashboarddetails";
import { variables } from "../Variables";

const baseUrl = variables.APP_API;
// MRF Summary Data
/*const mrfSummaryData = [
    { Status: 'Drafted',  TotalCount: 5 },
    { Status: 'Submitted by HR',  TotalCount: 5 },
    { Status: 'Submission Required',  TotalCount: 5 },
    { Status: 'Open',  TotalCount: 5 },
    { Status: 'Rejected',  TotalCount: 5 },
    { Status: 1,  TotalCount: 5 },
  ];
  */

const MRFSummaryTable = () => {
  const [selectedReference, setSelectedReference] = useState(null);
  const handleReferenceClick = (referenceNo) => {
    // Open your popup or perform any action here
    console.log("Reference clicked:", referenceNo);
    setSelectedReference(referenceNo);
  };

  const [mrfSummaryData, setMrfSummaryData] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch(
          "https://localhost:7128/api/Dashboard/GetMrfStatusSummary",
        );
        const responseData = await response.json();

        if (Array.isArray(responseData.result)) {
          const data = responseData.result;
          const newMrfSummaryData = data.map((item) => {
            return {
              id: item.id,
              referenceNo: item.referenceNo,
              count: item.statusCount,
            };
          });

          setMrfSummaryData(newMrfSummaryData);
        } else {
          console.error("API response result is not an array:", responseData);
        }
      } catch (error) {
        console.error("Fetch error:", error);
      }
    };

    fetchData();
  }, []);

  return (
    <div className="headMrfSummary">
      <div className="spSummary">MRF Summary</div>
      <DataTable value={mrfSummaryData}>
        <Column field="referenceNo" header="Reference No"></Column>
        <Column field="count" header="Total Count"></Column>
      </DataTable>
    </div>
  );
};

const ResumeSummaryTable = () => {
  const data = [
    {
      id: "02/MUM/CFR/JAN/15/003",
      new: 5,
      shortlisted: 3,
      rejected: 1,
      onHold: 2,
    },
    {
      id: "03/MUM/CFR/JAN/15/003",
      new: 2,
      shortlisted: 7,
      rejected: 0,
      onHold: 1,
    },
    // Add more data as needed
  ];

  return (
    <div className="headResumeSummary">
      <div className="spSummary">Resume Summary</div>
      <table>
        <thead>
          <tr className="table-header">
            <th rowSpan="2" className="column-widthFirst">
              MRF ID
            </th>
            <th colSpan="4" className="column-width">
              Status
            </th>
          </tr>
          <tr className="StatusHeader">
            <th>New</th>
            <th>Shortlisted</th>
            <th>Rejected</th>
            <th>On Hold</th>
          </tr>
        </thead>
        <tbody>
          {data.map((rowData) => (
            <tr key={rowData.id} className="table-row">
              <td>{rowData.id}</td>
              <td>{rowData.new}</td>
              <td>{rowData.shortlisted}</td>
              <td>{rowData.rejected}</td>
              <td>{rowData.onHold}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

const InterviewSummaryTable = () => {
  const data = [
    {
      id: "02/MUM/CFR/JAN/15/003",
      new: 5,
      shortlisted: 3,
      rejected: 1,
      onHold: 2,
    },
    {
      id: "03/MUM/CFR/JAN/15/003",
      new: 2,
      shortlisted: 7,
      rejected: 0,
      onHold: 1,
    },
    // Add more data as needed
  ];

  return (
    <div className="headinterviewSummary">
      <div className="spSummary">Interview Summary</div>
      <table>
        <thead>
          <tr className="table-header">
            <th rowSpan="2" className="column-widthFirst">
              MRF ID
            </th>
            <th colSpan="4" className="column-width">
              Status
            </th>
          </tr>
          <tr className="StatusHeader">
            <th>New</th>
            <th>Shortlisted</th>
            <th>Rejected</th>
            <th>On Hold</th>
          </tr>
        </thead>
        <tbody>
          {data.map((rowData) => (
            <tr key={rowData.id} className="table-row">
              <td>{rowData.id}</td>
              <td>{rowData.new}</td>
              <td>{rowData.shortlisted}</td>
              <td>{rowData.rejected}</td>
              <td>{rowData.onHold}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

const DashboardContent = () => {
  return (
    <div>
      {/* <div className="containerH">
        <div className="box">
          <label>My Dashboard</label>
        </div>
        <SearchText />
      </div> */}
      <SearchHeader title="My Dashboard" />
      <div className="left-div">
        <MRFSummaryTable></MRFSummaryTable>
      </div>
      <div className="right-div">
        <ResumeSummaryTable />
        <br></br>
        <InterviewSummaryTable />
      </div>
    </div>
  );
};

export default DashboardContent;
