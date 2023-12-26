import { useEffect, useState } from "react";
import { Dialog } from "primereact/dialog";
import { API_URL } from "../constants/config";
import { getData } from "../constants/Utils";

import "../css/InterviewFeedback.css";

const InterviewFeedbackComponent = ({ visible, onHide, cId = null }) => {
	const [feedData, setFeedData] = useState({});
	useEffect(() => {
		async function getFeedData() {
			const apiUrl = API_URL.INTERVIEW_FEEDBACK + "/" + cId;
			const data = await getData(apiUrl);
			setFeedData(data.result);
		}

		if (cId) {
			getFeedData();
		}
	}, [cId]);

	const feedList = [
		{ key: "softSkills", label: "Softskills : " },
		{ key: "hardSkills", label: "Hardskills : " },
		{ key: "requiredTraining", label: "Required Training : " },
		{ key: "comments", label: "Comments : " },
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
			{!feedData ? (
				<p className="no-feed">No Feedback Yet</p>
			) : (
				<ol className="feed-popup-content">
					{feedList.map((e) => (
						<li className="feed-li">
							<p>{e.label}</p>
							<p>{feedData[e.key]}</p>
						</li>
					))}
				</ol>
			)}
		</Dialog>
	);
};

export default InterviewFeedbackComponent;
