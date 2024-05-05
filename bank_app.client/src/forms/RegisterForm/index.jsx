import "./RegisterForm.scss";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faUser, faLock } from "@fortawesome/free-solid-svg-icons";
import { useState } from "react";
import axios from "axios";
import apiConfig from "../../config";

// eslint-disable-next-line react/prop-types
function RegisterForm({ onClick }) {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");

  const handleSubmit = async (event) => {
    event.preventDefault();
    if (!(password == confirmPassword)) {
      console.log("mat khau xac nhan khong khop voi nhau");
      return;
    }
    try {
      const apiUrl = apiConfig.apiurls.register;
      const body = {
        username: username,
        password: password,
      };

      const response = await axios.post(apiUrl, body);
      response.data
        ? console.log("dang ky thanh cong")
        : console.log("dang ky that bai");
    } catch (error) {
      console.error("Error fetching data:", error);
    }
  };

  return (
    <div className="container-registerform">
      <div
        className="registerform-background"
        onClick={() => onClick(false)}
      ></div>
      <div className="registerform-content">
        <h2 className="registerform-title">Đăng ký</h2>
        <form
          onSubmit={handleSubmit}
          method="POST"
          className="registerform-inputform"
        >
          <div className="register-input">
            {" "}
            <FontAwesomeIcon icon={faUser} className="register-icon" />
            <input
              type="text"
              id="register-username"
              name="username"
              placeholder="Tên tài khoản"
              required
              onChange={(e) => {
                setUsername(e.target.value);
              }}
            />
          </div>

          <div className="register-input">
            {" "}
            <FontAwesomeIcon icon={faLock} className="register-icon" />
            <input
              type="text"
              id="register-password"
              name="password"
              placeholder="Mật khẩu"
              required
              onChange={(e) => {
                setPassword(e.target.value);
              }}
            />
          </div>

          <div className="register-input">
            {" "}
            <FontAwesomeIcon icon={faLock} className="register-icon" />
            <input
              type="text"
              id="register-confirmpassword"
              name="confirmPassword"
              placeholder="Xác nhận mật khẩu"
              required
              onChange={(e) => {
                setConfirmPassword(e.target.value);
              }}
            />
          </div>

          <button type="submit" className="register-submit">
            Đăng ký
          </button>
        </form>
      </div>
    </div>
  );
}

export default RegisterForm;
