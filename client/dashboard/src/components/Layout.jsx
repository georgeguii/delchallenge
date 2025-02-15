import Sidebar from './Sidebar';
import Navbar from './Navbar';

const Layout = ({ children }) => {
  return (
    <div className="app-container">
      <Sidebar />
      <section id="content">
        <Navbar />
        <main>{children}</main>
      </section>
    </div>
  );
};

export default Layout;
