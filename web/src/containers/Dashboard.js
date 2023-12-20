import React, { useEffect, useState } from "react";
import "./../css/Dashboard.css";
import { getData } from "../constants/Utils";
import { API_URL } from "../constants/config";
import { storageService } from "../constants/storage";
import InterviewSummary from "../components/InterviewSummary";
import DashMrfStatus from "../components/DashMrfStatus";
import ResumeSummary from "../components/ResumeSummary";
import DashBoardDataTable from "../components/DashBoardDataTable";
import {
  filterSelectedColumn,
  filterResultGroupByCandidatestatus,
} from "../constants/Utils";

function Dashboard({roleId,userId}) {
  const [mrfStatus, setMrfStatus] = useState([]);
  const [resumeSummary, setResumeSummary] = useState([]);
  const [interviewSummary, setInterviewSummary] = useState([]);
  const [interviewPopup, setInterviewPopup] = useState(false);
  const [interviewPopupId, setInterviewPopupId] = useState(null);
  const [mrfStatusPopup, setMrfStatusPopup] = useState(false);
  const [mrfStatusPopupId, setrfStatusPopupId] = useState(null);
  const [resumePopup, setResumePopup] = useState(false);
  const [resumePopupId, setResumePopupId] = useState(null);

  useEffect(() => {
    getSummaryData();
  }, []);

    async function getSummaryData() {
    const mrfStatusData = await getData(API_URL.MRF_STATUS_SUMMARY+"?roleId="+roleId+"&userId="+userId);
    // const mrfStatusData = API_URL.MRF_STATUS_SUMMARY;
    const resumeSummaryData = await getData(API_URL.RESUME_SUMMARY+"?Count=0&roleId="+roleId+"&userId="+userId);
    const interviewSummaryData = await getData(API_URL.INTERVIEW_SUMMARY+"?Count=0&roleId="+roleId+"&userId="+userId);
    setMrfStatus(mrfStatusData.result);
    setResumeSummary(resumeSummaryData.result);
    setInterviewSummary(interviewSummaryData.result);
  }

  const interviewSummaryTableData = filterResultGroupByCandidatestatus(
    interviewSummary,
    ["Selected", "Assignment Received", "Onboarded", "Assignment Sent"]
  );

  const onMRFIdClicked = (e) => {
    console.log(e)
    setrfStatusPopupId(e);
    setMrfStatusPopup(true);
  };

  const onInterviewMRFIdClicked = (e) => {
    setInterviewPopupId(e);
    setInterviewPopup(true);
  };
  const onResumeMRFIdClicked = (e) => {
    setResumePopupId(e);
    setResumePopup(true);
  };


  const mrfIdInterviewRefernceTemplate = (rowData) => {
    return (
      <div>
        <a
          className="btn_mrf_id"
          onClick={(e) => onInterviewMRFIdClicked(rowData.mrfId)}
        >
          {rowData.referenceno}
        </a>
      </div>
    );
  };

  const mrfIdResumeRefernceTemplate = (rowData) => {
    return (
      <div>
        <a
          className="btn_mrf_id"
          onClick={(e) => onResumeMRFIdClicked(rowData.mrfId)}
        >
          {rowData.referenceno}
        </a>
      </div>
    );
  };
  const resumeSummaryColums = [
    {
      field: "referenceno",
      header: "Mrf Id",
      body: mrfIdResumeRefernceTemplate,
    },
    {
      field: "new",
      header: "New",
      body: (rowData) => filterSelectedColumn(rowData, "New"),
    },

    {
      field: "shortlisted",
      header: "Shortlisted",
      body: (rowData) => filterSelectedColumn(rowData, "Shortlisted"),
    },

    {
      field: "rejected",
      header: "Rejected",
      body: (rowData) => filterSelectedColumn(rowData, "Rejected"),
    },
    {
      field: "on Hold",
      header: "on Hold",
      body: (rowData) => filterSelectedColumn(rowData, "on Hold"),
    },
  ];

  const interviewSummaryColums = [
    {
      field: "referenceno",
      header: "MRF ID",
      body: mrfIdInterviewRefernceTemplate,
    },
    {
      field: "Selected",
      header: "Selected",
      body: (rowData) => filterSelectedColumn(rowData, "Selected"),
    },
    {
      field: "Onboarded",
      header: "Onboarded",
      body: (rowData) => filterSelectedColumn(rowData, "Onboarded"),
    },
    {
      field: "new",
      header: "Assignment Sent",
      body: (rowData) => filterSelectedColumn(rowData, "Assignment Sent"),
    },

    {
      field: "Assignment Received",
      header: "Assignment Received",
      body: (rowData) => filterSelectedColumn(rowData, "Assignment Received"),
    },
  ];

  return (
    <div className="dashboard_wrapper">
      <div className="dashboard_header">
        <h3>My Dashboard</h3>
      </div>
      <div className="dashboard_body">
        <div className="dashboard_body_left">
          <div className="mrf_status_summary">
            <div className="header">
              <h4>MRF Summary</h4>
              <DashMrfStatus
                visible={mrfStatusPopup}
                onHide={() => setMrfStatusPopup(false)}
                statusId={mrfStatusPopupId}
                userId={userId}
                roleId={roleId}
              />
            </div>
            <table className="mrf_table">
              <thead>
                <tr>
                  <th className="table_status">Status</th>
                  <th className="table_count">Total Count</th>
                </tr>
              </thead>
              <tbody className="mrf_table_body">
                {mrfStatus.map((data, index) => {
                 
                  return (
                    <tr key={"mrf_" + index}>
                      <td>{data.status}</td>
                      <td
                        className={
                          data.totalCount > 0
                            ? "mrf_summary_total_count"
                            : "mrf_summary_total_count count_zero"
                        }
                      >
                        {data.totalCount > 0 && (
                          <a onClick={(e) => onMRFIdClicked(data.mrfStatusId)}>
                            {data.totalCount}
                          </a>
                        )}
                        {data.totalCount === 0 && data.totalCount}
                      </td>
                    </tr>
                  );
                })}
              </tbody>
            </table>
          </div>
        </div>
        <div className="dashboard_body_right">
          <DashBoardDataTable
            value={interviewSummaryTableData}
            column={interviewSummaryColums}
            headerHeading={"Interview Status"}
            table_title={"Interview Summary"}
          />
          <InterviewSummary
            visible={interviewPopup}
            onHide={() => setInterviewPopup(false)}
            mrfId={interviewPopupId}
          />

          <DashBoardDataTable
            value={resumeSummary}
            column={resumeSummaryColums}
            headerHeading={"Resume Status"}
            table_title={"Resume Summary"}
          />
          <ResumeSummary
            visible={resumePopup}
            onHide={() => setResumePopup(false)}
            mrfId={resumePopupId}
          />
        </div>
      </div>
    </div>
  );
}

export default Dashboard;