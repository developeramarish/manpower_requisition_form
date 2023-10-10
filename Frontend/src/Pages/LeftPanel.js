// LeftPanel.js
import React from 'react';
import { Menubar } from 'primereact/menubar';
import { PanelMenu } from 'primereact/panelmenu';
import '../styles/layout/Dashboard.css'
import { Link } from 'react-router-dom';

const LeftPanel = () => {
    
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
        textAlign: 'center',
        // Add more styles as needed
      }; 
       
      const iconStyle = {
        color: 'gray', 
        padding:'5px',
        paddingLeft:'15px',
        
      };
      const divStyle = {
        textAlign: 'center',
      }
    
    const items = [
      {
        label: (
              <Link to="/Dashboard" className="linkStyles">
              <div className="divStyle">
              <i className="pi pi-chart-bar" style={iconStyle }></i>
              </div>
              <span style={menuDashBoard}>DASHBOARD</span>
              
              </Link>
          ),
      },
      {
        label:(
            <Link to="/MyRequisitions" className="linkStyles">
               <div className="divStyle">
              <i className="pi pi-list" style={iconStyle }></i>
              </div>
        <span style={menuDashBoard}>MY REQUISITIONS</span>
      </Link>),
        
      },
      {
       
        label:(<Link to="/CreateRequisition" className="linkStyles">
               <div className="divStyle">
              <i className="pi pi-file-edit" style={iconStyle }></i>
              </div>
        <span style={menuDashBoard}>CREATE REQUISITIONS</span>
        </Link>),
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
  