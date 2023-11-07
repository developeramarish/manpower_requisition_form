import React, { useState, useEffect } from "react";
import SearchText from "./SearchText";
import DataTableCustom from "../Components/DataTableCustom";
import { InputTextarea } from "primereact/inputtextarea";
import DropdownComponent from "../Components/Dropdown";
import {
  SingleDropDownOptions,
  MultiDropDownOptions,
} from "../Components/DataTableCustomComponents";
const MyReumes = () => {
  const [statusOptions, setStatusOptions] = useState([]);
  const [forwardOptions, setForwardOptions] = useState([]);
  const [myResumeData, setMyResumeData] = useState([]);

  useEffect(() => {
    fetch("https://localhost:7128/api/Candidatestatus")
      .then((response) => response.json())
      .then((data) => {
        if (Array.isArray(data.result)) {
          const value = data.result.map((x) => {
            return { id: x.id, status: x.status };
          });
          setStatusOptions(value);
        } else {
          console.log("Api is not array");
        }
      })
      .catch((error) => {
        console.error("Error fetching data:", error);
      });

    fetch("https://localhost:7128/api/Candidatedetail")
      .then((response) => response.json())
      .then((data) => {
        if (Array.isArray(data.result)) {
          let i = 1;
          const value = data.result.map((x) => {
            return {
              SrNo: i++,
              Resume: x.resumePath,
              Status:"",
              // Status: ( < DropdownComponent   optionLabel={"status"}     placeholder={"Select Status"}  options={statusOptions || []} />),
              // Status: (<SingleDropDownOptions  singleSelectOptions={statusOptions} optionLabel={"status"} placeholder={"Select Status"}  />   ),
              ForwardTo: "",
              Reason: (
                <InputTextarea
                  value={x.reason}
                  placeholder="Enter Reason"
                  rows={2}
                  cols={25}
                />
              ),
            };
          });
         
          setMyResumeData(value);
        } else {
          console.log("Api is not array");
        }
      })
      .catch((error) => {
        console.error("Error fetching data:", error);
      });

    fetch("https://localhost:7128/api/Employeerolemap/GetEmployeebyRole/5")
      .then((response) => response.json())
      .then((data) => {
        if (Array.isArray(data.result)) {
          const forwardOptions = data.result;
          const value = data.result.map((x) => {
            return { id: x.id, name: x.name };
          });
          setForwardOptions(value);
        } else {
          console.log("Api is not array");
        }
      })
      .catch((error) => {
        console.error("Error fetching data:", error);
      });
  }, []);



  
  // const [resumeData, setResumeData] = useState([
  //   {
  //     SrNo: 1,
  //     Resume: "Ramksrishna",
  //     Status: "",
  //     ForwardTo: "DummyForForward",
  //     Reason: <InputTextarea placeholder="Enter Reason" rows={1} cols={25} />,
  //   },
  //   {
  //     SrNo: 2,
  //     Resume: "Ashutosh m",
  //     Status: "",
  //     ForwardTo: "DummyForForward",
  //     Reason: <InputTextarea placeholder="Enter Reason" rows={1} cols={25} />,
  //   },
  //   {
  //     SrNo: 3,
  //     Resume: "Gaurav",
  //     Status: "",
  //     ForwardTo: "DummyForForward",
  //     Reason: <InputTextarea placeholder="Enter Reason" rows={1} cols={25} />,
  //   },
  //   {
  //     SrNo: 4,
  //     Resume: "Aditya",
  //     Status: "DummyDataforDroDown",
  //     ForwardTo: "DummyForForward",
  //     Reason: <InputTextarea placeholder="Enter Reason" rows={1} cols={25} />,
  //   },
  //   {
  //     SrNo: 5,
  //     Resume: "priyanka",
  //     Status: "",
  //     ForwardTo: "DummyForForward",
  //     Reason: <InputTextarea placeholder="Enter Reason" rows={2} cols={25} />,
  //   },
  //   {
  //     SrNo: 6,
  //     Resume: "Manish",
  //     Status: "Dummy",
  //     ForwardTo: "DummyForForward",
  //     Reason: <InputTextarea placeholder="Enter Reason" rows={1} cols={25} />,
  //   },
  //   {
  //     SrNo: 7,
  //     Resume: "Pratey",
  //     Status: "DummyDataforDroDown",
  //     ForwardTo: "DummyForForward",
  //     Reason: <InputTextarea placeholder="Enter Reason" rows={1} cols={25} />,
  //   },
  // ]);

  const multiSelectObject = {
    ForwardTo: forwardOptions,

    //add more option in this way
  };

  const singleSelectObject = {
    Status: statusOptions,

    //add more option in this way
  };

  return (
    <div>
      <div className="MyDashBoard">
        <div className="containerH">
          <div className="box">
            <label>My Resumes</label>
          </div>
          <SearchText />
        </div>
        <div className="bar">
          <DataTableCustom
            data={myResumeData}
            // data={resumeData}
            tableName={"My Resume"}
            searching={true}
            paginator={true}
            // addTextBoxTo={"Reason"}
            addMultiSelect={multiSelectObject}
            addSingleSelect={singleSelectObject}
            row={9}
            singleSelectLabel={"status"}
            multiSelectLabel={"name"}
            showColum={["SrNo", "Resume", "Status", "ForwardTo", "Reason"]}
          />
        </div>
      </div>
    </div>
  );
};

export default MyReumes;
