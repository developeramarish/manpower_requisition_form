import React, { useEffect, useState } from "react";
import "./../css/Dashboard.css";
import { getData } from "../constants/Utils";
import { API_URL, ROLES } from "../constants/config";
import { storageService } from "../constants/storage";
import InterviewSummary from "../components/InterviewSummary";
import DashMrfStatus from "../components/DashMrfStatus";
import ResumeSummary from "../components/ResumeSummary";
import DashBoardDataTable from "../components/DashBoardDataTable";

import {
  filterSelectedColumn,
  filterResultGroupByCandidatestatus,
} from "../constants/Utils";
import { Dialog } from "primereact/dialog";
// import HrResumeSummary from "../components/HrResumeSummary";
import "../css/ResumeSummary.css";
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
    const resumeSummaryData = await getData(API_URL.RESUME_SUMMARY+"?Count=0&roleId="+ roleId+"&userId="+userId);
    const interviewSummaryData = await getData(API_URL.INTERVIEW_SUMMARY+"?Count=0&roleId="+ roleId+"&userId="+userId);
    setMrfStatus(mrfStatusData.result);
    setResumeSummary(resumeSummaryData.result);
    setInterviewSummary(interviewSummaryData.result);
  }

  const interviewSummaryTableData = filterResultGroupByCandidatestatus(
    interviewSummary,
    ["Selected", "Assignment Received", "Onboarded", "Assignment Sent"]
  );


console.log(roleId)

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

// console.log(roleId)
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
      header: "MRF ID",
      body: mrfIdResumeRefernceTemplate,
    },
    {
      field: "New",
      header: "New",
      body: (rowData) => filterSelectedColumn(rowData, "New"),
    },

    {
      field: "Shortlisted",
      header: "Shortlisted",
      body: (rowData) => filterSelectedColumn(rowData, "Shortlisted"),
    },

    {
      field: "Rejected",
      header: "Rejected",
      body: (rowData) => filterSelectedColumn(rowData, "Rejected"),
    },
    {
      field: "On Hold",
      header: "On Hold",
      body: (rowData) => filterSelectedColumn(rowData, "On Hold"),
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
        <h3 className="dashboard_title">My Dashboard</h3>

      { roleId==ROLES.resumeReviwer && (<><div className="resume-viwer-table">
      <ResumeSummary roleId={5} userId={2}/>

      </div></>)

      }
      {(roleId ==ROLES.mrfOwner   || roleId==ROLES.hr) && (<div className="dashboard_body">
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
  )}
        <div className="dashboard_body_right">

{/* <div className="mrf_interview_summary "> */}
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
            roleId={roleId}
          />

          <DashBoardDataTable
            value={resumeSummary}
            column={resumeSummaryColums}
            headerHeading={"Resume Status"}
            table_title={"Resume Summary"}
          />




<Dialog  header={"Resume Summary"} visible={resumePopup} onHide={() => setResumePopup(false)} draggable={false} className="resume-card">
        <ResumeSummary mrfId={resumePopupId}
            roleId={roleId} 
/>
        </Dialog>

        </div>      

          
        </div>
      )  }

       {/* <div className="dashboard_body">
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
            roleId={roleId}
          />

          <DashBoardDataTable
            value={resumeSummary}
            column={resumeSummaryColums}
            headerHeading={"Resume Status"}
            table_title={"Resume Summary"}
          />




<Dialog  header={"shshss"
        // <div>
        //    Resume Summary- MRF ID:{"\u00A0\u00A0"}
        //   <span  style={{ fontWeight: 'bold', color: '#d9362b' }}>
        //     {data[0].referenceNo}
        //   </span>{"\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0"}
        //   Position Title:{"\u00A0\u00A0"}
        //   <span style={{ fontWeight: 'bold', color: '#d9362b' }}>
        //   {data[0].positionTitle}</span>
        // </div>
      } visible={resumePopup} onHide={() => setResumePopup(false)} draggable={false}>
        <ResumeSummary mrfId={resumePopupId}
            roleId={roleId}/>
        </Dialog>

        

          {/* <ResumeSummary
            
            
            mrfId={resumePopupId}
            roleId={roleId}
          /> */}
{/* <DashBoardDataTable
            value={resumeSummary}
            column={resumeSummaryColums}
            headerHeading={"Resume Status"}
            table_title={"Resume Summary"}
          />
          <HrResumeSummary  visible={false}
            onHide={() => setResumePopup(false)}
              mrfId={resumePopupId}/>
        </div>
      </div>  */}
    </div>
  );
}

export default Dashboard;