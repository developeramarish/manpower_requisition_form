// InputTextareaComponent.js
import React from "react";
import { InputTextarea } from "primereact/inputtextarea";

const InputTextareaComponent = ({
  value,
  onChange,
  rows = 5,
  cols,
  autoResize = true,
}) => {
  return (
    <InputTextarea
      rows={rows}
      cols={cols}
      value={value}
      onChange={onChange}
      autoResize={autoResize}
    />
  );
};

export default InputTextareaComponent;
