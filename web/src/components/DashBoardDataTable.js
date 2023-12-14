import React, { useState } from "react";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import { ColumnGroup } from "primereact/columngroup";
import { Row } from "primereact/row";
import { FilterMatchMode } from "primereact/api";
import InputTextCp from "./Textbox";
import "../css/DashBoardDataTable.css"
const DashBoardDataTable = ({ value, header_title,headerRow ,coloumn}) => {
  
  const [globalFilterValue, setGlobalFilterValue] = useState("");
  const [filters, setFilters] = useState({
    global: { value: null, matchMode: FilterMatchMode.STARTS_WITH },
  });


  const renderHeader = () => {
    return (
      <div className="dash_table_header ">
        <h5 className="m-2	">{header_title} </h5>
        <span className="p-input-icon-left">
          <i className="pi pi-search" />
          <InputTextCp
            value={globalFilterValue}
            onChange={onGlobalFilterChange}
            placeholder="Keyword Search"
          />
        </span>
      </div>
    );
  };

  const bodyTemplate=(rowData,options)=>{
// console.log(rowData.resultGroups[options.rowIndex].totalstatusCount)
    return rowData.resultGroups[0].totalstatusCount
  }

  const onGlobalFilterChange = (e) => {
    const value = e.target.value;
    let _filters = { ...filters };

    _filters["global"].value = value;

    setFilters(_filters);
    setGlobalFilterValue(value);
  };
  const header = renderHeader();


  const headerGroup = (
    <ColumnGroup>
      <Row className="tiiile">
        <Column header="MRF ID" rowSpan={2} />
        <Column header={headerRow} colSpan={4} />
      </Row>
      <Row>
        {value.map((data, index) => {
          return (
            <Column
              header={data.resultGroups[index].candidatestatus}
              field={data.resultGroups[index].totalstatusCount}
            />
          );
        })}
      </Row>
    </ColumnGroup>
  );

  return (
    <div className="dashBoard_table">
      <DataTable
        value={value}
        filters={filters}
        header={header}
        headerColumnGroup={headerGroup}
        
      >
        
        {coloumn.map((col, index) => {
          return (
           
            <Column
              field={col.field}
              header={col.header}
              body={col.body}
            />
          );
        })} 
      </DataTable>
    </div>
  );
};

export default DashBoardDataTable;
