import React, { useState } from 'react'
import './../css/Sidebar.css'
import { navigateTo } from '../constants/Utils';
import { ROLES } from '../constants/config';

const Sidebar = ({roleId}) => {
    const [active, setActive] = useState("dashboard");
    const onLinkClicked = (e)=>{
        e.preventDefault();
        e.stopPropagation();
        var oTarget = e.currentTarget,
            sPageKey = oTarget.getAttribute('href');
            
        setActive(sPageKey);
        navigateTo(sPageKey)
    }
    
    return (
        <div className='sidebar'>
        <ul>
            {(roleId === ROLES.mrfOwner || roleId === ROLES.hr) && (
                <>
                    <li>
                        <a className={active === 'dashboard' ? 'active' : ''} href="dashboard" onClick={(e) => onLinkClicked(e)}>
                            <img src="./images/my_dashboard_selected.png" alt="my_dashboard" /><br />My Dashboard
                        </a>
                    </li>
                    {roleId !== ROLES.hr && (
                        <li>
                            <a className={active === 'create_requisition' ? 'active' : ''} href="create_requisition" onClick={(e) => onLinkClicked(e)}>
                                <img src="./images/create_requisition_normal.png" alt="create_requisition" /><br />Create Requisition
                            </a>
                        </li>
                    )}
                    <li>
                        <a className={active === 'my_requisition' ? 'active' : ''} href="my_requisition" onClick={(e) => onLinkClicked(e)}>
                            <img src="./images/my_requisition_normal.png" alt="my_requisition" /><br />My Requisition
                        </a>
                    </li>
                </>
            )}
        </ul>
    </div>
    

    )
}

export default Sidebar;