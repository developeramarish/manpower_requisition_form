import { MultiSelect } from "primereact/multiselect";

export default function MultiSelectDropdown({
  options,
  value,
  onChange,
  placeholder,
  optionLabel,
  optionValue,
  addFilter = false,
  className,
  maxSelectedLabels,
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
    />
  );
}
