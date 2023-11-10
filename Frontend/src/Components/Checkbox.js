// CheckboxComponent.js
import React, { useState } from "react";
import { Checkbox } from "primereact/checkbox";

const CheckboxComponent = ({ label, checked, onChange }) => {
  // const [isChecked, setIsChecked] = useState(false);

  // const handleCheckboxChange = () => {
  //   const newCheckedState = !isChecked;
  //   setIsChecked(newCheckedState);
  //   onCheckboxChange(newCheckedState); // Pass the new state to the parent component
  // };

  return (
    <div className="p-field-checkbox">
      <Checkbox
        inputId="checkbox"
        onChange={onChange}
        checked={checked}
      />
      <label htmlFor="checkbox">{label}</label>
    </div>
  );
};

export default CheckboxComponent;
