import { MultiSelect } from "primereact/multiselect";
import "./../css/MultiselectDropdown.css";

export default function MultiSelectDropdown({
  options,
  value,
  onChange,
  placeholder,
  optionLabel,
  optionValue,
  addFilter = true,
  className,
  maxSelectedLabels,
  disable,
  display=false
}) {
  return (
    <MultiSelect
      value={value}
      onChange={onChange}
      options={options}
      optionLabel={optionLabel}
      optionValue={optionValue}
      filter={addFilter}
      placeholder={placeholder}
      maxSelectedLabels={maxSelectedLabels}
      className={className}
      disabled={disable}
      display={display} 
    />

  );
}
