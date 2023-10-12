// Dashboard.js
import React from "react";
import DashboardHeader from "./Pages/Header";
import LeftPanel from "./Pages/LeftPanel";
import DashboardContent from "./Pages/DashboardContent";

const Dashboard = () => {
  return (
    <div>
      <DashboardHeader />
      <div className="flex">
        <LeftPanel />
        <div className="MyDashboard">
          <DashboardContent />
        </div>
      </div>
    </div>
  );
};

export default Dashboard;
