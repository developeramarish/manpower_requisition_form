import React, { useState } from 'react'
import { Outlet, Link } from "react-router-dom";
import './../css/Sidebar.css'
import { navigateTo } from '../constants/Utils';

const Sidebar = () => {
    const [active, setActive] = useState("dashboard");
    const onLinkClicked = (e)=>{
        e.preventDefault();
        e.stopPropagation();
        var oTarget = e.currentTarget,
            sPageKey = oTarget.getAttribute('href');
        // console.log(e.target)
        setActive(sPageKey);
        navigateTo(sPageKey)
    }
    return (
        <div className='sidebar'>
            <ul>
                <li><a className={active === 'dashboard' ? 'active' : ''} href="dashboard" onClick={(e)=>onLinkClicked(e)}><img src="./images/my_dashboard_selected.png" alt="my_dashboard" /><br/>My Dashboard</a></li>
                <li><a className={active === 'my_requisition' ? 'active' : ''} href="my_requisition" onClick={(e)=>onLinkClicked(e)}><img src="./images/my_requisition_normal.png" alt="my_requistion" /><br/>My Requisition</a></li>
                <li><a className={active === 'create_requisition' ? 'active' : ''} href="create_requisition" onClick={(e)=>onLinkClicked(e)}><img src="./images/create_requisition_normal.png" alt="create_requistion" /><br/>Create Requisition</a></li>
            </ul>
        </div>

    )
}

export default Sidebar;