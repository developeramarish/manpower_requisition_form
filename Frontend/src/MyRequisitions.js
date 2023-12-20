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
          <MyRequisitionsBody roleId={4} />
        </div>
      </div>
    </>
  );
};
export default MyRequisitions;
