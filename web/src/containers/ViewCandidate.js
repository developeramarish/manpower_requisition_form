import { useEffect, useState } from "react";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import "../css/Candidate.css";
import "../css/MyRequisitions.css";
import { API_URL, FILE_URL } from "../constants/config";
import { getDataAPI, resumeBodyTemplate } from "../constants/Utils";
function ViewCandidate() {
  const [reqData, setReqData] = useState([]);
  useEffect(() => {
   const apiUrl = API_URL.GET_CANDIDATE_DETAILS;
const fetch=async()=>{
let results=await getDataAPI(apiUrl);
let response=await results.json();

 setReqData(response.result);
}
fetch();

  }, []);

  const createdOnBodyTemplate = (mrf) => {
    return new Date(mrf.createdOnUtc).toLocaleDateString().replaceAll("/", "-");
  };




  const columns = [
	{
		field: "referenceNo",
		header: "Sr. No.",
		body: (data, options) => options.rowIndex + 1,
		bodyClassName: "sr_No_ViewCandidate ",
	  },
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
      body: resumeBodyTemplate,
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
		rowsPerPageOptions={[5, 10, 25, 50,100]} 
        rows={10}
		showGridlines
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
