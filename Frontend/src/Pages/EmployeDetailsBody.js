import React, { useState, useEffect } from 'react';
import DropdownComponent from '../Components/Dropdown';
import InputTextCp  from "../Components/Textbox";
import ButtonC  from "../Components/Button";
 
const EmployeDetailsBody = () => {
    const [role, setRole] = useState([]);
    const[data,getData]=useState([]);
    const [formData, setFormData] = useState({name:"",email:"",contactNo:""}); 
    useEffect(() => {
        fetchData();
        }, []);
    const  fetchData = () => {
        const apiUrl = `https://localhost:7128/api/Role`;
        fetch(apiUrl)
                .then(response => response.json())
                .then(responseData => {
                  if (Array.isArray(responseData.result)) {
                    const data = responseData.result;
                    
                     setRole(data);
                  } else {
                    console.error('API response result is not an array:', responseData);
                  }
                })
                .catch(error => {
                  console.error('Fetch error:', error);
                });
       
      };
      const handleSubmit = ( ) => {
        try {
          const response =  fetch('https://localhost:7128/api/Employeedetails', {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json',
            },
              body: JSON.stringify(formData),
            
          });
    
          
            const data = response.json();
            console.log('Response Data:', data);
          
          
        } catch (error) {
          console.error('Error:', error);
        }  
      };
      const handleCancel = () => {
        setRole([]);
      };
    return (
        <div
        className="border-round-lg bg-white text-black-alpha-90 p-3 flex flex-column justify-content-between"
        style={{ height: "81vh" }}
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
              <InputTextCp id="refno" onChange={(e) => setFormData({...formData,  name: e.target.data})} />
            </div>
            </div>
            <div className="flex justify-content-between gap-5">
            <div className="flex flex-column w-6 gap-2">
              <label htmlFor="position-title" className="font-bold text-sm">
                 Email Address
              </label>
              <InputTextCp id="position-title" onChange={(e) => setFormData({...formData, email: e.target.data})} />
            </div>
            </div>
            <div className="flex justify-content-between gap-5">
            <div className="flex flex-column w-6 gap-2">
              <label htmlFor="position-title" className="font-bold text-sm">
                 Contact Number
              </label>
              <InputTextCp  id="position-title" onChange={(e) => setFormData({...formData, contactNo: e.target.data})} />
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
            type="role"
            options={role}
            selectedOption={role}
            onChange={fetchData}
        />
        </div>
      </div>
          
      
      </section>
     
        { <div className="flex flex-wrap justify-content-end gap-5 mt-3">
          { <ButtonC label="CANCEL" outlined className="mr-auto w-2" onClick={handleCancel}/> }
          { <ButtonC label="SUBMIT" className="w-2" disabled onClick={() => handleSubmit()}/> }
        </div> }
      </div>
    );
};
export default EmployeDetailsBody;