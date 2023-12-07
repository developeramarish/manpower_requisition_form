import { useEffect, useState } from "react";
import { Dialog } from "primereact/dialog";
import { DataTable } from "primereact/datatable";
import { Button } from "primereact/button";
import { Column } from "primereact/column";
import { APIPath } from "../Components/constant";
import { constantResumePath, constantAssignmentPath } from "./constant";
import "../styles/layout/InterviewSummary.css";
import MultiSelectDropdown from "./multiselectDropdown";
import InterviewFeedbackComponent from "./InterviewFeedbackComponent";

const InterviewSummary = ({ visible, onHide, mrfId = 1 }) => {
  const [interviewData, setInterviewData] = useState([]);
  const [interviewStatus, setInterviewStatus] = useState([]);
  const [interviewerData, setInterviewerData] = useState([]);
  const [saveBttn, setSaveBttn] = useState([]);
  const [showFeed, setShowFeed] = useState(false);
  const [feedRow, setFeedRow] = useState({});

  useEffect(() => {
    const apiUrl =
      APIPath +
      `Mrfinterviewermap/GetInterviewDetails/GetInterviewDetails?id=${mrfId}&DashBoard=true`;
    fetch(apiUrl)
      .then((response) => response.json())
      .then((response) => {
        const data = response.result;
        let arr = new Array(data.interviewDetails.length).fill(false); // for save bttn
        setInterviewData(data.interviewDetails);
        setInterviewStatus(
          data.interviewstatus.filter((x) => x.candidateorEvalution == "E")
        );
        setInterviewerData(data.interviewReviewer);
        setSaveBttn(arr);
      })
      .catch((error) => {
        console.error("Fetch error:", error);
      });
  }, []);

  const openPdfInNewTab = (pdfLink) => {
    window.open(pdfLink, "_blank");
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
    return [selectedOpt];
  };

  const objToArray = (selectedOpt = []) => {
    return selectedOpt.map((e) => e.employeeId);
  };

  const uploadedOnBodyTemplate = (interview) => {
    return new Date(interview.createdOnUtc)
      .toLocaleDateString()
      .replaceAll("/", "-");
  };

  const statusBodyTemplate = (interview, options) => {
    let is = interviewStatus.filter((x) => x.id == interview.evaluationId);
    return <p className="drop-width">{is[0].status}</p>;
    // return (
    //   <DropdownComponent
    //     optionLabel="status"
    //     optionValue="id"
    //     className="drop-width"
    //     options={interviewStatus}
    //     value={interview.evaluationId}
    //     onChange={(e) => {
    //       let interviewDataCopy = JSON.parse(JSON.stringify(interviewData));
    //       let sv = [...saveBttn];
    //       sv[options.rowIndex] = true;
    //       interviewDataCopy[options.rowIndex].evaluationId = e.target.value;
    //       setInterviewData(interviewDataCopy);
    //       setSaveBttn(sv);
    //     }}
    //   />
    // );
  };

  const resumeBodyTemplate = (interview) => {
    let resumeLink = constantResumePath + interview.resumePath;
    return (
      <a
        href={resumeLink}
        target="_blank"
        rel="noopener noreferrer"
        className="int-link-cell"
        onClick={(e) => {
          e.preventDefault();
          openPdfInNewTab(resumeLink);
        }}
      >
        {interview.resumePath}
      </a>
    );
  };

  const interviewerBodyTemplate = (interview, options) => {
    return (
      <MultiSelectDropdown
        className="drop-width"
        options={interviewerData}
        value={arrayToObj(interviewerData, interview.interviewerEmployeeId)}
        onChange={(e) => {
          let interviewDataCopy = JSON.parse(JSON.stringify(interviewData));
          let sv = [...saveBttn];
          sv[options.rowIndex] = e.value.length > 0 ? true : false;
          interviewDataCopy[options.rowIndex].interviewerEmployeeId =
            objToArray(e.value);
          setInterviewData(interviewDataCopy);
          setSaveBttn(sv);
          console.log(interviewData);
        }}
        optionLabel="name"
        // optionValue="employeeId"
      />
    );
  };
  const feedbackBodyTemplate = (interview) => {
    return (
      <p
        onClick={() => {
          setFeedRow(interview);
          setShowFeed(true);
        }}
        className="int-link-cell"
      >
        Click here
      </p>
    );
  };

  const attachmentBodyTemplate = (interview) => {
    let attachmentLink = constantAssignmentPath + interview.attachment;
    return (
      <a
        href={attachmentLink}
        target="_blank"
        rel="noopener noreferrer"
        className="int-link-cell"
        onClick={(e) => {
          e.preventDefault();
          openPdfInNewTab(attachmentLink);
        }}
      >
        Assignment
      </a>
    );
  };

  const actionBodyTemplate = (interview, options) => {
    if (saveBttn[options.rowIndex]) {
      return <Button icon="pi pi-save" />;
    }
    return <Button icon="pi pi-save" disabled />;
  };

  return (
    <Dialog
      header="MRF ID (Interview Summary)"
      visible={visible}
      onHide={onHide}
      draggable={false}
      className="int-card"
    >
      <DataTable
        value={interviewData}
        paginator
        removableSort
        rows={10}
        scrollable
        scrollHeight="62vh"
        draggable={false}
      >
        <Column
          field="referenceNo"
          header="Sr. No."
          body={(data, options) => options.rowIndex + 1}
          bodyClassName="int-col int-edit-col"
        />
        <Column
          field="resumePath"
          header="Resume"
          body={resumeBodyTemplate}
          bodyClassName="int-col"
          sortable
        />
        <Column
          field="createdOnUtc"
          header="Uploaded On"
          body={uploadedOnBodyTemplate}
          bodyClassName="int-col"
          sortable
        />
        <Column
          field="createdName"
          header="Uploaded By"
          bodyClassName="int-col"
          sortable
        />
        <Column
          field="interviewerName"
          header="Interviewer/ Panel"
          body={interviewerBodyTemplate}
          bodyClassName="int-col drop-col"
          sortable
        />
        <Column
          field="evaluationId"
          header="Interview Status"
          body={statusBodyTemplate}
          bodyClassName="int-col int-status-col"
          sortable
        />
        <Column
          field="evalutionStatus"
          header="Interview Feedback"
          body={feedbackBodyTemplate}
          bodyClassName="int-col"
          sortable
        />
        <Column
          field="attachment"
          header="Attachment"
          body={attachmentBodyTemplate}
          bodyClassName="int-col"
          sortable
        />
        <Column
          header="Action"
          body={actionBodyTemplate}
          bodyClassName="int-col int-edit-col"
        />
      </DataTable>
      <InterviewFeedbackComponent
        visible={showFeed}
        onHide={() => setShowFeed(false)}
        intData={feedRow}
      />
    </Dialog>
  );
};

export default InterviewSummary;
