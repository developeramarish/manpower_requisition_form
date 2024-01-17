import { Dialog } from "primereact/dialog";
import React, { useRef, useState } from "react";
import ButtonC from "./Button";
import { storageService } from "../constants/storage";
import SingleFileUpload from "./FileUpload";
import { API_URL } from "../constants/config";
import { navigateTo, postData } from "../constants/Utils";
import ToastMessages from "./../components/ToastMessages";
import { removeSpaces } from "./constant";
const AssignmentUpload = ({ visible, data, onHide }) => {
  const [selectedFile, setSelectedFile] = useState(null);
  const toastRef = useRef(null);
  const handleFileChange = (event) => {
    setSelectedFile(event);
  };


  const handleSubmit = async () => {
    const fileUploadData = new FormData();
    fileUploadData.append("file", selectedFile);

    const fileName = removeSpaces(data.candidateName) + "_assign";
    const interviewEvaluationIDD = data.interviewevaluationId;
    
    try {
      const fileUploadResponse = await fetch(
        API_URL.ASSIGNMENT_UPLOAD + fileName,
        {
          method: "POST",
          body: fileUploadData,
        }
      );
      if (fileUploadResponse.ok) {
        const uploadData = {
          id: 0,
          interviewEvaluationId: interviewEvaluationIDD,
          filePath: fileName + ".pdf",
          createdByEmployeeId: storageService.getData("profile").employeeId,
          createdOnUtc: new Date().toISOString(),
          updatedByEmployeeId: storageService.getData("profile").employeeId,
          updatedOnUtc: new Date().toISOString(),
        };

        console.log(uploadData);
        try {
          const response = await postData(
            `${API_URL.ASSIGNMENT_POST}`,
            uploadData
          );
          if (response.ok) {
            const responseData = await response.json();
            console.log("Response Data:", responseData);
            toastRef.current.showSuccessMessage(
              "Assignment submitted successfully!"
            );
            
          } else {
            console.error("Request failed with status:", response.status);
            if (response.status === 400) {
              toastRef.current.showBadRequestMessage(
                "Bad request: " + response.url
              );
            }
          }
        } catch (error) {
          console.error("Error:", error);
        }
      } else {
        if (fileUploadResponse.status === 400) {
          toastRef.current.showBadRequestMessage(
            "you have to upload Assignment!"
          );
        }
        console.error("Request failed with status:", fileUploadResponse.status);
      }
    } catch (error) {
      console.error("Error:", error);
    }
  };

  return (
    <>
      <Dialog header={"Upload Assignment"} visible={visible} onHide={onHide}>
       

        <div className="my-4">
          <SingleFileUpload onChange={handleFileChange} />
        </div>

        <ButtonC
          label={"Submit"}
          className={"update_btn"}
          onClick={() => handleSubmit()}
        />
      </Dialog>
      <ToastMessages ref={toastRef} />
    </>
  );
};

export default AssignmentUpload;
