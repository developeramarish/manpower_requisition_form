import { useEffect, useState } from "react";
import { Dialog } from "primereact/dialog";
import { API_URL } from "../constants/config";
import { getData } from "../constants/Utils";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import "../css/InterviewFeedback.css";

const InterviewFeedbackComponent = ({ visible, onHide, cId = null }) => {
    const [feedData, setFeedData] = useState([{}]);
	
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

    const columns = [
        {
            field: "interviewRound",
            header: "Round",
            bodyClassName: ".feed-col",
            sortable: true,
        },
        {
            field: "comments",
            header: "Comments",
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
        </Dialog>
    );
};

export default InterviewFeedbackComponent;

