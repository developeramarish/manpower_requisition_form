// DashboardHeader.js
import React from 'react';
import { Menubar } from 'primereact/menubar';

const DashboardHeader = () => {
  const items = [
    {
      label: 'MANPOWER REQUISITION',
      icon: 'pi pi-fw pi-home',
    },
    {
      label: 'Username',
      icon: 'pi pi-fw pi-user',
      items: [
        {
          label: 'Logout',
          icon: 'pi pi-fw pi-sign-out',
        },
      ],
    },
  ];

  return <Menubar model={items} />;
};

export default DashboardHeader;
