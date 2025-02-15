const Navbar = () => {
    return (
      <nav>
        <i className="bx bx-menu"></i>
        <a href="#" className="nav-link"></a>
        <form action="#">
          <div className="form-input">
            <input type="search" placeholder="Search..." />
            <button type="submit" className="search-btn">
              <i className="bx bx-search"></i>
            </button>
          </div>
        </form>
      </nav>
    );
  };
  
  export default Navbar;
  