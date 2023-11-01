// DateTimePicker.js
import React from 'react';

const DateTimePicker = ({ value, onChange }) => {
  return <input type="datetime-local" value={value} onChange={onChange} />;
};

export default DateTimePicker;
