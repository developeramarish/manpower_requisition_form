import "./css/App.css";
import "./css/theme.css";
import "primereact/resources/primereact.css";
import "primeflex/primeflex.css";
import "primeicons/primeicons.css";
import "primereact/resources/primereact.min.css";
import "./css/InputComponent.css";
import { useSelector, useDispatch } from "react-redux";
import { useEffect, useState } from "react";
import {
  detectDevice,
  getData,
  getDataAPI,
  getKeyFromLocation,
  isTouchDevice,
  navigateTo,
} from "./constants/Utils";
import { DEVICE_ACTIONS } from "./reducers/Device_r";
import Login from "./containers/Login";
import HeaderBar from "./components/HeaderBar";

import {
  AuthenticatedTemplate,
  UnauthenticatedTemplate,
  useMsal,
} from "@azure/msal-react";
import { graphConfig, loginRequest } from "./authConfig";
import { storageService } from "./constants/storage";
import { APP_KEY, ROUTES } from "./constants/config";
import Sidebar from "./components/Sidebar";
import { PAGE_ACTIONS, Page } from "./reducers/Page_r";
import Dashboard from "./containers/Dashboard";
import MyRequisitions from "./containers/MyRequisitions";
import CreateRequisition from "./containers/CreateRequisition";
import AddCandidate from "./containers/AddCandidate";
import Footer from "./components/Footer";
import EmployeDetails from "./Pages/EmployeDetails";
import AllEmployees from "./Pages/AllEmployees";
import EmployeeDtailsEdit from "./Pages/EmployeeDtailsEdit";
import MyResume from "./containers/MyResume";
import ViewCandidate from "./containers/ViewCandidate";
import { Dialog } from "primereact/dialog";
import LoginFail from "./components/LoginFail";
import ResumeSummary from "./containers/ResumeSummary";
import DashMrfStatus from "./components/DashMrfStatus";
import InterviewSummary from "./containers/InterviewSummary";
import InterviewSummaryAllStatus from "./containers/InterviewSummaryAllStatus";
function App() {
  const [token, setToken] = useState();
  // const [profile, setProfile] = useState();
  const [userData, setUserData] = useState("");
  const dispatch = useDispatch();
  const { currentPageKey, params, profile, locationParams, currentRole } = useSelector((state) => state.page);
  const { currentDevice, touchDevice } = useSelector((state) => state.device);
  const { instance, accounts } = useMsal();

  useEffect(() => {
    if (window.localStorage && !window.localStorage.getItem(APP_KEY)) {
      storageService.init();
    }
    let currentDevice = detectDevice();
    dispatch(DEVICE_ACTIONS.setCurrentDevice(currentDevice));

    /**** 
        Description: Calls on window resize or on touch device rotation by the user.
    ****/
    window.addEventListener("resize", function () {
      let currentDevice = detectDevice();
      dispatch(DEVICE_ACTIONS.setCurrentDevice(currentDevice));
    });

    dispatch(DEVICE_ACTIONS.setTouchDevice(isTouchDevice()));
  }, []);

  useEffect(() => {
    if (accounts.length === 0) {
      // debugger
      return;
    }
    // Silently acquires an access token which is then attached to a request for MS Graph data
    instance
      .acquireTokenSilent({
        ...loginRequest,
        account: accounts[0],
      })
      .then((response) => {
        storageService.setData("token", response.accessToken);
        callLoginAPI();
      });
  }, [accounts]);

  async function callLoginAPI() {
    const result = await getDataAPI(graphConfig.graphMeEndpoint);
    const response = await result.json();
    // console.log(response);
    var oData =
      response && response.hasOwnProperty("result") ? response.result : null;
    if (oData === null) {
      setUserData(oData);
      return;
    }
    storageService.setData("profile", oData);
    storageService.setData("isLoggedIn", true);
    // setProfile(oData);
    dispatch(PAGE_ACTIONS.setProfile(oData))
    getLocationHash();
  }

  const getLocationHash = () => {
    let initialPageKey = storageService.getData("isLoggedIn")
      ? "dashboard"
      : "login";
    /* added window hash change event so we can detect browser next back*/
    window.onhashchange = function (e) {
      let oLocation = getKeyFromLocation(),
        locationRouteKeyFrom = oLocation.key,
        routes = ROUTES,
        routeKeys =
          locationRouteKeyFrom && locationRouteKeyFrom.length > 0
            ? locationRouteKeyFrom.split("/")
            : [],
        mainRouteKey =
          routeKeys[0].indexOf("?") > -1
            ? routeKeys[0].split("?")[0]
            : routeKeys[0],
        sValidRouteKey = routeKeys[0],
        isValidRoute = routes.hasOwnProperty(mainRouteKey);

      mainRouteKey = !isValidRoute ? initialPageKey : mainRouteKey;
      dispatch(
        PAGE_ACTIONS.setCurrentPageKey({
          pageKey: mainRouteKey,
        })
      );
      dispatch(
        PAGE_ACTIONS.setLocationParams({
          locationParams: oLocation.hasOwnProperty("params")
            ? oLocation.params
            : [],
        })
      );
      navigateTo(sValidRouteKey);
    };

    if (window.location.hash === "") {
      console.log("## No Hash ##");
      navigateTo(initialPageKey);
    } else {
      // console.log('## Has Hash ##');
      window.dispatchEvent(new HashChangeEvent("hashchange"));
    }
  };

  let Comp = ROUTES[currentPageKey];
  let currentRoleName = currentRole && currentRole.name.split(" ").join("_").toLowerCase();
  return (
    <div className={"App " + currentDevice + " " + currentPageKey+" "+currentRoleName}>
      <AuthenticatedTemplate>
        {profile && profile.roleId && (
          <>
            <HeaderBar
              userFirstName={
                accounts.length > 0 && accounts[0].name.split(" ")[0]
              }
              userLastName={
                accounts.length > 0 && accounts[0].name.split(" ")[1]
              }
              roleId={profile.roleId}
              multipleRoleIds={profile.multipleRoleIds}
            />
            <div className="content">
              <Sidebar roleId={profile.roleId} sPageKey={currentPageKey} />
              <div className="content_right_wrapper">
                {currentPageKey === "dashboard" && (
                  <Dashboard
                    roleId={profile.roleId}
                    userId={profile.employeeId}
                  />
                )}
                {currentPageKey === "resume_summary" && (
                  <ResumeSummary
                    roleId={profile.roleId}
                    userId={profile.employeeId}
                  />
                )}
                {currentPageKey === "mrf_summary" && (
                  <DashMrfStatus
                    roleId={profile.roleId}
                    userId={profile.employeeId}
                  />
                )}
                {currentPageKey === "interview_summary" && (
                  <InterviewSummary
                    roleId={profile.roleId}
                    userId={profile.employeeId}
                  />
                )}
                {currentPageKey === "interview_summary_more" && (
                  <InterviewSummaryAllStatus
                    roleId={profile.roleId}
                    userId={profile.employeeId}
                  />
                )}
                {currentPageKey === "my_requisition" && (
                  <MyRequisitions
                    roleId={profile.roleId}
                    userId={profile.employeeId}
                  />
                )}
                {currentPageKey === "allemployees" && <AllEmployees />}
                {currentPageKey === "employee" && <EmployeDetails />}
                {currentPageKey === "employee_edit" && <EmployeeDtailsEdit />}
                {currentPageKey === "add_candidate" && (
                  <AddCandidate
                    // reqId={params.mrfId}
                    // referenceNo={params.referenceNo}
                  />
                )}
                {currentPageKey === "create_requisition" && (
                  <CreateRequisition roleId={profile.roleId} />
                )}
                {currentPageKey === "view_candidate" && <ViewCandidate />}
                {currentPageKey === "my_resume" && (
                  <MyResume
                    roleId={profile.roleId}
                    userId={profile.employeeId}
                  />
                )}
                {currentPageKey === "edit_requisition" && (
                  <CreateRequisition
                    {...(params && params.id ? { reqId: params.id } : "")}
                    // reqstatus={params.statusForTitle}
                    {...(params && params.roleId
                      ? { reqRoleId: params.roleId }
                      : { roleId: profile.roleId })}
                    // reqstatusId={params.mrfstatusId}
                  />
                )}
                {currentPageKey === "mrf_details" && (
                  <CreateRequisition
                    reqId={locationParams[0].mrfid}
                    reqRoleId={profile.roleId}
                    reqstatus={true}
                  />
                )}
                <Footer />
               
              </div>
            </div>
          </>
        )}
      </AuthenticatedTemplate>

      {userData === null && <LoginFail />}
      <UnauthenticatedTemplate>
        <Login />
        <div className="login-load">
          <img src="./images/start_Logo.png" alt="mrf logo" />
        </div>
      </UnauthenticatedTemplate>
    </div>
  );
}

export default App;
