import React, { useState, useEffect, useRef } from "react";
import InputTextCp from "./../components/Textbox";
import ButtonC from "./../components/Button";
import ToastMessages from "./../components/ToastMessages";
import SingleFileUpload from "./../components/FileUpload";
import { removeSpaces } from "./../components/constant";
import { storageService } from "../constants/storage";
import { navigateTo } from "../constants/Utils";
import { API_URL } from "../constants/config";
import {FILE_URL} from "../constants/config";
import DropdownComponent from "../components/Dropdown";
const AddCandidate = (reqId) => {
  const toastRef = useRef(null);
  const [selectedFile, setSelectedFile] = useState(null);
  const handleFileChange = (event) => {
    setSelectedFile(event);
  };
 // console.log(referenceNo);
  const formSchema = {
    id: 0,
    mrfId: reqId.reqId,
    name: "",
    emailId: "",
    contactNo: "",
    resumePath: "",
    candidateStatusId: 1,
    createdByEmployeeId: storageService.getData("profile").employeeId,
    createdOnUtc: new Date().toISOString(),
    updatedByEmployeeId: storageService.getData("profile").employeeId,
    updatedOnUtc: new Date().toISOString(),
    reason: "",
    sourceId: 0,
  };

  // Initialize the formData state using the form schema
  const [formData, setFormData] = useState(formSchema);
  const[dropdowns,setDropdownData]=useState();
  useEffect(() => {
    // Fetch the data for all the dropdowns
    fetch(API_URL.ADD_SOURCE_NAME)
      .then((response) => response.json())
      .then((data) => {
        
          console.log(data.result);
        // Store the dropdown data in localStorage using your storageService
        // storageService.set("dropdownData", dropdown);
        // Update the state with the new dropdown data
       setDropdownData(data.result);
      })
      .catch((error) => {
        console.error("Error fetching data:", error);
      });
    }, []);
    

  const handleSubmit = async () => {
    const fileUploadData = new FormData();
    fileUploadData.append("file", selectedFile);

    try {
      const fileUploadResponse = await fetch(FILE_URL.RESUME_UPLOAD +
          removeSpaces(formData.name),
        {
          method: "POST",
          body: fileUploadData,
        }
      );

      if (fileUploadResponse.ok) {
        const data = {
          id: 0,
          mrfId: formData.mrfId,
          name: formData.name,
          emailId: formData.emailId,
          contactNo: formData.contactNo,
          resumePath: removeSpaces(formData.name) + ".pdf",
          candidateStatusId: 1,
          reviewedByEmployeeIds: "",
          createdByEmployeeId:  formData.createdByEmployeeId,
          createdOnUtc:formData.createdOnUtc,
          updatedByEmployeeId: formData.updatedByEmployeeId,
          updatedOnUtc:formData.updatedOnUtc,
          reason: "",
          sourceId:formData.sourceId,
        };
        try {
          const response = await fetch(API_URL.ADD_CANDIDATE, {
            method: "POST",
            headers: {
              "Content-Type": "application/json",
            },
            body: JSON.stringify(data),
          });
          if (response.ok) {
            const responseData = await response.json();
            console.log("Response Data:", responseData);
            toastRef.current.showSuccessMessage("Form submitted successfully!");
            setTimeout(() => {
               navigateTo("my_requisition");
            }, 2000);
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
          toastRef.current.showBadRequestMessage("you have to upload Resume!");
        }
        console.error("Request failed with status:", fileUploadResponse.status);
      }
    } catch (error) {
      console.error("Error:", error);
    }
  };

  //need to change this
  const handleCancel = () => {
    navigateTo("my_requisition")
  };

  return (
    <div>
      <div className="flex bg-gray-200">
        <div className="flex flex-column gap-2 w-full p-3 py-2 h-full ">
          <div
            className="border-round-lg bg-white text-black-alpha-90 p-3 flex flex-column justify-content-between"
            style={{ height: "81vh" }}
          >
            <h3 className="text-xl my-2">Fill the Details : 
            
            </h3>
            <section
              className="flex flex-column flex-nowrap gap-3 border-y-2 border-gray-300 py-3 px-1 overflow-y-scroll"
              style={{ height: "95%" }}
            >
              <h4 className="text-xl my-2">Reference Number :
              <span className="text-red-600"> {reqId.referenceNo}</span>
              </h4>
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
                  <label htmlFor="contact" className="font-bold text-sm">
                     Source Name
                  </label>
                  <DropdownComponent
              optionLabel="name"
              optionValue="id"
              type="source"
              options={dropdowns}
              value={formData.sourceId}
              onChange={(e) => {
                setFormData({ ...formData, sourceId: e.target.value });
              }}
            />
                </div>
                </div>
          
                <div className="flex flex-column w-6 gap-2">
                  <label htmlFor="resume" className="font-bold text-sm">
                    Resume
                  </label>
                  <SingleFileUpload onChange={handleFileChange} />
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
      </div>
    </div>
  );
};

export default AddCandidate;
