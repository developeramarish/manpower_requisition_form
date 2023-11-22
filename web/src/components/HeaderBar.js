import React from 'react'
import mrfLogo from '../src/Asset/img/mrf.png';
import './css/HeaderBar.css'

const HeaderBar = ({userFirstName, userLastName}) => {
  return (
    <div>
        <header>
            <div className="left"> 
            <div className="company-info">
        <img src={mrfLogo} alt="Company Logo" className="company-logo" />
        <span className="company-name">MANPOWER REQUISITION</span>
      </div>
            </div>
            <div className="right">
            <div className="user-initials">
          <div className="initial-circle">
            {userFirstName.charAt(0)}
            {userLastName.charAt(0)}
            
          </div>
        </div>
      <div className="username">{userFirstName} {userLastName}</div>
      <span>|</span>
      <a href="#" className="logout">LOGOUT</a>
    </div>

        </header>
    </div>
  )
}

export default HeaderBar;