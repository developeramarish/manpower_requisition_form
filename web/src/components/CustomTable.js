import React, { useState } from 'react';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { InputText } from 'primereact/inputtext';


const CustomTable = ({ data,tablename, paginator, row, sorting, searching }) => {
  const [globalFilter, setGlobalFilter] = useState('');
 
 

  return (
    <div className=''>
      {searching && ( 
        <span className="p-input-icon-left p-mb-2">
          <i className="pi pi-search" />
          <InputText
            type="text"
            placeholder="Search..."
            value={globalFilter}
            onChange={(e) => setGlobalFilter(e.target.value)}
            
          />
        </span>
      )}
      <div className="table-wrapper">
    <h2 className="table-name">{tablename}</h2>
    
      <DataTable  value={data} paginator={paginator} rows={row} globalFilter={globalFilter}  tableStyle={{ minWidth: "50 rem" }}>
        {Object.keys(data[0]).map((field, index) => (
          <Column
            key={index}  field={field} header={field} sortable={sorting}
           
          />
        ))}
      </DataTable>
    </div>
    </div>
  );
};

export default CustomTable;
