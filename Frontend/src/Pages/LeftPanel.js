// LeftPanel.js
import React from 'react';
import { Menubar } from 'primereact/menubar';
import { PanelMenu } from 'primereact/panelmenu';
import './Dashboard.css'
// YourComponent.js
const LeftPanel = () => {
    const items = [
      {
        icon: 'pi pi-home',
      }
      ,
        {
        label: 'DASHBOARD',
       
      },
      {
        icon: 'pi pi-tags',
      },
      {
        label: 'MY REQUISITION',
        
        
      },
      {
        label: 'CREATE REQUISITION',
        icon: 'pi pi-info',
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
  