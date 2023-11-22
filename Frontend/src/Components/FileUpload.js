import React, { useRef } from 'react';
import { FileUpload } from 'primereact/fileupload';

const SingleFileUpload = ({ onChange }) => {
  const fileInputRef = useRef(null);

  const onChoose = () => {
    fileInputRef.current.choose();
  };

  const onFileSelect = (event) => {
    // Handle the selected file
    const selectedFile = event.files && event.files.length > 0 ? event.files[0] : null;
    if (selectedFile) {
      // Call the parent component's onChange callback with the selected file
      onChange(selectedFile);
    }
  };

  return (
    <div>
      <FileUpload
        ref={fileInputRef}
        mode="basic"
        accept="image/*"  // Set the accepted file types
        customUpload // Use a custom upload handler
        onChoose={onChoose}
        uploadHandler={(event) => {
          // Custom upload logic if needed
          console.log('Custom Upload Handler:', event.files);
        }}
        onSelect={onFileSelect}
      />
    </div>
  );
};

export default SingleFileUpload;
