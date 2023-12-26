// Button.js
import React from "react";
import { Button } from "primereact/button";

const ButtonC = ({ onClick, label, icon, severity, className, outlined ,disable}) => {
  return (
    <Button
      onClick={onClick}
      icon={icon}
      severity={severity}
      className={className}
      label={label}
      outlined={outlined}
      disabled={disable}
    />
  );
};

export default ButtonC;
