import React from 'react'
import { Outlet, Link } from "react-router-dom";
import './../css/Sidebar.css'
import { navigateTo } from '../constants/Utils';

const Sidebar = () => {
    const onLinkClicked = (e)=>{
        e.preventDefault();
        var oTarget = e.target,
            sUrl = oTarget.getAttribute('href');
        console.log(e.target)
        navigateTo(sUrl)
    }
    return (
        <div>
            <ul>
                <li><a href="dashboard" onClick={(e)=>onLinkClicked(e)}>My Dashboard</a></li>
                <li><a href="my_requisition" onClick={(e)=>onLinkClicked(e)}>My Requisition</a></li>
                <li><a href="create_requisition" onClick={(e)=>onLinkClicked(e)}>Create Requisition</a></li>
            
            
            </ul>
        </div>

    )
}

export default Sidebar;