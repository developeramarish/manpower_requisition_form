import React, { useEffect, useState } from "react";
import PropTypes from "prop-types";
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

// DropdownComponent.propTypes = {
//   optionLabel: PropTypes.string.isRequired,
//   optionValue: PropTypes.string.isRequired,
//   type: PropTypes.string.isRequired,
//   options: PropTypes.array.isRequired,
//   selectedOption: PropTypes.any,
//   onChange: PropTypes.func.isRequired,
//   value: PropTypes.any,
// };

export default DropdownComponent;
