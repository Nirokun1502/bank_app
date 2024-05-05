import Header from "../../components/Header";
import Navigation from "../../components/Navigation";
import "./DefaultLayout.scss";

// eslint-disable-next-line react/prop-types
function DefaultLayout({ children }) {
  return (
    <div className="wrapper">
      <Header />
      <div className="container">
        <Navigation />
        <div className="content">{children}</div>
      </div>
    </div>
  );
}

export default DefaultLayout;
