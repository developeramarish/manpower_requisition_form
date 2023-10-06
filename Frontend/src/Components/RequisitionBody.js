import { Card } from "primereact/card";
import { Button } from "primereact/button";
import { InputText } from "primereact/inputtext";
import { Dropdown } from "primereact/dropdown";
import { Calendar } from "primereact/calendar";
import { Checkbox } from "primereact/checkbox";
import { InputTextarea } from "primereact/inputtextarea";

export default function RequisitionBody() {
  const footer = (
    <div className="flex flex-wrap justify-content-end gap-5">
      <Button label="CANCEL" outlined className="mr-auto w-2" />
      <Button label="SAVE AS A DRAFT" className="w-2" />
      <Button label="SUBMIT" className="w-2" disabled />
    </div>
  );

  return (
    <Card
      title="Fill the Details"
      footer={footer}
      className="border-round-lg  "
      style={{ height: "78vh" }}
    >
      <section
        className="flex flex-column flex-nowrap gap-3 border-y-2 border-gray-300 py-3 px-1 overflow-y-scroll"
        style={{ height: "53vh" }}
      >
        <div className="flex justify-content-between gap-5">
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="refno" className="font-bold text-sm">
              Reference Number
            </label>
            <InputText id="refno" className="surface-100" />
          </div>
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="position-title" className="font-bold text-sm">
              Position Title
            </label>
            <InputText id="position-title" className="surface-100" />
          </div>
        </div>
        <div className="flex justify-content-between gap-5">
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="department" className="font-bold text-sm">
              Department
            </label>
            <InputText id="department" className="surface-100" />
          </div>
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="sub-department" className="font-bold text-sm">
              Sub-Department
            </label>
            <InputText id="sub-department" className="surface-100" />
          </div>
        </div>
        <div className="flex flex-column w-full gap-2">
          <label htmlFor="project" className="font-bold text-sm">
            Project
          </label>
          <InputText id="project" className="surface-100" />
        </div>
        <div className="flex justify-content-between gap-5">
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="no-vacancies" className="font-bold text-sm">
              Number of Vacancies
            </label>
            <InputText id="no-vacancies" className="surface-100" />
          </div>
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="gender" className="font-bold text-sm">
              Gender
            </label>
            <Dropdown id="gender" className="surface-100" />
          </div>
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="initiation-date" className="font-bold text-sm">
              Hiring Initiation Date
            </label>
            <InputText id="initiation-date" className="surface-100" />
          </div>
        </div>
        <div className="flex justify-content-between gap-5">
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="position-reporting" className="font-bold text-sm">
              Position Reporting to
            </label>
            <Dropdown id="position-reporting" className="surface-100" />
          </div>
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="grade" className="font-bold text-sm">
              Grade of the proposed employee
            </label>
            <Dropdown id="grade" className="surface-100" />
          </div>
        </div>
        <div className="flex justify-content-between gap-5">
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="employment-type-req" className="font-bold text-sm">
              Type of employment required
            </label>
            <Dropdown id="employment-type-req" className="surface-100" />
          </div>
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="vacancy-type" className="font-bold text-sm">
              Type of vacancy
            </label>
            <Dropdown id="vacancy-type" className="surface-100" />
          </div>
        </div>
        <div className="flex justify-content-between gap-5">
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="no-vacancies" className="font-bold text-sm">
              Employee Email ID disable request date
            </label>
            <Calendar id="no-vacancies" inputClassName="surface-100" showIcon />
          </div>
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="gender" className="font-bold text-sm">
              Last Working Date
            </label>
            <Calendar id="gender" inputClassName="surface-100" showIcon />
          </div>
          <div className="flex flex-row align-items-center w-6 gap-2 px-4 border-round-sm border-1 border-300 surface-100">
            <Checkbox inputId="replacement" />
            <label htmlFor="replacement" className="font-bold text-sm">
              Replacement for the employee
            </label>
          </div>
        </div>
        <div className="flex justify-content-between gap-5 ">
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="experience" className="font-bold text-sm">
              Experience
            </label>
            <InputTextarea autoResize id="experience" className="surface-100" />
          </div>
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="qualification" className="font-bold text-sm">
              Qualification
            </label>
            <InputTextarea
              autoResize
              id="qualification"
              className="surface-100"
            />
          </div>
        </div>
        <div className="flex justify-content-between gap-5">
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="experience" className="font-bold text-sm">
              Experience
            </label>
            <Dropdown id="experience" className="surface-100" />
          </div>
          <div className="flex flex-column w-6 gap-2">
            <label htmlFor="qualification" className="font-bold text-sm">
              Qualification
            </label>
            <Dropdown id="qualification" className="surface-100" />
          </div>
        </div>
      </section>
    </Card>
  );
}
