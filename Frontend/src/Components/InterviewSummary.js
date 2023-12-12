import { useEffect, useState } from "react";
import { Dialog } from "primereact/dialog";
import { DataTable } from "primereact/datatable";
import { Button } from "primereact/button";
import { Column } from "primereact/column";
import { Dropdown } from "primereact/dropdown";
import { MultiSelect } from "primereact/multiselect";
import {
	APIPath,
	Roles,
	constantAssignmentPath,
	constantResumePath,
} from "../Components/constant";
import "../styles/layout/InterviewSummary.css";
import InterviewFeedbackComponent from "./InterviewFeedbackComponent";

const roleId = 3; //need to get role id from local storage

const strToArray = (s) => {
	if (typeof s === "string") {
		s = s.split(",").map(Number);
	}
	return s;
};

//for multiselect because it doesn't work properly
const arrayToObj = (options = [], selectedOpt) => {
	if (Array.isArray(selectedOpt)) {
		return options.filter((e) => selectedOpt.includes(e.employeeId));
	}
	return [selectedOpt];
};

const objToArray = (selectedOpt = []) => {
	return selectedOpt.map((e) => e.employeeId);
};

const uploadedOnBodyTemplate = (interview) => {
	return new Date(interview.createdOnUtc)
		.toISOString()
		.slice(0, 10)
		.replaceAll("/", "-");
};

const resumeBodyTemplate = (interview) => {
	let resumeLink = constantResumePath + interview.resumePath;
	return (
		<a href={resumeLink} target="_blank" className="int-link-cell">
			{interview.resumePath}
		</a>
	);
};

const attachmentBodyTemplate = (interview) => {
	let attachmentLink = constantAssignmentPath + interview.attachment;
	return (
		<a href={attachmentLink} target="_blank" className="int-link-cell">
			Assignment
		</a>
	);
};

//summary component
const InterviewSummary = ({ visible, onHide, mrfId = 1 }) => {
	const [interviewData, setInterviewData] = useState([]);
	const [interviewStatus, setInterviewStatus] = useState([]);
	const [interviewerData, setInterviewerData] = useState([]);
	const [saveBttn, setSaveBttn] = useState([]);
	const [showFeed, setShowFeed] = useState(false);
	const [selectedId, setSelectedId] = useState(null);

	useEffect(() => {
		const apiUrl =
			APIPath +
			`Mrfinterviewermap/GetInterviewDetails/GetInterviewDetails?id=${mrfId}&DashBoard=true`;
		fetch(apiUrl)
			.then((response) => response.json())
			.then((response) => {
				const data = response.result;
				let arr = new Array(data.interviewDetails.length).fill(false); // for save bttn
				setInterviewData(data.interviewDetails);
				setInterviewStatus(
					data.interviewstatus.filter((x) => x.candidateorEvalution == "E")
				);
				setInterviewerData(data.interviewReviewer);
				setSaveBttn(arr);
			})
			.catch((error) => {
				console.error("Fetch error:", error);
			});
	}, [mrfId]);

	const statusBodyTemplate = (interview, options) => {
		if (roleId === Roles.mrfOwner) {
			let is = interviewStatus.filter((x) => x.id == interview.evaluationId);
			return <p className="drop-width">{is[0].status}</p>;
		}

		return (
			<Dropdown
				optionLabel="status"
				optionValue="id"
				className="drop-width"
				options={interviewStatus}
				value={interview.evaluationId}
				onChange={(e) => {
					let interviewDataCopy = [...interviewData];
					let sv = [...saveBttn];
					sv[options.rowIndex] = true;
					interviewDataCopy[options.rowIndex].evaluationId = e.target.value;
					setInterviewData(interviewDataCopy);
					setSaveBttn(sv);
				}}
			/>
		);
	};

	const interviewerBodyTemplate = (interview, options) => {
		return (
			<MultiSelect
				className="drop-width"
				options={interviewerData}
				value={arrayToObj(interviewerData, interview.interviewerEmployeeId)}
				onChange={(e) => {
					//let interviewDataCopy = JSON.parse(JSON.stringify(interviewData));
					let interviewDataCopy = [...interviewData];
					let sv = [...saveBttn];
					sv[options.rowIndex] = e.value.length > 0 ? true : false;
					interviewDataCopy[options.rowIndex].interviewerEmployeeId =
						objToArray(e.value);
					setInterviewData(interviewDataCopy);
					setSaveBttn(sv);
				}}
				optionLabel="name"
				// optionValue="employeeId"
			/>
		);
	};

	const feedbackBodyTemplate = (interview) => {
		if (interview.evaluationId < 5) return "To be updated";
		return (
			<p
				onClick={() => {
					setSelectedId(interview.candidateId);
					setShowFeed(true);
				}}
				className="int-link-cell"
			>
				Click Here
			</p>
		);
	};

	const actionBodyTemplate = (interview, options) => {
		if (saveBttn[options.rowIndex]) {
			return <Button icon="pi pi-save" />;
		}
		return <Button icon="pi pi-save" disabled />;
	};

	const columns = [
		{
			field: "referenceNo",
			header: "Sr. No.",
			body: (data, options) => options.rowIndex + 1,
			bodyClassName: "int-edit-col",
		},
		{
			field: "resumePath",
			header: "Resume",
			body: resumeBodyTemplate,
			sortable: true,
		},
		{
			field: "createdOnUtc",
			header: "Uploaded On",
			body: uploadedOnBodyTemplate,
			sortable: true,
		},
		{
			field: "createdName",
			header: "Uploaded By",
			sortable: true,
		},
		{
			field: "interviewerName",
			header: "Interviewer/ Panel",
			body: interviewerBodyTemplate,
			bodyClassName: "drop-col",
			sortable: true,
		},
		{
			field: "evaluationId",
			header: "Interview Status",
			body: statusBodyTemplate,
			bodyClassName: "drop-col",
			sortable: true,
		},
		{
			field: "evalutionStatus",
			header: "Interview Feedback",
			body: feedbackBodyTemplate,
		},
		{
			field: "attachment",
			header: "Attachment",
			body: attachmentBodyTemplate,
		},
		{
			header: "Action",
			body: actionBodyTemplate,
			bodyClassName: "int-edit-col",
		},
	];

	return (
		<Dialog
			header="MRF ID (Interview Summary)"
			visible={visible}
			onHide={onHide}
			draggable={false}
			className="int-card"
		>
			<DataTable
				value={interviewData}
				paginator
				removableSort
				rows={10}
				scrollable
				scrollHeight="62vh"
				draggable={false}
			>
				{columns.map((col) => (
					<Column
						field={col.field}
						header={col.header}
						body={col.body}
						bodyClassName={"int-col " + col.bodyClassName}
						sortable={col.sortable}
					/>
				))}
			</DataTable>
			<InterviewFeedbackComponent
				visible={showFeed}
				onHide={setShowFeed}
				selectedId={selectedId}
			/>
		</Dialog>
	);
};

export default InterviewSummary;
