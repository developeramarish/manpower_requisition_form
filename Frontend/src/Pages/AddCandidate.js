import React, { useState, useEffect, useRef } from "react";
import DashboardHeader from "./Header";
import LeftPanel from "./LeftPanel";
import InputTextCp from "../Components/Textbox";
import ButtonC from "../Components/Button";
import ToastMessages from "../Components/ToastMessages";
import SingleFileUpload from "../Components/FileUpload";
import {APIPath,removeSpaces} from "../Components/constant";
import { useNavigate } from "react-router-dom";
const AddCandidate = () => {
  const toastRef = useRef(null);
  const [selectedFile, setSelectedFile] = useState(null);
  const navigate = useNavigate();
  const handleFileChange = (event) => {
    setSelectedFile(event);
  };
  
  const formSchema = {
    id: 0,
  mrfId: 2,
  name: "",
  emailId: "",
  contactNo: "",
  resumePath: "",
  candidateStatusId: 1,
  reviewedByEmployeeId: 1,
  createdByEmployeeId: 1,
  createdOnUtc: "",
  updatedByEmployeeId: 1,
  updatedOnUtc: "",
  reason: ""
  };

  // Initialize the formData state using the form schema
  const [formData, setFormData] = useState(formSchema);
 
  const handleSubmit = async () => {
  
    const fileUploadData = new FormData();
    fileUploadData.append('file', selectedFile);
   
    try {
        const fileUploadResponse = await fetch(APIPath+'Upload?ResumeOrAssign=Resume&FileName='+removeSpaces(formData.name), {
        method: 'POST',
        body: fileUploadData,
      });

      if (fileUploadResponse.ok) {
        const data = {
          id:0, 
      mrfId: formData.mrfId,
      name: formData.name,
      emailId: formData.emailId,
      contactNo: formData.contactNo,
      resumePath: removeSpaces(formData.name)+".pdf",
      candidateStatusId: 1,
     // resumePath: "ddd.pdf",
      reviewedByEmployeeId: 1,
      reviewedByEmployeeIds: "",
      createdByEmployeeId: 1,
      createdOnUtc: new Date().toISOString(),
      updatedByEmployeeId: 1,
      updatedOnUtc: new Date().toISOString(),
      reason: ""
      
     
    };
    try {
        const response = await fetch(
            APIPath+"Candidatedetail/Post",
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
        

          toastRef.current.showSuccessMessage("Form submitted successfully!");
          setTimeout(() => {
            navigate("/Candidate");
          }, 2000);
        
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
      
    }
} else {
    // Handle error
  }
} catch (error) {
  console.error('Error:', error);
}
  };


  //need to change this
  const handleCancel = () => {
    
  };

  return (
    <div>
    <DashboardHeader />
    <div className="flex bg-gray-200">
      <LeftPanel />
      <div className="flex flex-column gap-2 w-full p-3 py-2 h-full ">
        
       
    <div
      className="border-round-lg bg-white text-black-alpha-90 p-3 flex flex-column justify-content-between"
      style={{ height: "81vh" }}
    >
      <h3 className="text-xl my-2">Fill the Details</h3>
      <section
        className="flex flex-column flex-nowrap gap-3 border-y-2 border-gray-300 py-3 px-1 overflow-y-scroll"
        style={{ height: "95%" }}
      >
       
        <div className="flex justify-content-between gap-5">
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="name" className="font-bold text-sm">
              Name
            </label>
            <InputTextCp
              id="name"
              onChange={(e) =>
                setFormData({ ...formData, name: e.target.value })
              }
              value={formData.name}
            />
          </div>
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="email" className="font-bold text-sm">
              Email
            </label>
            <InputTextCp
              id="email"
              onChange={(e) =>
                setFormData({ ...formData, emailId: e.target.value })
              }
              value={formData.emailId}
            />
          </div>
        </div>

        <div className="flex justify-content-between gap-5">
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="contact" className="font-bold text-sm">
              Contact
            </label>
            <InputTextCp
              id="contact"
              onChange={(e) =>
                setFormData({ ...formData, contactNo: e.target.value })
              }
              value={formData.contactNo}
            />
          </div>
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="resume" className="font-bold text-sm">
              Resume
            </label>
            <SingleFileUpload onChange={handleFileChange} />
     
          </div>
        </div>
        
       
     
      </section>

      <div className="flex flex-wrap justify-content-end gap-5 mt-3">
        <ButtonC
          label="CANCEL"
          outlined
          className="mr-auto w-2 border-red-600 text-red-600"
          onClick={handleCancel}
        />
        
        <ButtonC
          label="SUBMIT"
          className="w-2 bg-red-600 border-red-600"
          onClick={() => handleSubmit()}
        />
        <ToastMessages ref={toastRef} />
      </div>
    </div>
    </div>
    </div></div>
  );
};

export default AddCandidate;
