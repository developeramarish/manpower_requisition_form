import { Dialog } from "primereact/dialog";
import { InputText } from "primereact/inputtext";

const InterviewFeedbackComponent = ({ visible, onHide, intData }) => {
  return (
    <Dialog
      header="Interview Feedback for "
      visible={visible}
      onHide={onHide}
    >
      <ol>
        <li>
          <label>Softskills:</label>
          <InputText value="" disabled />
        </li>
      </ol>
    </Dialog>
  );
};

export default InterviewFeedbackComponent;
