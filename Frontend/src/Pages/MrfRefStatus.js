import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { mrfStatus } from "../Components/constant";
import { Dialog } from "primereact/dialog";
import { Button } from "primereact/button";
import "../styles/layout/MrfStatus.css";

const MrfLink = ({
  mrfRef,
  sendTo,
  addPop = false,
  message,
  addButton = false,
}) => {
  const [visible, setVisible] = useState(false);

  return (
    <div className="mrf-ref-cell">
      {addPop ? (
        <Dialog
          className="ref-popup"
          visible={visible}
          onHide={() => setVisible(false)}
          draggable={false}
          dismissableMask
          showHeader={false}
        >
          <div className="ref-popup-content">
            {addButton ? (
              <PopupMessage
                message={message}
                handleHide={() => setVisible(false)}
              />
            ) : (
              message
            )}
          </div>
        </Dialog>
      ) : (
        ""
      )}
      <Link
        to={sendTo}
        onClick={() => setVisible(true)}
        className="mrf-ref-link"
      >
        {mrfRef}
      </Link>
    </div>
  );
};

const PopupMessage = ({ handleHide, message }) => {
  const navigate = useNavigate();

  const handleYes = () => {
    navigate("/Dashboard");
  };

  return (
    <>
      <p>{message}</p>
      <div className="ref-popup-bttns">
        <Button label="YES" className="ref-bttn yes-bttn" onClick={handleYes} />
        <Button
          label="NO"
          className="ref-bttn no-bttn"
          onClick={handleHide}
          outlined="true"
        />
      </div>
    </>
  );
};

const ReferenceBodyTemplate = (mrf) => {
  var roleId = 3;
  const mrfRef = mrf.referenceNo;

  if (roleId === 3) {
    switch (mrf.mrfStatusId) {
      case mrfStatus.draft:
        return (
          <MrfLink mrfRef={mrfRef} sendTo={`/EditRequisition/${mrf.mrfId}`} />
        );
      case mrfStatus.submToHr:
        return (
          <MrfLink
            mrfRef={mrfRef}
            addPop={true}
            addButton={true}
            message="Do you want to Withdraw it?"
          />
        );
      case mrfStatus.open:
        return (
          <MrfLink
            mrfRef={mrfRef}
            addPop={true}
            addButton={true}
            message="Do you want to Withdraw it?"
          />
        );
      case mrfStatus.resubReq: //need to update with note
        return (
          <MrfLink mrfRef={mrfRef} addPop={true} message="Note added by HR" />
        );
      case mrfStatus.rejected:
        return (
          <MrfLink
            mrfRef={mrfRef}
            addPop={true}
            message="This MRF is Rejected"
          />
        );
      case mrfStatus.closed:
        return (
          <MrfLink mrfRef={mrfRef} addPop={true} message="This MRF is Closed" />
        );
      case mrfStatus.withdrawn:
        return (
          <MrfLink
            mrfRef={mrfRef}
            addPop={true}
            message="This MRF is Withdrawn"
          />
        );
      case mrfStatus.onHold:
        return (
          <MrfLink
            mrfRef={mrfRef}
            addPop={true}
            message="This MRF is on Hold"
          />
        );
    }
    return <MrfLink mrfRef={mrfRef} />;
  }
};

export default ReferenceBodyTemplate;
