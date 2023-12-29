import { useEffect, useState } from "react";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";

import { APIPath } from "./../components/constant";
import "../styles/layout/Candidate.css";
import "../css/MyRequisitions.css";
import { API_URL } from "../constants/config";
function ViewCandidate() {
  const [reqData, setReqData] = useState([]);
  useEffect(() => {
   const apiUrl = API_URL.GET_CANDIDATE_DETAILS;
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

  const createdOnBodyTemplate = (mrf) => {
    return new Date(mrf.createdOnUtc).toLocaleDateString().replaceAll("/", "-");
  };
  const updatedOnBodyTemplate = (mrf) => {
    return new Date(mrf.createdOnUtc).toLocaleDateString().replaceAll("/", "-");
  };
  const columns = [
		{
			field: "referenceNo",
			header: "MRF ID",
			//bodyClassName: "ref-col",
		},
		{
			field: "positiontitle",
			header: "Position",
			sortable: true,
		},
    {
			field: "name",
			header: "Name",
			sortable: true,
		},
    {
			field: "emailId",
			header: "Email",
			sortable: true,
		},
    {
			field: "resumePath",
			header: "Resume",
			sortable: true,
		},
		{
			field: "createdOnUtc",
			header: "Created On",
			body: createdOnBodyTemplate,
			sortable: true,
		},	 
		{
			field: "contactNo",
			header: "Contact",
			sortable: true,
		},
	];
  return (
    <div className="my-req">
      <h3 className="my-req-title">View Candidate</h3>
      <div className="req-table"> 
      <DataTable
        header=""
        value={reqData}
        paginator={reqData.length > 10}
        removableSort
        rows={10}
        scrollable
        scrollHeight="62vh"
      >
        {columns.map((col,index) => (
						<Column
						key={index}
							field={col.field}
							header={col.header}
							body={col.body}
							bodyClassName={"req-col " + col.bodyClassName}
							sortable={col.sortable}
						/>
					))}
        
       
      </DataTable>
    </div>
    </div>
  );
}

export default ViewCandidate;
