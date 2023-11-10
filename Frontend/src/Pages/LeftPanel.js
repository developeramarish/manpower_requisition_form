// LeftPanel.js
import React from "react";
import { Menubar } from "primereact/menubar";
import "../styles/layout/LeftPanel.css";
import { Link } from "react-router-dom";

const LeftPanel = () => {
  const items = [
    {
      label: (
        <Link to="/Dashboard" className="menu-link-class">
          <i className="pi pi-chart-bar text-2xl"></i>

          <h4 className="my-2 line-height-2 text-center text-xs">
            MY DASHBOARD
          </h4>
        </Link>
      ),
    },
    {
      label: (
        <Link to="/MyRequisitions" className="menu-link-class">
          <i className="pi pi-list text-2xl"></i>
          <h4 className="my-2 line-height-2 text-center text-xs">
            MY REQUISITIONS
          </h4>
        </Link>
      ),
    },
    {
      label: (
        <Link to="/EmployeDetails" className="menu-link-class">
          <i className="pi pi-file-edit text-2xl"></i>
          <h4 className="my-2 line-height-2 text-center text-xs">
             Create EmployeDetails
          </h4>
        </Link>
      ),
    },
    {
      label: (
        <Link to="/CreateRequisition" className="menu-link-class">
          <i className="pi pi-file-edit text-2xl"></i>
          <h4 className="my-2 line-height-2 text-center text-xs">
            CREATE REQUISITIONS
          </h4>
        </Link>
      ),
    },
    {
      label: (
        <Link to="/MyReumes" className="menu-link-class">
          <i className="pi pi-file-edit text-2xl"></i>
          <h4 className="my-2 line-height-2 text-center text-xs">
            My Resume
          </h4>
        </Link>
      ),
    },
    
    // Add more menu items as needed
  ];

  // const start = <div></div>;

  return (
    <Menubar model={items} className="menu-class" />
    // <div className="full-page-container">
    //   <div className="left-panel py-2">
    //     <Menubar model={items} start={start} />
    //   </div>
    // </div>
  );
};

export default LeftPanel;
