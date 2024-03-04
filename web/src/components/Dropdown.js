import React, { useState, useEffect } from "react";
import { Dropdown } from "primereact/dropdown";

const DropdownComponent = ({
  optionLabel,
  optionValue,
  options = [],
  placeholder,
  className,
  onChange,
  value = null,
  disable,
  clearIcon=false,
  filter
}) => {
  const [filteredOptions, setFilteredOptions] = useState(options);

  useEffect(() => {
    setFilteredOptions(options);
  }, [options]);

  return (
    <Dropdown
      optionLabel={optionLabel}
      optionValue={optionValue}
      value={value}
      options={filteredOptions}
      onChange={onChange}
      placeholder={placeholder}
      className={className}
      disabled={disable}
      showClear={clearIcon}
      filter ={filter}
    />
  );
};

export default DropdownComponent;
