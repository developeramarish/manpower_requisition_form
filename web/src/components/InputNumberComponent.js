import React from "react";
import { InputNumber } from "primereact/inputnumber";
const InputNumberComponent = ({id,value,onChange,disable}) => {
  return (
    <InputNumber

      inputId={id}
      value={value}
      onValueChange={onChange}
      disabled={disable}
      className={`bg-gray-100`}
    />
  );
};

export default InputNumberComponent;
