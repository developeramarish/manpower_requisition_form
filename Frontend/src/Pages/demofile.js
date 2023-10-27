import React, { useState, useEffect } from 'react';
import DropdownComponent from '../Components/Dropdown';
import InputText  from "../Components/Textbox";
import Button  from "../Components/Button";
import CalendarComponent  from "../Components/Calendar";
import CheckboxComponent  from "../Components/Checkbox";
import InputTextareaComponent from "../Components/InputTextarea";

const DemoFile = () => { // Changed the component name to start with an uppercase letter
  const [departments, setDepartments] = useState([]);
  const [selectedDepartment, setSelectedDepartment] = useState(null);
  const [projects, setProjects] = useState([]);
  const [selectedProject, setSelectedProject] = useState(null);
  const [subDepartments, setSubDepartments] = useState([]); // State for sub-departments
  const [selectedSubDepartment, setSelectedSubDepartment] = useState(null);
  const [grades, setGrades] = useState([]);
  const [selectedGrades, setSelectedGrades] = useState(null);
  const [vaccancies, setVaccancies] = useState([]);
  const [selectedVaccancies, setSelectedVaccancies] = useState(null);
  const [employmentTypes, setEmploymentTypes] = useState([]);
  const [selectedEmploymentTypes, setSelectedEmploymentTypes] = useState(null);
  const [location, setLocation] = useState([]);
  const [selectedLocation, setSelectedLocation] = useState(null);
  const [Qualification, setQualification] = useState([]);
  const [selectedQualification, setSelectedQualification] = useState(null);
  const [ReportingTo, setReportingTo] = useState([]);
  const [selectedReportingTo, setSelectedReportingTo] = useState(null);
 
  // Define the handleDepartmentChange function outside the useEffect
  const handleDepartmentChange = (e) => {
    setSelectedDepartment(e.value);
  };
  const handleProjectChange = (e) => {
    setSelectedProject(e.value);
  };
 
  const handlegradesChange = (e) => {
    setSelectedGrades(e.value);
  };
  const handlevaccanciesChange = (e) => {
    setSelectedVaccancies(e.value);
  };
  const handleemploymentTypesChange = (e) => {
    setSelectedEmploymentTypes(e.value);
  };
  const handleQualificationChange = (e) => {
    setSelectedQualification(e.value);
  };
  const handlelocationChange = (e) => {
    setSelectedLocation(e.value);
  };
  const handleReportingToChange = (e) => {
    setSelectedReportingTo(e.value);
  };
 
  const handleSubDepartmentChange = (e) => {
    setSelectedSubDepartment(e.value);
  };

  useEffect(() => {
    // Fetch the data for all the dropdowns
    fetch('https://localhost:7128/api/Mrfdetail/GetMRFDropdownlist')
      .then((response) => response.json())
      .then((data) => {
        
        const projectData = data.result.projects;
        const departments = data.result.departments;
        const grades = data.result.grades;
        const vaccancies = data.result.vaccancies;
        const employmentTypes = data.result.employmentTypes;
        const location = data.result.location;
        const Qualification = data.result.Qualification;
        const ReportingTo = data.result.ReportingTo;
        
        setProjects(projectData);
        setDepartments(departments);
        setGrades(grades);
        setVaccancies(vaccancies);
        setEmploymentTypes(employmentTypes);
        setLocation(location);
        setQualification(Qualification);
        setReportingTo(ReportingTo);
        
      })
      .catch((error) => {
        console.error('Error fetching data:', error);
      });
  }, []);
 
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
            <InputText id="refno"  />
          </div>
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="position-title" className="font-bold text-sm">
              Position Title
            </label>
            <InputText id="position-title"  />
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
      options={departments}
      selectedOption={selectedDepartment}
      onChange={handleDepartmentChange} 
    />

    </div>
    <div className="flex flex-column w-6 gap-2">
            <label htmlFor="sub-department" className="font-bold text-sm">
              Sub-Department
            </label>
            <DropdownComponent
        type="SubDepartment"
        options={subDepartments}
        selectedOption={selectedSubDepartment}
        onChange={handleSubDepartmentChange}
        optionLabel="name" 
      optionValue="id"   
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
      options={projects}
      selectedOption={selectedProject}
      onChange={handleProjectChange} 
    /></div><div className="flex flex-column w-6 gap-2">
    <label htmlFor="location" className="font-bold text-sm">
      Location
    </label>
    <DropdownComponent
      optionLabel="name" 
      optionValue="id"   
      type="location"
      options={location}
      selectedOption={selectedLocation}
      onChange={handlelocationChange} 
    />
  
        </div>
        </div>
        <div className="flex justify-content-between gap-5">
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="no-vacancies" className="font-bold text-sm">
              Number of Vacancies
            </label>
            <InputText id="no-vacancies" className="bg-gray-100" />
          </div>
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="gender" className="font-bold text-sm">
              Gender
            </label>
            <InputText id="gender" className="bg-gray-100" />
          </div>
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="initiation-date" className="font-bold text-sm">
              Hiring Initiation Date
            </label>
            <InputText id="initiation-date" className="bg-gray-100" />
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
      type="reporting To"
      options={ReportingTo}
      selectedOption={selectedReportingTo}
      onChange={handleReportingToChange} 
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
      options={grades}
      selectedOption={selectedGrades}
      onChange={handlegradesChange} 
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
      type="Employee Type"
      options={employmentTypes}
      selectedOption={selectedEmploymentTypes}
      onChange={handleemploymentTypesChange} 
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
      options={vaccancies}
      selectedOption={selectedVaccancies}
      onChange={handlevaccanciesChange} 
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
            <CheckboxComponent inputId="replacement" />
            <label htmlFor="replacement" className="font-bold text-sm">
              Replacement for the employee
            </label>
          </div>
        </div>
        <div className="flex justify-content-between gap-5 ">
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="experience" className="font-bold text-sm">
              Experience
            </label>
            <InputTextareaComponent autoResize id="experience" className="bg-gray-100" />
          </div>
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="qualification" className="font-bold text-sm">
              Qualification
            </label>
            <DropdownComponent
      optionLabel="type" 
      optionValue="id"   
      type="Qualification"
      options={Qualification}
      selectedOption={selectedQualification}
      onChange={handleQualificationChange} 
    />
          </div>
        </div>
    
    </section>
   
      <div className="flex flex-wrap justify-content-end gap-5 mt-3">
        <Button label="CANCEL" outlined className="mr-auto w-2" />
        <Button label="SAVE AS A DRAFT" className="w-2" />
        <Button label="SUBMIT" className="w-2" disabled />
      </div>
    </div>
  );
};

export default DemoFile; // Updated the export name
