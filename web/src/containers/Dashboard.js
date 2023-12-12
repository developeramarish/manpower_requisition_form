import React, { useEffect, useState } from "react";
import "./../css/Dashboard.css";
import { getData } from "../constants/Utils";
import { API_URL } from "../constants/config";
import { storageService } from "../constants/storage";
import InterviewSummary from "../components/InterviewSummary";
import DashMrfStatus from "../components/DashMrfStatus";

function Dashboard() {
  const [mrfStatus, setMrfStatus] = useState([]);
  const [resumeSummary, setResumeSummary] = useState([]);
  const [interviewSummary, setInterviewSummary] = useState([]);
  const [interviewPopup, setInterviewPopup] = useState(false);
  const [interviewPopupId, setInterviewPopupId] = useState(null);
  const [mrfStatusPopup, setMrfStatusPopup] = useState(false);
  const [mrfStatusPopupId, setrfStatusPopupId] = useState(null);

  useEffect(() => {
    getSummaryData();
  }, []);

  async function getSummaryData() {
    const mrfStatusData = await getData(API_URL.MRF_STATUS_SUMMARY);
    const resumeSummaryData = await getData(API_URL.RESUME_SUMMARY);
    const interviewSummaryData = await getData(API_URL.INTERVIEW_SUMMARY);
    setMrfStatus(mrfStatusData.result);
    setResumeSummary(resumeSummaryData.result);
    setInterviewSummary(interviewSummaryData.result);
    console.log(interviewSummaryData);
  }

  const onMRFIdClicked = (e) => {
    console.log(e);
    setrfStatusPopupId(e);
    setMrfStatusPopup(true);
  };

  const onInterviewMRFIdClicked = (e) => {
    setInterviewPopupId(e);
    setInterviewPopup(true);
  };

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
          <div className="mrf_interview_summary">
            <div className="header">
              <h4>Interview Summary</h4>
            </div>
            <InterviewSummary
              visible={interviewPopup}
              onHide={() => setInterviewPopup(false)}
              mrfId={interviewPopupId}
            />
            <div className="mrf_table">
              <table>
                <thead>
                  <tr>
                    <th rowSpan="2" className="table_heading">
                      MRF ID
                    </th>
                    <th colSpan="4" className="table_heading_Resume">
                      Interview Status
                    </th>
                  </tr>
                  <tr>
                    <th className="subheading">Assignment Sent</th>
                    <th className="subheading">Assignment Received</th>
                    <th className="subheading">Onboarded</th>
                    <th className="subheading"> Candidate Selected</th>
                  </tr>
                </thead>
                <tbody>
                  {interviewSummary.map((data, index) => {
                    return (
                      <tr key={"interviewSum_" + index}>
                        <td>
                          <a
                            className="btn_mrf_id"
                            onClick={(e) => onInterviewMRFIdClicked(data.mrfId)}
                          >
                            {data.referenceno}
                          </a>
                        </td>
                        {data.resultGroups
                          .sort((a, b) => {
                            return a.candidatestatus.toLowerCase() >
                              b.candidatestatus.toLowerCase()
                              ? 1
                              : -1;
                          })
                          .map((resData, index) => {
                            return (
                              <React.Fragment key={"interviewSum_res_" + index}>
                                {resData.candidatestatus ===
                                  "Assignment Sent" && (
                                  <td>{resData.totalstatusCount}</td>
                                )}
                                {resData.candidatestatus ===
                                  "Assignment Received" && (
                                  <td>{resData.totalstatusCount}</td>
                                )}
                                {resData.candidatestatus === "Onboarded" && (
                                  <td>{resData.totalstatusCount}</td>
                                )}
                                {resData.candidatestatus === "Selected" && (
                                  <td>{resData.totalstatusCount}</td>
                                )}
                              </React.Fragment>
                            );
                          })}
                      </tr>
                    );
                  })}
                </tbody>
              </table>
            </div>
          </div>
          <div className="mrf_resume_summary">
            <div className="header">
              <h4>Resume Summary</h4>
            </div>
            <div className="mrf_table">
              <table>
                <thead>
                  <tr>
                    <th rowSpan="2" className="table_heading">
                      MRF ID
                    </th>
                    <th colSpan="4" className="table_heading_Resume">
                      Resume Status
                    </th>
                  </tr>
                  <tr>
                    <th className="subheading">New</th>
                    <th className="subheading">Shortlisted</th>
                    <th className="subheading">Rejected</th>
                    <th className="subheading">On Hold</th>
                  </tr>
                </thead>
                <tbody>
                  {resumeSummary.map((data, index) => {
                    return (
                      <tr key={"interviewSum_" + index}>
                        <td>
                          <a
                            className="btn_mrf_id"
                            onClick={(e) => onMRFIdClicked(data.mrfId)}
                          >
                            {data.referenceno}
                          </a>
                        </td>
                        {data.resultGroups.map((resData, index) => {
                          return (
                            <React.Fragment key={"interviewSum_res_" + index}>
                              {resData.candidatestatus === "New" && (
                                <td>{resData.totalstatusCount}</td>
                              )}
                              {resData.candidatestatus === "Shortlisted" && (
                                <td>{resData.totalstatusCount}</td>
                              )}
                              {resData.candidatestatus === "Rejected" && (
                                <td>{resData.totalstatusCount}</td>
                              )}
                              {resData.candidatestatus === "on Hold" && (
                                <td>{resData.totalstatusCount}</td>
                              )}
                            </React.Fragment>
                          );
                        })}
                      </tr>
                    );
                  })}
                </tbody>
              </table>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default Dashboard;
