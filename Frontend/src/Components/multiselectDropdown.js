import React, { useState } from "react";
import { MultiSelect } from "primereact/multiselect";

export default function MultiSelectDropdown({
  data = [],
  placeholder,
  addFilter = false,
  ClassName,
  maxSelectedLabels,
}) {
  const [selectedItem, setSelectedItem] = useState();
  const optionData = data;

  return (
    <MultiSelect
      value={selectedItem}
      onChange={(e) => setSelectedItem(e.value)}
      options={optionData}
      optionLabel="name"
      filter={addFilter}
      placeholder={placeholder}
      maxSelectedLabels={maxSelectedLabels}
      className={ClassName}
    />
  );
}
