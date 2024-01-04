import { MRF_STATUS } from "../constants/config";

export const commonSettings = {
  setReadOnly: false,
  setHiringManager: false,
  setSiteHRSPOCApproval: false,
  setHodapproval: false,
  setCooapproval: false,
  setFinanceHeadApproval: false,
  awatingFinance: false,
  recievedFinance: false,
};

export function applySettingsBasedOnRoleAndStatus(
  getReqRoleId,
  mrfStatusId,
  roleId
) {
  console.log(getReqRoleId);
  console.log(roleId);
  switch (getReqRoleId) {
    case 3 /* MRFOwner */:
      if (
        mrfStatusId === MRF_STATUS.draft ||
        mrfStatusId === MRF_STATUS.resubReq
      ) {
        applyCommonSettings({
          setReadOnly: false,
        });
      } else {
        applyCommonSettings({
          setReadOnly: true,
          setHodapproval: true,
          setHiringManager: true,
          setSiteHRSPOCApproval: true,
          setFinanceHeadApproval: true,
          setCooapproval: true,
          awatingFinance: true,
          recievedFinance: true,
        });
      }
      break;
    case 4: //HR
      if (mrfStatusId == MRF_STATUS.submToHr) {
        applyCommonSettings({
          setReadOnly: true,
          setHodapproval: false,
          setHiringManager: false,
          setSiteHRSPOCApproval: false,
          setFinanceHeadApproval: true,
          setCooapproval: true,
          awatingFinance: true,
          recievedFinance: true,
        });
      }else if(mrfStatusId == MRF_STATUS.awaitHodApproval){
        applyCommonSettings({
          setReadOnly: true,
          setHodapproval: true,
          setHiringManager: false,
          setSiteHRSPOCApproval: false,
          setFinanceHeadApproval: true,
          setCooapproval: false,
          awatingFinance: true,
          recievedFinance: true,
        });
      }else if( mrfStatusId == MRF_STATUS.awaitCooApproval){
        applyCommonSettings({
          setReadOnly: true,
          setHodapproval: true,
          setHiringManager: false,
          setSiteHRSPOCApproval: false,
          setFinanceHeadApproval: false,
          setCooapproval: true,
          awatingFinance: true,
          recievedFinance: true,
        });

      }else if( mrfStatusId == MRF_STATUS.awaitfinanceHeadApproval){
        applyCommonSettings({
          setReadOnly: true,
          setHodapproval: true,
          setHiringManager: false,
          setSiteHRSPOCApproval: false,
          setFinanceHeadApproval: true,
          setCooapproval: true,
          awatingFinance: false,
          recievedFinance: true,
        });
      }else{
        applyCommonSettings({
          setReadOnly: true,
          setHodapproval: true,
          setHiringManager: true,
          setSiteHRSPOCApproval: true,
          setFinanceHeadApproval: true,
          setCooapproval: true,
          awatingFinance: true,
          recievedFinance: true,
        });

      }

    //     switch (mrfStatusId) {
    //       case MRF_STATUS.submToHr:
    //         applyCommonSettings({

    //           setReadOnly: false,
    //           setHodapproval: true,
    //           setHiringManager: true,
    //           setSiteHRSPOCApproval: true,
    //           setFinanceHeadApproval: true,
    //           setCooapproval: true,
    //           awatingFinance:true,
    // recievedFinance:true,

    //       });

    //       case MRF_STATUS.awaitHodApproval:
    //       case MRF_STATUS.awaitCooApproval:
    //       case MRF_STATUS.awaitfinanceHeadApproval:
    //         applyCommonSettings({
    //           setReadOnly: true,
    //           setHodapproval: false,
    //           setHiringManager: false,
    //           setSiteHRSPOCApproval: false,
    //           setFinanceHeadApproval: false,
    //           setCooapproval: true
    //         });
    //         break;
    //       default:
    //         applyCommonSettings(commonSettings);
    //     }
    //     break;

    default:
      if (roleId === 3) {
        // applyCommonSettings(commonSettings);
        applyCommonSettings({
          setReadOnly: false,
          setHodapproval: true,
          setHiringManager: true,
          setSiteHRSPOCApproval: true,
          setFinanceHeadApproval: true,
          setCooapproval: true,
          awatingFinance: true,
          recievedFinance: true,
        });
        break;
      }
  }
}

function applyCommonSettings(settings) {
  Object.assign(commonSettings, settings);
}

// if (getReqRoleId == 4 && mrfStatusId == MRF_STATUS.submToHr) {
//   setReadOnly(true);
//   setHiringManager(false);
//   setSiteHRSPOCApproval(false)
//   setHodapproval(false)
//   setCooapproval(true)
//   setFinanceHeadApproval(true)

// }
// else if(getReqRoleId == 4 && mrfStatusId == MRF_STATUS.awaitHodApproval){
//   setReadOnly(true);
//   setHiringManager(true);
//   setSiteHRSPOCApproval(true)
//   setHodapproval(true)
//   setCooapproval(false)
//   setFinanceHeadApproval(true)
// }
// else if(getReqRoleId == 4 && mrfStatusId == MRF_STATUS.awaitCooApproval){
//   setReadOnly(true);
//   setHiringManager(true);
//   setSiteHRSPOCApproval(true)
//   setHodapproval(true)
//   setCooapproval(true)
//   setFinanceHeadApproval(false)
// }
// else if(getReqRoleId == 4 && mrfStatusId == MRF_STATUS.awaitfinanceHeadApproval){
//   setReadOnly(true);
//   setHiringManager(true);
//   setSiteHRSPOCApproval(true)
//   setHodapproval(true)
//   setCooapproval(true)
//   setFinanceHeadApproval(false)
// }

// else if (getReqRoleId == 4) {
//   setReadOnly(true);
//   setHiringManager(true);
//   setSiteHRSPOCApproval(true)
//   setHodapproval(true)
//   setCooapproval(true)
//   setFinanceHeadApproval(true)
//   // setEmailApprovalReadOnly(true);
// } else if (
//   (getReqRoleId == 3 && mrfStatusId == MRF_STATUS.draft) ||
//   mrfStatusId == MRF_STATUS.resubReq
// ) {
//   setReadOnly(false);
//   setHiringManager(true);
//   setSiteHRSPOCApproval(true)
//   setHodapproval(true)
//   setCooapproval(true)
//   setFinanceHeadApproval(true)
// } else if (getReqRoleId == 3) {
//   setReadOnly(true);
//   setHiringManager(true);
//   setSiteHRSPOCApproval(true)
//   setHodapproval(true)
//   setCooapproval(true)
//   setFinanceHeadApproval(true)
// } else if (roleId == 3) {
//   setHiringManager(true);
//   setSiteHRSPOCApproval(true)
//   setHodapproval(true)
//   setCooapproval(true)
//   setFinanceHeadApproval(true)
// }
