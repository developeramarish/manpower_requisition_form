import React, { useState } from 'react';

const FileUpload = () => {
  const [selectedFile, setSelectedFile] = useState(null);
  const [resumeOrAssign, setResumeOrAssign] = useState('');

  const handleFileChange = (event) => {
    setSelectedFile(event.target.files[0]);
  };

  const handleResumeOrAssignChange = (event) => {
    setResumeOrAssign(event.target.value);
  };

  const handleFileUpload = () => {
    if (selectedFile) {
      const formData = new FormData();
      formData.append('file', selectedFile);

      // Replace 'YOUR_UPLOAD_API_ENDPOINT' with your actual API endpoint
      fetch('https://localhost:7128/api/Upload?ResumeOrAssign=' + resumeOrAssign, {
        method: 'POST',
        body: formData,
      })
        .then(response => response.json())
        .then(data => {
          console.log('File uploaded successfully:', data);
          // Clear the selected file after upload
          setSelectedFile(null);
        })
        .catch(error => {
          console.error('Error uploading file:', error);
        });
    } else {
      console.error('No file selected');
    }
  };

  return (
    <div>
      <input type="file" onChange={handleFileChange} />
      <input type="text" placeholder="ResumeOrAssign" onChange={handleResumeOrAssignChange} value={resumeOrAssign} />
      <button onClick={handleFileUpload}>Upload</button>
    </div>
  );
};

export default FileUpload;
