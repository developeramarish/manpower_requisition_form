// InputTextareaComponent.js
import React from "react";
import { InputTextarea } from "primereact/inputtextarea";

const InputTextareaComponent = ({ value, onChange, rows = 5, cols }) => {
  return (
    <InputTextarea
      rows={rows}
      cols={cols}
      value={value}
      onChange={onChange}
      autoResize={true}
    />
  );
};

export default InputTextareaComponent;
