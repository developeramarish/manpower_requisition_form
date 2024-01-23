import React, { useRef, useState } from "react";
import ToastMessages from "./ToastMessages";

const SingleFileUpload = ({ onChange, fileExtension, disable }) => {
  const fileInputRef = useRef(null);
  const toastRef = useRef(null);
  

  const onFileSelect = (event) => {
    let selectedFile =
      event.target.files && event.target.files.length > 0
        ? event.target.files[0]
        : null;

    if (selectedFile) {
      const allowedExtension = fileExtension
        .split(",")
        .map((ext) => ext.trim().toLowerCase());

      const selectedfileExtension = selectedFile.name
        .split(".")
        .pop()
        .toLowerCase();

      const fileExtensionIsValid = allowedExtension.includes(
        selectedfileExtension
      );

      if (fileExtensionIsValid) {
        // Call the parent component's onChange callback with the selected file
        onChange(selectedFile);
      } else {
        toastRef.current.showWarrningMessage(
          `Please select a file  with  follwing extension ${fileExtension}`
        );
        if (fileInputRef.current) {
          fileInputRef.current.value = null;
        }
      }
    } else {
      toastRef.current.showWarrningMessage(`Please select a file.`);
      if (fileInputRef.current) {
        fileInputRef.current.value = null;
      }
    }
  };
  // const clearFileInput = () => {
  //       // Clear the file input
  //       if (fileInputRef.current) {
  //         fileInputRef.current.value = null;
  //       }
  //       selectedFile=null;
  //     };
    
  //     const handleReset = () => {
  //       clearFileInput();
  //     };

  return (
    <div>
      <input
        ref={fileInputRef}
        type="file"
        accept={fileExtension}
        onChange={onFileSelect}
        disabled={disable}
        style={{
          // Add your CSS styles here
          padding: '10px',
          border: '1px solid #ccc',
          borderRadius: '5px',
          width: '400px',
          backgroundColor: 'var(--gray-100)',
          color: '#495057',
          fontweight: '400',
          fontsize: '1rem',
          // Add more styles as needed
        }}
      /> 
      {/* <button onClick={handleReset} disabled={disable}>
            X
             </button> */}
      <ToastMessages ref={toastRef} />
    </div>
  );
};

export default SingleFileUpload;

