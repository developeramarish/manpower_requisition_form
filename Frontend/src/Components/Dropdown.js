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
  // const [selectedOption, setSelectedOption] = useState(null);

  // const handleDropdownChange = (e) => {
  //   setSelectedOption(e.value);
  //   onChange(e);
  // };

  // return (
  //   <Dropdown
  //     optionLabel={optionLabel}
  //     optionValue={optionValue}
  //     value={selectedOption}
  //     options={options}
  //     onChange={handleDropdownChange}
  //     placeholder={placeholder}
  //   />
  // );
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
