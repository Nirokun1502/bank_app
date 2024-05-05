const apiurls = {
  //acount
  account: "https://localhost:7121/api/Account",
  //authen
  register: "https://localhost:7121/api/Authentication/register",
  changepassword: "https://localhost:7121/api/Authentication/changepassword",
  login: "https://localhost:7121/api/Authentication/login",
  logout: "https://localhost:7121/api/Authentication/logout",
  myinfo: "https://localhost:7121/api/Authentication/getaccountinfo",
  //permission
  permission: "https://localhost:7121/api/Permission",
  mypermission: "https://localhost:7121/api/Permission/GetAccountPermissions",
  //role
  role: "https://localhost:7121/api/Role",
};

export default apiurls;
