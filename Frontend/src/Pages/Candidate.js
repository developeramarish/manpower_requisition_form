import RequisitionBody from "../Components/RequisitionBody";
import SearchHeader from "../Components/SearchHeader";
import DashboardHeader from "./Header";
import LeftPanel from "./LeftPanel";
import AddCandidate from "./AddCandidate";


export default function Candidate() {
  return (
    <>
      <DashboardHeader />
      <div className="flex bg-gray-200">
        <LeftPanel />
        <div className="flex flex-column gap-2 w-full p-3 py-2 h-full ">
         
          <AddCandidate />
        </div>
      </div>
    </>
  );
}
