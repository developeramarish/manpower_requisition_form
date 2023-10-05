// LeftPanel.js
import React from 'react';
import { Menubar } from 'primereact/menubar';
import { PanelMenu } from 'primereact/panelmenu';
import '../styles/layout/Dashboard.css'
import '../styles/layout/header.css'; 
const LeftPanel = () => {
    const imagePath = './MyDashboard.png';

    const menuDashBoard = {
        
        fontWeight: 'bold',
        fontSize:'10px',
        
        // Add more styles as needed
      }; 
      const menuMyRequi = {
       
        fontWeight: 'bold',
        fontSize:'10px',
        // Add more styles as needed
      }; 
      const menuCreateRequi = {
       
        fontWeight: 'bold',
        fontSize:'10px',
        // Add more styles as needed
      }; 
    
    const items = [
      {
        label: (
            <div className="menu-item-container">
              
              <span style={menuDashBoard}>DASHBOARD</span>
            </div>
          ),
      },
      {
        label:(<div className="menu-item-container">
              
        <span style={menuDashBoard}>MY REQUISITIONS</span>
      </div>),
        
      },
      {
       
        label:(<div className="menu-item-container">
              
        <span style={menuDashBoard}>CREATE REQUISITIONS</span>
      </div>),
      },
      // Add more menu items as needed
    ];
  
    const start = <div></div>;
  
    return (
      <div className="full-page-container">
       
        <div className="left-panel">
          <Menubar model={items} start={start} />
        </div>
       
        
      </div>
    );
  };
  
  export default LeftPanel;
  