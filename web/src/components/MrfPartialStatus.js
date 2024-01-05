import React, { useEffect, useState, useRef } from "react";
import { API_URL, MRF_STATUS, REQUISITION_TYPE,isFormDataEmptyForSaveasDraft,
isFormDataEmptyForSubmit } from "../constants/config";
import { storageService } from "../constants/storage";
import { formatDateToYYYYMMDD, navigateTo } from "../constants/Utils";
import { Dialog } from "primereact/dialog";
import ButtonC from "./Button";
import InputTextareaComponent from "./InputTextarea";
import ToastMessages from "./ToastMessages";

const MrfPartialStatus = ({
  mrfId = null,
  mrfStatusId = null,
  label = null,
  message = null,
  textbox = false,
  header = null,
  formData = {},
  disabled = null,
  updatedClick = null,
  roleID = null,
}) => {
  const [visible, setVisible] = useState(false);
  const [note, setNote] = useState("");
  const toastRef = useRef(null);
  const [isLoading, setIsLoading] = useState(false);

  const strToArray = (s) => {
    s = s ?? "";
    if (s !== "" && typeof s === "string") {
      s = s.split(",").map(Number);
    }
    return s;
  };

  const footerContent = (value) => {
    return (
      <div>
        {(roleID == 3 && mrfStatusId == MRF_STATUS.submToHr) ||
        mrfStatusId == MRF_STATUS.draft ? (
          <ButtonC
            label="Yes"
            className="w-2 bg-red-600 border-red-600 p-2 mr-3"
            onClick={() => {
              handleSubmit(value);
              setVisible(false);
            }}
          />
        ) : (
          <ButtonC
            label="Yes"
            className="w-2 bg-red-600 border-red-600 p-2 mr-3"
            onClick={() => {
              submitPartial(value);
              setVisible(false);
            }}
          />
        )}

        <ButtonC
          label="No"
          className=" w-2 bg-red-600 border-red-600 p-2 "
          onClick={() => {
            setVisible(false);
          }}
        />
      </div>
    );
  };

  
  const formatAndShowErrorMessage = (emptyFields) => {
    const formattedEmptyFields = emptyFields.map(field => field.replace(/Id$/, ''));
    const errorMessage = `Some required fields are empty: ${formattedEmptyFields.join(', ')}`;
    toastRef.current.showBadRequestMessage(errorMessage);
  };

  const handleSubmit = async (mrfStatusId) => {
    if (mrfStatusId==2 && isFormDataEmptyForSubmit(formData).length > 0) {
      const emptyFields = isFormDataEmptyForSubmit(formData);
      formatAndShowErrorMessage(emptyFields);
      
      
    } else if(mrfStatusId==1 && isFormDataEmptyForSaveasDraft(formData).length > 0){
      const emptyFields = isFormDataEmptyForSaveasDraft(formData);
      formatAndShowErrorMessage(emptyFields);
      
    }
    
    else {
      console.log("Form data is valid. Submitting...");
      
    setIsLoading(true);
    const data = {
      referenceNo: formData.referenceNo,
      requisitionType:
        formData.requisitionType == ""
          ? REQUISITION_TYPE[0].code
          : formData.requisitionType,
      positionTitleId: formData.positionTitleId,
      departmentId: formData.departmentId,
      subDepartmentId: formData.subDepartmentId,
      projectId: formData.projectId,
      vacancyNo: Number(formData.vacancyNo),
      genderId: formData.genderId,
      qualification: formData.qualification,
      // requisitionDateUtc: formData.requisitionDateUtc.toISOString().slice(0,10),
      requisitionDateUtc: formatDateToYYYYMMDD(formData.requisitionDateUtc),
      reportsToEmployeeId: formData.reportsToEmployeeId,
      minGradeId: formData.minGradeId,
      maxGradeId: formData.maxGradeId,
      employmentTypeId: formData.employmentTypeId,
      minExperience: formData.minExperience,
      maxExperience: formData.maxExperience,
      vacancyTypeId: formData.vacancyTypeId,
      isReplacement: formData.isReplacement,
      mrfStatusId: mrfStatusId,
      jdDocPath: "string",
      locationId: formData.locationId,
      qualificationId: formData.qualificationId,
      createdByEmployeeId: storageService.getData("profile").employeeId,
      createdOnUtc: new Date().toISOString(),
      updatedByEmployeeId: storageService.getData("profile").employeeId,
      updatedOnUtc: new Date().toISOString(),
      justification: formData.justification,
      jobDescription: formData.jobDescription,
      skills: formData.skills,
      minTargetSalary: formData.minTargetSalary,
      maxTargetSalary: formData.maxTargetSalary,
      employeeName: formData.employeeName,
      emailId: formData.emailId,
      note: formData.note,
      employeeCode: formData.employeeCode != "" ? formData.employeeCode : 0,
      lastWorkingDate: formatDateToYYYYMMDD(formData.lastWorkingDate),
      // lastWorkingDate:formData.lastWorkingDate !="" ?  formatDateToYYYYMMDD(formData.lastWorkingDate): new Date().toISOString().slice(0,10),
      annualCtc: formData.annualCtc,
      annualGross: formData.annualGross,
      replaceJustification: formData.replaceJustification,
      resumeReviewerEmployeeIds: strToArray(
        formData.resumeReviewerEmployeeIds
      ).toString(),
      interviewerEmployeeIds: strToArray(
        formData.interviewerEmployeeIds
      ).toString(),
      hiringManagerId: formData.hiringManagerId,
      hiringManagerEmpId: formData.hiringManagerEmpId,
      functionHeadId: formData.functionHeadId,
      functionHeadEmpId: formData.functionHeadEmpId,
      siteHRSPOCId: formData.siteHRSPOCId,
      siteHRSPOCEmpId: formData.siteHRSPOCEmpId,
      financeHeadId: formData.financeHeadId,
      financeHeadEmpId: formData.financeHeadEmpId,
      presidentnCOOId: formData.presidentnCOOId,
      presidentnCOOEmpId: formData.presidentnCOOEmpId,
    };
    console.log(data);
    try {
      const response = await fetch(API_URL.POST_CREATE_REQUISITION, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(data),
      });

      if (response.ok) {
        const responseData = await response.json();
        console.log("Response Data:", responseData);
        if (responseData.statusCode === 409) {
          toastRef.current.showConflictMessage(responseData.message);
        } else {
          if (mrfStatusId == 1) {
            toastRef.current.showSuccessMessage(
              "The MRF has been saved as Draft!"
            );
          } else {
            toastRef.current.showSuccessMessage("Form submitted successfully!");
          }
          setTimeout(() => {
            navigateTo("my_requisition");
          }, 1000);
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
  }
  };


// const financeHead=()=>{
//   console.log("from finanace head functon")
//   formData.presidentnCOOId=0;
//   formData.presidentnCOOEmpId=0;
// }
// const coo=()=>{
//   console.log("from coo functon")
//   formData.financeHeadId=0;
//   formData.financeHeadEmpId=0;
// }


  const submitPartial = async () => {


// {financeHeadClick && (financeHead())}
// {cooClick && (coo())}

console.log(formData)
    const updattingHiringMangerandSiteHR = {
      mrfStatusId: mrfStatusId,
      note: note || null,
      updatedByEmployeeId: storageService.getData("profile").employeeId,
      updatedOnUtc: new Date().toISOString(),
      hiringManagerId: formData.hiringManagerId,
      hiringManagerEmpId: formData.hiringManagerEmpId,
      siteHRSPOCId: formData.siteHRSPOCId,
      siteHRSPOCEmpId: formData.siteHRSPOCEmpId,
      hmApprovalDate: formatDateToYYYYMMDD(formData.hmApprovalDate),
      spApprovalDate: formatDateToYYYYMMDD(formData.spApprovalDate),
    };

    const partialStatus = {
      mrfStatusId: mrfStatusId,
      note: note || null,
      updatedByEmployeeId: storageService.getData("profile").employeeId,
      updatedOnUtc: new Date().toISOString(),
      functionHeadId: formData.functionHeadId,
      functionHeadEmpId: formData.functionHeadEmpId,
      financeHeadId: formData.financeHeadId,
      financeHeadEmpId: formData.financeHeadEmpId,
      presidentnCOOId: formData.presidentnCOOId,
      presidentnCOOEmpId: formData.presidentnCOOEmpId,
      pcApprovalDate: formatDateToYYYYMMDD(formData.pcApprovalDate),
      fhApprovalDate: formatDateToYYYYMMDD(formData.fhApprovalDate),
      fiApprovalDate: formatDateToYYYYMMDD(formData.fiApprovalDate),
    };

    try {
      let response;
      if (updatedClick) {
        response = await fetch(API_URL.MRF_PARTIAL_STATUS_UPDATE + mrfId, {
          method: "Put",
          headers: { "content-type": "application/json" },
          body: JSON.stringify(updattingHiringMangerandSiteHR),
        });
      } else {
        response = await fetch(API_URL.MRF_PARTIAL_STATUS_UPDATE + mrfId, {
          method: "Put",
          headers: { "content-type": "application/json" },
          body: JSON.stringify(partialStatus),
        });
      }

      if (response.ok) {
        const responseData = await response.json();
        if (responseData.statusCode === 409) {
          toastRef.current.showConflictMessage(responseData.message);
        } else {
          toastRef.current.showSuccessMessage("Action Submitted");

          if (updatedClick) {
            // window.location.reload();
          } else {
            setTimeout(() => {
              navigateTo("my_requisition");
            }, 1000);
          }
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
    }
  };

  return (
    <>
      {/* {popupmessage &&(
        <>
         <Dialog dismissableMask >{popupmessage}</Dialog>
         </>
       )} */}

      {((roleID == 3 && mrfStatusId == MRF_STATUS.submToHr) ||
        mrfStatusId == MRF_STATUS.draft) && (
        <Dialog
          className="w-3 "
          visible={visible}
          header={header}
          draggable={false}
          onHide={() => setVisible(false)}
          footer={footerContent(mrfStatusId)}
        >
          {message && <h3>{message}</h3>}
        </Dialog>
      )}

      {label && (
        <>
          <ButtonC
            label={label}
            className="w-2 bg-red-600 border-red-600"
            onClick={() => setVisible(true)}
            disable={disabled}
          ></ButtonC>

          <Dialog
            className="w-3 "
            visible={visible}
            header={header}
            draggable={false}
            onHide={() => setVisible(false)}
            footer={footerContent(mrfStatusId)}
          >
            {textbox && (
              <div>
                <label className="font-bold text-sm">Add Note:</label>
                <br />
                <InputTextareaComponent
                  value={note}
                  onChange={(e) => setNote(e.target.value)}
                  rows={3}
                  cols={35}
                  className="bg-gray-100"
                />
              </div>
            )}

            {message && <h3>{message}</h3>}
          </Dialog>
        </>
      )}
      <ToastMessages ref={toastRef} />
    </>
  );
};

export default MrfPartialStatus;
