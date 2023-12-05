import React, { useState } from 'react';

import { Checkbox } from 'primereact/checkbox';

import '../styles/layout/OpenStatus.css'; // Import your CSS file
import DashboardHeader from './Header';
import { useParams } from 'react-router-dom';
import ButtonC from '../Components/Button';
import { useNavigate } from "react-router-dom";
const ReceivedCOOApproval = () => {
  const { reqId } =  useParams();
  const navigate = useNavigate();
 
  const [secondChecked, setSecondChecked] = useState(false);

  const [thirdChecked, setThirdChecked] = useState(false);
 
  const handleSecondCheck = () => {

    setSecondChecked(true);

    setThirdChecked(false);

  };
 
  const handleThirdCheck = () => {
    setSecondChecked(false);

    setThirdChecked(true);
  };
 
  const handleCancel = () => {
    setSecondChecked(false);

    setThirdChecked(false);
  };
  const handleSave = () => {

    // Logic to save data
    const empdata = {
    "requisitionType": "string",
    "referenceNo": "string",
    "positionTitle": "string",
    "departmentId": 0,
    "subDepartmentId": 0,
    "projectId": 0,
    "vacancyNo": 0,
    "genderId": 0,
    "requisitionDateUtc": "2023-12-04",
    "reportsToEmployeeId": 0,
    "minGradeId": 0,
    "maxGradeId": 0,
    "employmentTypeId": 0,
    "minExperience": 0,
    "maxExperience": 0,
    "vacancyTypeId": 0,
    "isReplacement": true,
    "mrfStatusId": 0,
    "jdDocPath": "string",
    "locationId": 0,
    "qualificationId": 0,
    "createdByEmployeeId": 0,
    "createdOnUtc": "2023-12-04T07:59:06.957Z",
    "updatedByEmployeeId": 0,
    "updatedOnUtc": "2023-12-04T07:59:06.957Z",
    "justification": "string",
    "jobDescription": "string",
    "skills": "string",
    "minTargetSalary": 0,
    "maxTargetSalary": 0,
    "employeeName": "string",
    "emailId": "string",
    "employeeCode": 0,
    "lastWorkingDate": "2023-12-04",
    "replaceJustification": "string",
    "annualCtc": 0,
    "annualGross": 0,
    "resumeReviewerEmployeeId": 0,
    "interviewerEmployeeId": 0,
    "resumeReviewerEmployeeIds": "string",
    "interviewerEmployeeIds": "string",
    "hiringManagerId": 0,
    "hiringManagerEmpId": 0,
    "hmApprovalDate": "2023-12-04T07:59:06.957Z",
    "functionHeadId": 0,
    "functionHeadEmpId": 0,
    "fhApprovalDate": "2023-12-04T07:59:06.957Z",
    "siteHRSPOCId": 0,
    "siteHRSPOCEmpId": 0,
    "spApprovalDate": "2023-12-04T07:59:06.957Z",
    "financeHeadId": 0,
    "financeHeadEmpId": 0,
    "fiApprovalDate": "2023-12-04T07:59:06.957Z",
    "presidentnCOOId": 0,
    "presidentnCOOEmpId": 0,
    "pcApprovalDate": "2023-12-04T07:59:06.957Z",
    "roleId": 0};
console.log(secondChecked);
if(secondChecked){
  empdata.mrfStatusId=9;
}
console.log(thirdChecked);
if(thirdChecked){
  empdata.mrfStatusId=10;
}
//open=9,onhold=10
    fetch("https://localhost:7128/api/Mrfdetail/PartialUpdateMRFStatus/"+reqId, {
      method: "Put",
      headers: { "content-type": "application/json" },
      body: JSON.stringify(empdata)
       
    }).then((res) => {
      alert('updated successfully.')
      setTimeout(() => {
            navigate("/MyRequisitions");
          }, 2000);

    }).catch((err) => {
      console.log(err.message)
    })

  };
 
  return (
    <div >
    <DashboardHeader />
    <div className='openstatus'>
    <div className="checkbox-container">
   <div>
        <label htmlFor="first">Do you want to change the status to Open?</label>
         <Checkbox
          inputId="first" 
          onChange={handleSecondCheck}
          checked={secondChecked}
         // disabled={thirdChecked}
          />
      </div>
      <div> 
        <label htmlFor="Second">Do you want to hold on this MRF?</label>
         <Checkbox
           inputId="Second" 
           onChange={handleThirdCheck} 
            checked={thirdChecked}
             //disabled={secondChecked}
              />
           </div>
  <div className="button-container">
      {<ButtonC className="p-button-danger" label="Cancel" onClick={handleCancel}></ButtonC>}
      {<ButtonC label="Save" className="p-button-primary" onClick={handleSave} />}
      </div>

    </div>
    </div>
    </div>
  );

};
 
export default ReceivedCOOApproval;
