import React, { useState, useEffect, useRef } from "react";

import DropdownComponent from "../Components/Dropdown";
import InputTextCp from "../Components/Textbox";
import ButtonC from "../Components/Button";
import CalendarComponent from "../Components/Calendar";
import CheckboxComponent from "../Components/Checkbox";
import InputTextareaComponent from "../Components/InputTextarea";
import ToastMessages from "../Components/ToastMessages";

import MultiSelectDropdown from "../Components/multiselectDropdown";

import {
  minExperienceOptions,
  maxExperienceOptions,
  Gender,
} from "../Components/constant";

const CreateRequisitionBody = () => {
  // State to hold all the dropdown data
  const [dropdownData, setDropdownData] = useState({});
  const [subDepartments, setSubDepartments] = useState([]);
  const [isLoading, setIsLoading] = useState(false);

  const toastRef = useRef(null);
  const getReqId = 1;

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
    minGradeId: 0,
    maxGradeId: 0,
    employmentTypeId: 0,
    minExperience: 0,
    maxExperience: 0,
    vacancyTypeId: 0,
    isReplacement: false,
    mrfStatusId: 0,
    jdDocPath: "",
    locationId: 0,
    justification: "",
    softwaresRequired: "",
    hardwaresRequired: "",
    minTargetSalary: 0,
    maxTargetSalary: 0,
    employeeName: "",
    emailId: "",
    employeeCode: 0,
    lastWorkingDate: "",
    annualCtc: 0,
    annualGross: 0,
    replaceJustification: "",
    resumeReviewerEmployeeIds: null,
    interviewerEmployeeIds: null,
  };

  // Initialize the formData state using the form schema
  const [formData, setFormData] = useState(formSchema);

  useEffect(() => {
    // Fetch the data for all the dropdowns
    fetch("https://localhost:7128/api/Mrfdetail/GetMRFDropdownlist")
      .then((response) => response.json())
      .then((data) => {
        const dropdown = data.result;
        // Update the state with the new dropdown data
        setDropdownData(dropdown);
      })
      .catch((error) => {
        console.error("Error fetching data:", error);
      });

    if (getReqId) {
      const apiUrl = `https://localhost:7128/api/Mrfdetail/GetRequisition/${getReqId}`;
      fetch(apiUrl)
        .then((response) => response.json())
        .then((response) => {
          setFormData({ ...formData, ...response });
        })
        .catch((error) => {
          console.error("Fetch error:", error);
        });
    }
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

  useEffect(() => {
    fetchSubDepartments(formData.departmentId);
  }, [formData.departmentId]);

  const handleSubmit = async (mrfStatusId) => {
    setIsLoading(true);

    const data = {
      referenceNo: formData.referenceNo,
      positionTitle: formData.positionTitle,
      departmentId: formData.departmentId,
      subDepartmentId: formData.subDepartmentId,
      projectId: formData.projectId,
      vacancyNo: formData.vacancyNo,
      genderId: formData.genderId,
      requisitionDateUtc: new Date().toISOString().slice(0, 10),
      reportsToEmployeeId: formData.reportingTo,
      gradeId: formData.gradeId,
      employmentTypeId: formData.employmentTypeId,
      minExperience: formData.minExperience,
      maxExperience: formData.maxExperience,
      vacancyTypeId: formData.vacancyTypeId,
      isReplacement: formData.isReplacement,
      mrfStatusId: mrfStatusId,
      jdDocPath: "string",
      locationId: formData.locationId,
      createdByEmployeeId: 1,
      createdOnUtc: new Date().toISOString(),
      updatedByEmployeeId: 1,
      updatedOnUtc: new Date().toISOString(),
      justification: formData.justification,
      jobDescription: formData.jobDescription,
      skills: formData.skills,
      minTargetSalary: formData.minTargetSalary,
      maxTargetSalary: formData.maxTargetSalary,
      employeeName: formData.employeeName,
      emailId: formData.emailId,
      employeeCode: formData.employeeCode,
      lastWorkingDate: new Date().toISOString().slice(0, 10),
      annualCtc: formData.annualCtc,
      annualGross: formData.annualGross,
      replaceJustification: formData.replaceJustification,
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
        }
      );

      if (response.ok) {
        const responseData = await response.json();
        console.log("Response Data:", responseData);
        if (responseData.statusCode === 409) {
          toastRef.current.showConflictMessage(responseData.message);
        } else {
          toastRef.current.showSuccessMessage("Form submitted successfully!");
          // window.location.href = '/MyRequisitions';
        }
      } else {
        console.error("Request failed with status:", response.status);
        const errorData = await response.text();
        console.error("Error Data:", errorData);
        if (response.status === 400) {
          toastRef.current.showBadRequestMessage(
            "Bad request: " + response.url
          );
        }
      }
    } catch (error) {
      console.error("Error:", error);
    } finally {
      setIsLoading(false);
    }
  };

  const strToArray = (s) => {
    if (typeof s === "string") {
      s = s.split(",").map(Number);
    }
    return s;
  };

  //need to change this
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
        style={{ height: "95%" }}
      >
        {getReqId ? (
          <div className="flex justify-content-between gap-5">
            <div className="flex flex-column w-6 gap-2">
              <h4 className="text-xl my-2">
                Reference Number: {formData.referenceNo}
              </h4>
            </div>
          </div>
        ) : (
          ""
        )}
        <div className="flex justify-content-between gap-5">
          <div className="flex flex-column gap-2 w-full">
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
              options={dropdownData.departments}
              value={formData.departmentId}
              onChange={(e) => {
                setFormData({ ...formData, departmentId: e.target.value });
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
              value={formData.subDepartmentId}
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
              value={formData.projectId}
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
              value={formData.locationId}
              onChange={(e) =>
                setFormData({ ...formData, locationId: e.target.value })
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
              value={formData.reportsToEmployeeId}
              onChange={(e) =>
                setFormData({ ...formData, reportingTo: e.target.value })
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
              value={new Date(formData.requisitionDateUtc)}
              onChange={(e) =>
                setFormData({
                  ...formData,
                  requisitionDateUtc: e.target.value,
                })
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
              value={formData.employmentTypeId}
              onChange={(e) =>
                setFormData({ ...formData, employmentTypeId: e.target.value })
              }
            />
          </div>

          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="grade" className="font-bold text-sm">
              Grade of the proposed employee
            </label>

            <div className="p-col-7">
              <label className="font-bold text-sm label-with-padding-right">
                Min
              </label>
              <DropdownComponent
                value={formData.minGradeId}
                options={dropdownData.grades}
                optionLabel="name"
                optionValue='id'
                placeholder="Min"
                onChange={(e) =>
                  setFormData({ ...formData, minGradeId: e.target.value })
                }
              />

              <label className="font-bold text-sm label-with-padding-left label-with-padding-right">
                Max
              </label>
              <DropdownComponent
                value={formData.maxGradeId}
                options={dropdownData.grades}
                optionLabel="name"
                optionValue='id'
                placeholder="Max"
                onChange={(e) =>
                  setFormData({ ...formData, maxGradeId: e.target.value })
                }
              />
            </div>
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
              value={formData.vacancyNo}
              onChange={(e) =>
                setFormData({ ...formData, vacancyNo: e.target.value })
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
              value={formData.vacancyTypeId}
              onChange={(e) =>
                setFormData({ ...formData, vacancyTypeId: e.target.value })
              }
            />
          </div>
        </div>
        <div className="flex justify-content-between gap-5 ">
          <div className="flex flex-column w-5 gap-2">
            <label htmlFor="experience" className="font-bold text-sm">
              Experience
            </label>
            <div className="p-col-7">
              <label className="font-bold text-sm label-with-padding-right">
                Min
              </label>
              <DropdownComponent
                value={formData.minExperience}
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
                value={formData.maxExperience}
                options={maxExperienceOptions}
                optionLabel="label"
                placeholder="Max"
                onChange={(e) =>
                  setFormData({ ...formData, maxExperience: e.target.value })
                }
              />
            </div>
          </div>
          <div className="flex flex-column w-6 row-gap-2">
            <label htmlFor="gender" className="font-bold text-sm">
              Gender
            </label>
            <DropdownComponent
              optionLabel="label"
              optionValue="id"
              type="Gender"
              options={Gender}
              value={formData.genderId}
              onChange={(e) =>
                setFormData({ ...formData, genderId: e.target.value })
              }
            />
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
              value={formData.qualification}
              onChange={(e) =>
                setFormData({ ...formData, qualification: e.target.value })
              }
            />
          </div>
        </div>
        <div className="flex justify-content-between gap-5">
          <div className="flex flex-row align-items-center h-3rem w-5 gap-2 px-4 border-round-sm border-1 border-300 bg-gray-100">
            <CheckboxComponent
              inputId="replacement"
              checked={formData.isReplacement}
              onChange={(e) =>
                setFormData({ ...formData, isReplacement: e.checked })
              }
            />
            <label htmlFor="replacement" className="font-bold text-sm">
              Replacement for the employee
            </label>
          </div>
        </div>
        {formData.isReplacement && (
          <>
            <div className="flex justify-content-between gap-5">
              <div className="flex flex-column w-6 gap-2">
                <label htmlFor="employeeName" className="font-bold text-sm">
                  Employee Name
                </label>
                <InputTextCp
                  id="employeeName"
                  onChange={(e) =>
                    setFormData({ ...formData, employeeName: e.target.value })
                  }
                  value={formData.employeeName}
                />
              </div>

              <div className="flex flex-column w-6 gap-2">
                <label htmlFor="lastworkingDate" className="font-bold text-sm">
                  Last Working Date
                </label>
                <CalendarComponent
                  id="lastworkingDate"
                  inputClassName="bg-gray-100"
                  value={new Date(formData.lastWorkingDate)}
                  onChange={(e) =>
                    setFormData({
                      ...formData,
                      lastWorkingDate: e.target.value,
                    })
                  }
                />
              </div>
            </div>
            <div className="flex justify-content-between gap-5">
              <div className="flex flex-column w-6 gap-2">
                <label htmlFor="EmployeeEmail" className="font-bold text-sm">
                  Employee Email
                </label>
                <InputTextCp
                  id="EmployeeEmail"
                  onChange={(e) =>
                    setFormData({ ...formData, emailId: e.target.value })
                  }
                  value={formData.emailId}
                />
              </div>

              <div className="flex flex-column w-6 gap-2">
                <label htmlFor="EmployeeCode" className="font-bold text-sm">
                  Employee Code
                </label>
                <InputTextCp
                  id="EmployeeCode"
                  onChange={(e) =>
                    setFormData({ ...formData, employeeCode: e.target.value })
                  }
                  value={formData.employeeCode}
                />
              </div>
            </div>
            <div className="flex justify-content-between gap-5">
              <div className="flex flex-column w-6 gap-2">
                <label htmlFor="AnnualCTC" className="font-bold text-sm">
                  Annual CTC
                </label>
                <InputTextCp
                  id="AnnualCTC"
                  onChange={(e) =>
                    setFormData({ ...formData, annualCtc: e.target.value })
                  }
                  value={formData.annualCtc}
                />
                <label htmlFor="AnnualGross" className="font-bold text-sm">
                  Annual Gross
                </label>
                <InputTextCp
                  id="AnnualGross"
                  onChange={(e) =>
                    setFormData({ ...formData, annualGross: e.target.value })
                  }
                  value={formData.annualGross}
                />
              </div>

              <div className="flex flex-column w-6 gap-2">
                <label
                  htmlFor="ReplaceJustification"
                  className="font-bold text-sm"
                >
                  Replacement Justification
                </label>
                <InputTextareaComponent
                  autoResize
                  id="ReplaceJustification"
                  className="bg-gray-100"
                  value={formData.replaceJustification}
                  onChange={(e) =>
                    setFormData({
                      ...formData,
                      replaceJustification: e.target.value,
                    })
                  }
                />
              </div>
            </div>
          </>
        )}
        <div className="flex justify-content-between gap-5">
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="jobDescription" className="font-bold text-sm">
              Job Description
            </label>
            <InputTextareaComponent
              autoResize
              id="jobDescription"
              className="bg-gray-100"
              value={formData.jobDescription}
              onChange={(e) =>
                setFormData({ ...formData, jobDescription: e.target.value })
              }
            />
          </div>
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="skills" className="font-bold text-sm">
              Skills
            </label>
            <InputTextareaComponent
              autoResize
              id="skills"
              className="bg-gray-100"
              value={formData.skills}
              onChange={(e) =>
                setFormData({ ...formData, skills: e.target.value })
              }
            />
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
              value={formData.justification}
              onChange={(e) =>
                setFormData({ ...formData, justification: e.target.value })
              }
            />
          </div>
          <div className="flex flex-column gap-4 w-6">
            <div className="flex flex-column gap-2">
              <label htmlFor="MinTargetSalary" className="font-bold text-sm">
                Min Target Salary
              </label>
              <InputTextCp
                id="MinTargetSalary"
                onChange={(e) =>
                  setFormData({ ...formData, minTargetSalary: e.target.value })
                }
                value={formData.minTargetSalary}
              />
            </div>
            <div className="flex flex-column gap-2">
              <label htmlFor="MaxTargetSalary" className="font-bold text-sm">
                Max Target Salary
              </label>
              <InputTextCp
                id="MaxTargetSalary"
                onChange={(e) =>
                  setFormData({ ...formData, maxTargetSalary: e.target.value })
                }
                value={formData.maxTargetSalary}
              />
            </div>
          </div>
        </div>
        <div className="flex justify-content-between gap-5">
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="resumeReviewer" className="font-bold text-sm">
              Resume Reviewer
            </label>

            <MultiSelectDropdown
              id="resumeReviewer"
              options={dropdownData.resumereviewer}
              value={strToArray(formData.resumeReviewerEmployeeIds)}
              onChange={(e) => {
                console.log(e.value);
                setFormData({
                  ...formData,
                  resumeReviewerEmployeeIds: e.value,
                });
              }}
              optionLabel="name"
              optionValue="employeeId"
            />
          </div>
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="interviewer" className="font-bold text-sm">
              Interviewer/Panel
            </label>

            <MultiSelectDropdown
              id="interviewer"
              options={dropdownData.interviewReviewer}
              value={strToArray(formData.interviewerEmployeeIds)}
              onChange={(e) =>
                setFormData({
                  ...formData,
                  interviewerEmployeeIds: e.value,
                })
              }
              optionLabel="name"
              optionValue="employeeId"
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
