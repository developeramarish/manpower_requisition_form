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


  console.log(myResumeData)
console.log(myResumeData.value)
const [value, setValue] = useState(myResumeData);

  

  useEffect(() => {
    fetch("https://localhost:7128/api/Candidatestatus")
      .then((response) => response.json())
      .then((data) => {
        console.log("candidate Status    ", data);
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
        console.log("candiate detail  ", data);
        if (Array.isArray(data.result)) {
          let i = 1;
          const value = data.result.map((x) => {
            // const [value, setValue] = useState(x.reason);
            return {
              SrNo: i++,
              Resume: x.resumePath,
               Status:"",
              // Status:(<DropdownComponent optionLabel="status"
              // optionValue="id"   options={statusOptions || [] } selectedOption={statusOptions}
              // onChange={(e) =>
              //   setFormData({ ...formData, statusOptions: e.target.value })
              // }
              // />),
            //   Status: (
            //     <SingleDropDownOptions
            //       singleSelectOptions={statusOptions}
            //       optionLabel={"status"}
            //       placeholder={"Select Status"}
            //     />
            //  ),
              ForwardTo: "",
                           
              Reason: (
                

                <InputTextarea
                  className="border-round-md 	"
                  value={x.reason || ""}
                  placeholder="Enter Reason"
                  rows={2}
                  cols={25}
                  onChange={(e) => setValue(e.target.value)}
                />
              ),
              // Edit:"", 
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
        console.log("get employee role   ", data);
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

  // useEffect(() => {
  //   const fetchData = async () => {
  //     try {
  //       // Fetch statusOptions
  //       const responseStatus = await fetch("https://localhost:7128/api/Candidatestatus");
  //       if (!responseStatus.ok) {
  //         throw new Error("Network response was not ok");
  //       }
  //       const dataStatus = await responseStatus.json();
  //       if (Array.isArray(dataStatus.result)) {
  //         const value = dataStatus.result.map((x) => {
  //           return { id: x.id, status: x.status };
  //         });
  //         setStatusOptions(value);
  //       } else {
  //         console.log("Api is not an array");
  //       }

  //       // Fetch Candidatedetail
  //       const responseCandidateDetail = await fetch("https://localhost:7128/api/Candidatedetail");
  //       if (!responseCandidateDetail.ok) {
  //         throw new Error("Network response was not ok");
  //       }
  //       const dataCandidateDetail = await responseCandidateDetail.json();
  //       if (Array.isArray(dataCandidateDetail.result)) {
  //         let i = 1;
  //         const value = dataCandidateDetail.result.map((x) => {
  //           return {
  //             SrNo: i++,
  //             Resume: x.resumePath,
  //               Status: ( < DropdownComponent   optionLabel={"status"}     placeholder={"Select Status"}  options={statusOptions || []} />),

  //             // Status: (
  //             //   <SingleDropDownOptions
  //             //     singleSelectOptions={statusOptions}
  //             //     optionLabel={"status"}
  //             //     placeholder={"Select Status"}
  //             //   />
  //             // ),
  //             ForwardTo: "",
  //             Reason: (
  //               <InputTextarea
  //                 value={x.reason}
  //                 placeholder="Enter Reason"
  //                 rows={2}
  //                 cols={25}
  //               />
  //             ),
  //           };
  //         });
  //         setMyResumeData(value);
  //       } else {
  //         console.log("Api is not an array");
  //       }

  //       // Fetch Employeerolemap
  //       const responseEmployeeRole = await fetch("https://localhost:7128/api/Employeerolemap/GetEmployeebyRole/5");
  //       if (!responseEmployeeRole.ok) {
  //         throw new Error("Network response was not ok");
  //       }
  //       const dataEmployeeRole = await responseEmployeeRole.json();
  //       if (Array.isArray(dataEmployeeRole.result)) {
  //         const forwardOptions = dataEmployeeRole.result;
  //         const value = dataEmployeeRole.result.map((x) => {
  //           return { id: x.id, name: x.name };
  //         });
  //         setForwardOptions(value);
  //       } else {
  //         console.log("Api is not an array");
  //       }
  //     } catch (error) {
  //       console.error("Error fetching data:", error);
  //     }
  //   };

  //   fetchData();
  // }, []);

  const [resumeData, setResumeData] = useState([
    {
      SrNo: 1,
      Resume: "Ramksrishna",
      Status: "",
      ForwardTo: "DummyForForward",
      Reason: <InputTextarea placeholder="Enter Reason" rows={1} cols={25} />,
    },
    {
      SrNo: 2,
      Resume: "Ashutosh m",
      Status: "",
      ForwardTo: "DummyForForward",
      Reason: <InputTextarea placeholder="Enter Reason" rows={1} cols={25} />,
    },
  ]);

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
        </div>
        <div>
          <DataTableCustom
            data={myResumeData}
            // data={resumeData}
            tableName={"My Resume"}
            searching={true}
            paginator={true}
            //addTextBoxTo={"Reason"}
            addMultiSelect={multiSelectObject}
            addSingleSelect={singleSelectObject}
            row={9}
            sorting={["SrNo","Resume"]}
            singleSelectLabel={"status"}
            multiSelectLabel={"name"}
            showColum={["SrNo", "Resume", "Status", "ForwardTo","Reason","Edit"]}
          />
        </div>
      </div>
    </div>
  );
};

export default MyReumes;
