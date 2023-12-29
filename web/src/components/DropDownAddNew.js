import React, { useState, useEffect } from "react";
import { Dropdown } from "primereact/dropdown";

const DropdownAddNew = ({
  optionLabel,
  optionValue,
  options = [],
  placeholder,
  className,
  onChange,
  value = null,
  disable,
  onAddItem,
}) => {
  const [filteredOptions, setFilteredOptions] = useState(options);

  useEffect(() => {
    setFilteredOptions(options);
  }, [options]);

  const handleInput = (event) => {
    const inputValue = event.target.value;
    // Filter options based on the input value
    const filtered = options.filter((option) =>
      option[optionLabel].toLowerCase().includes(inputValue.toLowerCase())
    );
    setFilteredOptions(filtered);
  };

  const handleKeyDown = (event) => {
    // Check if Enter key is pressed
    if (event.key === "Enter") {
      const inputValue = event.target.value;
      // Check if the input value is not in the list
      if (!options.find((option) => option[optionLabel] === inputValue)) {
        // Call the onAddItem callback with the input value
        onAddItem(inputValue);
      }
    }
  };

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
      onKeyDown={handleKeyDown}
      //onInput={handleInput} // Handle input for search
      //editable // Allow typing directly into the dropdown
      filter
      
    />
  );
};

export default DropdownAddNew;
