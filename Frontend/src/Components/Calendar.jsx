// CalendarComponent.js
import React from 'react';
import { Calendar } from 'primereact/calendar';

const CalendarComponent = ({ value, onChange }) => {
  return (
    <Calendar value={value} onChange={onChange} showIcon={true} />
  );
};

export default CalendarComponent;
