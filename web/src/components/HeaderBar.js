import React from 'react'
/* import mrfLogo from '../src/Asset/img/mrf.png'; */
import './../css/Header.css'

const HeaderBar = ({ userFirstName, userLastName }) => {
  return (
    <div>
      <header className="flex-add">
        <div className="flex-add logo">
          <img src="./images/logo.jpg" alt="mrf logo" />
          <h4>MANPOWER REQUISITION</h4>
        </div>
        <div className="flex-add acc-name">
          <button className="flex-add">
            <p className="acc-logo"> 
              {userFirstName.charAt(0)}
              {userLastName.charAt(0)}
            </p>
            <p className='logged-in-user'>{userFirstName} {userLastName}</p>
          </button>
          |
          <button>LOGOUT</button>
        </div>
      </header>
    </div>
  )
}

export default HeaderBar;