import { InputText } from "primereact/inputtext";

export default function RequisitonSearch() {
  return (
    <div className="flex flex-row align-items-center h-3rem w-full">
      <h3 className="text-black-alpha-90 mr-auto text-xl ">
        Create Requisition
      </h3>
      <div className="p-input-icon-left w-3">
        <i className="pi pi-search " />
        <InputText
          className="border-round-3xl w-full h-2rem text-sm"
          placeholder="Search"
        />
      </div>
    </div>
  );
}
