import routesConfig from "../config";

//Layouts
// import { HeaderOnly } from "../layouts";

import Home from "../pages/Home";
import Account from "../pages/Account";
import Role from "../pages/Role";
import Permission from "../pages/Permission";
import About from "../pages/About";
import Profile from "../pages/Profile";
import Unauthorized from "../pages/Unauthorized";
import ChildrenOnlyLayout from "../layouts/ChildrenOnlyLayout";

//Public Routes
const publicRoutes = [
  { path: routesConfig.routes.default, component: Home },
  { path: routesConfig.routes.home, component: Home },
  { path: routesConfig.routes.account, component: Account },
  { path: routesConfig.routes.role, component: Role },
  { path: routesConfig.routes.permission, component: Permission },
  { path: routesConfig.routes.about, component: About },
  { path: routesConfig.routes.profile, component: Profile },
  {
    path: routesConfig.routes.unauthorized,
    component: Unauthorized,
    layout: ChildrenOnlyLayout,
  },
  // { path: routesConfig.routes.about, component: About, layout: HeaderOnly },
];

const privateRoutes = [];

export { publicRoutes, privateRoutes };
