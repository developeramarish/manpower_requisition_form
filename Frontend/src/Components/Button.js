// Button.js
import React from 'react';
import { Button } from "primereact/button";
const ButtonC = ({ onClick, label }) => {
  return <Button onClick={onClick}>{label}</Button>;
};

export default ButtonC;
