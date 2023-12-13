import { useState } from "react";
import { mrfStatus } from "./constant";
import { Dialog } from "primereact/dialog";
import { Button } from "primereact/button";
import { storageService } from "../constants/storage";
import { useDispatch } from "react-redux";
import { PAGE_ACTIONS } from "../reducers/Page_r";
import { navigateTo } from "../constants/Utils";
import "../css/MrfRefStatus.css";

const MrfLink = ({
	mrfRef,
	mrfId = null,
	message = null,
	addButton = false,
}) => {
	const [visible, setVisible] = useState(false);
	const dispatch = useDispatch();

	const handleAClick = (id) => {
		dispatch(
			PAGE_ACTIONS.setParams({
				params: id,
			})
		);
		navigateTo("edit_requisition");
	};

	const handleYesClick = () => {
		if (mrfId) {
			handleAClick(mrfId);
		} else {
			navigateTo("dashboard");
		}
	};

	return (
		<div className="mrf-ref-cell">
			{message ? (
				<>
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
									handleYes={handleYesClick}
									handleNo={() => setVisible(false)}
								/>
							) : (
								message
							)}
						</div>
					</Dialog>
					<button onClick={() => setVisible(true)} className="mrf-ref-link">
						{mrfRef}
					</button>
				</>
			) : (
				<button onClick={(e) => handleAClick(mrfId)} className="mrf-ref-link">
					{mrfRef}
				</button>
			)}
		</div>
	);
};

const PopupMessage = ({ handleYes, handleNo, message }) => {
	return (
		<>
			<p>{message}</p>
			<div className="ref-popup-bttns">
				<Button label="YES" className="ref-bttn yes-bttn" onClick={handleYes} />
				<Button
					label="NO"
					className="ref-bttn no-bttn"
					onClick={handleNo}
					outlined
				/>
			</div>
		</>
	);
};

const ReferenceBodyTemplate = (mrf) => {
	// const roleId = storageService.getData("profile").roleId;
	const roleId = 3;
	const mrfRef = mrf.referenceNo;

	if (roleId === 3) {
		switch (mrf.mrfStatusId) {
			case mrfStatus.draft:
				return <MrfLink mrfRef={mrfRef} mrfId={mrf.mrfId} />;
			case mrfStatus.submToHr:
				return (
					<MrfLink
						mrfRef={mrfRef}
						addButton={true}
						message="Do you want to Withdraw it?"
					/>
				);
			case mrfStatus.open:
				return (
					<MrfLink
						mrfRef={mrfRef}
						addButton={true}
						message="Do you want to Withdraw it?"
					/>
				);
			case mrfStatus.resubReq: //need to add hr note
				return (
					<MrfLink
						mrfRef={mrfRef}
						mrfId={mrf.mrfId}
						message="Note added by HR"
					/>
				);
			case mrfStatus.rejected:
				return <MrfLink mrfRef={mrfRef} message="This MRF is Rejected" />;
			case mrfStatus.closed:
				return <MrfLink mrfRef={mrfRef} message="This MRF is Closed" />;
			case mrfStatus.withdrawn:
				return <MrfLink mrfRef={mrfRef} message="This MRF is Withdrawn" />;
			case mrfStatus.onHold:
				return <MrfLink mrfRef={mrfRef} message="This MRF is on Hold" />;
		}
		return <MrfLink mrfRef={mrfRef} />;
	}
};

export default ReferenceBodyTemplate;
