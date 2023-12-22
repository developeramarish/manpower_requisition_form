import React, { useEffect, useState } from 'react';
import { Checkbox } from 'primereact/checkbox';
import { InputText } from 'primereact/inputtext';
import '../styles/layout/OpenStatus.css'; // Import your CSS file
import DashboardHeader from './Header';
import ButtonC from './../components/Button';
// import { useNavigate, useParams } from 'react-router-dom';
 
const ReceivedStatus = ({reqId}) => {
  // const { reqId } =   useParams();
  // const navigate = useNavigate();
  const [value, setValue] = useState('');
  const [firstChecked, setFirstChecked] = useState(false);

  const [secondChecked, setSecondChecked] = useState(false);

  const [thirdChecked, setThirdChecked] = useState(false);

  const [showTextBox, setShowTextBox] = useState(false);
 
  const handleFirstCheck = () => {

    setFirstChecked(true);

    setSecondChecked(false);

    setThirdChecked(false);

    setShowTextBox(true);

  };
 
  const handleSecondCheck = () => {

    setFirstChecked(false);

    setSecondChecked(true);

    setThirdChecked(false);

    setShowTextBox(false);

  };
 
  const handleThirdCheck = () => {

    setFirstChecked(false);

    setSecondChecked(false);

    setThirdChecked(true);

    setShowTextBox(false);

  };
 
  const handleCancel = () => {

    setFirstChecked(false);

    setSecondChecked(false);

    setThirdChecked(false);

    setShowTextBox(false);

  };
  
  const handleSave = () => {
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
      "roleId": 0,
      "note":"string"
    };
    console.log("Note",value);
  if(firstChecked){
    empdata.mrfStatusId=4;
    empdata.note= value;
  }
  if(secondChecked){
    empdata.mrfStatusId=6;
  }
  if(thirdChecked){
    empdata.mrfStatusId=10;
  }

  //hod approval=5,onhold=10
      fetch("https://localhost:7128/api/Mrfdetail/PartialUpdateMRFStatus/"+reqId, {
        method: "Put",
        headers: { "content-type": "application/json" },
        body: JSON.stringify(empdata)
         
      }).then((res) => {
        alert('updated successfully.')
        setTimeout(() => {
              // navigate("/MyRequisitions");
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
        <label htmlFor="first">Is Resubmission Required?</label>

        <Checkbox

          inputId="first"

          onChange={handleFirstCheck}

          checked={firstChecked}

         // disabled={secondChecked || thirdChecked}

        />
          {showTextBox && (

<div className="textbox-container">
<InputText value={value} onChange={(e) => setValue(e.target.value)} />

</div>

)}

      </div>

      <div>
        <label htmlFor="second">Do You Want to Submit it for HOD approval?</label>
        
        <Checkbox

          inputId="second"

          onChange={handleSecondCheck}

          checked={secondChecked}

         // disabled={firstChecked || thirdChecked}

        />

      </div>

      <div>

        <label htmlFor="third">Do You Want to Hold on this MRF?</label>
        
        <Checkbox

          inputId="third"

          onChange={handleThirdCheck}

          checked={thirdChecked}

          //disabled={firstChecked || secondChecked}

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
 
export default ReceivedStatus;
