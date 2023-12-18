// Textbox.js
import React from "react";
import { InputText } from "primereact/inputtext";
const InputTextCp = ({ value, onChange, className,placeholder }) => {
  return (
    <InputText
      type="text"
      value={value}
      onChange={onChange}
      placeholder={placeholder}
      className={`bg-gray-100 ${className}`}
    />
  );
};

export default InputTextCp;
