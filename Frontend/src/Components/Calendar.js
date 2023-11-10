// CalendarComponent.js
import React from "react";
import { Calendar } from "primereact/calendar";

const CalendarComponent = ({ value, onChange, inputClassName }) => {
  return (
    <Calendar
      value={value}
      onChange={onChange}
      showIcon={true}
      inputClassName={inputClassName}
    />
  );
};

export default CalendarComponent;
