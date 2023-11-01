// InputTextareaComponent.js
import React from 'react';
import { InputTextarea } from 'primereact/inputtextarea';

const InputTextareaComponent = ({ value, onChange }) => {
  return (
    <InputTextarea rows={5} value={value} onChange={onChange} autoResize={true} />
  );
};

export default InputTextareaComponent;
