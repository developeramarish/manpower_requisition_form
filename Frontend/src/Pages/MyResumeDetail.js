import React, { useState, useEffect } from "react";
import { Button } from "primereact/button";
import { Dialog } from "primereact/dialog";
import SearchText from "./SearchText";
import { InputTextarea } from "primereact/inputtextarea";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import { MultiSelect } from "primereact/multiselect";
import { constantResumePath } from "../Components/constant";
import "../styles/layout/ResumeSummary.css";

const MyResumeDetail = ({ visible, onHide }) => {
  const [data, setdata] = useState([]);
  const [resumeReviewer, setResumeReviewer] = useState([]);

  useEffect(() => {
    const fetchData = () => {
      try {
        fetch(
          "https://localhost:7128/api/Mrfresumereviewermap/GetResumeStatusDetails/GetResumeStatusDetails?id=2&DashBoard=true"
        )
          .then((response) => response.json())
          .then((data) => {
            setdata(data.result.resumeDetails);
            setResumeReviewer(data.result.employeeRoleMap);
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

  console.log(data);
  console.log(resumeReviewer);

  const MultiSelectDrop = (data) => {
    console.log("shows", data);

    // const selte=arrayToObj(resumeReviewer,strToArray(data.resumeReviewerEmployeeIds));

    // const [selectedReviewers, setSelectedReviewers] = useState(selte);
    return (
      <div>
        <MultiSelect
          id="resumeReviewerEmployeeIds"
          value={arrayToObj(resumeReviewer, strToArray(data.value))}
          // value={selectedReviewers}
          onChange={(e) => {
            // const valueInArray=objToArray(e.value)
            // console.log(valueInArray)
            // const arraytoStr=valueInArray.toString();
            // console.log(arraytoStr)
            // setSelectedReviewers(e.value)

            console.log(e.value);
            data.editorCallback(objToArray(e.target.value));
          }}
          options={resumeReviewer}
          optionLabel="name"
          filter
          placeholder="Select Reviewer"
          maxSelectedLabels={2}
          className="w-full "
        />
      </div>
    );
  };

  const strToArray = (s) => {
    if (typeof s === "string") {
      s = s.split(",").map(Number);
    }
    return s;
  };
  const arrayToObj = (options = [], selectedOpt) => {
    if (Array.isArray(selectedOpt)) {
      return options.filter((e) => selectedOpt.includes(e.employeeId));
    }
  };
  const srt = "89,90,67,5";

  const arr = strToArray(srt);
  // console.log(arr)

  const obj = arrayToObj(resumeReviewer, arr);
  console.log(obj);

  const objToArray = (selectedOpt = []) => {
    return selectedOpt.map((e) => e.employeeId);
  };

  const openPdfInNewTab = (pdfLink) => {
    window.open(pdfLink, "_blank");
  };

  const actionBodyTemplate = (rowData) => {
    // console.log("action buttton",rowData)

    return (
      <React.Fragment>
        <Button
          icon="pi pi-save"
          outlined
          className="mr-2 text-white"
          onClick={() => {
            // updateData(
            // rowData.candidateId,
            // rowData.reason,
            // rowData.candidatestatus,
            // rowData.resumeReviewerEmployeeIds,
            // )
          }}
        />
      </React.Fragment>
    );
  };
  const update = (data) => {
    console.log("from on complete function");
    console.log(data)
    
    
    // {
      //   "id": 0,
      //   "mrfId": 0, ==========
      //   "name": "string",
      //   "emailId": "string",
      //   "contactNo": "string",
      //   "resumePath": "string",  =====
      //   "candidateStatusId": 0,
      //   "reviewedByEmployeeId": 0,
      //   "reviewedByEmployeeIds": "string",
      //   "createdByEmployeeId": 0,
      //   "createdOnUtc": "2023-11-30T14:11:20.978Z",
      //   "updatedByEmployeeId": 0,
      //   "updatedOnUtc": "2023-11-30T14:11:20.978Z",
      //   "reason": "string"   =====
      // }
      
      // const id=
      
      const resumeRevierInArray = data.newData.resumeReviewerEmployeeIds;
      const resumeReviewerEmployeeIds = resumeRevierInArray.toString();
      console.log("resumeReviewerEmployeeIds",resumeReviewerEmployeeIds);
      const name="ramkrsishna"
      const emailId="ramkrsishna@gamil.com"
      const contactNo="8790998980"
      const id=data.newData.candidateId
      const candidateStatusId=data.newData.candidateStatusId
const candidatestatus=data.newData.candidatestatus
const createdName=data.newData.createdName
const mrfId=data.newData.mrfId
const reason=data.newData.reason
const referenceNo=data.newData.referenceNo
const resumePath=data.newData.resumePath
const createdByEmployeeId=data.newData.createdByEmployeeId
const createdOnUtc=data.newData.createdOnUtc

const candidateDetailsData={
  id,
  mrfId,
  name, 
  emailId, 
  contactNo,
  resumePath,
  candidateStatusId,
  resumeReviewerEmployeeIds,
  createdByEmployeeId,
  createdOnUtc,
  reason,

}

console.log(id);
console.log(candidateStatusId);

    console.log(candidatestatus);
    console.log(createdName);
    console.log(mrfId);
    console.log(reason);
    console.log(referenceNo);
    console.log(resumePath);
    // console.log(candidateId);
    // console.log(data.newData.resumeReviewerEmployeeIds);



    fetch("https://localhost:7128/api/Candidatedetail/Put/"+id,{
      method:"Put",
      headers:{"content-type":"application/json"},
      body: JSON.stringify(candidateDetailsData),
    }).then((res) => {
      alert("updated successfully dbbbbbbb.");
    })
    .catch((err) => {
      console.log(err.message);
    });

  };

  const createdOnBodyTemplate = (mrf) => {
    return new Date(mrf.createdOnUtc).toLocaleDateString().replaceAll("/", "-");
  };

  const handleEditorCallback = (rowData, value) => {
    rowData.resumeReviewerEmployeeIds = value;
  };

  const columnHeaderTemplate = (title) => {
    return <h3 className="resume-table-header">{title}</h3>;
  };

  const ResumeHyperLink = (resume) => {
    // console.log(resume.resumePath);
    let resumeLink = `${constantResumePath}${resume.resumePath}`;
    // console.log(resumeLink);

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

  return (
    <div>
      <Dialog
        header="MRF ID (Resume Summary)"
        visible={visible}
        className="resume-card"
        onHide={onHide}
      >
        <DataTable
          value={data}
          paginator
          rows={5}
          scrollable
          onRowEditComplete={update}
          editMode="row"
          scrollHeight="50vh"
          rowsPerPageOptions={[5, 10, 25, 50]}
        >
          <Column
            // field="mrfId"
            header={columnHeaderTemplate("Sr.No")}
            body={(data, options) => options.rowIndex + 1}
            bodyClassName="resume-col  "
            sortable
          ></Column>
          <Column
            field="resumePath"
            header={columnHeaderTemplate("Resume")}
            body={ResumeHyperLink}
          ></Column>
          <Column
            field="createdOnUtc"
            header={columnHeaderTemplate("Created On")}
            body={createdOnBodyTemplate}
            sortable
            bodyClassName="resume-col  "
          ></Column>
          <Column
            field="createdName"
            header={columnHeaderTemplate("Uploaded By")}
            bodyClassName="resume-col  "
            sortable
          ></Column>

          <Column
            field="resumeReviewerEmployeeIds"
            header={columnHeaderTemplate("Resume Reviewers")}
            // body={MultiSelectDrop}
            editor={(options) => MultiSelectDrop(options)}
            // body={(rowData) => (
            //   <MultiSelectDrop
            //     data={rowData}
            //     editorCallbacks={handleEditorCallback}/>)}

            bodyClassName="resume-col resume-ref-col  "
          ></Column>
          <Column
            field="candidatestatus"
            header={columnHeaderTemplate("Resume Status")}
            bodyClassName="resume-col  "
            sortable
          ></Column>
          <Column
            field="reason"
            header={columnHeaderTemplate("Reason")}
            bodyClassName="resume-col resume-ref-col  "
          ></Column>
          <Column
            header={columnHeaderTemplate("Action")}
            // headerStyle={{ width: "10%", minWidth: "8rem" }}
            bodyStyle={{ textAlign: "left" }}
            rowEditor
            // body={actionBodyTemplate}
          ></Column>
        </DataTable>
      </Dialog>
    </div>
  );
};

export default MyResumeDetail;
