import React, { useState } from "react";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import { InputText } from "primereact/inputtext";
import { FilterMatchMode } from "primereact/api";
import "../styles/layout/DataTableCus.css";
import { InputTextarea } from "primereact/inputtextarea";
import {
  SingleDropDownOptions,
  MultiDropDownOptions,
} from "../Components/DataTableCustomComponents";
import DropdownComponent from "./Dropdown";

const DataTableCustom = ({
  data,
  tableName,
  paginator,
  row,
  sorting = [],
  searching,
  addTextBoxTo = [],
  addMultiSelect = [],
  addSingleSelect = [],
  singleSelectLabel = [],
  multiSelectLabel,
  showColum,
}) => {
  

  const [globalFilterValue, setGlobalFilterValue] = useState("");
  const [filters, setFilters] = useState({
    global: { value: null, matchMode: FilterMatchMode.STARTS_WITH },
  });
  const onGlobalFilterChange = (e) => {
    const value = e.target.value;
    let _filters = { ...filters };

    _filters["global"].value = value;

    setFilters(_filters);
    setGlobalFilterValue(value);
  };

  const textEditor = (value, placeholder) => {
    return (
      <InputTextarea
        className="textarea"
        rows={2}
        cols={25}
        value={value || ""}
        placeholder={`Enter ${placeholder}`}
      />
    );
  };

  // const singleTemplate = (value, placeholder) => {
  //   return (
  //     <DropdownComponent
  //       optionsLabel={"status"}
  //       optionValue={"id"}
  //       options={value}
  //       placeholder={placeholder}
  //     />
  //   );
  // };

  const multiTemplate = (value, placeholder, multiSelectLabel) => {
    return (
      <MultiDropDownOptions
        multiSelectOptions={value}
        optionLabel={multiSelectLabel}
        placeholder={placeholder}
      />
    );
  };

  const dropDownCompo = (field, placeholder, singleSelectLabel) => {
    return (
      <SingleDropDownOptions
        singleSelectOptions={field}
        optionLabel={singleSelectLabel}
        placeholder={placeholder}
      />
    );
  };

  return (
    <div className="">
      <div className="table-wrapper">
        <div className="table-header">
          <h2 className="table-name">{tableName} </h2>
          {searching && (
            <span className="p-input-icon-left p-mb-2">
              <i className="pi pi-search" />
              <InputText
                type="text"
                placeholder="Search..."
                value={globalFilterValue}
                onChange={onGlobalFilterChange}
              />
            </span>
          )}
        </div>

        <DataTable
          value={data}
          paginator={paginator}
          rows={row === undefined ? 8 : row}
          filters={filters}
          tableStyle={{ minWidth: "50rem" }}
        >
          {data.length > 0 &&
            Object.keys(data[0]).map((field, index) => {
              if (showColum.includes(field)) {
                if (addTextBoxTo.includes(field)) {
                  console.log(addTextBoxTo);
                  return (
                    <Column
                      key={index}
                      field={field}
                      header={field}
                      body={textEditor(field)}
                    />
                  );
                } else if (field in addMultiSelect) {
                  return (
                    <Column
                      key={index}
                      field={field}
                      header={field}
                      body={multiTemplate(
                        addMultiSelect[field],
                        field,
                        multiSelectLabel
                      )}
                    />
                  );
                } else if (field in addSingleSelect) {
                  return (
                    <Column
                      key={index}
                      field={field}
                      header={field}
                      // bodyStyle={singleTemplate(addSingleSelect[field],field)}
                      body={dropDownCompo(
                        addSingleSelect[field],
                        field,
                        singleSelectLabel
                      )}
                    />
                  );
                } else if (sorting.includes(field)) {
                  return (
                    <Column key={index} field={field} header={field} sortable />
                  );
                } else {
                  return <Column key={index} field={field} header={field} />;
                }
              } else {
                return null;
              }
            })}
        </DataTable>
      </div>
    </div>
  );
};

export default DataTableCustom;
