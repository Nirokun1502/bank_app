/* eslint-disable react/prop-types */
import { useState } from "react";
import { UserIcon } from "../../images/icons";
import MenuItem from "../MenuItem";
import "./UserMenu.scss";
// import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faAddressCard } from "@fortawesome/free-solid-svg-icons";
import ChangePasswordForm from "../../forms/ChangePasswordForm";
import axios from "axios";
import apiConfig from "../../config";

function UserMenu() {
  const [changePassForm, setChangePassForm] = useState(false);

  const handleChangePassword = () => {
    setChangePassForm(true);
  };

  const handleLogout = async () => {
    try {
      const jwt = localStorage.getItem("jwt");
      const apiUrl = apiConfig.apiurls.logout;
      const body = {
        key: "value",
      };
      const config = {
        headers: { Authorization: `Bearer ${jwt}` },
      };
      const response = await axios.post(apiUrl, body, config);
      localStorage.removeItem("jwt");
      localStorage.removeItem("username");
      console.log(response.data);
    } catch (error) {
      console.error("Error fetching data:", error);
    }
  };

  const items = [
    {
      icon: faAddressCard,
      name: "Trang cá nhân",
      onClick: () => {
        console.log("hello!");
      },
      to: "/profile",
    },
    {
      icon: faAddressCard,
      name: "Đổi mật khẩu",
      onClick: () => {
        handleChangePassword();
      },
    },
    {
      icon: faAddressCard,
      name: "Đăng xuất",
      onClick: () => {
        handleLogout();
      },
    },
    { icon: faAddressCard, name: "About us", onClick: () => {}, to: "/about" },
  ];

  const [hover, setHover] = useState(false);
  return (
    <div className={`usermenu-container `}>
      <div
        className={`usermenu-trigger ${hover ? "menu-hover" : ""}`}
        onMouseOver={() => setHover(true)}
        onMouseOut={() => setHover(false)}
      >
        <button className="user-button">
          <UserIcon />
        </button>
        {localStorage.getItem("username") ? (
          <p>{localStorage.getItem("username")}</p>
        ) : (
          <p>Nguyen Quoc Phi</p>
        )}
      </div>

      {hover && (
        <div
          className="dropdown-menu"
          onMouseOver={() => setHover(true)}
          onMouseOut={() => setHover(false)}
        >
          <ul>
            {items.map((item, index) => {
              return (
                <MenuItem
                  key={`menuitem${index}`}
                  name={item.name}
                  icon={item.icon}
                  onClick={item.onClick}
                  to={item.to}
                />
              );
            })}

            {/* set logic khi hover vào usermenu thì thêm class cho thẻ đó và mất hover thì tắt class đó đi */}
          </ul>
        </div>
      )}

      {changePassForm && <ChangePasswordForm onClick={setChangePassForm} />}
    </div>
  );
}

export default UserMenu;
