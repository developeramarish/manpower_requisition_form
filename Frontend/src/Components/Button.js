// Button.js
import React from 'react';
import { Button } from "primereact/button";

const ButtonC = ({ onClick, label,icon,severity }) => {
  return <Button onClick={onClick} icon={icon} severity={severity}>{label}</Button>;
};

export default ButtonC;
