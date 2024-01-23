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
    </div>
  );
};

export default SingleFileUpload;
