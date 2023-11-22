import React, { useState, useEffect } from 'react';
import DropdownComponent from '../Components/Dropdown';
import InputTextCp from "../Components/Textbox";
import ButtonC from "../Components/Button";
import { Link, useNavigate } from 'react-router-dom';
import LeftPanel from './LeftPanel';
import DashboardHeader from './Header';

const EmployeDetailsCreate = () => {
  const [roleId, setRole] = useState(null);
  const [name, namechange] = useState("");
  const [email, emailchange] = useState("");
  const [contactNo,phonechange] = useState("");
  const [employeeCode,employeeChange] = useState("");
  const [allowedByEmployeeId] = useState("1");
  const [createdByEmployeeId] = useState("1");
  const [createdOnUtc] = useState(new Date().toISOString());
  const [updatedByEmployeeId] = useState("1");
  const [isAllowed] = useState(true);
  const [updatedOnUtc] = useState(new Date().toISOString());
  const [roleOptions, roleOptionchange] = useState([]);
  const navigate = useNavigate();
  useEffect(() => {
    fetchData();
  }, []);
  const fetchData = () => {
    const apiUrl = `https://localhost:7128/api/Role`;
    fetch(apiUrl)
      .then(response => response.json())
      .then(responseData => {
        if (Array.isArray(responseData.result)) {
          const data = responseData.result;
          const options = data.map(x => { return { value: x.id,  name: x.name } })
          roleOptionchange(data);
        } else {
          console.error('API response result is not an array:', responseData);
        }
      })
      .catch(error => {
        console.error('Fetch error:', error);
      });

  };
  const handlesubmit = (e) => {
    e.preventDefault();
    const empdata = { name, email, contactNo,employeeCode, roleId: roleId.value,isAllowed,allowedByEmployeeId,createdByEmployeeId,
      createdOnUtc,updatedByEmployeeId,updatedOnUtc};


    fetch("https://localhost:7128/api/Employeedetails/Post" , {
      method: "Post",
      headers: { "content-type": "application/json" },
      body: JSON.stringify(empdata)
    }).then((res) => {
      alert('Saved successfully.')

    }).catch((err) => {
      console.log(err.message)
    })

  }
  
  return (
    <div >
    <DashboardHeader />
    <div style={{ display: 'flex' }}>
      <LeftPanel />
      
       
    <div
      className="border-round-lg bg-white text-black-alpha-90 p-3 flex flex-column justify-content-between"
      style={{ width: "210vh" } }
    >
      <h3 className="text-xl my-2">Fill the Details</h3>
      <section
        className="flex flex-column flex-nowrap gap-3 border-y-2 border-gray-300 py-3 px-1 overflow-y-scroll"
        style={{ height: "70%" }}
      >
        <div className="flex justify-content-between gap-5">
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="refno" className="font-bold text-sm">
              Name
            </label>
            <InputTextCp id="refno" onChange={(e) => namechange(e.target.value)} />
          </div>
        </div>
        <div className="flex justify-content-between gap-5">
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="position-title" className="font-bold text-sm">
              Email Address
            </label>
            <InputTextCp id="position-title" onChange={(e) => emailchange(e.target.value)} />
          </div>
        </div>
        <div className="flex justify-content-between gap-5">
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="position-title" className="font-bold text-sm">
              Contact Number
            </label>
            <InputTextCp id="position-title" onChange={(e) => phonechange(e.target.value)} />
          </div>
        </div>
        <div className="flex justify-content-between gap-5">
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="position-title" className="font-bold text-sm">
               Employee Id
            </label>
            <InputTextCp id="position-title" onChange={(e) => employeeChange(e.target.value)} />
          </div>
        </div>
        <div className="flex justify-content-between gap-5">
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="department" className="font-bold text-sm">
              Role
            </label>

            <DropdownComponent
              optionLabel="name"
              value={roleId}
              optionValue="id"//"value"
              type="roleId"
              placeholder={"Select Role"}
              options={roleOptions}
              onChange={e => {
                console.log(e)
                
                setRole(e.target)
                
              }
              
              }
              
            />
         
         </div>
         {<div className="flex flex-wrap justify-content-end gap-5 mt-3">
        {<ButtonC to="/" className="btn btn-danger" label="CANCEL" disabled onClick={() => navigate("/EmployeDetails")}></ButtonC>}
        {<ButtonC label="SUBMIT" className="w-2" disabled onClick={handlesubmit} />}
      </div>}
        </div>
        </section>
      </div>  
      </div>
   
        </div>
  );
};
export default EmployeDetailsCreate;