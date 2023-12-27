import React, { useState, useEffect, useRef } from "react";
// import { useNavigate } from "react-router-dom";
import "../styles/layout/InputComponents.css";
import DropdownComponent from "./../components/Dropdown";
import InputTextCp from "./../components/Textbox";
import ButtonC from "./../components/Button";
import CalendarComponent from "./../components/Calendar";
import CheckboxComponent from "./../components/Checkbox";
import InputTextareaComponent from "./../components/InputTextarea";
import { Editor } from 'primereact/editor';
import ToastMessages from "./../components/ToastMessages";
import MultiSelectDropdown from "./../components/multiselectDropdown";
import { mrfStatus } from "../components/constant";
import { navigateTo } from "../constants/Utils";
import { API_URL, GENDER, MAX_EXPERIENCE_OPTIONS, MIN_EXPERIENCE_OPTIONS, MRF_STATUS, REQUISITION_TYPE } from "../constants/config";
import { storageService } from "../constants/storage";
import MrfPartialStatus from "../components/MrfPartialStatus";
import { useDispatch } from "react-redux";
import { PAGE_ACTIONS } from "../reducers/Page_r";

const CreateRequisitionBody = ({
  getReqId = null,
  getReqRoleId = null,
  status = null,
  mrfStatusId = null,
}) => {
  // State to hold all the dropdown data
  const [dropdownData, setDropdownData] = useState({});
  const [subDepartments, setSubDepartments] = useState([]);
  const [isLoading, setIsLoading] = useState(false);
  const RedAsterisk = () => <span className="text-red-500">*</span>;
  const [visible, setVisible] = useState(false);
  const [readOnly, setReadOnly] = useState(false);

  const dispatch = useDispatch();

  const toastRef = useRef(null);

  const formSchema = {
    referenceNo: "",

    positionTitleId: "",
    requisitionType: "",

    departmentId: 0,
    subDepartmentId: 0,
    projectId: 0,
    vacancyNo: 0,
    genderId: 0,
    qualification: "",
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
    qualificationId: 0,
    justification: "",
    softwaresRequired: "",
    hardwaresRequired: "",
    minTargetSalary: 0,
    maxTargetSalary: 0,
    employeeName: "",
    emailId: "",
    employeeCode: "",
    lastWorkingDate: "",
    annualCtc: 0,
    annualGross: 0,
    replaceJustification: "",
    jobDescription: "",
    skills: "",
    resumeReviewerEmployeeIds: [],
    interviewerEmployeeIds: [],
    hiringManagerId: 0,
    hiringManagerEmpId: 0,
    functionHeadId: 0,
    functionHeadEmpId: 0,
    siteHRSPOCId: 0,
    siteHRSPOCEmpId: 0,
    financeHeadId: 0,
    financeHeadEmpId: 0,
    presidentnCOOId: 0,
    presidentnCOOEmpId: 0,
  };

  // Initialize the formData state using the form schema
  const [formData, setFormData] = useState(formSchema);

  useEffect(() => {
    // Fetch the data for all the dropdowns
    fetch(API_URL.GET_CREATE_REQUISITION_DROPDOWN)
      .then((response) => response.json())
      .then((data) => {
        const dropdown = data.result;
        // Store the dropdown data in localStorage using your storageService
        // storageService.set("dropdownData", dropdown);
        // Update the state with the new dropdown data
        setDropdownData(dropdown);
      })
      .catch((error) => {
        console.error("Error fetching data:", error);
      });

    if (getReqId) {
      const apiUrl = API_URL.GET_CREATE_REQUISITION_DEATILS + getReqId;
      fetch(apiUrl)
        .then((response) => response.json())
        .then((response) => {
          setFormData({ ...formData, ...response });
        })
        .catch((error) => {
          console.error("Fetch error:", error);
        });
    } else {
      setFormData(formSchema);
    }

    if (getReqRoleId == 4) {
      setReadOnly(true);
    } else if (
      mrfStatusId == MRF_STATUS.draft ||
      mrfStatusId == MRF_STATUS.resubReq
    ) {
      setReadOnly(false);
    } else if (getReqRoleId == 3) {
      setReadOnly(true);
    }
  }, []);

  const fetchSubDepartments = (selectedDepartment) => {
    const apiUrl =
      API_URL.GET_CREATE_REQUISITION_DEPARTMENT + selectedDepartment;
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


  const strToArray = (s) => {
    s = s ?? "";
    if (typeof s === "string") {
      s = s.split(",").map(Number);
    }
    return s;
  };

  const renderHeader = () => {
    return (
      <span className="ql-formats">
        <button className="ql-bold" aria-label="Bold"></button>
        <button className="ql-italic" aria-label="Italic"></button>
        <button className="ql-'list': 'ordered'" aria-label="Italic"></button>
        <button className="ql-underline" aria-label="Underline"></button>{" "}
        {/* */}
      </span>
    );
  };

  const header = renderHeader();

  

  const arrayToObj = (options = [], selectedOpt) => {
    if (Array.isArray(selectedOpt)) {
      return options.filter((e) => selectedOpt.includes(e.employeeId));
    }
  };

  const objToArray = (selectedOpt = []) => {
    return selectedOpt.map((e) => e.employeeId);
  };

  const handleCancel = () => {
    setFormData(formSchema);
    setDropdownData({});
    setSubDepartments([]);
    navigateTo("my_requisition");
  };

  
  return (
    <div
      className="border-round-lg bg-white text-black-alpha-90 p-3 flex flex-column justify-content-between"
      style={{ height: "81vh" }}
    >
      {getReqId ? "" : <h3 className="text-xl my-2">Fill the Details</h3>}

      <section
        className="flex flex-column flex-nowrap gap-3 
        border-y-2 border-gray-300 
        py-3 px-1 overflow-y-scroll"
        style={{ height: "95%" }}
      >
        {/* border-y-2 border-white-300  */}
        {getReqId ? (
          <div className="flex justify-content-between gap-5">
            <div className="flex flex-column w-6 gap-2">
              <h4 className="text-xl my-2">
                Reference Number:{" "}
                <span className="text-red-600">
                  {formData.referenceNo}
                  {" - "}
                  {`(${status})`}
                </span>
              </h4>
            </div>
          </div>
        ) : (
          ""
        )}

        {formData.mrfStatusId == 3 ? (
          <label htmlFor="RequisitionType" className="font-semibold text-base">
            <span className="font-bold text-red-600 text-lg">Note: </span>
            {formData.note}
          </label>
        ) : (
          ""
        )}
        <div className="flex justify-content-between gap-5">
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="RequisitionType" className="font-bold text-sm">
              Requisition Type
              <RedAsterisk />
            </label>
            <DropdownComponent
              optionLabel="name"
              optionValue="code"
              type="RequisitionType"
              options={REQUISITION_TYPE}
              disable={readOnly}
              value={
                formData.requisitionType ||
                (REQUISITION_TYPE.length > 0 ? REQUISITION_TYPE[0].code : null)
              }
              onChange={(e) => {
                setFormData({ ...formData, requisitionType: e.target.value });
              }}
            />
          </div>
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="position-title" className="font-bold text-sm">
              Position Title
              <RedAsterisk />
            </label>
           <DropdownComponent
              optionLabel="name"
              optionValue="id"
              type="position"
              options={dropdownData.position}
              value={formData.positionTitleId}
              onChange={(e) => {
                setFormData({ ...formData, positionTitleId: e.target.value });
              }}
            />

          </div>
        </div>

        <div className="flex justify-content-between gap-5">
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="department" className="font-bold text-sm">
              Department
              <RedAsterisk />
            </label>
            <DropdownComponent
              optionLabel="name"
              optionValue="id"
              type="Department"
              options={dropdownData.departments}
              value={formData.departmentId}
              disable={readOnly}
              onChange={(e) => {
                setFormData({ ...formData, departmentId: e.target.value });
              }}
            />
          </div>
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="sub-department" className="font-bold text-sm">
              Sub-Department
              <RedAsterisk />
            </label>
            <DropdownComponent
              optionLabel="name"
              optionValue="id"
              type="subDepartmentId"
              options={subDepartments}
              disable={readOnly}
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
              <RedAsterisk />
            </label>
            <DropdownComponent
              optionLabel="name"
              optionValue="id"
              type="project"
              options={dropdownData.projects}
              disable={readOnly}
              value={formData.projectId}
              onChange={(e) =>
                setFormData({ ...formData, projectId: e.target.value })
              }
            />
          </div>
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="location" className="font-bold text-sm">
              Location
              <RedAsterisk />
            </label>
            <DropdownComponent
              optionLabel="location"
              optionValue="id"
              type="location"
              options={dropdownData.location}
              value={formData.locationId}
              disable={readOnly}
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
              <RedAsterisk />
            </label>
            <DropdownComponent
              optionLabel="name"
              optionValue="id"
              type="reportingTo"
              options={dropdownData.reportingTo}
              value={formData.reportsToEmployeeId}
              disable={readOnly}
              onChange={(e) =>
                setFormData({
                  ...formData,
                  reportsToEmployeeId: e.target.value,
                })
              }
            />
          </div>

          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="initiation-date" className="font-bold text-sm">
              Requirement Initiation Date
              <RedAsterisk />
            </label>
            <CalendarComponent
              id="initiation-date"
              inputClassName="bg-gray-100"
              value={new Date(formData.requisitionDateUtc)}
              disable={readOnly}
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
              <RedAsterisk />
            </label>
            <DropdownComponent
              optionLabel="type"
              optionValue="id"
              type="employmenttypes"
              options={dropdownData.employmentTypes}
              value={formData.employmentTypeId}
              disable={readOnly}
              onChange={(e) =>
                setFormData({ ...formData, employmentTypeId: e.target.value })
              }
            />
          </div>

          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="mingrade" className="font-bold text-sm">
              Grade of the proposed employee
              <RedAsterisk />
            </label>

            <div className="p-col-7">
              <label className="font-bold text-sm label-with-padding-right">
                Min
              </label>
              <DropdownComponent
                value={formData.minGradeId}
                options={dropdownData.grades}
                optionLabel="name"
                optionValue="id"
                placeholder="Min"
                disable={readOnly}
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
                optionValue="id"
                placeholder="Max"
                disable={readOnly}
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
              Number of Vacancies
              <RedAsterisk />
            </label>
            <InputTextCp
              id="vaccancies"
              className="bg-gray-100"
              value={formData.vacancyNo}
              onChange={(e) =>
                setFormData({ ...formData, vacancyNo: e.target.value })
              }
              disable={readOnly}
            />
          </div>

          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="vacancy-type" className="font-bold text-sm">
              Type of vacancy
              <RedAsterisk />
            </label>
            <DropdownComponent
              optionLabel="type"
              optionValue="id"
              type="vaccancy"
              options={dropdownData.vaccancies}
              value={formData.vacancyTypeId}
              disable={readOnly}
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
              <RedAsterisk />
            </label>
            <div className="p-col-7">
              <label className="font-bold text-sm label-with-padding-right">
                Min
              </label>
              <DropdownComponent
                value={formData.minExperience}
                options={MIN_EXPERIENCE_OPTIONS}
                optionLabel="label"
                placeholder="Min"
                disable={readOnly}
                onChange={(e) =>
                  setFormData({ ...formData, minExperience: e.target.value })
                }
              />

              <label className="font-bold text-sm label-with-padding-left label-with-padding-right">
                Max
              </label>
              <DropdownComponent
                value={formData.maxExperience}
                options={MAX_EXPERIENCE_OPTIONS}
                optionLabel="label"
                placeholder="Max"
                disable={readOnly}
                onChange={(e) =>
                  setFormData({ ...formData, maxExperience: e.target.value })
                }
              />
            </div>
          </div>
          <div className="flex flex-column w-6 row-gap-2">
            <label htmlFor="gender" className="font-bold text-sm">
              Gender
              <RedAsterisk />
            </label>
            <DropdownComponent
              optionLabel="label"
              optionValue="id"
              type="Gender"
              options={GENDER}
              disable={readOnly}
              value={formData.genderId}
              onChange={(e) =>
                setFormData({ ...formData, genderId: e.target.value })
              }
            />
          </div>
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="qualification" className="font-bold text-sm">
              Qualification
              <RedAsterisk />
            </label>
            <DropdownComponent
              optionLabel="type"
              optionValue="id"
              type="Qualification"
              options={dropdownData.qualification}
              value={formData.qualificationId}
              disable={readOnly}
              onChange={(e) =>
                setFormData({ ...formData, qualificationId: e.target.value })
              }
            />
          </div>
        </div>
        <div className="flex justify-content-between gap-5">
          <div className="flex flex-row align-items-center h-3rem w-5 gap-2 px-4 border-round-sm border-1 border-300 bg-gray-100">
            <CheckboxComponent
              inputId="replacement"
              checked={formData.isReplacement}
              disable={readOnly}
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
                  disable={readOnly}
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
                  disable={readOnly}
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
                  disable={readOnly}
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
                  disable={readOnly}
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
                  disable={readOnly}
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
                  disable={readOnly}
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
                  disable={readOnly}
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
              <RedAsterisk />
            </label>
            <InputTextareaComponent
              autoResize
              id="jobDescription"
              className="bg-gray-100"
              value={formData.jobDescription}
              disable={readOnly}
              onChange={(e) =>
                setFormData({ ...formData, jobDescription: e.target.value })
              }
            />
          </div>

          {/* <div className="flex flex-column mb-6 w-6 gap-2">
            <label htmlFor="jobDescription" className="font-bold text-sm">
              Job Description
              <RedAsterisk />
            </label>
              <Editor
             autoResize
               value={formData.jobDescription}
            // headerTemplate={header}
            onChange={(e) =>
                 setFormData({ ...formData, jobDescription: e.htmlValue })
               }
            /> 
          </div> */}

          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="skills" className="font-bold text-sm">
              Skills
              <RedAsterisk />
            </label>
            <InputTextareaComponent
              autoResize
              id="skills"
              className="bg-gray-100"
              value={formData.skills}
              disable={readOnly}
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
              <RedAsterisk />
            </label>

            <InputTextareaComponent
              autoResize
              id="Justification"
              className="bg-gray-100"
              value={formData.justification}
              onChange={(e) =>
                setFormData({ ...formData, justification: e.target.value })
              }
              disable={readOnly}
            />
          </div>
          <div className="flex flex-column gap-4 w-6">
            <div className="flex flex-column gap-2">
              <label htmlFor="MinTargetSalary" className="font-bold text-sm">
                Min Target Salary
                <RedAsterisk />
              </label>
              <InputTextCp
                id="MinTargetSalary"
                onChange={(e) =>
                  setFormData({ ...formData, minTargetSalary: e.target.value })
                }
                value={formData.minTargetSalary}
                disable={readOnly}
              />
            </div>
            <div className="flex flex-column gap-2">
              <label htmlFor="MaxTargetSalary" className="font-bold text-sm">
                Max Target Salary
                <RedAsterisk />
              </label>
              <InputTextCp
                id="MaxTargetSalary"
                onChange={(e) =>
                  setFormData({ ...formData, maxTargetSalary: e.target.value })
                }
                value={formData.maxTargetSalary}
                disable={readOnly}
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
              value={arrayToObj(
                dropdownData.resumereviewer,
                strToArray(formData.resumeReviewerEmployeeIds)
              )}
              onChange={(e) =>
                setFormData({
                  ...formData,
                  resumeReviewerEmployeeIds: objToArray(e.value),
                })
              }
              optionLabel="name"
              disable={readOnly}

              // optionValue="employeeId"
            />
          </div>
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="interviewer" className="font-bold text-sm">
              Interviewer/Panel
            </label>

            <MultiSelectDropdown
              id="interviewer"
              options={dropdownData.interviewReviewer}
              value={arrayToObj(
                dropdownData.interviewReviewer,
                strToArray(formData.interviewerEmployeeIds)
              )}
              onChange={(e) =>
                setFormData({
                  ...formData,
                  interviewerEmployeeIds: objToArray(e.value),
                })
              }
              disable={readOnly}
              optionLabel="name"
              // optionValue="employeeId"
            />
          </div>
        </div>
        <div className="flex justify-content-between">
          <h1 className="my-2 mx-3">
            EMAIL APPROVAL/SIGNATURE DATES:
            <RedAsterisk />
          </h1>
        </div>
        <div id="first" className="flex justify-content-evenly gap-4">
          <div className="flex flex-column gap-2">
            <label htmlFor="Position" className="font-bold text-sm">
              Position
            </label>
            <InputTextCp
              type="text"
              id="Position"
              className="p-disabled"
              onChange={(e) => setFormData({ ...formData, Position: 7 })}
              //
              value="Hiring Manager"
            />
          </div>

          <div className="flex flex-column gap-2 w-3">
            <label htmlFor="Name" className="font-bold text-sm">
              Name
            </label>
            {/* Assuming DropdownComponent renders an input */}
            <DropdownComponent
              optionLabel="name"
              optionValue="employeeId"
              type="hiringManager"
              options={dropdownData.hiringManager}
              value={formData.hiringManagerId}
              disable={readOnly}
              onChange={(e) => {
                const selectedHiringManagerId = e.target.value;
                const selectedHiringManager = dropdownData.hiringManager.find(
                  (manager) => manager.employeeId === selectedHiringManagerId
                );

                if (selectedHiringManager) {
                  setFormData({
                    ...formData,
                    hiringManagerId: selectedHiringManagerId,
                    hiringManagerEmpId: selectedHiringManager.employeeCode,
                  });
                }
              }}
            />
          </div>

          <div className="flex flex-column gap-2">
            <label htmlFor="EmployeeCode" className="font-bold text-sm">
              Employee ID
            </label>
            <InputTextCp
              id="hiringManagerEmpId"
              className="p-disabled"
              onChange={(e) =>
                setFormData({ ...formData, hiringManagerEmpId: e.target.value })
              }
              value={formData.hiringManagerEmpId}
              disable={readOnly}
            />
          </div>

          <div className="flex flex-column gap-2"></div>
        </div>
        <div id="second" className="flex justify-content-evenly gap-4">
          <div className="flex flex-column gap-2">
            <InputTextCp
              type="text"
              id="Position"
              className="p-disabled"
              disable={readOnly}
              onChange={(e) => setFormData({ ...formData, Position: 8 })}
              value="Function Head"
            />
          </div>

          <div className="flex flex-column gap-2 w-3">
            {/* Assuming DropdownComponent renders an input */}
            <DropdownComponent
              optionLabel="name"
              optionValue="employeeId"
              type="functionHead"
              options={dropdownData.functionHead}
              value={formData.functionHeadId}
              disable={readOnly}
              onChange={(e) => {
                const selectedfunctionHeadId = e.target.value;
                const selectedfunctionHead = dropdownData.functionHead.find(
                  (manager) => manager.employeeId === selectedfunctionHeadId
                );

                if (selectedfunctionHead) {
                  setFormData({
                    ...formData,
                    functionHeadId: selectedfunctionHeadId,
                    functionHeadEmpId: selectedfunctionHead.employeeCode,
                  });
                }
              }}
            />
          </div>

          <div className="flex flex-column gap-2">
            <InputTextCp
              id="functionHeadEmpId"
              className="p-disabled"
              onChange={(e) =>
                setFormData({ ...formData, functionHeadEmpId: e.target.value })
              }
              value={formData.functionHeadEmpId}
              disable={readOnly}
            />
          </div>

          <div className="flex flex-column gap-2"></div>
        </div>
        <div id="third" className="flex justify-content-evenly gap-4">
          <div className="flex flex-column gap-2">
            <InputTextCp
              type="text"
              id="Position"
              className="p-disabled"
              onChange={(e) => setFormData({ ...formData, Position: 9 })}
              value="Site HR SPOC"
              disable={readOnly}
            />
          </div>
          <div className="flex flex-column gap-2 w-3">
            {/* Assuming DropdownComponent renders an input */}
            <DropdownComponent
              optionLabel="name"
              optionValue="employeeId"
              type="siteHRSPOCId"
              options={dropdownData.siteHRSPOC}
              value={formData.siteHRSPOCId}
              disable={readOnly}
              onChange={(e) => {
                const selectedsiteHRSPOCId = e.target.value;
                const selectedsiteHRSPOCEmpId = dropdownData.siteHRSPOC.find(
                  (manager) => manager.employeeId === selectedsiteHRSPOCId
                );

                if (selectedsiteHRSPOCEmpId) {
                  setFormData({
                    ...formData,
                    siteHRSPOCId: selectedsiteHRSPOCId,
                    siteHRSPOCEmpId: selectedsiteHRSPOCEmpId.employeeCode,
                  });
                }
              }}
            />
          </div>
          <div className="flex flex-column gap-2">
            <InputTextCp
              id="siteHRSPOCEmpId"
              className="p-disabled"
              onChange={(e) =>
                setFormData({ ...formData, siteHRSPOCEmpId: e.target.value })
              }
              value={formData.siteHRSPOCEmpId}
              disable={readOnly}
            />
          </div>
          <div className="flex flex-column gap-2"></div>{" "}
        </div>
        <div id="forth" className="flex justify-content-evenly gap-4">
          <div className="flex flex-column gap-2">
            <InputTextCp
              type="text"
              id="Position"
              className="p-disabled"
              onChange={(e) => setFormData({ ...formData, Position: 10 })}
              value="Finance Head"
              disable={readOnly}
            />
          </div>
          <div className="flex flex-column gap-2 w-3">
            {/* Assuming DropdownComponent renders an input */}
            <DropdownComponent
              optionLabel="name"
              optionValue="employeeId"
              type="financeHead"
              options={dropdownData.financeHead}
              value={formData.financeHeadId}
              disable={readOnly}
              onChange={(e) => {
                const selectedfinanceHeadId = e.target.value;
                const selectedfinanceHeadEmpId = dropdownData.financeHead.find(
                  (manager) => manager.employeeId === selectedfinanceHeadId
                );

                if (selectedfinanceHeadEmpId) {
                  setFormData({
                    ...formData,
                    financeHeadId: selectedfinanceHeadId,
                    financeHeadEmpId: selectedfinanceHeadEmpId.employeeCode,
                  });
                }
              }}
            />
          </div>
          <div className="flex flex-column gap-2">
            <InputTextCp
              id="financeHeadEmpId"
              className="p-disabled"
              onChange={(e) =>
                setFormData({ ...formData, financeHeadEmpId: e.target.value })
              }
              value={formData.financeHeadEmpId}
              disable={readOnly}
            />
          </div>
          <div className="flex flex-column gap-2"></div>{" "}
        </div>
        <div id="fifth" className="flex justify-content-evenly gap-4">
          <div className="flex flex-column gap-2">
            <InputTextCp
              type="text"
              id="Position"
              className="p-disabled"
              onChange={(e) => setFormData({ ...formData, Position: 11 })}
              value="President & COO"
              disable={readOnly}
            />
          </div>

          <div className="flex flex-column gap-2 w-3">
            {/* Assuming DropdownComponent renders an input */}
            <DropdownComponent
              optionLabel="name"
              optionValue="employeeId"
              type="presidentnCOO"
              options={dropdownData.presidentnCOO}
              value={formData.presidentnCOOId}
              disable={readOnly}
              onChange={(e) => {
                const selectedpresidentnCOOId = e.target.value;
                const selectedpresidentnCOOEmpId =
                  dropdownData.presidentnCOO.find(
                    (manager) => manager.employeeId === selectedpresidentnCOOId
                  );

                if (selectedpresidentnCOOEmpId) {
                  setFormData({
                    ...formData,
                    presidentnCOOId: selectedpresidentnCOOId,
                    presidentnCOOEmpId: selectedpresidentnCOOEmpId.employeeCode,
                  });
                }
              }}
            />
          </div>

          <div className="flex flex-column gap-2">
            <InputTextCp
              id="presidentnCOOEmpId"
              className="p-disabled"
              onChange={(e) =>
                setFormData({ ...formData, presidentnCOOEmpId: e.target.value })
              }
              value={formData.presidentnCOOEmpId}
            />
          </div>

          <div className="flex flex-column gap-2"></div>
        </div>
      </section>

      <div className="flex flex-wrap justify-content-end gap-5 mt-3">
        {/* <CreateRequisitionButtonHandle getReqId = {getReqId}
    RoleId = {getReqRoleId}
    status = {status}
    formData={formData}
    /> */}

        {(() => {
          if (getReqRoleId == 3) {
            switch (formData.mrfStatusId) {
              case MRF_STATUS.draft:
                return (
                  <>
                    <ButtonC
                      label="CANCEL"
                      className=" w-2 border-red-600 text-red-600"
                      onClick={handleCancel}
                      outlined="true"
                      // disable="true"
                    />
                    <MrfPartialStatus
                      mrfId={getReqId}
                      mrfStatusId={1}
                      label={"Save as Draft"}
                      message={"Do you want to Submit this MRF as Draft?"}
                      formData={formData}
                    />

                    <MrfPartialStatus
                      mrfId={getReqId}
                      mrfStatusId={2}
                      label={"Submit"}
                      message={"Do you want to Submit this MRF?"}
                      formData={formData}
                    />
                  </>
                );
              case MRF_STATUS.submToHr:
                return (
                  <>
                    <ButtonC
                      label="CANCEL"
                      className=" w-2 border-red-600 text-red-600"
                      onClick={handleCancel}
                      outlined="true"
                      // disable="true"
                    />
                    <MrfPartialStatus
                      mrfId={getReqId}
                      mrfStatusId={9}
                      label={"Withdraw"}
                      message={"Do you want to withdraw"}
                    />
                  </>
                );
              case MRF_STATUS.closed:
                return (
                  <>
                    <ButtonC
                      label="CANCEL"
                      className=" w-2 border-red-600 text-red-600"
                      onClick={handleCancel}
                      outlined="true"
                      // disable="true"
                    />
                  </>
                );
              case MRF_STATUS.withdrawn:
                return (
                  <>
                    <ButtonC
                      label="CANCEL"
                      className=" w-2 border-red-600 text-red-600"
                      onClick={handleCancel}
                      outlined="true"
                      // disable="true"
                    />
                  </>
                );
              case MRF_STATUS.onHold:
                return (
                  <>
                    <ButtonC
                      label="CANCEL"
                      className=" w-2 border-red-600 text-red-600"
                      onClick={handleCancel}
                      outlined="true"
                      // disable="true"
                    />
                  </>
                );
              case MRF_STATUS.resubReq:
                return (
                  <>
                    <ButtonC
                      label="CANCEL"
                      className=" w-2 border-red-600 text-red-600"
                      onClick={handleCancel}
                      outlined="true"
                      // disable="true"
                    />

                    <MrfPartialStatus
                      mrfId={getReqId}
                      mrfStatusId={2}
                      label={"Submit"}
                      message={"Do you want to Submit this MRF?"}
                      formData={formData}
                    />
                  </>
                );
              case MRF_STATUS.rejected:
                return (
                  <>
                    <ButtonC
                      label="CANCEL"
                      className=" w-2 border-red-600 text-red-600"
                      onClick={handleCancel}
                      outlined="true"
                      // disable="true"
                    />
                  </>
                );
              case MRF_STATUS.open:
                return (
                  <>
                    <ButtonC
                      label="CANCEL"
                      className=" w-2 border-red-600 text-red-600"
                      onClick={handleCancel}
                      outlined="true"
                      // disable="true"
                    />
                    <MrfPartialStatus
                      mrfId={getReqId}
                      mrfStatusId={9}
                      label={"Withdraw"}
                      message={"Do you want to withdraw"}
                    />
                  </>
                );
            }
          } else if (getReqRoleId == 4) {
            switch (formData.mrfStatusId) {
              case MRF_STATUS.submToHr:
                return (
                  <>
                    <ButtonC
                      label="CANCEL"
                      className=" w-2 border-red-600 text-red-600"
                      onClick={handleCancel}
                      outlined="true"
                      // disable="true"
                    />
                    <MrfPartialStatus
                      mrfId={getReqId}
                      mrfStatusId={3}
                      header={"Resubmission"}
                      label={"Resubmission Required"}
                      textbox={true}
                    />

                    <MrfPartialStatus
                      mrfId={getReqId}
                      mrfStatusId={11}
                      label={"Send for HOD approval"}
                      message={"“Do you want to submit it for HOD approval?"}
                    />

                    <MrfPartialStatus
                      mrfId={getReqId}
                      mrfStatusId={7}
                      label={"On Hold"}
                      message={"Do you want to hold on this MRF?"}
                    />
                  </>
                );
              case MRF_STATUS.closed:
                return (
                  <>
                    <ButtonC
                      label="CANCEL"
                      className=" w-2 border-red-600 text-red-600"
                      onClick={handleCancel}
                      outlined="true"
                      // disable="true"
                    />
                  </>
                );
              case MRF_STATUS.withdrawn:
                return (
                  <>
                    <ButtonC
                      label="CANCEL"
                      className=" w-2 border-red-600 text-red-600"
                      onClick={handleCancel}
                      outlined="true"
                      // disable="true"
                    />
                  </>
                );
              case MRF_STATUS.onHold:
                return (
                  <>
                    <ButtonC
                      label="CANCEL"
                      className=" w-2 border-red-600 text-red-600"
                      onClick={handleCancel}
                      outlined="true"
                      // disable="true"
                    />
                    <MrfPartialStatus
                      mrfId={getReqId}
                      mrfStatusId={4}
                      label={"Received HOD approval"}
                      message={"“Do you want to submit it for HOD approval?"}
                    />
                  </>
                );
              case MRF_STATUS.hodapproval:
                return (
                  <>
                    <ButtonC
                      label="CANCEL"
                      className=" w-2 border-red-600 text-red-600"
                      onClick={handleCancel}
                      outlined="true"
                      // disable="true"
                    />
                    <MrfPartialStatus
                      mrfId={getReqId}
                      mrfStatusId={5}
                      label={"Received COO approval"}
                      message={"Do you want to submit it for COO approval?"}
                    />

                    <MrfPartialStatus
                      mrfId={getReqId}
                      mrfStatusId={7}
                      label={"On Hold"}
                      message={"Do you want to hold on this MRF?"}
                    />
                  </>
                );
              case MRF_STATUS.cooapproval:
                return (
                  <>
                    <ButtonC
                      label="CANCEL"
                      className=" w-2 border-red-600 text-red-600"
                      onClick={handleCancel}
                      outlined="true"
                      // disable="true"
                    />
                    <MrfPartialStatus
                      mrfId={getReqId}
                      mrfStatusId={6}
                      label={"Open"}
                      message={"Do you want to change the status to Open?"}
                    />
                    <MrfPartialStatus
                      mrfId={getReqId}
                      mrfStatusId={7}
                      label={"On Hold"}
                      message={"Do you want to hold on this MRF?"}
                    />
                  </>
                );
              case MRF_STATUS.resubReq:
                return (
                  <>
                    <ButtonC
                      label="CANCEL"
                      className=" w-2 border-red-600 text-red-600"
                      onClick={handleCancel}
                      outlined="true"
                      // disable="true"
                    />
                  </>
                );
              case MRF_STATUS.rejected:
                return (
                  <>
                    <ButtonC
                      label="CANCEL"
                      className=" w-2 border-red-600 text-red-600"
                      onClick={handleCancel}
                      outlined="true"
                      // disable="true"
                    />
                  </>
                );
              case MRF_STATUS.awaitHodApproval:
                return (
                  <>
                    <ButtonC
                      label="CANCEL"
                      className=" w-2 border-red-600 text-red-600"
                      onClick={handleCancel}
                      outlined="true"
                      // disable="true"
                    />
                    <MrfPartialStatus
                      mrfId={getReqId}
                      mrfStatusId={12}
                      label={"Received HOD approval"}
                      message={"“Do you want to submit it for COO approval?"}
                    />
                  </>
                );
              case MRF_STATUS.awaitCooApproval:
                return (
                  <>
                    <ButtonC
                      label="CANCEL"
                      className=" w-2 border-red-600 text-red-600"
                      onClick={handleCancel}
                      outlined="true"
                      // disable="true"
                    />
                    <MrfPartialStatus
                      mrfId={getReqId}
                      mrfStatusId={6}
                      label={"Open"}
                      message={"“Do you want to Open this MRF?"}
                    />
                  </>
                );
              case MRF_STATUS.open:
                return (
                  <>
                    <ButtonC
                      label="CANCEL"
                      className=" w-2 border-red-600 text-red-600"
                      onClick={handleCancel}
                      outlined="true"
                      // disable="true"
                    />
                    <ButtonC
                      label="Add Resume"
                      className="w-2 bg-red-600 border-red-600"
                      onClick={() => {
                        dispatch(
                          PAGE_ACTIONS.setParams({
                            params: {
                              mrfId: getReqId,
                              referenceNo: formData.referenceNo,
                            },
                          })
                        );
                        navigateTo("add_candidate");
                      }}
                      // disable="true"
                    />
                  </>
                );
            }
          } else {
            return (
              <>
                <ButtonC
                  label="CANCEL"
                  className=" w-2 border-red-600 text-red-600"
                  onClick={handleCancel}
                  outlined="true"
                  // disable="true"
                />
                <MrfPartialStatus
                  mrfId={getReqId}
                  mrfStatusId={1}
                  label={"SAVE AS DRAFT"}
                  message={"Do you want to Submit this MRF as Draft?"}
                  formData={formData}
                />

                <MrfPartialStatus
                  mrfId={getReqId}
                  mrfStatusId={2}
                  label={"Submit"}
                  message={"Do you want to Submit this MRF?"}
                  formData={formData}
                />
              </>
            );
          }
        })()}

        <ToastMessages ref={toastRef} />
      </div>
    </div>
  );
};

export default CreateRequisitionBody;
