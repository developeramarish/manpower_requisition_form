import './css/App.css';
import { useSelector, useDispatch } from 'react-redux';
import { useEffect, useState } from 'react';
import { detectDevice, getData, getKeyFromLocation, isTouchDevice, navigateTo } from './constants/Utils';
import { DEVICE_ACTIONS } from './reducers/Device_r';
import Login from './containers/Login';
import HeaderBar from './components/HeaderBar';

import { AuthenticatedTemplate, UnauthenticatedTemplate, useMsal } from "@azure/msal-react";
import { graphConfig, loginRequest } from "./authConfig";
import { storageService } from './constants/storage';
import { APP_KEY, ROUTES } from './constants/config';
import Sidebar from './components/Sidebar';
import { PAGE_ACTIONS, Page } from './reducers/Page_r';
import Dashboard from './containers/Dashboard';

function App() {

  const [token, setToken] = useState();
  const [profile, setProfile] = useState();
  const dispatch = useDispatch();
  const { currentPageKey } = useSelector((state) => state.page);
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
    window.addEventListener('resize', function () {
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
    instance.acquireTokenSilent({
      ...loginRequest,
      account: accounts[0]
    }).then((response) => {
      storageService.setData('token', response.accessToken);
      callLoginAPI();
    });

  }, [accounts])

  async function callLoginAPI() {
    const response = await getData(graphConfig.graphMeEndpoint);
    var oData = response && response.hasOwnProperty('result') ? response.result : null;
    if (oData === null) {
      return;
    }
    storageService.setData('profile', oData);
    storageService.setData('isLoggedIn', true);
    setProfile(oData);
    getLocationHash();
  }

  const getLocationHash = () => {
    let initialPageKey = storageService.getData('isLoggedIn') ? 'dashboard' : 'login';
    /* added window hash change event so we can detect browser next back*/
    window.onhashchange = function (e) {

      let locationRouteKeyFrom = getKeyFromLocation(),
        routes = ROUTES,
        routeKeys = (locationRouteKeyFrom && locationRouteKeyFrom.length > 0) ? locationRouteKeyFrom.split('/') : [],
        mainRouteKey = routeKeys[0],
        isValidRoute = routes.hasOwnProperty(mainRouteKey);

      mainRouteKey = (!isValidRoute) ? initialPageKey : mainRouteKey;


      dispatch(PAGE_ACTIONS.setCurrentPageKey({
        pageKey: mainRouteKey
      }));
      navigateTo(mainRouteKey);
    }

    if (window.location.hash === "") {
      console.log('## No Hash ##');
      navigateTo(initialPageKey);
    } else {
      // console.log('## Has Hash ##');
      window.dispatchEvent(new HashChangeEvent("hashchange"));
    }
  }
  let Comp = ROUTES[currentPageKey];
  return (
    <div className={"App " + currentDevice + " "+currentPageKey}>
      <AuthenticatedTemplate>
        {profile && profile.roleId &&
          <>
            <HeaderBar userFirstName={accounts.length > 0 && accounts[0].name.split(" ")[0]} userLastName={accounts.length > 0 && accounts[0].name.split(" ")[1]} />
            <div className='content'>
              <Sidebar />
              {currentPageKey === 'dashboard' && <Dashboard />}
            </div>
          </>
        }
      </AuthenticatedTemplate>

      <UnauthenticatedTemplate>
        <Login />
        <h5 className="card-title">Please sign-in to see your profile information.</h5>
      </UnauthenticatedTemplate>
    </div>
  );
}

export default App;
