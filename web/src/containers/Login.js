import { useState } from "react";
import './../css/Login.css';

import { useMsal } from "@azure/msal-react";
import { loginRequest } from "../authConfig";

function Login() {
  const { instance } = useMsal();
  const loginHandler = (err, data, msal) => {
    instance.loginRedirect(loginRequest).catch(e => {
      // console.log(e, "   loginnnn");
  });
  };

  return <>{loginHandler()}</>;

}

export default Login;