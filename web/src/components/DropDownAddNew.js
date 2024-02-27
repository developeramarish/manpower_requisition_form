import React, { useState, useEffect } from "react";
import { Dropdown } from "primereact/dropdown";
import "./../css/DropDownAddNew.css";

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

  const filterData = (e) => {
    var elem = document.getElementById("dropdown_" + "new"),
      addButton = document.querySelector(".p-dropdown-filter-container");
    var inputElement = document.querySelector(
      ".p-dropdown-filter-container input"
    );
    if (inputElement) {
      var inputValue = inputElement.value;
    }
    if (addButton === e.target) {
      onAddItem(inputValue);
      addButton.classList.remove("add-enabled");
    }
  };

  const handleInput = (event) => {
    const inputElement = document.querySelector(
      ".p-dropdown-filter-container input"
    );
    if (inputElement) {
      const inputValue = inputElement.value.toLowerCase();

      const container = document.querySelector(".p-dropdown-filter-container");
      if (
        !options.find((option) =>
          option[optionLabel].toLowerCase().includes(inputValue)
        )
      ) {
        container.classList.add("add-enabled");
      } else {
        container.classList.remove("add-enabled");
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
      onMouseDown={filterData}
      onInput={handleInput} // Handle input for search
      filter
    />
  );
};

export default DropdownAddNew;
