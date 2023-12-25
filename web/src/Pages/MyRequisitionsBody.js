import { useEffect, useState } from "react";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import { APIPath } from "./../components/constant";
import ReferenceBodyTemplate from "./MrfRefStatus";
import "../styles/layout/MyRequisitionsBody.css";
import { getData } from "../constants/Utils";

function MyRequisitionsBody({roleId}) {
  const [reqData, setReqData] = useState([]);
   
  useEffect(() => {
    
      getReqData();
  }, []);
async function getReqData(){
  const apiUrl = APIPath + "Mrfdetail/GetMrfDetails/GetMrfDetails?statusId="+0+"&"+roleId 
  const aData = await getData(apiUrl);
  setReqData(aData.result);
}
  const header = <h3 className="req-header">My Requisitions</h3>;

  
  const createdOnBodyTemplate = (mrf) => {
    return new Date(mrf.createdOnUtc).toLocaleDateString().replaceAll("/", "-");
  };
  const updatedOnBodyTemplate = (mrf) => {
    return new Date(mrf.createdOnUtc).toLocaleDateString().replaceAll("/", "-");
  };

  return (
    <div className="req-card">
      <DataTable
        value={reqData}
        paginator
        removableSort
        rows={10}
        scrollable
        header={header}
        scrollHeight="62vh"
      >
        <Column
          field="referenceNo"
          header="MRF ID"
          body={ReferenceBodyTemplate}
          bodyClassName="req-col ref-col"
        ></Column>
        <Column
          field="name"
          header="Created By"
          bodyClassName="req-col"
          sortable
        ></Column>
        <Column
          field="createdOnUtc"
          header="Created On"
          body={createdOnBodyTemplate}
          bodyClassName="req-col"
          sortable
        ></Column>
        <Column
          field="updatedOnUtc"
          header="Last Updated"
          body={updatedOnBodyTemplate}
          bodyClassName="req-col"
          sortable
        ></Column>
        <Column
          field="requisitionType"
          header="Requisition Type"
          bodyClassName="req-col"
          sortable
        ></Column>
        <Column
          field="vacancyNo"
          header="No. of Positions"
          bodyClassName="req-col"
          sortable
        ></Column>
        <Column
          field="experience"
          header="Exp Required"
          bodyClassName="req-col"
          sortable
        ></Column>
        <Column
          field="salary"
          header="Salary Range"
          bodyClassName="req-col"
          sortable
        ></Column>
        <Column
          field="mrfStatus"
          header="Status"
          bodyClassName="req-col"
          sortable
        ></Column>
      </DataTable>
    </div>
  );
}

export default MyRequisitionsBody;
