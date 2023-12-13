import React, { useState, useEffect, useRef } from "react";
import DashboardHeader from "./Header";
import LeftPanel from "./LeftPanel";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import "../styles/layout/ResumeDashboard.css"
import { constantResumePath } from "../Components/constant";

const ResumeDashboard = () => {
  const [myResumeData, setMyResumeData] = useState({});
  const [statusData, setStatusData] = useState({});
  const [forwardData, setForwardData] = useState({});
  const [values, setValues] = useState([]);

  useEffect(() => {
    const fetchData = () => {
      try {
        fetch(
          "https://localhost:7128/api/Candidatedetail/GetResumeDropdownlist"
        )
          .then((response) => response.json())
          .then((data) => {
            setMyResumeData(data.result);
            setValues(data.result.candidateDetails);
            setForwardData(data.result.resumereviewer);
            setStatusData(data.result.status);
          })
          .catch((error) => {
            console.error("Error fetching data:", error);
          });
      } catch (error) {
        console.error("Error fetching data:", error);
      }
    };

    fetchData();
  }, []);

  console.log(values);
  console.log(forwardData);
  const createdOnBodyTemplate = (mrf) => {
    return new Date(mrf.createdOnUtc).toLocaleDateString().replaceAll("/", "-");
  };

  const columnHeaderTemplate = (title) => {
    return <h3 className="resume-table-header">{title}</h3>;
  };

  const header = <h3 className="req-header">Resumes</h3>;

  const ResumeHyperLink = (resume) => {
    let resumeLink = `${constantResumePath}${resume.resumePath}`;
    return (
      <div>
        <a
          href={resumeLink}
          target="_blank"
          rel="noopener noreferrer"
          onClick={(e) => {
            e.preventDefault();
            openPdfInNewTab(resumeLink);
          }}
          style={{
            color: "#d32f2e",
            fontFamily: "Poppins",
            fontWeight: 500,
            fontSize: "14px",
          }}
        >
          {resume.resumePath}
        </a>
      </div>
    );
  };
  const openPdfInNewTab = (pdfLink) => {
    window.open(pdfLink, "_blank");
  };


  const Resumereviewer=(data)=>{
// console.log(data)
if(!data.reviewedByEmployeeIds) return 'ww';
const reviewerIds=data.reviewedByEmployeeIds.split(',').map(id=>parseInt(id.trim(),10));
console.log("id:",data.id,"===>",reviewerIds)
const reviwerName=reviewerIds.map(id=>{
    const reviwer=forwardData.find(reviwer=> reviwer.employeeId==id);
    return reviwer? reviwer.name :"";
    // if(reviwer){
    //     return reviwer.name;
    // }
})
return reviwerName.filter(space=>space!=='').join(', ');
  }

  const Resumestatus = (data) => {
    const bool = statusData.find(
      (element) => element.id == data.candidateStatusId
    );
    return bool ? bool.status : "Status Not Found";

    // statusData.forEach((element) => {
    //   console.log(element);
    //   console.log(element.id);
    //   console.log(data.candidateStatusId);
    //   if (element.id == data.candidateStatusId) {
    //     console.log(element.status);
    //     const dddd = element.status;
    //     // setdd(dddd)
    //     return;
    //   }
    // });
  };

  return (
    <>
      <div>
        <div>
          <DashboardHeader />
        </div>
        <div style={{ display: "flex" }}>
          <div className=" ">
            <LeftPanel />
          </div>
          <div className="MyResumeDash">
            <div>
              <h3 className="text-black-alpha-90  text-2xl font-bold  m-4">
               Dashboard
              </h3>
            </div>
            <div className="table-part">
              <DataTable
                value={values}
                  // paginator
                removableSort
                rows={4}
                scrollable
                // header={header}
                scrollHeight="300px"
              >
                <Column field="id" header="Sr.NO"></Column>
                <Column
                  field="resumePath"
                  header={columnHeaderTemplate("Resume")}
                  body={ResumeHyperLink}
                //   sortable
                ></Column>
                <Column
                  field="createdOnUtc"
                  header={columnHeaderTemplate("Uploaded On")}
                  body={createdOnBodyTemplate}
                //   sortable
                  bodyClassName="resume-col  "
                ></Column>
                <Column
                  field="createdName"
                  header={columnHeaderTemplate("Uploaded By")}
                  bodyClassName="resume-col  "
                //   sortable
                ></Column><Column
                  field="reviewedByEmployeeIds"
                  header={columnHeaderTemplate("Resume Reviewer")}
                    bodyClassName="resumedash-col  "
                  body={Resumereviewer}
                //   sortable
                ></Column>
                  <Column
                    field="candidateStatusId"
                    header={columnHeaderTemplate("Resume Status")}
                      bodyClassName="resume-col  "
                    body={Resumestatus}
                    // sortable
                  ></Column>
                
                <Column
                  field="reason"
                  header={columnHeaderTemplate("Reason")}
                  bodyClassName=" resumedash-col  "
                //   sortable
                ></Column>
              </DataTable>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default ResumeDashboard;
