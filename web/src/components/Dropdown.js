import React, { useState } from "react";
import { Dropdown } from "primereact/dropdown";

const DropdownComponent = ({
  optionLabel,
  optionValue,
  options,
  placeholder,
  className,
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
      className={className}
    />
  );
};

export default DropdownComponent;
