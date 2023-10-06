
import { InputText } from 'primereact/inputtext';



export default function RequisitonSearch() {
    return(
        <div className='flex flex-row align-items-center h-3rem w-full'>
            <h1 className="text-black-alpha-90 mr-auto text-2xl">Create Requisition</h1>
            <div className="p-input-icon-left w-3" >
                <i className="pi pi-search " />
                <InputText className='border-round-3xl w-full' placeholder="Search" />
            </div>
        </div>
        
    );
}