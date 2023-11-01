import React, { useState, useEffect } from "react";
import { DataTable } from "primereact/datatable";
import { InputText } from "primereact/inputtext";
import { Column } from "primereact/column";
import "../styles/layout/Dashboard.css";
const SearchText = () => {
  const [data, setData] = useState([]); // Your data goes here
  const [filteredData, setFilteredData] = useState([]);
  const [globalFilter, setGlobalFilter] = useState("");

  useEffect(() => {
    // Fetch or set your data
    // Example: fetchData().then(data => setData(data));
  }, []); // Include dependencies if needed

  const onInputChange = (e) => {
    // Update the global filter when the input value changes
    setGlobalFilter(e.target.value);
  };

  const filterData = () => {
    // Filter the data based on the global filter value
    const filteredResults = data.filter((item) =>
      Object.values(item).some(
        (field) =>
          field &&
          field.toString().toLowerCase().includes(globalFilter.toLowerCase()),
      ),
    );

    setFilteredData(filteredResults);
  };

  useEffect(() => {
    filterData();
  }, [globalFilter, data]); // Re-run the filter when the global filter or data changes

  const inputStyle = {
    borderRadius: "35px", // Adjust the border-radius as needed
    // Add more inline styles as needed
  };
  return (
    <div className="box">
      <label htmlFor="searchInput" className="p-sr-only">
        Search
      </label>
      <InputText
        id="searchInput"
        type="text"
        value={globalFilter}
        onChange={onInputChange}
        placeholder="Search..."
        style={inputStyle}
      />
    </div>
  );
};

export default SearchText;
