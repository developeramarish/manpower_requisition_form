import React, { useEffect, useState, useRef } from "react";
import { API_URL } from "../constants/config";
import { storageService } from "../constants/storage";
import { navigateTo } from "../constants/Utils";
import { Dialog } from "primereact/dialog";
import ButtonC from "./Button";
import InputTextCp from "./Textbox";
import InputTextareaComponent from "./InputTextarea";
import ToastMessages from "./ToastMessages";

const MrfPartialStatus = ({
  mrfId = null,
  mrfStatusId = null,
  label = null,
  message = null,
  textbox = false,
  header = null,
  popupmessage = null,
}) => {
  const [visible, setVisible] = useState(false);
  const [note, setNote] = useState("");
  const toastRef = useRef(null);

  //   console.log(mrfId);
  //   console.log(mrfStatusId);
  //   console.log(label);
  //   console.log(message);
  //   console.log(addButton);

  const footerContent = (value) => {
    //   console.log(value);
    //   console.log("imam a a aclickckckkc");
    return (
      <div>
        <ButtonC
          label="Yes"
          className="w-2 bg-red-600 border-red-600 p-2 mr-3"
          onClick={() => {
            submitPartial(value);
            setVisible(false);
          }}
        />
        <ButtonC
          label="No"
          className=" w-2 bg-red-600 border-red-600 p-2 "
          onClick={() => {
            setVisible(false);
          }}
        />
      </div>
    );
  };

  const submitPartial = async () => {
    const partialStatus = {
      mrfStatusId: mrfStatusId,
      note: note || null,
      updatedByEmployeeId: storageService.getData("profile").employeeId,
      updatedOnUtc: new Date().toISOString(),
    };
    console.log(partialStatus);
    //    console.log("we got yooo") ;

    try {
      const response = await fetch(API_URL.MRF_PARTIAL_STATUS_UPDATE + mrfId, {
        method: "Put",
        headers: { "content-type": "application/json" },
        body: JSON.stringify(partialStatus),
      });

      if (response.ok) {
        const responseData = await response.json();
        if (responseData.statusCode === 409) {
          toastRef.current.showConflictMessage(responseData.message);
        } else {
          toastRef.current.showSuccessMessage("Action Submitted");
          setTimeout(() => {
            navigateTo("my_requisition");
          }, 1000);
        }
      } else {
        console.error("Request failed with status:", response.status);
        const errorData = await response.text();
        console.error("Error Data:", errorData);
        if (response.status === 400) {
          toastRef.current.showBadRequestMessage(
            "Bad request: " + response.url
          );
        }
      }
    } catch (error) {
      console.error("Error:", error);
    }
  };

  //       .then((res) => {
  //         console.log("submitted");

  //         setTimeout(() => {
  //           navigateTo("my_requisition");
  //         }, 1000);
  //       })
  //       .catch((err) => {
  //         console.log(err.message);
  //       });
  //   };

  return (
    <>
      {/* {popupmessage &&(
        <>
         <Dialog dismissableMask >{popupmessage}</Dialog>
         </>
       )} */}

      {label && (
        <>
          <ButtonC
            label={label}
            className="w-2 bg-red-600 border-red-600"
            onClick={() => setVisible(true)}
          ></ButtonC>

          <Dialog
            className="w-3 "
            visible={visible}
            header={header}
            draggable={false}
            onHide={() => setVisible(false)}
            footer={footerContent(mrfStatusId)}
          >
            {textbox && (
              <div>
                <label className="font-bold text-sm">Add Note:</label>
                <br />
                <InputTextareaComponent
                  value={note}
                  onChange={(e) => setNote(e.target.value)}
                  rows={3}
                  cols={35}
                  className="bg-gray-100"
                />
              </div>
            )}

            {message && <h3>{message}</h3>}
          </Dialog>
        </>
      )}
      <ToastMessages ref={toastRef} />
    </>
  );
};

export default MrfPartialStatus;
