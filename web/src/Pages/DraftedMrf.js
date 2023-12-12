import { Column } from "primereact/column";
import { DataTable } from "primereact/datatable";
import React, { useState, useEffect } from "react";
import { APIPath } from "./../components/constant";
import { Dialog } from "primereact/dialog";
import "../styles/layout/MrfDrafted.css";
import ReferenceBodyTemplate from "./MrfRefStatus";

const DraftedMrf = ({ header, visible, onHide, roleId = 3, statusId = 2 }) => {
  const [data, setdata] = useState([]);

  useEffect(() => {
    const fetchData = () => {
      try {
        fetch(
          `${APIPath}Mrfdetail/GetMrfDetails/GetMrfDetails?statusId=${statusId}&roleId=${roleId}`)
          .then((response) => response.json())
          .then((data) => {
            setdata(data.result);
          })
          .catch((error) => {
            console.error("Error fetching data:", error);
          });
      } catch (error) {
        console.error("Error fetching data:", error);
      }
    };

    fetchData();
  }, []);

  const columnHeaderTemplate = (title) => {
    return <h3 className="mrfdraft-table-header">{title}</h3>;
  };

  const createdOnBodyTemplate = (mrf) => {
    return new Date(mrf.createdOnUtc).toLocaleDateString().replaceAll("/", "-");
  };
  const updatedOnBodyTemplate = (mrf) => {
    return new Date(mrf.createdOnUtc).toLocaleDateString().replaceAll("/", "-");
  };

  const salaryTemplate = (mrf) => {
    return (
      <h4 className="mrfdraft-col">
        {mrf.salary}
        {" LPA"}
      </h4>
    );
  };
  return (
    <Dialog
      header={header}
      visible={visible}
      onHide={onHide}
      className="mrfdraft-card"
    >
      <DataTable
        value={data}
        paginator
        rows={10}
        scrollable
        scrollHeight="400px"
      >
        <Column
          field="referenceNo"
          header={columnHeaderTemplate("MRF ID")}
          body={ReferenceBodyTemplate}
          bodyClassName="mrfdraft-col mrfdraft-ref-col  "
          sortable
        ></Column>
        <Column
          field="name"
          header={columnHeaderTemplate("Created By")}
          sortable
          bodyClassName="mrfdraft-col mrfdraft-ref-col   "
        ></Column>
        <Column
          field="createdOnUtc"
          header={columnHeaderTemplate("Created On")}
          body={createdOnBodyTemplate}
          bodyClassName="mrfdraft-col  "
          sortable
        ></Column>
        <Column
          field="updatedOnUtc"
          body={updatedOnBodyTemplate}
          header={columnHeaderTemplate("Last Updated")}
          bodyClassName="mrfdraft-col "
          sortable
        ></Column>
        <Column
          field="requisitionType"
          header={columnHeaderTemplate("Requistion Type")}
          bodyClassName="mrfdraft-col"
          sortable
        ></Column>
        <Column
          field="vacancyNo"
          header={columnHeaderTemplate("No. of Postion")}
          bodyClassName="mrfdraft-col"
          sortable
        ></Column>
        <Column
          field="experience"
          header={columnHeaderTemplate("Exp Required")}
          bodyClassName="mrfdraft-col "
          sortable
        ></Column>
        <Column
          field="salary"
          header={columnHeaderTemplate("Salary Range")}
          sortable
          body={salaryTemplate}
          bodyClassName="mrfdraft-col mrfdraft-ref-col "
        ></Column>
        <Column
          field="mrfStatus"
          header={columnHeaderTemplate("Status")}
          sortable
          bodyClassName="mrfdraft-col mrfdraft-ref-col"
        ></Column>
      </DataTable>
    </Dialog>
  );
};

export default DraftedMrf;
