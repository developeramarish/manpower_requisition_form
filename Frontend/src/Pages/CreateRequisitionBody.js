import React, { useState, useEffect } from 'react';
import DropdownComponent from '../Components/Dropdown';
import InputTextCp  from "../Components/Textbox";
import ButtonC  from "../Components/Button";
import CalendarComponent  from "../Components/Calendar";
import CheckboxComponent  from "../Components/Checkbox";
import InputTextareaComponent from "../Components/InputTextarea";
import GenderDropdown from "../Components/GenderDropdown";
import ExperienceDropdown from "../Components/ExperienceDropdown";

const CreateRequisitionBody = () => {
  // State to hold all the dropdown data
  const [dropdownData, setDropdownData] = useState({});
  const [subDepartments, setSubDepartments] = useState([]);
  const [formData, setFormData] = useState({}); 
  const [isLoading, setIsLoading] = useState(false);
  const [isCheckboxChecked, setIsCheckboxChecked] = useState(false);

  const handleCheckboxChange = (isChecked) => {
    setIsCheckboxChecked(isChecked);
  };


  const dropdownNames = [
    'departments','employmenttypes','reportingto','vaccancies','grades', 'projects', 'location','qualification'];

  useEffect(() => {
    // Fetch the data for all the dropdowns
    fetch('https://localhost:7128/api/Mrfdetail/GetMRFDropdownlist')
      .then((response) => response.json())
      .then((data) => {
        const dropdownData = data.result;

        // Create an object to store dropdown data
        const newDropdownData = {};

        // Loop through the mapping and set the data for each dropdown
        dropdownNames.forEach((name) => {
          const stateKey = name.toLowerCase();
          newDropdownData[stateKey] = dropdownData[stateKey];
        });

        // Update the state with the new dropdown data
        setDropdownData(newDropdownData);
      })
      .catch((error) => {
        console.error('Error fetching data:', error);
      });
  }, []);


  const fetchSubDepartments = (selectedDepartment) => {
    const apiUrl = `https://localhost:7128/api/Subdepartment/GetInfo/${selectedDepartment}`;
    fetch(apiUrl)
            .then(response => response.json())
            .then(responseData => {
              if (Array.isArray(responseData.result)) {
                const data = responseData.result;
                
                setSubDepartments(data);
              } else {
                console.error('API response result is not an array:', responseData);
              }
            })
            .catch(error => {
              console.error('Fetch error:', error);
            });
   
  };
  
  const handleSubmit = async (mrfStatusId) => {
    setIsLoading(true); 
    const formData = {
      referenceNumber: formData.referenceNumber, 
      positionTitle: formData.positionTitle,
      department: formData.department,
      subDepartment: formData.subDepartment,
      projectId: formData.project,
      location: formData.location,
      vacancyTypeId: formData.numberOfVacancies,
      locationId: formData.gender,
      hiringInitiationDate: formData.hiringInitiationDate,
      vacancyNo: 0,
      requisitionDateUtc: "2023-10-26",
      reportsToEmployeeId: formData.reportsToEmployeeId,
      gradeId: formData.gradeId,
      employmentTypeId: formData.employmentTypeId,
      minExperience: formData.minExperience,
      maxExperience: formData.maxExperience,
      vacancyTypeId: formData.vacancyTypeId,
      isReplacement: isCheckboxChecked ? 'Yes' : 'No',
      mrfStatusId: mrfStatusId,
      jdDocPath: "string"
  
      
    };
    try {
      const response = await fetch('https://localhost:7128/api/mrfdetail', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(formData),
      });

      if (response.ok) {
        const data = await response.json();
        console.log('Response Data:', data);
      } else {
        console.error('Request failed');
      }
    } catch (error) {
      console.error('Error:', error);
    } finally {
      setIsLoading(false); 
    }
  };

  const handleCancel = () => {
    setDropdownData({});
    setSubDepartments([]);

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
              Reference Number
            </label>
            <InputTextCp id="refno"  />
          </div>
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="position-title" className="font-bold text-sm">
              Position Title
            </label>
            <InputTextCp id="position-title"  />
          </div>
        </div>
        <div className="flex justify-content-between gap-5">
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="department" className="font-bold text-sm">
              Department
            </label>
        
        <DropdownComponent
          optionLabel="name"
          optionValue="id"
          type="Department"
          options={dropdownData.departments || []}
          selectedOption={dropdownData['departments']}
          onChange={(e) => { const selectedDepartment = e.value; 
            fetchSubDepartments(selectedDepartment); }}
        />
      </div>
    <div className="flex flex-column w-6 gap-2">
            <label htmlFor="sub-department" className="font-bold text-sm">
              Sub-Department
            </label>
            <DropdownComponent
          optionLabel="name"
          optionValue="id"
          type="subDepartments"
          options={subDepartments}
          selectedOption={subDepartments}
          onChange={(e) => { const selectedDepartment = e.value; }}
        />
      </div>
    </div>
    <div className="flex justify-content-between gap-5">
          <div className="flex flex-column w-6 gap-2">
          <label htmlFor="project" className="font-bold text-sm">
            Project
          </label>
          <DropdownComponent
      optionLabel="name" 
      optionValue="id"   
      type="project"
      options={dropdownData.projects}
      selectedOption={dropdownData['sub-department']}
      onChange={(e) => { const selectedProject = e.value; }}
    /></div><div className="flex flex-column w-6 gap-2">
    <label htmlFor="location" className="font-bold text-sm">
      Location
    </label>
    <DropdownComponent
      optionLabel="name" 
      optionValue="id"   
      type="location"
      options={dropdownData.location}
      selectedOption={dropdownData.location}
      onChange={(e) => { const selectedDepartment = e.value; }}
    />
  
        </div>
        </div>
        <div className="flex justify-content-between gap-5">
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="no-vacancies" className="font-bold text-sm">
              Number of Vacancies
            </label>
            <InputTextCp id="no-vacancies" className="bg-gray-100" />
          </div>
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="gender" className="font-bold text-sm">
              Gender
            </label>
            <GenderDropdown id="gender"  />
          </div>
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="initiation-date" className="font-bold text-sm">
              Hiring Initiation Date
            </label>
            <CalendarComponent id="initiation-date" inputClassName="bg-gray-100" showIcon />
            
          </div>
        </div>
        <div className="flex justify-content-between gap-5">
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="position-reporting" className="font-bold text-sm">
              Position Reporting to
            </label>
            <DropdownComponent
      optionLabel="name" 
      optionValue="id"   
      type="reportingTo"
      options={dropdownData.reportingto}
      selectedOption={dropdownData.Reportingto}
      onChange={(e) => { const selectedDepartment = e.value; }}
    />
          </div>
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="grade" className="font-bold text-sm">
              Grade of the proposed employee
            </label>
            <DropdownComponent
      optionLabel="name" 
      optionValue="id"   
      type="grade"
      options={dropdownData.grades}
      selectedOption={dropdownData.grades}
      onChange={(e) => { const selectedDepartment = e.value; }}
    />
          </div>
        </div>
        <div className="flex justify-content-between gap-5">
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="employment-type-req" className="font-bold text-sm">
              Type of employment required
            </label>
            <DropdownComponent
      optionLabel="type" 
      optionValue="id"   
      type="employmenttypes"
      options={dropdownData.employmenttypes}
      selectedOption={dropdownData.employmenttypes}
      onChange={(e) => { const selectedDepartment = e.value; }}
    />
          </div>
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="vacancy-type" className="font-bold text-sm">
              Type of vacancy
            </label>
            <DropdownComponent
      optionLabel="type" 
      optionValue="id"   
      type="vaccancy"
      options={dropdownData.vaccancies}
      selectedOption={dropdownData.vaccancies}
      onChange={(e) => { const selectedDepartment = e.value; }}
    />
          </div>
        </div>
        <div className="flex justify-content-between gap-5">
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="no-vacancies" className="font-bold text-sm">
              Employee Email ID disable request date
            </label>
            <CalendarComponent id="no-vacancies" inputClassName="bg-gray-100" showIcon />
          </div>
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="gender" className="font-bold text-sm">
              Last Working Date
            </label>
            <CalendarComponent id="gender" inputClassName="bg-gray-100" showIcon />
          </div>
          <div className="flex flex-row align-items-center w-6 gap-2 px-4 border-round-sm border-1 border-300 bg-gray-100">
            <CheckboxComponent inputId="replacement" onCheckboxChange={handleCheckboxChange} />
            <label htmlFor="replacement" className="font-bold text-sm">
            Replacement for the employee
            </label>
          </div>
        </div>
        <div className="flex justify-content-between gap-5 ">
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="Justification" className="font-bold text-sm">
              Justification
            </label>
            <InputTextareaComponent autoResize id="Justification" className="bg-gray-100" />
          </div>
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="skills" className="font-bold text-sm">
            skills
            </label>
            <InputTextareaComponent autoResize id="skills" className="bg-gray-100" />
          </div>
        </div>
        <div className="flex justify-content-between gap-5 ">
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="experience" className="font-bold text-sm">
              Experience
            </label>
            <ExperienceDropdown/> 
          </div>
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="qualification" className="font-bold text-sm">
              Qualification
            </label>
            <DropdownComponent
      optionLabel="type" 
      optionValue="id"   
      type="Qualification"
      options={dropdownData.qualification}
      selectedOption={dropdownData.qualification}
      onChange={(e) => { const selectedDepartment = e.value; }}
    />
          </div>
        </div>
    
    </section>
   
      <div className="flex flex-wrap justify-content-end gap-5 mt-3">
        <ButtonC label="CANCEL" outlined className="mr-auto w-2" onClick={handleCancel}/>
        <ButtonC label="SAVE AS A DRAFT" className="w-2" onClick={() => handleSubmit(2)}/>
        <ButtonC label="SUBMIT" className="w-2" disabled onClick={() => handleSubmit(1)}/>
      </div>
    </div>
  );
};


export default CreateRequisitionBody;
