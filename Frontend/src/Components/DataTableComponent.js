import React, { useState } from "react";
import { Column } from "primereact/column";
import { DataTable } from "primereact/datatable";

const DataTableComponents = (props) => {
  console.log(props.data);
  
  return (
    <DataTable
      value={props.data}
      paginator
      rows={props.rows || 5}
      rowsPerPageOptions={[5, 10, 25, 50]}
      tableStyle={{ minWidth: "69rem" }}
      scrollable 
      scrollHeight="250px" 
      //virtualScrollerOptions={{ itemSize: 46 }}
    >
      {props.columns.map((x) => {
        return (
          <Column
            field={x.field}
            header={x.columnName}
            style={{ width: "20%", color: "#0e0808" }}
            
          ></Column>
        );
      })}
       <Column body={props.body} exportable={false} style={{ minWidth: '12rem' }} ></Column> 
    </DataTable>
  );
};
export default DataTableComponents;

