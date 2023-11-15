import React, { useState, useEffect } from "react";
import SearchText from "./SearchText";
import DataTableCustom from "../Components/DataTableCustom";
import { InputTextarea } from "primereact/inputtextarea";
import DropdownComponent from "../Components/Dropdown";
import MultiSelectDropdown from "../Components/multiselectDropdown";
import InputTextareaComponent from "../Components/InputTextarea";
import "primeicons/primeicons.css";
import ButtonC from "../Components/Button";

const MyReumes = () => {
  const [statusOptions, setStatusOptions] = useState([]);
  const [forwardOptions, setForwardOptions] = useState([]);
  const [myResumeData, setMyResumeData] = useState([]);
  const [candidateStatusId, setSelectedststatus] = useState(null);
  const [selectedforwardedStatus, setForwardedStatus] = useState(null);


  useEffect(() => {
    const fetchData = async () => {
      try {
        // Fetch statusOptions
        const responseStatus = await fetch(
          "https://localhost:7128/api/Candidatestatus"
        );
        if (!responseStatus.ok) {
          throw new Error("Network response was not ok");
        }
        const dataStatus = await responseStatus.json();
        if (Array.isArray(dataStatus.result)) {
          const value = dataStatus.result.map((x) => {
            return { id: x.id, status: x.status };
          });

          setStatusOptions(value);
          console.log("candidate Status    ", value);
        } else {
          console.log("Api is not an array");
        }

        // Fetch Candidatedetail
        const responseCandidateDetail = await fetch(
          "https://localhost:7128/api/Candidatedetail"
        );
        if (!responseCandidateDetail.ok) {
          throw new Error("Network response was not ok");
        }
        const dataCandidateDetail = await responseCandidateDetail.json();
        if (Array.isArray(dataCandidateDetail.result)) {
          setMyResumeData(dataCandidateDetail.result);
          const detail = dataCandidateDetail.result;

          console.log("candidate deatil    ", dataCandidateDetail.result);
        } else {
          console.log("Api is not an array");
        }

        // Fetch Employeerolemap
        const responseEmployeeRole = await fetch(
          "https://localhost:7128/api/Employeerolemap/GetEmployeebyRole/5"
        );
        if (!responseEmployeeRole.ok) {
          throw new Error("Network response was not ok");
        }
        const dataEmployeeRole = await responseEmployeeRole.json();
        if (Array.isArray(dataEmployeeRole.result)) {
          const forwardOptions = dataEmployeeRole.result;
          const value = dataEmployeeRole.result.map((x) => {
            return { id: x.id, name: x.name };
          });
          setForwardOptions(value);
          console.log("candidate employee role    ", value);
        } else {
          console.log("Api is not an array");
        }
      } catch (error) {
        console.error("Error fetching data:", error);
      }
    };

    fetchData();
  }, []);
  const TextBoxComponent = (Reason) => {
    const [textBoxValue, setTextBoxValue] = useState(Reason.value);
    const handleTextBoxChange = (e) => {
      console.log("changes",e.target.value);
      setTextBoxValue(e.target.value);
      // Additional logic if needed
    };
    return (
      <InputTextareaComponent
        value={textBoxValue}
        onChange={handleTextBoxChange}
      />
    );
  };
  

  


  const openPdfInNewTab = (pdfLink) => {
    window.open(pdfLink, "_blank");
  };

  const value = myResumeData;
  let i = 1;

  const tableData =
    value &&
    value.map((x) => {
      return {
        SrNo: i++,
        Resume: (
          <a
            href={x.resumePath}
            target="_blank"
            rel="noopener noreferrer"
            onClick={(e) => {
              e.preventDefault();
              openPdfInNewTab(x.resumePath);
            }}
            style={{ color: "red" }}
          >
            {x.name}
            {".pdf"}
          </a>
        ),

        Status: (
          <DropdownComponent
            optionLabel="status"
            optionValue="id"
            options={statusOptions || []}
            placeholder={"Select Status"}
            onChange={e =>{
              console.log("changes done",e);
              setSelectedststatus(e.target.value)
            }}
          />
        ),

        ForwardTo: (<MultiSelectDropdown 
          data={forwardOptions}
        value={forwardOptions}
        optionLabel="name"
        placeholder="Select Forward To"
        options={forwardOptions}
        maxSelectedLabels={3}
        onChange={e =>{
          console.log("changes done",e);
          setForwardedStatus(e.value)
        }}
        />),
       
        Reason: <TextBoxComponent value={x.reason} />,
        
        Action: (
          <ButtonC
            icon="pi pi-save"
            rounded
            outlined
            className="mr-2"
            severity="primary"
            onClick={() => {
             
              updateData(x.id,x.name,x.emailId,x.contactNo,x.resumePath,x.reviewedByEmployeeId);
            }}
          />
        ),
      };
    });

    

  const updateData = (id,name,emailId,contactNo,resumePath,reviewedByEmployeeId) => {
    console.log("frromm updatea data", id);
    
    const empdata = {name,emailId,contactNo,resumePath,candidateStatusId,reviewedByEmployeeId};
    fetch("https://localhost:7128/api/Candidatedetail/"+id, {
      method: "Put",
      headers: { "content-type": "application/json" },
      body: JSON.stringify(empdata)
       
    }).then((res) => {
      alert('updated successfully.')

    }).catch((err) => {
      console.log(err.message)
    })

  

  };

  return (
    <div>
      <div className="MyDashBoard">
        <div className="containerH">
          <div className="box">
            <label>My Resumes</label>
          </div>
        </div>
        <div>
          <DataTableCustom
            data={tableData}
            tableName={"My Resume"}
            searching={true}
            paginator={true}
            // addTextBoxTo={"Reason"}
            // addMultiSelect={multiSelectObject}
            // addSingleSelect={singleSelectObject}
            row={9}
            sorting={["SrNo", "Resume"]}
            singleSelectLabel={"status"}
            multiSelectLabel={"name"}
            showColum={[
              "SrNo",
              "Resume",
              "Status",
              "ForwardTo",
              "Reason",
              "Action",
            ]}
          />
        </div>
      </div>
    </div>
  );
};

export default MyReumes;
