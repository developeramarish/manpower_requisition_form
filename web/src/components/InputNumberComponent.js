import React from "react";
import { InputNumber } from "primereact/inputnumber";
const InputNumberComponent = ({id,value,onChange,disable,maxLength,useGrouping,onBlur, className}) => {
  return (
    <InputNumber

      inputId={id}
      value={value}
      onValueChange={onChange}
      disabled={disable}
      maxLength={maxLength}
      className={`bg-gray-100`}
      useGrouping={useGrouping}
      onBlur={onBlur}
    />
  );
};

export default InputNumberComponent;
