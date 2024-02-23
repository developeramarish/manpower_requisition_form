import React, { useEffect, useRef, useState } from 'react'
/* import mrfLogo from '../src/Asset/img/mrf.png'; */
import './../css/Header.css'
import { useMsal } from '@azure/msal-react';
import DropdownComponent from './Dropdown';
import { API_URL, ROLES_WITH_ID } from '../constants/config';
import "../css/HeaderBar.css"
import { storageService } from '../constants/storage';
import ToastMessages from './ToastMessages';
import { useDispatch, useSelector } from 'react-redux';
import { PAGE_ACTIONS } from '../reducers/Page_r';

const HeaderBar = ({ userFirstName, userLastName, roleId, multipleRoleIds }) => {
  const { instance, accounts } = useMsal();
  const toastRef = useRef(null);
  const [roleDropdown, setRoleDropdown] = useState([]);
  const [role, setRole] = useState({});
  const { profile } = useSelector((state) => state.page);
  const dispatch = useDispatch();

  useEffect(() => {
    fetchData();
  }, []);
  const updateRole = (role) =>{
    setRole(role);
    const empdata = JSON.parse(JSON.stringify(profile));
    empdata.roleId = role.id;
    empdata.updatedOnUtc = new Date().toISOString();
    empdata.createdOnUtc = new Date().toISOString();
    empdata["isAllowed"]= true;
    empdata["allowedByEmployeeId"]= empdata.employeeId;
    sendData(empdata);
    
  }
  const sendData = async(empdata) =>{
    try {
      const response = await fetch(API_URL.UPDATE_EMPLOYEE +empdata.id, {
        method: "Put",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(empdata),
      });
      if (response.ok) {
        const responseData = await response.json();
        console.log("Response Data:", responseData);
        toastRef.current.showSuccessMessage("Role Updated successfully!");
        setTimeout(() => {
          //  navigateTo("dashborad");
          //  window.location.reload();
          dispatch(PAGE_ACTIONS.setProfile(empdata))
        }, 1000);
      } else {
        console.error("Request failed with status:", response.status);
        if (response.status === 400) {
          toastRef.current.showBadRequestMessage(
            "Bad request: " + response.url
          );
        }
      }
    } catch (error) {
      console.error("Error:", error);
    }
  }
  const fetchData = () => {
    const apiUrl = API_URL.GET_ROLE;
    fetch(apiUrl)
      .then(response => response.json())
      .then(responseData => {
        if (Array.isArray(responseData.result)) {
          const data = responseData.result,
          aMultipleRoleIds = multipleRoleIds.split(",");
          const options = data.map(x => { return { id: x.id, name: x.name } })
          const oDropdownData = options.filter((data, index)=>{
            return (aMultipleRoleIds.indexOf(data.id.toString()) > -1)
          })
          setRoleDropdown(oDropdownData);
          var currentRole = oDropdownData.filter((data)=>{return data.id === roleId});
          setRole(currentRole[0]);
          console.log(currentRole[0], roleId);
        } else {
          console.error('API response result is not an array:', responseData);
        }
      })
      .catch(error => {
        console.error('Fetch error:', error);
      });

  };

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
                <p className="acc-logo"> {userFirstName.charAt(0)}{userLastName.charAt(0)} </p>
                <p className='logged-in-user'>{userFirstName} {userLastName}</p>
              </>
            }


          </button>
          {roleDropdown &&
            <>
              <span>|</span>
              <DropdownComponent
                optionLabel="name"
                value={role}
                options={roleDropdown}
                onChange={(e) =>
                  // setRole(e.value)
                  updateRole(e.value)
                }
                placeholder="Select Role"
                className="w-full md:w-13rem selected-role" />
            </>
          }
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
      <ToastMessages ref={toastRef} />
    </div>
  )
}

export default HeaderBar;