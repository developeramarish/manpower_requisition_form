import React, { useEffect, useState, useRef } from "react";
import { API_URL, MRF_STATUS } from "../constants/config";
import { storageService } from "../constants/storage";
import { navigateTo } from "../constants/Utils";
import { Dialog } from "primereact/dialog";
import ButtonC from "./Button";
import InputTextCp from "./Textbox";
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
}) => {
  const [visible, setVisible] = useState(false);
  const [note, setNote] = useState("");
  const toastRef = useRef(null);
  const [isLoading, setIsLoading] = useState(false);

  const strToArray = (s) => {
    s = s ?? "";
    if (typeof s === "string") {
      s = s.split(",").map(Number);
    }
    return s;
  };

  const footerContent = (value) => {
    return (
      <div>
        {(mrfStatusId == MRF_STATUS.submToHr || mrfStatusId == MRF_STATUS.draft) ? (
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

  const handleSubmit = async (mrfStatusId) => {
    setIsLoading(true);

    console.log(formData.jobDescription)
    const data = {
      referenceNo: formData.referenceNo,
      requisitionType: formData.requisitionType,
      positionTitle: formData.positionTitle,
      departmentId: formData.departmentId,
      subDepartmentId: formData.subDepartmentId,
      projectId: formData.projectId,
      vacancyNo: Number(formData.vacancyNo),
      genderId: formData.genderId,
      qualification: formData.qualification,
      requisitionDateUtc: new Date().toISOString().slice(0, 10),
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
      lastWorkingDate: new Date().toISOString().slice(0, 10),
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
          }, 2000);
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

  const submitPartial = async () => {
    const partialStatus = {
      mrfStatusId: mrfStatusId,
      note: note || null,
      updatedByEmployeeId: storageService.getData("profile").employeeId,
      updatedOnUtc: new Date().toISOString(),
    };

    try {
      const response = await fetch(API_URL.MRF_PARTIAL_STATUS_UPDATE + mrfId, {
        method: "Put",
        headers: { "content-type": "application/json" },
        body: JSON.stringify(partialStatus),
      });

      if (response.ok) {
        const responseData = await response.json();
        if (responseData.statusCode === 409) {
          toastRef.current.showConflictMessage(responseData.message);
        } else {
          toastRef.current.showSuccessMessage("Action Submitted");
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
    }
  };


  return (
    <>
      {/* {popupmessage &&(
        <>
         <Dialog dismissableMask >{popupmessage}</Dialog>
         </>
       )} */}

      {(mrfStatusId == MRF_STATUS.submToHr || mrfStatusId == MRF_STATUS.draft) && (
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
