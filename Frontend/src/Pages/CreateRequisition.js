import RequisitionBody from "../Components/RequisitionBody";
import RequisitonSearch from "../Components/RequisitionSearch";




export default function CreateRequisition(){
    return (
        <div className="flex flex-column gap-2 w-full p-3 py-2 h-full" >
            <RequisitonSearch/>
            <RequisitionBody />
        </div>
        
    );
}