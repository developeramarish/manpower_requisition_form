
import React, { useState, useEffect, useRef } from "react";
import DropdownComponent from "../Components/Dropdown";
import MultiSelectDropdown from "../Components/multiselectDropdown";
import "primeicons/primeicons.css";
import ButtonC from "../Components/Button";
import { InputTextarea } from "primereact/inputtextarea";
import { constantResumePath } from "../Components/constant";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import { InputText } from "primereact/inputtext";
import { Tag } from "primereact/tag";
import { MultiSelect } from "primereact/multiselect";
const MyReumes = () => {
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

  // const  TextBoxComponent = (reason,param) => {
  //    const [textBoxValue, setTextBoxValue] = useState(reason.reason);
  //   const handleTextBoxChange = (e) => {
  //   console.log(e.target.value);
  //  // setTextBoxValue(e.target.value);
  //   reason.editorCallback(e.target.value);
  //    };
  // return (
  //     <InputTextarea  value={textBoxValue} onChange={handleTextBoxChange} rows={2} cols={30}  />
  //     );
  // };
  const openPdfInNewTab = (pdfLink) => {
    window.open(pdfLink, "_blank");

  };
  const SingleSelect = (statusdata) => {
    const id = statusdata.candidateStatusId;
    const [selectStatus, setSelectedststatus] = useState(id);
    return (
      <DropdownComponent
        optionLabel="status"
        optionValue="id"
        value={statusdata.candidateStatusId}
        options={statusData || []}
        placeholder="Select Status"
        className="w-full md:w-23rem"
        onChange={(e) => statusdata.editorCallback(e.target.value)}
      />
    );
  };
  const MultiSelect1 = (data) => {
    console.log("multiselect", data);
  //   const[review,setreviewedByEmployeeId]=useState(data.reviewedByEmployeeIds);
  //  console.log(review);
    return (
      // <MultiSelectDropdown
      //   data={forwardData}
      //   value={reviewedByEmployeeId}
      //   optionLabel="name"
      //   options={forwardData}
      //   placeholder="Select Forward To"
      //   maxSelectedLabels={3}
      //   className="w-full md:w-23rem"
      // // onChange={(e) => { 
      //   //   console.log("changes done", e.target.value);
      //   //   setreviewedByEmployeeId(e.value);
      //   //   //param.fnCallback(e.target.value);
      //   // }}
      //   onChange= {(e) =>  data.editorCallback(e.target.value)}
      // />
      <MultiSelect
      onChange={(e)=>{
        data.editorCallback(e.value)
      }}
      options={forwardData}
      optionLabel="name"
      optionValue="employeeId"
      
    />
     
    );
  };
  const statusBodyTemplate = (rowData) => {
    return <Tag value={rowData.reviewedByEmployeeIds} ></Tag>;
};
  const updateData = (
    id,
    name,
    emailId,
    contactNo,
    resumePath,
    reviewedByEmployeeId,
    reviewedByEmployeeIds,
    candidateStatusId,
    reason

  ) => {
    const empdata = {
      name,
      emailId,
      contactNo,
      resumePath,
      reviewedByEmployeeId,
      reviewedByEmployeeIds,
      candidateStatusId,
      reason
    };
    fetch("https://localhost:7128/api/Candidatedetail/Put/" + id, {
      method: "Put",
      headers: { "content-type": "application/json" },
      body: JSON.stringify(empdata),
    })
      .then((res) => {
        alert("updated successfully.");
      })
      .catch((err) => {
        console.log(err.message);
      });
  };
  const header = <h3 className="req-header">My Resume</h3>;
  const textEditor = (options) => {
    console.log("reason", options.value);
    return <InputText type="text" value={options.value}
      onChange={(e) => options.editorCallback(e.target.value)
      } />;
  }

  const actionBodyTemplate = (rowData) => {
    console.log("click", rowData);
    return (
      <React.Fragment>
        <ButtonC icon="pi pi-pencil" rounded outlined className="mr-2" onClick={() => {
          updateData(rowData.id,
            rowData.name, rowData.emailId, rowData.contactNo, rowData.resumePath, rowData.reviewedByEmployeeId, rowData.reviewedByEmployeeIds
            , rowData.candidateStatusId, rowData.reason)
        }} />


      </React.Fragment>
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

  const objToArray = (selectedOpt = []) => {
    return selectedOpt.map((e) => e.employeeId);
  };
  const ResumeHyperLink = (resume) => {
    console.log(resume)
    let resumeLink = `${constantResumePath}/Resume/${resume.resumePath}`;

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
          style={{ color: "red" }}
        >
          {resume.name}
          {".pdf"}
        </a>
      </div>
    )
  }
  return (
    <div className="req-card">
      <DataTable
        value={values}
        paginator
        removableSort
        rows={6}
        scrollable
        header={header}
        scrollHeight="62vh"
      >
        <Column
          field="resumePath"
          header="Resume"
          body={ResumeHyperLink}
        ></Column>
        <Column
          field="reviewedByEmployeeIds"
          header="Forward to"
          body={MultiSelect1}
          editor={(options) => MultiSelect1(options)}
        ></Column>
        <Column
          field="candidateStatusId"
          header="Status"
          body={SingleSelect}
          editor={(options) => SingleSelect(options)}
        ></Column>
        <Column
          field="reason"
          header="Reason"
          //body={TextBoxComponent}
          editor={(options) => textEditor(options)}
        ></Column>

        <Column

          headerStyle={{ width: '10%', minWidth: '8rem' }}
          bodyStyle={{ textAlign: 'center' }}
          body={actionBodyTemplate}>
        </Column>
      </DataTable>
    </div>

  );
};
export default MyReumes;
