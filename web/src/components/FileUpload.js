import React, { useRef } from 'react';

const SingleFileUpload = ({ onChange }) => {
  const fileInputRef = useRef(null);

  const onFileSelect = (event) => {
    const selectedFile = event.target.files && event.target.files.length > 0 ? event.target.files[0] : null;

    if (selectedFile && selectedFile.type === 'application/pdf') {
      // Call the parent component's onChange callback with the selected file
      onChange(selectedFile);
    } else {
      alert('Please select a PDF file.');
      // Clear the file input
      if (fileInputRef.current) {
        fileInputRef.current.value = null;
      }
    }
  };

  return (
    <div>
      <input
        ref={fileInputRef}
        type="file"
        accept=".pdf"
        onChange={onFileSelect}
      />
    </div>
  );
};

export default SingleFileUpload;
