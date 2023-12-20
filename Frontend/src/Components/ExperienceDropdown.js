import React, { useState } from "react";
import { Dropdown } from "primereact/dropdown";

const ExperienceDropdown = () => {
  const minExperienceOptions = Array.from({ length: 31 }, (_, i) => ({
    label: i.toString(),
    value: i,
  }));
  const maxExperienceOptions = Array.from({ length: 31 }, (_, i) => ({
    label: i.toString(),
    value: i,
  }));

  const [selectedMinExperience, setSelectedMinExperience] = useState(null);
  const [selectedMaxExperience, setSelectedMaxExperience] = useState(null);

  const onMinExperienceChange = (e) => {
    setSelectedMinExperience(e.value);
  };

  const onMaxExperienceChange = (e) => {
    setSelectedMaxExperience(e.value);
  };

  return (
    <div>
      <div className="p-grid p-justify-center">
        <div className="p-col-2">
          <label className="font-bold text-sm label-with-padding-right">
            Min
          </label>
          <Dropdown
            value={selectedMinExperience}
            options={minExperienceOptions}
            onChange={onMinExperienceChange}
            optionLabel="label"
            placeholder="Min"
          />

          <label className="font-bold text-sm label-with-padding-left label-with-padding-right">
            Max
          </label>
          <Dropdown
            value={selectedMaxExperience}
            options={maxExperienceOptions}
            onChange={onMaxExperienceChange}
            optionLabel="label"
            placeholder="Max"
          />
        </div>
      </div>
    </div>
  );
};

export default ExperienceDropdown;
