// LeftPanel.js
import React from 'react';
import { Menubar } from 'primereact/menubar';
import { PanelMenu } from 'primereact/panelmenu';
import '../styles/layout/Dashboard.css'
import { Link } from 'react-router-dom';

const LeftPanel = () => {
    const imagePath = './MyDashboard.png';
    const linkStyles = {
      textDecoration: 'none',
      color: 'white',
      padding: '8px',
      
      borderRadius: '4px',
      display: 'inline-block',
    };
    const menuDashBoard = {
      color: 'white',
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
              <Link to="/Dashboard" className="linkStyles">
              
              <span style={menuDashBoard}>DASHBOARD</span>
              </Link>
          ),
      },
      {
        label:(
            <Link to="/MyRequisitions" className="linkStyles">
              
        <span style={menuDashBoard}>MY REQUISITIONS</span>
      </Link>),
        
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
  