// Textbox.js
import React from "react";
import { InputText } from "primereact/inputtext";
const InputTextCp = ({ value, onChange, className,placeholder,disable,onBlur }) => {
  return (
    <InputText
      type="text"
      value={value}
      onChange={onChange}
      placeholder={placeholder}
      className={`bg-gray-100 ${className}`}
      disabled ={disable}
      onBlur={onBlur}
    />
  );
};

export default InputTextCp;
