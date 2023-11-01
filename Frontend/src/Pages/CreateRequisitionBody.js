import React, { useState, useEffect, useRef } from "react";
import DropdownComponent from "../Components/Dropdown";
import InputTextCp from "../Components/Textbox";
import ButtonC from "../Components/Button";
import CalendarComponent from "../Components/Calendar";
import CheckboxComponent from "../Components/Checkbox";
import InputTextareaComponent from "../Components/InputTextarea";
import ToastMessages from "../Components/ToastMessages";
import ExperienceDropdown from "../Components/ExperienceDropdown";
import MultiSelectDropdown from "../Components/multiselectDropdown";
import DateTimeFormatter from "../Components/DateTimeFormatter";
import {
  multiSoftwareSkill,
  multiHardwareSkill,
  minExperienceOptions,
  maxExperienceOptions,
  Gender,
} from "../Components/constant";
const CreateRequisitionBody = () => {
  // State to hold all the dropdown data
  const [dropdownData, setDropdownData] = useState({});
  const [subDepartments, setSubDepartments] = useState([]);
  const [isLoading, setIsLoading] = useState(false);
  const [isCheckboxChecked, setIsCheckboxChecked] = useState(false);
  const [selectedMinExperience, setSelectedMinExperience] = useState(null);
  const [selectedMaxExperience, setSelectedMaxExperience] = useState(null);
  const [selectedSoftwareSkills, setSelectedSoftwareSkills] = useState(null);
  const [selectedHardwareSkills, setSelectedHardwareSkills] = useState(null);
  const [isSubmissionSuccessful, setIsSubmissionSuccessful] = useState(false);
  const toastRef = useRef(null);
  /*const handleSoftwareSkillsChange = (e) => {
    setSelectedSoftwareSkills(e);
  };

  const handleHardwareSkillsChange = (e) => {
    setSelectedHardwareSkills(e);
  };*/
  const onMinExperienceChange = (e) => {
    setSelectedMinExperience(e.value);
  };

  const onMaxExperienceChange = (e) => {
    setSelectedMaxExperience(e.value);
  };
  const handleCheckboxChange = (isChecked) => {
    setIsCheckboxChecked(isChecked);
  };

  useEffect(() => {
    // Fetch the data for all the dropdowns
    fetch("https://localhost:7128/api/Mrfdetail/GetMRFDropdownlist")
      .then((response) => response.json())
      .then((data) => {
        const dropdownData = data.result;
        // Update the state with the new dropdown data
        setDropdownData(dropdownData);
      })
      .catch((error) => {
        console.error("Error fetching data:", error);
      });
  }, []);

  const fetchSubDepartments = (selectedDepartment) => {
    const apiUrl = `https://localhost:7128/api/Subdepartment/GetInfo/${selectedDepartment}`;
    fetch(apiUrl)
      .then((response) => response.json())
      .then((responseData) => {
        if (Array.isArray(responseData.result)) {
          const data = responseData.result;

          setSubDepartments(data);
        } else {
          console.error("API response result is not an array:", responseData);
        }
      })
      .catch((error) => {
        console.error("Fetch error:", error);
      });
  };
  const formSchema = {
    referenceNo: "",
    positionTitle: "",
    departmentId: 0,
    subDepartmentId: 0,
    projectId: 0,
    vacancyNo: 0,
    genderId: 0,
    requisitionDateUtc: "",
    reportsToEmployeeId: 0,
    gradeId: 0,
    employmentTypeId: 0,
    minExperience: 0,
    maxExperience: 0,
    vacancyTypeId: 0,
    isReplacement: false,
    mrfStatusId: 0,
    jdDocPath: "",
    locationId: 0,
  };

  // Initialize the formData state using the form schema
  const [formData, setFormData] = useState(formSchema);

  const handleSubmit = async (mrfStatusId) => {
    setIsLoading(true);
    const data = {
      referenceNo: formData.referenceNumber,
      positionTitle: formData.positionTitle,
      departmentId: formData.departmentId,
      subDepartmentId: formData.subDepartmentId,
      projectId: formData.projectId,
      vacancyNo: formData.vacancyNo,
      genderId: formData.genderId,
      requisitionDateUtc: "2023-10-31",
      reportsToEmployeeId: formData.reportingTo,
      gradeId: formData.gradeId,
      employmentTypeId: formData.employmentTypeId,
      minExperience: formData.minExperience,
      maxExperience: formData.maxExperience,
      vacancyTypeId: formData.vacancyTypeId,
      isReplacement: true,
      mrfStatusId: mrfStatusId,
      jdDocPath: "string",
      locationId: formData.locationId,
      createdByEmployeeId: 1,
      createdOnUtc: new Date().toISOString(),
      updatedByEmployeeId: 1,
      updatedOnUtc: new Date().toISOString(),
    };
    try {
      const response = await fetch(
        "https://localhost:7128/api/mrfdetail/POST",
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(data),
        },
      );

      if (response.ok) {
        const responseData = await response.json();
        console.log("Response Data:", responseData);
        toastRef.current.showSuccessMessage();
      } else {
        console.error("Request failed with status:", response.status);
        const errorData = await response.text();
        console.error("Error Data:", errorData);
      }
    } catch (error) {
      console.error("Error:", error);
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
            <InputTextCp
              id="refno"
              onChange={(e) =>
                setFormData({ ...formData, referenceNumber: e.target.value })
              }
              value={formData.referenceNumber}
            />
          </div>
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="position-title" className="font-bold text-sm">
              Position Title
            </label>
            <InputTextCp
              id="position-title"
              onChange={(e) =>
                setFormData({ ...formData, positionTitle: e.target.value })
              }
              value={formData.positionTitle}
            />
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
              selectedOption={dropdownData["departments"]}
              onChange={(e) => {
                const selectedDepartment = e.value;
                setFormData({ ...formData, departmentId: e.target.value });
                fetchSubDepartments(selectedDepartment);
              }}
            />
          </div>
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="sub-department" className="font-bold text-sm">
              Sub-Department
            </label>
            <DropdownComponent
              optionLabel="name"
              optionValue="id"
              type="subDepartmentId"
              options={subDepartments}
              selectedOption={subDepartments}
              onChange={(e) =>
                setFormData({ ...formData, subDepartmentId: e.target.value })
              }
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
              selectedOption={dropdownData.projects}
              onChange={(e) =>
                setFormData({ ...formData, projectId: e.target.value })
              }
            />
          </div>
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="location" className="font-bold text-sm">
              Location
            </label>
            <DropdownComponent
              optionLabel="location"
              optionValue="id"
              type="location"
              options={dropdownData.location}
              selectedOption={dropdownData.location}
              onChange={(e) =>
                setFormData({ ...formData, locationId: e.target.value })
              }
            />
          </div>
        </div>
        <div className="flex justify-content-between gap-5">
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="no-vacancies" className="font-bold text-sm">
              Number of Vaccancies
            </label>
            <InputTextCp
              id="vaccancies"
              className="bg-gray-100"
              onChange={(e) =>
                setFormData({ ...formData, vacancyNo: e.target.value })
              }
            />
          </div>
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="gender" className="font-bold text-sm">
              Gender
            </label>
            <DropdownComponent
              optionLabel="label"
              optionValue="id"
              type="Gender"
              options={Gender}
              selectedOption={Gender}
              onChange={(e) =>
                setFormData({ ...formData, genderId: e.target.value })
              }
            />
          </div>
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="initiation-date" className="font-bold text-sm">
              Hiring Initiation Date
            </label>
            <CalendarComponent
              id="initiation-date"
              inputClassName="bg-gray-100"
              showIcon
              onChange={(e) =>
                setFormData({ ...formData, requisitionDateUtc: e.target.value })
              }
            />
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
              options={dropdownData.reportingTo}
              selectedOption={dropdownData.ReportingTo}
              onChange={(e) =>
                setFormData({ ...formData, reportingTo: e.target.value })
              }
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
              onChange={(e) =>
                setFormData({ ...formData, gradeId: e.target.value })
              }
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
              options={dropdownData.employmentTypes}
              selectedOption={dropdownData.employmenttypes}
              onChange={(e) =>
                setFormData({ ...formData, employmentTypeId: e.target.value })
              }
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
              onChange={(e) =>
                setFormData({ ...formData, vacancyTypeId: e.target.value })
              }
            />
          </div>
        </div>
        <div className="flex justify-content-between gap-5">
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="no-vacancies" className="font-bold text-sm">
              Employee Email ID disable request date
            </label>
            <CalendarComponent
              id="disablerequest"
              inputClassName="bg-gray-100"
              showIcon
              onChange={(e) => setFormData({ ...formData, di: e.value })}
            />
          </div>
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="lastworkingDate" className="font-bold text-sm">
              Last Working Date
            </label>
            <CalendarComponent
              id="lastworkingDate"
              inputClassName="bg-gray-100"
              showIcon
            />
          </div>
          <div className="flex flex-row align-items-center w-6 gap-2 px-4 border-round-sm border-1 border-300 bg-gray-100">
            <CheckboxComponent
              inputId="replacement"
              onCheckboxChange={handleCheckboxChange}
            />
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
            <InputTextareaComponent
              autoResize
              id="Justification"
              className="bg-gray-100"
            />
          </div>
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="Software skills" className="font-bold text-sm">
              Software Skills
            </label>

            <MultiSelectDropdown
              data={multiSoftwareSkill}
              value={selectedSoftwareSkills}
              onChange={(e) => setSelectedSoftwareSkills(e.value)}
              options={multiSoftwareSkill}
              optionLabel="name"
              placeholder="Select Software Skill"
              maxSelectedLabels={3}
              className="w-full md:w-20rem"
            />

            <label htmlFor="Hardware skills" className="font-bold text-sm">
              Hardware Skills
            </label>
            <MultiSelectDropdown
              data={multiHardwareSkill}
              value={selectedHardwareSkills}
              onChange={(e) => setSelectedHardwareSkills(e.value)}
              options={multiHardwareSkill}
              optionLabel="name"
              placeholder="Select Hardware Skill"
              maxSelectedLabels={3}
              className="w-full md:w-20rem"
            />
          </div>
        </div>
        <div className="flex justify-content-between gap-5 ">
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="experience" className="font-bold text-sm">
              Experience
            </label>
            <div className="p-col-2">
              <label className="font-bold text-sm label-with-padding-right">
                Min
              </label>
              <DropdownComponent
                value={selectedMinExperience}
                options={minExperienceOptions}
                optionLabel="label"
                placeholder="Min"
                onChange={(e) =>
                  setFormData({ ...formData, minExperience: e.target.value })
                }
              />

              <label className="font-bold text-sm label-with-padding-left label-with-padding-right">
                Max
              </label>
              <DropdownComponent
                value={selectedMaxExperience}
                options={maxExperienceOptions}
                optionLabel="label"
                placeholder="Max"
                onChange={(e) =>
                  setFormData({ ...formData, maxExperience: e.target.value })
                }
              />
            </div>
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
              onChange={(e) =>
                setFormData({ ...formData, qualification: e.target.value })
              }
            />
          </div>
        </div>
      </section>

      <div className="flex flex-wrap justify-content-end gap-5 mt-3">
        <ButtonC
          label="CANCEL"
          outlined
          className="mr-auto w-2"
          onClick={handleCancel}
        />
        <ButtonC
          label="SAVE AS A DRAFT"
          className="w-2"
          onClick={() => handleSubmit(2)}
        />
        <ButtonC
          label="SUBMIT"
          className="w-2"
          disabled
          onClick={() => handleSubmit(1)}
        />
        <ToastMessages ref={toastRef} />
      </div>
    </div>
  );
};

export default CreateRequisitionBody;
