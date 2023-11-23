import React, { useState } from "react";
import { Dropdown } from "primereact/dropdown";

const DropdownComponent = ({
  optionLabel,
  optionValue,
  options,
  placeholder,
  onChange,
  value = null,
}) => {
  return (
    <Dropdown
      optionLabel={optionLabel}
      optionValue={optionValue}
      value={value}
      options={options}
      onChange={onChange}
      placeholder={placeholder}
    />
  );
};

export default DropdownComponent;
