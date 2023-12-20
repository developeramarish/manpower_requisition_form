import { useEffect, useState } from "react";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import { Link } from "react-router-dom";
import { APIPath } from "../Components/constant";
import ReferenceBodyTemplate from "./MrfRefStatus";
import "../styles/layout/MyRequisitionsBody.css";

function MyRequisitionsBody({roleId}) {
  const [reqData, setReqData] = useState([]);
   
  useEffect(() => {
    //if we pass id 0 then ge get all the data otherwise we get specific data like id=1
    const apiUrl = APIPath + "Mrfdetail/GetMrfDetails/GetMrfDetails?statusId=0&roleId="+roleId;
    fetch(apiUrl)
      .then((response) => response.json())
      .then((response) => {
        if (Array.isArray(response.result)) {
          const data = response.result;
          setReqData(data);
        } else {
          console.error("API response result is not an array:", response);
        }
      })
      .catch((error) => {
        console.error("Fetch error:", error);
      });
  }, []);

  const header = <h3 className="req-header">My Requisitions</h3>;

  //underline text-red-600
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
