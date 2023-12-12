import { useEffect, useState } from "react";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";

import { APIPath } from "./../components/constant";
import "../styles/layout/Candidate.css";
import ButtonC from "./../components/Button"; 
import AddCandidate from "./AddCandidate";
// import { useNavigate } from 'react-router-dom';
function ViewCandidate() {
  const [reqData, setReqData] = useState([]);
  // const navigate= useNavigate();
  useEffect(() => {
   
    const apiUrl = APIPath + "Candidatedetail/Get";
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

  const header = <h3 className="req-header">View Candidate</h3>;
  const headerTemplate = (
    <div>
       <ButtonC
          label="Add Candidate"
          className="w-2 bg-red-600 border-red-600"
          onClick={() => {/* navigate("/AddCandidate") */}}
        />
     <div>
        <br></br>
      <h3 className="req-header">View Candidate</h3>
      </div>
    </div>
  );

 
  const columnHeaderTemplate = (title) => {
    return <h3 className="req-table-header">{title}</h3>;
  };

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
        header={headerTemplate}
        scrollHeight="62vh"
      >
        <Column
          field="id"
          header={columnHeaderTemplate("Candidate ID")}
          sortable          
          bodyClassName="req-col ref-col"
        ></Column>
        <Column
          field="name"
          header={columnHeaderTemplate("Candidate Name")}
          bodyClassName="req-col"
          sortable
        ></Column>
        <Column
          field="resumePath"
          header={columnHeaderTemplate("ResumePath")}
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
          field="emailId"
          header={columnHeaderTemplate("EmailId")}
          bodyClassName="req-col"
          sortable
        ></Column>
        <Column
          field="contactNo"
          header={columnHeaderTemplate("Contact No")}
          bodyClassName="req-col"
          sortable
        ></Column>
       
      </DataTable>
    </div>
  );
}

export default ViewCandidate;
