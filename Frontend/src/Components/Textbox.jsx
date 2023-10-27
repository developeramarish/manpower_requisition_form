// Textbox.js
import React from 'react';
import { InputText } from "primereact/inputtext";
const InputTextCp = ({ value, onChange }) => {
  return <InputText type="text" value={value} onChange={onChange} className="bg-gray-100"/>;
};

export default InputTextCp;
