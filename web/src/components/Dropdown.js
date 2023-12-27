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
  disable
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
      disabled ={disable}
    />
  );
};

export default DropdownComponent;
