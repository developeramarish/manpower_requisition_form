import { InputText } from "primereact/inputtext";
import { Password } from "primereact/password";

import { useState } from "react";

export default function Input({ inputType = "" }) {
  const [value1, setValue1] = useState();
  const [inClass, setInClass] = useState("");

  const validatePassword = (passValue) => {
    passValue.length < 6 ? setInClass("p-invalid") : setInClass("");
    setValue1(passValue);
  };

  const validateEmail = (emailValue) => {
    emailValue.includes("@") ? setInClass("") : setInClass("p-invalid");
    setValue1(emailValue);
  };

  let validateIn = (value) => {
    setValue1(value);
  };

  if (inputType === "password") {
    return (
      <Password
        className={inClass}
        value={value1}
        onChange={(e) => validatePassword(e.target.value)}
        feedback={false}
        toggleMask
      />
    );
  } else if (inputType === "email") {
    validateIn = validateEmail;
  }

  return (
    <InputText
      className={inClass}
      keyfilter={inputType}
      value={value1}
      onChange={(e) => validateIn(e.target.value)}
    />
  );
}
