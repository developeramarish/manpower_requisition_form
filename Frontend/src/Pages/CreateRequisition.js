import RequisitionBody from "../Components/RequisitionBody";
import RequisitonSearch from "../Components/RequisitionSearch";
import DashboardHeader from './Header';
import LeftPanel from './LeftPanel';
import SearchText from './SearchText';



export default function CreateRequisition(){
    return (
        <div>
      <DashboardHeader />
      <div style={{ display: 'flex' }}>
        <LeftPanel />
        <div className="MyDashboard">
        <div className="containerH">
        <div className="box">
       <label>MY REQUISITIONS</label>
       </div>
       <SearchText/>
       </div>
        <div className = "bar"></div>
        <div className="flex flex-column gap-2 w-full p-3 py-2 h-full" >
            <RequisitonSearch/>
            <RequisitionBody />
        </div>
        </div>
    </div>
 
        </div>
    );
}