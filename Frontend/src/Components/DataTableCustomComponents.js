import React, { useState } from "react";
import { MultiSelect } from "primereact/multiselect";
import { Dropdown } from "primereact/dropdown";


const MultiDropDownOptions = ({multiSelectOptions,optionLabel,placeholder}) => {
    const [selectedOptions, setSelectedOptions] = useState(null);
    const options = multiSelectOptions;
    return (
      <div className="card flex justify-content-center">
        <MultiSelect
          value={selectedOptions}
          onChange={(e) => setSelectedOptions(e.value)}
          options={options}
          optionLabel={optionLabel}
          
          filter
      placeholder={`Select ${placeholder}`}
          maxSelectedLabels={3}
          className="w-full md:w-20rem"
        />
      </div>
    );
  };

  const SingleDropDownOptions = ({singleSelectOptions,optionLabel,placeholder}) => {
    const [statusDropdownValue,setStatusDropdownValue]=useState(null);
    const statusOptions = singleSelectOptions;
    return (
      <Dropdown
        value={statusDropdownValue}
        options={statusOptions}
        onChange={(e) => setStatusDropdownValue(e.value)}
        optionLabel={optionLabel}
        placeholder={`Select ${placeholder}`}
      />
    );
  };


  export {SingleDropDownOptions,MultiDropDownOptions}