import RequisitionBody from "./../components/RequisitionBody";
import SearchHeader from "./../components/SearchHeader";
import DashboardHeader from "./Header";
import LeftPanel from "./LeftPanel";
import AddCandidate from "./AddCandidate";
import ViewCandidate from "./ViewCandidate";

export default function Candidate() {
  return (
    <>
      <DashboardHeader />
      <div className="flex bg-gray-200">
        <LeftPanel />
        <div className="flex flex-column gap-2 w-full p-3 py-2 h-full ">
         
          <ViewCandidate />
        </div>
      </div>
    </>
  );
}
