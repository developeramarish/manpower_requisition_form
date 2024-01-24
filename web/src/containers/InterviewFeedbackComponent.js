import { useEffect, useState } from "react";
import { Dialog } from "primereact/dialog";
import { API_URL, ROLES } from "../constants/config";
import { getData, getDataAPI } from "../constants/Utils";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import ButtonC from "./../components/Button";
import "../css/InterviewFeedback.css";
import FeedbackForm from './FeedbackForm';
const InterviewFeedbackComponent = ({ visible, onHide, cId = null,roleId = null }) => {
    const [feedData, setFeedData] = useState([{}]);
	const [showForm, setShowForm] = useState(false);
    const [buttonDisplayed, setButtonDisplayed] = useState(true);
    const [count, setCount] = useState(0);
    const RedAsterisk = () => <span className="text-red-500">*</span>;
    const onLoad = async() => {

const result=await getDataAPI(`${API_URL.INTERVIEW_FEEDBACK}/${cId}`)
const response=await result.json();

        
				setFeedData(response.result);
                
				const updatedResult = response.result.map(dataItem => {
                     if (dataItem.interviewRound === 3) {
                        setButtonDisplayed(false) ;
                       
                    }
                    
                    setCount(dataItem.interviewRound);
                });
			 
		
			 
		  
    };
    const refreshParentComponent = () => {
        onLoad();
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
          count={count}
          candidateId={cId}
          refreshParent={refreshParentComponent}
        />
        ) : (
          <>
            {feedData.length === 0 ? (
              <p className="no-feed">No Feedback Yet</p>
            ) : (
              <div className="feed-table">
                
              {feedData.map((dataItem, index) => (
                <div key={index} className="round-container">
                  <lable className="lableRound">Round: {dataItem.interviewRound}</lable>
                  {dataItem.resultGroups && Array.isArray(dataItem.resultGroups) && dataItem.resultGroups.length > 0 ? (
                    <table className="viewfeedback-table">
                      <thead>
                        <tr>
                          <th>Type</th>
                          <th>Comment</th>
                        </tr>
                      </thead>
                      <tbody>
                        {dataItem.resultGroups.map((group, i) => (
                          <tr key={i}>
                            <td>{group.feedBack}</td>
                            <td>{group.comment}</td>
                          </tr>
                        ))}
                      </tbody>
                    </table>
                  ) : (
                    <p>No Feedback for this round.</p>
                  )}
                </div>
              ))}
            </div>
            )}
          
    {buttonDisplayed && roleId===ROLES.interviewer && (
      <div className="dvAddFeedback">
        <ButtonC
          label="Add Feedback"
          className="w-15 bg-red-600 border-red-600 BtnAddFeedback"
          onClick={() => handleAddFeedbackClick()}  // Pass index or any identifier you need
          outlined="true"
        />
      </div>
    )}
  

          </>
        )}
      </Dialog>
    );
};

export default InterviewFeedbackComponent;

