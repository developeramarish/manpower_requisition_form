import React, { useState, useEffect } from "react";
import DataTableCustom from "../Components/DataTableCustom";
import DropdownComponent from "../Components/Dropdown";
import MultiSelectDropdown from "../Components/multiselectDropdown";
import InputTextareaComponent from "../Components/InputTextarea";
import "primeicons/primeicons.css";
import ButtonC from "../Components/Button";
import { MultiSelect } from "primereact/multiselect";
import { InputTextarea } from "primereact/inputtextarea";
import { constantResumePath } from "../Components/constant";
import DashboardHeader from "./Header";
import LeftPanel from "./LeftPanel";

const MyReumes = () => {
  const [myResumeData, setMyResumeData] = useState({});
  const [myResumeDsata, setMyResumeDsata] = useState({});

  const [textBoxValue, setTextBoxValue] = useState([]);

  const [dbSelect, setDBSelect] = useState();
  const updateDB = (param) => {
    setDBSelect(param);
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

  const TextBoxComponent = (reason) => {
    // console.log(reason)
    const [textBoxValue, setTextBoxValue] = useState(reason.reason);
    const handleTextBoxChange = (e) => {
      console.log(e.target.value);
      setTextBoxValue(e.target.value);
      // Additional logic if needed
    };
    return (
      <InputTextarea value={textBoxValue} onChange={handleTextBoxChange} />
    );
  };

  const openPdfInNewTab = (pdfLink) => {
    window.open(pdfLink, "_blank");
  };

  const SingleSelect = (data, param) => {
    // console.log(statusId)
    const statusop = data.data;
    const id = param.statusId;
    const [selectStatus, setSelectedststatus] = useState(id);
    // console.log(param);
    return (
      <DropdownComponent
        optionLabel="status"
        optionValue="id"
        value={selectStatus}
        options={statusop || []}
        placeholder="Select Status"
        onChange={(e) => {
          console.log("changes done", e.target.value);
          setSelectedststatus(e.target.value);
          param.fnCallback(e.target.value);
        }}
      />
    );
  };

  // const MultiSelect = (data) => {
  //   console.log(data.data)
  //   const [forwarddata,setForeard]=useState(data.data)
  //        const [reviewedByEmployeeId, setreviewedByEmployeeId] = useState(null);
  //     return (
  //         <MultiSelectDropdown
  //           data={forwarddata}
  //           value={reviewedByEmployeeId}
  //           optionLabel="name"
  //           options={forwarddata}
  //           placeholder="Select Forward To"
  //           maxSelectedLabels={3}
  //           onChange={(e) => {
  //               console.log("changes done", e);
  //               setreviewedByEmployeeId(e.value);
  //             }}
  //           />
  //         );
  //       };

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
        onChange={(e) => {
          console.log("changes done", e.target.value);
          setreviewedByEmployeeId(e.value);
          // param.fnCallback(e.target.value);
        }}
      />
    );
  };

  console.log(myResumeData);
  console.log(myResumeDsata);
  const value = myResumeData.candidateDetails;

  let i = 1;
  // console.log(i);
  const tableData =
    value &&
    value.map((x) => {
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
            statusId={x.candidateStatusId}
            fnCallback={updateDB}
          />
        ),

        ForwardTo: <MultiSelect data={myResumeData.resumereviewer} />,

        Reason: <TextBoxComponent reason={x.reason} />,

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
                dbSelect
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
    empId,
    selectVal
  ) => {
    console.log("id", id);
    console.log("status ", selectVal);
    // console.log("reason",)
    const empdata = {
      name,
      emailId,
      contactNo,
      resumePath,
      // candidateStatusId
      //TextBoxComponent
      //candidateStatusId,
      //  reviewedByEmployeeId,
    };
    fetch("https://localhost:7128/api/Candidatedetail/" + id, {
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

  console.log(tableData);

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
              <h3 className="text-black-alpha-90	text-2xl font-bold  m-4">
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
