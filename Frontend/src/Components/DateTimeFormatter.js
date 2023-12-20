import React from "react";

const DateTimeFormatter = ({ date }) => {
  const formattedDate = date.toISOString(); // "YYYY-MM-DDTHH:mm:ss.SSSZ" format

  return { formattedDate };
};
export default DateTimeFormatter;
