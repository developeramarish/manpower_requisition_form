import React from "react";
import { InputNumber } from "primereact/inputnumber";
const InputNumberamount = ({
  id,
  value,
  onChange,
  disable,
  maxLength,
  useGrouping,
}) => {
  return (
    <InputNumber
      inputId={id}
      value={value}
      onValueChange={onChange}
      disabled={disable}
      maxLength={maxLength}
      className={`bg-gray-100`}
      useGrouping={false}
      max={100}
    />
  );
};

export default InputNumberamount;
