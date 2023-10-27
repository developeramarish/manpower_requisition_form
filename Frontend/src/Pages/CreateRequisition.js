import RequisitionBody from "../Components/RequisitionBody";
import SearchHeader from "../Components/SearchHeader";
import DashboardHeader from "./Header";
import LeftPanel from "./LeftPanel";
import CreateRequisitionBody from "./CreateRequisitionBody";    
import DemoFile from "./demofile"; 
export default function CreateRequisition() {
  return (
    <>
      <DashboardHeader />
      <div className="flex bg-gray-200">
        <LeftPanel />
        <div className="flex flex-column gap-2 w-full p-3 py-2 h-full ">
          <SearchHeader title="Create Requisition" />
          <CreateRequisitionBody />
        </div>
      </div>
    </>
  );
}
