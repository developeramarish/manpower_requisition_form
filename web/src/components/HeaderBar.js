import React from 'react'
/* import mrfLogo from '../src/Asset/img/mrf.png'; */
import './../css/Header.css'
import { useMsal } from '@azure/msal-react';

const HeaderBar = ({ userFirstName, userLastName }) => {
  const { instance, accounts } = useMsal();
  return (
    <div className='top_band'>
      <header className="flex-add">
        <div className="flex-add logo">
          <img src="./images/logo.png" alt="mrf logo" />
          <h4>MANPOWER REQUISITION</h4>
        </div>
        <div className="flex-add acc-name">
          <button className="flex-add">
          {userFirstName && userLastName &&
              <>
                <p className="acc-logo"> {userFirstName.charAt(0)} {userLastName.charAt(0)} </p>
                <p className='logged-in-user'>{userFirstName} {userLastName}</p>
              </>
            }

            
          </button>
         <span>|</span> 
         <button className='logoutId' onClick={(e) => {
            /*  const logoutRequest = {
               account: instance.getAccountByHomeId(homeAccountId),
               postLogoutRedirectUri: "your_app_logout_redirect_uri",
             }; */
            instance.logoutRedirect();
          }}>LOGOUT</button>
        </div>
      </header>
    </div>
  )
}

export default HeaderBar;