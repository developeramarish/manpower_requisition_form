import { Dialog } from 'primereact/dialog'
import React, { useState } from 'react'
import ButtonC from './Button'
import { storageService } from '../constants/storage'
import SingleFileUpload from './FileUpload'
import { API_URL } from '../constants/config'

const AssignmentUpload = ({visible,data,onHide}) => {
    const [selectedFile, setSelectedFile] = useState(null);
    
    const handleFileChange=(event)=>{
        setSelectedFile(event);
    }
    
    console.log(data)
const formSchema={
    id:0,
    interviewEvaluationId:0,
    filePath:"",
    createdByEmployeeId: storageService.getData("profile").employeeId,
    createdOnUtc: new Date().toISOString(),
    updatedByEmployeeId: storageService.getData("profile").employeeId,
    updatedOnUtc: new Date().toISOString(),

}
const [formData, setFormData] = useState(formSchema);

const handleSubmit=async()=>{
    const fileUploadData = new FormData();
    fileUploadData.append("file", selectedFile);
    console.log(fileUploadData)

    console.log(API_URL.ASSIGNMENT_UPLOAD)
    try {
        const fileUploadResponse = await fetch(
          API_URL.ASSIGNMENT_UPLOAD +"ass",
          {
            method: "POST",
            body: fileUploadData,
          }
        );
if(fileUploadResponse.ok){

}

}catch{

}


}

  return (
    <><Dialog header={"Upload Assignment"} visible={visible} onHide={onHide}>
        
    {/* <h4>Upload Assignment:</h4> */}

<SingleFileUpload onChange={handleFileChange}/>
    

<ButtonC label={"Submit"} className={"update_btn"} onClick={() => handleSubmit()} />

        </Dialog></>
  )
}

export default AssignmentUpload