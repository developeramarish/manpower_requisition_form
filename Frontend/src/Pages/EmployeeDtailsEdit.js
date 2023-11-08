import React, { useState, useEffect } from 'react';
import DropdownComponent from '../Components/Dropdown';
import InputTextCp from "../Components/Textbox";
import ButtonC from "../Components/Button";
import { Link, useNavigate, useParams } from 'react-router-dom';
import LeftPanel from './LeftPanel';
import DashboardHeader from './Header';

const EmployeeDtailsEdit = ({id, updateData}) => {
  useEffect(() => {
    fetchData();
  }, []);
useEffect(() => {
  fetch("https://localhost:7128/api/Employeedetails/GetEmployee/" + id).then((res) => {
      return res.json();
  }).then((result) => {
     namechange(result.result[0].name);
    emailchange(result.result[0].email);
      phonechange(result.result[0].contactNo);
      setRole(result.result[0].roleName);
  }).catch((err) => {
      console.log(err.message);
  })
}, []);
const [name, namechange] = useState("");
const [email, emailchange] = useState("");
const [roleId, setRole] = useState(null);
const [contactNo,phonechange] = useState("");
const [allowedByEmployeeId] = useState("1");
const [createdByEmployeeId] = useState("1");
const [createdOnUtc] = useState(new Date().toISOString());
const [updatedByEmployeeId] = useState("1");
const [isAllowed] = useState(true);
const [isDeleted] = useState(false);
const [updatedOnUtc] = useState(new Date().toISOString());
const [roleOptions, roleOptionchange] = useState([]);
  const fetchData = () => {
    const apiUrl = `https://localhost:7128/api/Role`;
    fetch(apiUrl)
      .then(response => response.json())
      .then(responseData => {
        if (Array.isArray(responseData.result)) {
          const data = responseData.result;
          const options = data.map(x => { return { value: x.id, name: x.name } })
          roleOptionchange(data);
        } else {
          console.error('API response result is not an array:', responseData);
        }
      })
      .catch(error => {
        console.error('Fetch error:', error);
      });

  };
  const updateEditmode = () =>{
    updateData(false);
  }
  const handlesubmit = (e) => {
    e.preventDefault();
    const empdata = { name, email, contactNo,  roleId: roleId.value,isAllowed,isDeleted,allowedByEmployeeId,createdByEmployeeId,
      createdOnUtc,updatedByEmployeeId,updatedOnUtc};
    fetch("https://localhost:7128/api/Employeedetails/Put/"+id, {
      method: "Put",
      headers: { "content-type": "application/json" },
      body: JSON.stringify(empdata)
       
    }).then((res) => {
      alert('updated successfully.')

    }).catch((err) => {
      console.log(err.message)
    })

  }
  return (
    <div >
    <div style={{ display: 'flex' }}>
    <div
      className="border-round-lg bg-white text-black-alpha-90 p-3 flex flex-column justify-content-between"
      style={{ width: "210vh" } }
    >
      <h3 className="text-xl my-2">Fill the Details</h3>
      <section
        className="flex flex-column flex-nowrap gap-3 border-y-2 border-gray-300 py-3 px-1 overflow-y-scroll"
        style={{ height: "90%" }}
      >
        <div className="flex justify-content-between gap-5">
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="refno" className="font-bold text-sm">
              Name
            </label>
            <p>{name}</p>
            <InputTextCp id="refno" value={name} onChange={(e) => namechange(e.target.value)} />
          </div>
        </div>
        <div className="flex justify-content-between gap-5">
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="position-title" className="font-bold text-sm">
              Email Address
            </label>
            <InputTextCp id="position-title" value={email} onChange={(e) => emailchange(e.target.value)} />
          </div>
        </div>
        <div className="flex justify-content-between gap-5">
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="position-title" className="font-bold text-sm">
              Contact Number
            </label>
            <InputTextCp id="position-title" value={contactNo} onChange={(e) => phonechange(e.target.value)} />
          </div>
        </div>

        <div className="flex justify-content-between gap-5">
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="department" className="font-bold text-sm">
              Role
            </label>

            <DropdownComponent
              optionLabel="name"
              optionValue="id"
              type="roleId"
              options={roleOptions}
              onChange={e => {
                console.log(e)
                setRole(e.target)
              }
              }
            />
         
         </div>
        </div>
      
        </section>  
      {<div className="flex flex-wrap justify-content-end gap-5 mt-3">
        {<ButtonC to="/" className="btn btn-danger" label="CENCEL" disabled onClick={() => updateEditmode(false)}></ButtonC>}
        {<ButtonC label="SUBMIT" className="w-2" disabled onClick={handlesubmit} />}
      </div>}
      
      
      </div>  
      </div>
   
        </div>
  );
};
export default EmployeeDtailsEdit;