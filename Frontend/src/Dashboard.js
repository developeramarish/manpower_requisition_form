// Dashboard.js
import React from 'react';
import DashboardHeader from './Pages/Header';
import LeftPanel from './Pages/LeftPanel';
import DashboardContent from './Pages/DashboardContent';


const Dashboard = () => {
  return (
    <div>
      <DashboardHeader />
      <div style={{ display: 'flex' }}>
        <LeftPanel />
        <div class="MyDashboard">
        <DashboardContent />
        </div>
      </div>
    </div>
  );
};

export default Dashboard;
