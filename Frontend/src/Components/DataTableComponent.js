import React, { useState } from 'react';
import { Column } from 'primereact/column';
import { DataTable } from 'primereact/datatable';
import { Button } from 'primereact/button';
 
const  DataTableComponents = (props) => {
console.log(props.data);

const actionBodyTemplate = (rowData) => {
    return (
        <React.Fragment>
            <Button icon="pi pi-pencil" rounded outlined className="mr-2" onClick={"kritika"} />
            <Button icon="pi pi-trash" rounded outlined severity="danger" onClick={"kritika"} />
            
        </React.Fragment>
    );
};
    return(
     
        <DataTable value={props.data}  paginator rows={props.rows || 5} rowsPerPageOptions={[5, 10, 25, 50]} tableStyle={{ minWidth: '69rem'}} >
            {
                props.columns.map(x=> {
                    return <Column field={x.field} header={x.columnName} style={{ width: '20%',color:'#0e0808'}}  ></Column>
                    
                })
            }
             
            
            <Column body={actionBodyTemplate} exportable={false} style={{ minWidth: '12rem' }}></Column>

        </DataTable>
    
      
     
        
    );
}
export default DataTableComponents  ;