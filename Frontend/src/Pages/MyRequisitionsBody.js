import { useEffect, useState } from "react";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import { Link } from "react-router-dom";

function MyRequisitionsBody() {
  const [reqData, setReqData] = useState([]);

  useEffect(() => {
    //if we pass id 0 then ge get all the data otherwise we get specific data like id=1
    const apiUrl = "https://localhost:7128/api/Mrfdetail/GetMrfDetails/0";
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

  const header = <h3 className="text-base m-0">My Requisitions</h3>;

  const columnHeaderClass = "text-base text-red-600";

  const referenceBodyTemplate = (mrf) => {
    return (
      <Link to={`/EditRequisition/${mrf.mrfId}`}>
        <h4 className="underline text-red-600">{mrf.referenceNo}</h4>
      </Link>
    );
  };

  return (
    <div
      className="card border-round-lg bg-white p-2"
      style={{ height: "81vh" }}
    >
      <DataTable
        value={reqData}
        paginator
        removableSort
        rows={6}
        scrollable
        header={header}
        scrollHeight="62vh"
        size="medium"
      >
        <Column
          field="referenceNo"
          header="MRF ID"
          body={referenceBodyTemplate}
          bodyClassName="w-2"
        ></Column>
        <Column field="name" header="Created By" sortable></Column>
        <Column field="createdOnUtc" header="Created On" sortable></Column>
        <Column field="updatedOnUtc" header="Last Updated" sortable></Column>
        <Column
          field="requisitionType"
          header="Requisition Type"
          sortable
        ></Column>
        <Column field="vacancyNo" header="No. of Positions" sortable></Column>
        <Column field="experience" header="Exp Required" sortable></Column>
        <Column field="salary" header="Salary Range" sortable></Column>
        <Column field="mrfStatus" header="Status" sortable></Column>
      </DataTable>
    </div>
  );
}

export default MyRequisitionsBody;
