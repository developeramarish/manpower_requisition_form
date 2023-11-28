import { useEffect, useState } from "react";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import { Link } from "react-router-dom";
import { APIPath } from "../Components/constant";
import "../styles/layout/MyRequisitionsBody.css";

function MyRequisitionsBody() {
  const [reqData, setReqData] = useState([]);
  const rolId=3;
  useEffect(() => {
    //if we pass id 0 then ge get all the data otherwise we get specific data like id=1
    const apiUrl = APIPath + "Mrfdetail/GetMrfDetails/0,"+rolId;
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

  const columnHeaderTemplate = (title) => {
    return <h3 className="req-table-header">{title}</h3>;
  };

  const referenceBodyTemplate = (mrf) => {
    return (
      <Link to={`/EditRequisition/${mrf.mrfId}`}>
        <h4 className="ref-col-cell">{mrf.referenceNo}</h4>
      </Link>
    );
  };
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
        rows={6}
        scrollable
        header={header}
        scrollHeight="62vh"
      >
        <Column
          field="referenceNo"
          header={columnHeaderTemplate("MRF ID")}
          s
          body={referenceBodyTemplate}
          bodyClassName="req-col ref-col"
        ></Column>
        <Column
          field="name"
          header={columnHeaderTemplate("Created By")}
          bodyClassName="req-col"
          sortable
        ></Column>
        <Column
          field="createdOnUtc"
          header={columnHeaderTemplate("Created On")}
          body={createdOnBodyTemplate}
          bodyClassName="req-col"
          sortable
        ></Column>
        <Column
          field="updatedOnUtc"
          header={columnHeaderTemplate("Last Updated")}
          body={updatedOnBodyTemplate}
          bodyClassName="req-col"
          sortable
        ></Column>
        <Column
          field="requisitionType"
          header={columnHeaderTemplate("Requisition Type")}
          bodyClassName="req-col"
          sortable
        ></Column>
        <Column
          field="vacancyNo"
          header={columnHeaderTemplate("No. of Positions")}
          bodyClassName="req-col"
          sortable
        ></Column>
        <Column
          field="experience"
          header={columnHeaderTemplate("Exp Required")}
          bodyClassName="req-col"
          sortable
        ></Column>
        <Column
          field="salary"
          header={columnHeaderTemplate("Salary Range")}
          bodyClassName="req-col"
          sortable
        ></Column>
        <Column
          field="mrfStatus"
          header={columnHeaderTemplate("Status")}
          bodyClassName="req-col"
          sortable
        ></Column>
      </DataTable>
    </div>
  );
}

export default MyRequisitionsBody;
