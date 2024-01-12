import { useEffect, useState } from "react";
import { Dialog } from "primereact/dialog";
import { API_URL } from "../constants/config";
import { getData } from "../constants/Utils";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import ButtonC from "./../components/Button";
import "../css/InterviewFeedback.css";
import FeedbackForm from '../Pages/FeedbackForm';
const InterviewFeedbackComponent = ({ visible, onHide, cId = null }) => {
    const [feedData, setFeedData] = useState([{}]);
	const [showForm, setShowForm] = useState(false);
    const RedAsterisk = () => <span className="text-red-500">*</span>;
    const onLoad = () => {
        fetch(API_URL.INTERVIEW_FEEDBACK + "/" + cId)
		.then((response) => {
				return response.json();
			  })
			  .then((json) => {
				setFeedData(json["result"]);
				
			  })
		
			  .catch((error) => console.log(error));
		  
    };

    useEffect(() => {
        onLoad();
    }, [cId]);

    const handleAddFeedbackClick = () => {
        setShowForm(true);
      };
    
      const handleFormSubmit = () => {
        // Add logic to handle form submission (e.g., saving data, updating state)
        setShowForm(false); // Close the form after submission
      };



    const columns = [
        {
            field: "interviewRound",
            header: "Round",
            bodyClassName: " feed-col-round",
            sortable: true,
        },
        {
            field: "evaluationFeedBack",
            header: "FeedBack Type",
            bodyClassName: " feed-col-round",
            sortable: true,
        },
        {
            field: "comments",
            header: "Comments",
            bodyClassName: " feed-col-comments",
            sortable: true,
        },
    ];

    return (
        <Dialog
        header="Interview Feedback"
        visible={visible}
        onHide={onHide}
        draggable={false}
        dismissableMask
        className="feed-popup"
      >
        {showForm ? (
          <FeedbackForm
          visible={true} 
          onHide={() => setShowForm(false)}
          onSubmit={handleFormSubmit}
          count={(feedData.length+1)}
          candidateId={cId}
        />
        ) : (
          <>
            {feedData.length === 0 ? (
              <p className="no-feed">No Feedback Yet</p>
            ) : (
              <div className="feed-table">
                <DataTable
                  value={feedData}
                  paginator={feedData.length > 10}
                  removableSort
                  rows={10}
                  scrollable
                  scrollHeight="flex"
                >
                  {columns.map((col, index) => (
                    <Column
                      key={index}
                      field={col.field}
                      header={col.header}
                      bodyClassName={"feed-col " + col.bodyClassName}
                      sortable={col.sortable}
                    />
                  ))}
                </DataTable>
              </div>
            )}
            {feedData.length < 3 && (
                <div className="dvAddFeedback">
                <ButtonC
                label="Add Feedback"
                // className=" w-2 surface-hover border-red-600 text-red-600"
                className="w-15 bg-red-600 border-red-600 BtnAddFeedback"
                onClick={handleAddFeedbackClick}
                outlined="true"
                // disable="true"
              />
            </div>
          )}
          </>
        )}
      </Dialog>
    );
};

export default InterviewFeedbackComponent;

