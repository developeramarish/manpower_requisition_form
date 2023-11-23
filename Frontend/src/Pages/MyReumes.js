
import React, { useState, useEffect ,useRef} from "react";
import DataTableCustom from "../Components/DataTableCustom";
import DropdownComponent from "../Components/Dropdown";
import MultiSelectDropdown from "../Components/multiselectDropdown";
import "primeicons/primeicons.css";
import ButtonC from "../Components/Button";
import { InputTextarea } from "primereact/inputtextarea";
import { constantResumePath } from "../Components/constant";
import DashboardHeader from "./Header";
import LeftPanel from "./LeftPanel";
 
const MyReumes = () => {
  const [myResumeData, setMyResumeData] = useState({});
  const [statusData, setStatusData] = useState({});
  const [forwardData, setForwardData] = useState({});
 
  const [textBoxValue, setTextBoxValue] = useState([]);
  const [values,setValues ]= useState([]);
 
  const [dbSelect, setDBSelect] = useState();
  const updateDB = (param) => {
    setDBSelect(param);
  };
 
 
  const [dbSelectReason, setDBSelectReason] = useState();
  const updateDBforReason = (param) => {
    console.log(dbSelectReason)
    setDBSelectReason(param);
  };
 
  useEffect(() => {
    const fetchData = () => {
      try {
        fetch(
          "https://localhost:7128/api/Candidatedetail/GetResumeDropdownlist"
        )
          .then((response) => response.json())
          .then((data) => {
            setMyResumeData(data.result);
            setValues(data.result.candidateDetails)
           
          })
          .catch((error) => {
            console.error("Error fetching data:", error);
          });
 
       
      } catch (error) {
        console.error("Error fetching data:", error);
        //   }
      }
    };
 
    fetchData();
  }, []);
 
  const  TextBoxComponent = (reason) => {
    // console.log(reason)
    
    const [textBoxValue, setTextBoxValue] = useState(reason.reason);
    const handleTextBoxChange = (e) => {
    console.log(e.target.value);
    setTextBoxValue(e.target.value);
     };
  return (
      <InputTextarea  value={textBoxValue} onChange={handleTextBoxChange} rows={2} cols={30}  />
      );
  };
    const openPdfInNewTab = (pdfLink) => {
      window.open(pdfLink, "_blank");
  };
  const[candidateStatusId,setCandidateStatusId]=useState();
  const SingleSelect = (data) => {
    const statusop = data.data;
    const id = data.statusId;
    const [selectStatus, setSelectedststatus] = useState(id);
    return (
      <DropdownComponent
      optionLabel="status"
      optionValue="id"
      value={selectStatus}
      options={statusop || []}
      placeholder="Select Status"
      className="w-full md:w-23rem"
      onChange={(e) => {
        console.log("changes done", e.target.value);
        setSelectedststatus(e.target.value);
        setCandidateStatusId(e.target.value); 
      }}
      />
   
      );
    };
  const MultiSelect = (data, param) => {
    const [forwarddata, setForwarddata] = useState(data.data);
    // const id = param.resumereviewer.roleId;
    const [reviewedByEmployeeId, setreviewedByEmployeeId] = useState(null);
    return (
      <MultiSelectDropdown
        data={forwarddata}
        value={reviewedByEmployeeId}
        optionLabel="name"
        options={forwarddata}
        placeholder="Select Forward To"
        maxSelectedLabels={3}
        className="w-full md:w-23rem"
        onChange={(e) => {
          console.log("changes done", e.target.value);
          setreviewedByEmployeeId(e.value);
          param.fnCallback(e.target.value);
        }}
      />
    );
  };
 
  console.log("from  api ",candidateStatusId)
  let i = 1;
  // console.log(i);
  const tableData =
    values.length >0 &&
    values.map((x) => {
      let resumeLink = `${constantResumePath}/Resume/${x.resumePath}`;
      return {
        SrNo: i++,
 
        Resume: (
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
            {x.name}
            {".pdf"}
          </a>
        ),
 
        Status: (
          <SingleSelect
          data={myResumeData.status}
          Candidateid={x.id}
            statusId={x.candidateStatusId}
            fnCallback={updateDB}
          />
        ),
 
        ForwardTo: <MultiSelect data={myResumeData.resumereviewer} />,
 
        Reason: <TextBoxComponent reason={x.reason}    
               fnCallback={updateDBforReason}
        />,
 
        Action: (
          <ButtonC
            icon="pi pi-save"
            rounded
            outlined
            className="mr-2"
            severity="primary"
            onClick={() => {
              updateData(
                x.id,
                x.name,
                x.emailId,
                x.contactNo,
                x.resumePath,
                x.reviewedByEmployeeId,
                x.reviewedByEmployeeIds,
                candidateStatusId,
                x.reason
              );
            }}
          />
        ),
      };
    });
 
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
      //TextBoxComponent
      //candidateStatusId,
      //  reviewedByEmployeeId,
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
          <div className="MyDashboard">
            <div className="">
              <h3 className="text-black-alpha-90  text-2xl font-bold  m-4">
                My Resumes
              </h3>
            </div>
            <div className=" ">
              <DataTableCustom
                data={tableData}
                tableName={"My Resume"}
                searching={true}
                paginator={true}
                row={9}
                sorting={["SrNo", "Resume"]}
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
      </div>
    </>
  );
};
export default MyReumes;
 