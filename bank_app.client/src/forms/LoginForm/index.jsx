import "./LoginForm.scss";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faUser, faLock } from "@fortawesome/free-solid-svg-icons";
import { useState } from "react";
import apiConfig from "../../config";
import axios from "axios";

// eslint-disable-next-line react/prop-types
function LoginForm({ onClick }) {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const handleSubmit = async (event) => {
    event.preventDefault();
    try {
      const apiUrl = apiConfig.apiurls.login;
      const body = {
        username: username,
        password: password,
      };

      const response = await axios.post(apiUrl, body);
      localStorage.setItem("username", username);
      localStorage.setItem("jwt", response.data);
      console.log(localStorage.getItem("jwt"));
    } catch (error) {
      console.error("Error fetching data:", error);
    }
  };

  return (
    <div className="container-loginform">
      <div
        className="loginform-background"
        onClick={() => onClick(false)}
      ></div>
      <div className="loginform-content">
        <h2 className="loginform-title">Đăng nhập</h2>

        <form
          method="POST"
          className="loginform-inputform"
          onSubmit={handleSubmit}
        >
          <div className="login-input">
            {" "}
            <FontAwesomeIcon icon={faUser} className="login-icon" />
            <input
              type="text"
              id="login-username"
              name="username"
              placeholder="Tên tài khoản"
              onChange={(e) => {
                setUsername(e.target.value);
              }}
              required
            />
          </div>

          <div className="login-input">
            {" "}
            <FontAwesomeIcon icon={faLock} className="login-icon" />
            <input
              type="text"
              id="login-password"
              name="password"
              placeholder="Mật khẩu"
              onChange={(e) => {
                setPassword(e.target.value);
              }}
              required
            />
          </div>

          <button className="login-submit" type="submit">
            Đăng nhập
          </button>
        </form>
      </div>
    </div>
  );
}

export default LoginForm;
