import React, { useState } from 'react';
import { Dropdown } from 'primereact/dropdown';

const GenderDropdown = () => {
  const genderOptions = [
    { label: 'Male', value: '1' },
    { label: 'Female', value: '2' },
    { label: 'Other', value: '3' },
  ];

  const [selectedGender, setSelectedGender] = useState(null);

  return (
    <div>
     
      <Dropdown
        optionLabel="label"
        optionValue="value"
        value={selectedGender}
        options={genderOptions}
        onChange={(e) => setSelectedGender(e.value)}
        placeholder="Select a gender"
      />
      
    </div>
  );
};

export default GenderDropdown;
