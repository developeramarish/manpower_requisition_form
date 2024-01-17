
import React, { useState, useEffect, useRef } from "react";
import "primeicons/primeicons.css";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import "../css/InputComponent.css";
import "../css/MyResume.css";
import { navigateTo, putData } from "../constants/Utils";
import { API_URL, FILE_URL, ROLES } from "../constants/config";
import {
  arrayToObj,
  objToIntArray,
  getData,
  strToArray,
} from "../constants/Utils";
import { Button } from "primereact/button";
import ToastMessages from "./ToastMessages";
import DropdownComponent from "./Dropdown";
import MultiSelectDropdown from "./multiselectDropdown";
import ButtonC from "./Button";
import InputTextareaComponent from "./InputTextarea";
const MyResume = ({roleId =null, mrfId =  0, userId=null}) => {
  const [statusData, setStatusData] = useState({});
  const [forwardData, setForwardData] = useState({});
  const [values, setValues] = useState([]);
  const [saveBttn, setSaveBttn] = useState([]);
  const toastRef = useRef(null);
  useEffect(() => {
    getResumeData();
  }, []);
 
  async function getResumeData() {
    const resumeData = await getData(API_URL.GET_MYRESUME+ "?id=0&roleId=" + roleId + "&userId=" + userId);
    if (roleId === ROLES.resumeReviwer) {
      var filterInterviewerResumtSumData = [];
      resumeData.result.candidateDetails.map(( res) => {
          if (res.mrfStatus !== 8 && res.mrfStatus !== 9 && res.mrfStatus !== 10 ) {
            filterInterviewerResumtSumData.push(res)
          }
      })
      setValues(filterInterviewerResumtSumData);
      setForwardData(resumeData.result.resumereviewer);
      setStatusData(resumeData.result.status);
    }
    else{
      setValues(resumeData.result.candidateDetails);
      setForwardData(resumeData.result.resumereviewer);
      setStatusData(resumeData.result.status);
       
    }
   
  }
  const SingleSelect = (data, options) => {
    const handleDropdownChange = (e) => {
      let statusdataCopy = [...values];
      let sv = [...saveBttn];
      sv[options.rowIndex] = true;
      statusdataCopy[options.rowIndex].candidateStatusId = e.target.value;
      setValues(statusdataCopy);
      setSaveBttn(sv);
    };
    return (
      <DropdownComponent
        optionLabel="status"
        optionValue="id"
        // className="drop-width"
        className="w-full md:w-15rem"
        options={statusData}
        value={data.candidateStatusId}
        placeholder={"Select Status"}
        onChange={handleDropdownChange}
      />
    );
  };
 
 
 
  const MultiSelect = (data, options) => {
    const handleMultiSelectChange = (e) => {
      let interviewDataCopy = [...values];
      let sv = [...saveBttn];
      sv[options.rowIndex] = e.value.length > 0 ? true : false;
      interviewDataCopy[options.rowIndex].resumeReviewerEmployeeIds  = objToIntArray(
        e.value,
        "employeeId"
      ).toString();
      setValues(interviewDataCopy);
      setSaveBttn(sv);
    };
 
    return (
      <MultiSelectDropdown
        // className="drop-width"
        options={forwardData}
        value={arrayToObj(
          forwardData,
          strToArray(data.resumeReviewerEmployeeIds
            ),
          "employeeId"
        )}
        placeholder={"Select Resume Reviwer"}
        onChange={handleMultiSelectChange}
        optionLabel="name"
        className="w-full md:w-15rem"
      //placeholder="Select Interviewer"
      // optionValue="employeeId"
      />
    );
  };
  const updateData = async (rowData) => {
    const reviewedByEmployeeIds = rowData.resumeReviewerEmployeeIds;
    const name =  rowData.candidateName; // this because we are handling data in backend it not save as string
    const emailId = "string";
    const id = rowData.candidateId;
    const candidateStatusId = rowData.candidateStatusId;
    const mrfId = rowData.mrfId;
    const contactNo = 0;
    const reason = rowData.reason;
    const resumePath = rowData.resumePath;
    const createdByEmployeeId = rowData.createdByEmployeeId;
    const createdOnUtc = rowData.createdOnUtc;
    const candidateName=rowData.candidateName;
    const candidateDetailsData = {
      id,
      mrfId,
      name,
      contactNo,
      emailId,
      resumePath,
      candidateStatusId,
      reviewedByEmployeeIds,
      createdByEmployeeId,
      createdOnUtc,
      reason,
    };
    let response = await putData(`${API_URL.RESUME_SUMMARY_POST + id}`,candidateDetailsData);
    if (response.ok) {
      const responseData = await response.json();
      console.log("Response Data:", responseData);
      toastRef.current.showSuccessMessage("Update successfully!");
      setTimeout(() => {
        navigateTo("dashboard");
      }, 1000);
    } else {
      console.error("Request failed with status:", response.status);
      if (response.status === 400) {
        toastRef.current.showBadRequestMessage(
          "Bad request: " + response.url
        );
      }
    }
  };
 
 
  const textEditor = (data, options) => {
    const TextChange = (e) => {
      let statusdataCopy = [...values];
 
      let sv = [...saveBttn];
      sv[options.rowIndex] = true;
 
      statusdataCopy[options.rowIndex].reason = e.target.value;
 
      setValues(statusdataCopy);
      setSaveBttn(sv);
    };
    return (
      <InputTextareaComponent
        
        value={data.reason}
        rows={2}  
        cols={55}
        onChange={TextChange}
        placeholder={"Enter Reason"}
 
      />
    );
  };
  const actionBodyTemplate = (rowData, options) => {
    //console.log(rowData);
    if (saveBttn[options.rowIndex]) {
      return (
        <React.Fragment>
          <ButtonC
            icon="pi pi-save"
            rounded
            // outlined
            className="myaction_btn"
            onClick={() => {
              updateData(rowData);
            }}
          />
          <ToastMessages ref={toastRef} />
        </React.Fragment>
      );
    }
    return <Button icon="pi pi-save" disabled className="myaction_btn"/>;
 
  };
  const resumeBodyTemplate = (interview) => {
    let resumeLink = FILE_URL.RESUME + interview.resumePath;
    return (
      <a href={resumeLink} target="_blank" className="int-link-cell">
        {interview.resumePath}
      </a>
    );
  };
 
  const columns = [
    {
      header: "Sr. No.",
      body: (data, options) => options.rowIndex + 1,
      //bodyClassName: "int-edit-col",
      bodyClassName: "my_resume-col",
    },
    {
      field: "candidateName",
      header: "Name",
      bodyClassName: "my_resume-col",
      sortable: true,
    },
    {
      field: "resumePath",
      header: "Resume",
      body: resumeBodyTemplate,
      sortable: true,
      bodyClassName: "my_resume-col",
    },
   
    {
      field: "candidateStatusId",
      header: "Status",
      body: SingleSelect,
      bodyClassName: "my_resume-col",
      sortable: true,
    },
    {
      field: "reviewedByEmployeeIds",
      header: "Forward To",
      body: MultiSelect,
      bodyClassName: "my_resume-col",
      sortable: true,
    },
    {
      field: "reason",
      header: "Reason",
      body: textEditor,
      sortable: true,
    },
    {
      header: "Action",
      body: actionBodyTemplate,
      bodyClassName: "my_resume-col",
    },
  ];
  return (
    <div className="my-resume">
      <h3 className="my-resume-title">My Resumes</h3>
 
      <div className="my-resume-table">
        <DataTable
          value={values}
          paginator={values.length > 10}
          removableSort
          rows={10}
          scrollable
          scrollHeight="flex"
        >
          {columns.map((col) => (
            <Column
              field={col.field}
              header={col.header}
              body={col.body}
 
              bodyClassName={"int-col " + col.bodyClassName}
              sortable={col.sortable}
            />
          ))}
 
        </DataTable>
      </div>
    </div>
  );
};
export default MyResume;