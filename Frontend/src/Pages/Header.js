// DashboardHeader.js
import React from "react";
import { Menubar } from "primereact/menubar";
import "../styles/layout/header.css";

const DashboardHeader = () => {
  return (
    <div>
      <header>
        <div className="left">
          <div className="iconimg"></div>
          <div className="icon">MANPOWER REQUISITION</div>
        </div>
        <div className="right">
          <div className="username">JOHN DSOUZA</div>
          <a href="#" className="logout">
            LOGOUT
          </a>
        </div>
      </header>
    </div>
  );
};

export default DashboardHeader;
