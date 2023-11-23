// import React, { useEffect, useState } from "react";
// import DataTableComponents from "./Components/DataTableComponent";
import SearchHeader from "./Components/SearchHeader";
import DashboardHeader from "./Pages/Header";
import LeftPanel from "./Pages/LeftPanel";
import MyRequisitionsBody from "./Pages/MyRequisitionsBody";

const MyRequisitions = () => {
  return (
    <>
      <DashboardHeader />
      <div className="flex bg-gray-200">
        <LeftPanel />
        <div className="flex flex-column gap-2 w-full p-3 py-2 h-full ">
          <SearchHeader title="My Requisitions" />
          <MyRequisitionsBody />
        </div>
      </div>
    </>
  );

  // const [data, setData] = useState([{}]);
  // //if we pass id 0 then ge get all the data otherwise we get specific data like id=1 means
  // React.useEffect(() => {
  //   const url = "https://localhost:7128/api/Mrfdetail/GetMrfDetails/0";
  //   fetch(url)
  //     .then((response) => {
  //       return response.json();
  //     })
  //     .then((json) => {
  //       setData(json["result"]);
  //     })

  //     .catch((error) => console.log(error));
  // }, []);
  // console.log(data);
  // const columns = [
  //   { columnName: "MRF ID", field: "referenceNo" },
  //   { columnName: "Created By", field: "name" },
  //   { columnName: "Created On", field: "createdOnUtc" },
  //   { columnName: "Last Updated", field: "updatedOnUtc" },
  //   { columnName: "Requision Type", field: "requisitionType" },
  //   { columnName: "No Of Positions", field: "vacancyNo" },
  //   { columnName: "Exp Required", field: "experience" },
  //   { columnName: "Salary Range", field: "salary" },
  //   { columnName: "Status", field: "mrfStatus" },
  // ];

  // return (
  //   <div>
  //     <DashboardHeader />
  //     <div style={{ display: "flex" }}>
  //       <LeftPanel />

  //       <div className="bar">
  //         <div class="containerH">
  //           <label class="box">My Requisitions</label>
  //           <div class="SearchText">
  //             <SearchText />
  //           </div>
  //         </div>

  //         <div className="bar">
  //           <DataTableComponents data={data} columns={columns} rows={5} />
  //         </div>
  //       </div>
  //     </div>
  //   </div>
  // );
};
export default MyRequisitions;
