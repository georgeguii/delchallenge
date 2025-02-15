const Dashboard = () => {
    return (
      <div>
        <div className="head-title">
          <div className="left">
            <h1>Dashboard</h1>
            <ul className="breadcrumb">
              <li><a href="#">Dashboard</a></li>
              <li><i className="bx bx-chevron-right"></i></li>
              <li><a className="active" href="#">Home</a></li>
            </ul>
          </div>
        </div>
        <ul className="box-info">
          <li>
            <i className="bx bxs-group"></i>
            <span className="text">
              <h3>2834</h3>
              <p>Contas</p>
            </span>
          </li>
        </ul>
      </div>
    );
  };
  
  export default Dashboard;
  