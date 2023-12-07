import React, { useState, useEffect, useRef } from "react";
import { Button } from "primereact/button";
import { Dialog } from "primereact/dialog";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import ToastMessages from "../Components/ToastMessages";
import { APIPath, constantResumePath } from "../Components/constant";
import "../styles/layout/ResumeSummary.css";
import MultiSelectDropdown from "../Components/multiselectDropdown";

const MyResumeDetail = ({ visible, onHide, mrfId = 2,dashboard=true }) => {
  const [data, setdata] = useState([]);
  const [resumeReviewer, setResumeReviewer] = useState([]);
  const [saveBttn, setSaveBttn] = useState([]);
  const toastRef = useRef(null);
  useEffect(() => {
    const fetchData = () => {
      try {
        fetch(
          `${APIPath}Mrfresumereviewermap/GetResumeStatusDetails/GetResumeStatusDetails?id=${mrfId}&DashBoard=${dashboard}`
        )
          .then((response) => response.json())
          .then((data) => {
            setdata(data.result.resumeDetails);
            setResumeReviewer(data.result.employeeRoleMap);
            let array = new Array(data.result.resumeDetails.length).fill(false);
            setSaveBttn(array);
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

  const MultiSelectDrop = (rowData, options) => {
    console.log(options)

    return (
      <div>
        <MultiSelectDropdown
          id="resumeReviewerEmployeeIds"
          value={arrayToObj(
            resumeReviewer,
            strToArray(rowData.resumeReviewerEmployeeIds)
          )}
          onChange={(e) => {
            let resumeReviewers = JSON.parse(JSON.stringify(data));
            let sv = [...saveBttn];
            sv[options.rowIndex] = e.value.length > 0 ? true : false;
            resumeReviewers[options.rowIndex].resumeReviewerEmployeeIds =
              objToArray(e.value);
            setdata(resumeReviewers);
            setSaveBttn(sv);
          }}
          options={resumeReviewer}
          optionLabel="name"
          filter
          placeholder="Select Reviewer"
          // maxSelectedLabels={5}
          className="w-full md:w-20rem "
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
    // return [selectedOpt];
  };

  const objToArray = (selectedOpt = []) => {
    return selectedOpt.map((e) => e.employeeId);
  };

  const openPdfInNewTab = (pdfLink) => {
    window.open(pdfLink, "_blank");
  };

  const actionBodyTemplate = (rowData, options) => {
    const onClickHandleSave = () => {
      update(rowData);
      let sv = [...saveBttn];
      sv[options.rowIndex] = false;
      setSaveBttn(sv);
    };

    if (saveBttn[options.rowIndex]) {
      return <Button icon="pi pi-save " onClick={onClickHandleSave} />;
    }
    return <Button icon="pi pi-save" disabled />;
  };
  const update = async (data) => {
    console.log(data);
    const resumeRevierInArray = data.resumeReviewerEmployeeIds;
    const reviewedByEmployeeIds = resumeRevierInArray.toString();
    const name = "string"; // this because we are handling data in backend it not save as string
    const emailId = "string";
    const contactNo = "string";

    const id = data.candidateId;
    const candidateStatusId = data.candidateStatusId;
    const createdName = data.createdName;
    const mrfId = data.mrfId;
    const reason = data.reason;
    const referenceNo = data.referenceNo;
    const resumePath = data.resumePath;
    const createdByEmployeeId = data.createdByEmployeeId;
    const createdOnUtc = data.createdOnUtc;

    const candidateDetailsData = {
      id,
      mrfId,
      name,
      emailId,
      contactNo,
      resumePath,
      candidateStatusId,
      reviewedByEmployeeIds,
      createdByEmployeeId,
      createdOnUtc,
      reason,
    };

    try {
      const response = await fetch(`${APIPath}Candidatedetail/Put/${id}`, {
        method: "Put",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(candidateDetailsData),
      });

      if (response.ok) {
        const responseData = await response.json();
        if (responseData.statusCode === 409) {
          toastRef.current.showConflictMessage(responseData.message);
        } else {
          toastRef.current.showSuccessMessage(
            "Resume Reviewers updated successfully!"
          );
        }
      } else {
        console.error("Request failed with status:", response.status);
        const errorData = await response.text();
        console.error("Error Data:", errorData);
        if (response.status === 400) {
          toastRef.current.showBadRequestMessage(
            "Bad request: " + response.url
          );
        }
      }
    } catch (error) {
      console.error("Error:", error);
    }
  };

  const createdOnBodyTemplate = (mrf) => {
    return new Date(mrf.createdOnUtc).toLocaleDateString().replaceAll("/", "-");
  };

  const columnHeaderTemplate = (title) => {
    return <h3 className="resume-table-header">{title}</h3>;
  };

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
          rows={10}
          scrollable
        scrollHeight="400px"
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
            sortable
          ></Column>
          <Column
            field="createdOnUtc"
            header={columnHeaderTemplate("Uploaded On")}
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
            body={MultiSelectDrop}
            bodyClassName="resume-col  "
            sortable
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
            sortable
          ></Column>
          <Column
            header={columnHeaderTemplate("Action")}
            bodyStyle={{ textAlign: "left" }}
            body={actionBodyTemplate}
          ></Column>
        </DataTable>
        <ToastMessages ref={toastRef} />
      </Dialog>
    </div>
  );
};

export default MyResumeDetail;
