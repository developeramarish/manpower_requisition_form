import React from "react";
import "../css/LoginFail.css";

const LoginFail = () => {
  return (
    <div className="fail_login">
      <h4 className="failed_message">Login Failed</h4>

      <div className="circle">
        <span className="excalamtory_sign">!</span>
      </div>

      <div className="container">
        <div className="message_container">
          <h5>Unfortunately, we are not able to login with your Email-Id</h5>
        </div>
        <div className="reason_container">
          <h5>Failure reason:</h5>
          <div className="reason_box">
            <h5>
              The email-id which you used is not in our DataBase.<br></br>
              Please conatct support Team
            </h5>
          </div>
        </div>
      </div>
    </div>
  );
};

export default LoginFail;
