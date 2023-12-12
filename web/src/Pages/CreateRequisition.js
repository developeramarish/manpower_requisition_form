import SearchHeader from "./../components/SearchHeader";
// import DashboardHeader from "./Header";
// import LeftPanel from "./LeftPanel";
import CreateRequisitionBody from "./CreateRequisitionBody";
// import { useParams } from "react-router-dom";

export default function CreateRequisition({reqId}) {
  // const { reqId } = useParams();
  return (
    <>
      {/* <DashboardHeader /> */}
      <div className="flex bg-gray-200">
       {/*  <LeftPanel /> */}
        <div className="flex flex-column gap-2 w-full p-3 py-2 h-full ">
          <SearchHeader title="Create Requisition" />

          <CreateRequisitionBody getReqId={reqId} />
        </div>
      </div>
    </>
  );
}
