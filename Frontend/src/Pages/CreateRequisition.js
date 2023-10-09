import RequisitionBody from "../Components/RequisitionBody";
import RequisitonSearch from "../Components/RequisitionSearch";
import DashboardHeader from "./Header";
import LeftPanel from "./LeftPanel";
import SearchText from "./SearchText";

export default function CreateRequisition() {
  return (
    <>
      <DashboardHeader />

      <div className="flex bg-gray-200">
        <LeftPanel />
        <div className="flex flex-column gap-2 w-full p-3 py-2 h-full ">
          <RequisitonSearch />
          <RequisitionBody />
        </div>
        {/* <div className="MyDashboard">
          <div className="containerH">
            <div className="box">
              <label>Create Requisiton</label>
            </div>
            <SearchText />
          </div>
          <div className="bar"></div>
          <div className="flex flex-column gap-2 w-full p-3 py-2 h-full">
            <RequisitonSearch />
            <RequisitionBody />
          </div>
        </div> */}
      </div>
    </>
  );
}
