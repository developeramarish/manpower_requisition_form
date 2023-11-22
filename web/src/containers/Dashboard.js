import React, { useEffect, useState } from 'react';
import './../css/Dashboard.css';
import { getData } from '../constants/Utils';
import { API_URL } from '../constants/config';

function Dashboard() {
  const [mrfStatus, setMrfStatus] = useState([]);
  const [resumeSummary, setResumeSummary] = useState([]);
  const [interviewSummary, setInterviewSummary] = useState([]);
  useEffect(() => {
    getSummaryData();
  }, [])

  async function getSummaryData() {
    const mrfStatusData = await getData(API_URL.MRF_STATUS_SUMMARY);
    const resumeSummaryData = await getData(API_URL.RESUME_SUMMARY);
    const interviewSummaryData = await getData(API_URL.INTERVIEW_SUMMARY);
    setMrfStatus(mrfStatusData.result);
    setResumeSummary(resumeSummaryData.result);
    setInterviewSummary(interviewSummaryData.result);

    console.log(interviewSummaryData)
    console.log(resumeSummaryData)
  }

  const onMRFIdClicked = ()=>{
    
  }
  return (
    <div className="dashboard ">
      <div className='dashboard_header'>
        <h3>My Dashboard</h3>
      </div>
      <div className='dashboard_body'>
        <div className='dashboard_body_left'>
          <div className='mrf_status_summary'>
            <div className='header'>
              <h4>MRF Summary</h4>
            </div>
            <table className='mrf_table'>
              <thead>
                <tr>
                  <th>Status</th>
                  <th>Total Count</th>
                </tr>
              </thead>
              <tbody>
                {
                  mrfStatus.map((data, index) => {
                    return (
                      <tr key={"mrf_" + index}>
                        <td>{data.status}</td>
                        <td>{data.totalCount}</td>
                      </tr>
                    )
                  })
                }
              </tbody>
            </table>
          </div>
        </div>
        <div className='dashboard_body_right'>
          <div className='mrf_interview_summary'>
            <div className='header'>
              <h4>Interview Summary</h4>
              <button className="btn-view-more">
                <p>View more</p>
                <i className="fa-solid fa-chevron-right"></i>
              </button>
              <button className="btn-view-less">
                <p>View less</p>
                <i className="fa-solid fa-chevron-right"></i>
              </button>
            </div>
            <div className='mrf_table'>
              <table>
                <thead>
                  <tr>
                    <th>MRF ID</th>
                    <th>Interview Status</th>
                  </tr>
                  <tr>
                    <th></th>
                    <th>Assignment Sent</th>
                    <th>Assignment Received</th>
                    <th>Candidate Selected</th>
                    <th>Onboarded</th>
                  </tr>
                </thead>
                <tbody>
                  {
                    interviewSummary.map((data, index) => {
                      return (
                        <tr key={"interviewSum_" + index}>
                          <td><button className='btn_mrf_id' onClick={(e)=>onMRFIdClicked(data.mrd)}>{data.referenceno}</button></td>
                          {data.resultGroups.map((resData, index) => {
                            return (
                              <React.Fragment key={"interviewSum_res_" + index}>
                                {resData.candidatestatus === "Assignment Sent" && <td>{resData.totalstatusCount}</td>}
                                {resData.candidatestatus === "Assignment Received" && <td>{resData.totalstatusCount}</td>}
                                {resData.candidatestatus === "Candidate Selected" && <td>{resData.totalstatusCount}</td>}
                                {resData.candidatestatus === "Onboarded" && <td>{resData.totalstatusCount}</td>}
                              </React.Fragment>
                            )
                          })}
                        </tr>

                      )
                    })
                  }
                </tbody>
              </table>
            </div>
          </div>
          <div className='mrf_resume_summary'>
            <div className='header'>
              <h4>Resume Summary</h4>
              <button className="btn-view-more">
                <p>View more</p>
                <i className="fa-solid fa-chevron-right"></i>
              </button>
              <button className="btn-view-less">
                <p>View less</p>
                <i className="fa-solid fa-chevron-right"></i>
              </button>
            </div>
            <div className='mrf_table'>
              <table>
                <thead>
                  <tr>
                    <th>MRF ID</th>
                    <th>Resume Status</th>
                  </tr>
                  <tr>
                    <th></th>
                    <th>New</th>
                    <th>Shortlisted</th>
                    <th>Rejected</th>
                    <th>On Hold</th>
                  </tr>
                </thead>
                <tbody>
                  {
                    resumeSummary.map((data, index) => {
                      return (
                        <tr key={"interviewSum_" + index}>
                          <td><button className='btn_mrf_id' onClick={(e)=>onMRFIdClicked(data.mrd)}>{data.referenceno}</button></td>
                          {data.resultGroups.map((resData, index) => {
                            return (
                              <React.Fragment key={"interviewSum_res_" + index}>
                                {resData.candidatestatus === "New" && <td>{resData.totalstatusCount}</td>}
                                {resData.candidatestatus === "Shortlisted" && <td>{resData.totalstatusCount}</td>}
                                {resData.candidatestatus === "Rejected" && <td>{resData.totalstatusCount}</td>}
                                {resData.candidatestatus === "on Hold" && <td>{resData.totalstatusCount}</td>}
                              </React.Fragment>
                            )
                          })}
                        </tr>

                      )
                    })
                  }
                </tbody>
              </table>
            </div>
          </div>
        </div>
      </div>
      {/* <div className="dash">
      
        <div className="flex-add">
          <div className="flex-add dash-left">

            <div className="mrf-table">
              <ul className="flex-add mrf-head">
                <li>Status</li>
                <li>Total Count</li>
              </ul>
              {
                mrfStatus.map((data, index) => {
                  return (
                    <ul key={"mrf_" + index} className="flex-add mrf-body">
                      <td>{data.status}</td>
                      
                    </ul>
                  )
                })
              }
            </div>
          </div>
          <div className="dash-right">
            <div className="flex-add tab-card">
              <div className="flex-add tab-head">
                <h4>Resume Summary</h4>
                <button popovertarget="my-req" className="flex-add">
                  <p>View more</p>
                  <i className="fa-solid fa-chevron-right"></i>
                </button>
              </div>
              <ul className="flex-add tab-cname">
                <li>MRF ID</li>
                <li>Resume Status</li>
              </ul>
              <ul className="flex-add tab-body add-gray">
                <li></li>
                <li>New</li>
                <li>Shortlisted</li>
                <li>Rejected</li>
                <li>On Hold</li>
              </ul>
              {
                resumeSummary.map((data, index) => {
                  return (
                    <ul key={"resumeSum_" + index} className="flex-add tab-body">
                      <li>{data.referenceno}</li>
                      {data.resultGroups.map((resData, index) => {
                        return (
                          <React.Fragment key={"resumeSum_res_" + index}>
                            {resData.candidatestatus === "New" && <li>{resData.totalstatusCount}</li>}
                            {resData.candidatestatus === "Shortlisted" && <li>{resData.totalstatusCount}</li>}
                            {resData.candidatestatus === "Rejected" && <li>{resData.totalstatusCount}</li>}
                            {resData.candidatestatus === "on Hold" && <li>{resData.totalstatusCount}</li>}
                          </React.Fragment>
                        )
                      }
                      )}
                    </ul>
                  )
                })
              }
            </div>
            <div className="flex-add tab-card">
              <div className="flex-add tab-head">
                <h4>Interview Summary</h4>
                <button className="flex-add">
                  <p>View more</p>
                  <i className="fa-solid fa-chevron-right"></i>
                </button>
              </div>
              <ul className="flex-add tab-cname">
                <li>MRF ID</li>
                <li>Interview Status</li>
              </ul>
              <ul className="flex-add tab-body add-gray">
                <li></li>
                <li>Assignment Sent</li>
                <li>Assignment Received</li>
                <li>Selected</li>
                <li>Onboarded</li>
              </ul>

              <ul className="flex-add tab-body">
                <li>02/ MUM/ CFR/ JAN/ 15/ 003</li>
                <li>1</li>
                <li>1</li>
                <li>1</li>
                <li>1</li>
              </ul>
              <ul className="flex-add tab-body">
                <li>02/ MUM/ CFR/ JAN/ 15/ 003</li>
                <li>1</li>
                <li>1</li>
                <li>1</li>
                <li>1</li>
              </ul>
            </div>
          </div>
        </div>
      </div> */}
    </div>
  );
}

export default Dashboard;