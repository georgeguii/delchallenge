import { useState } from 'react';
import Layout from './components/Layout';
import Dashboard from './pages/Dashboard';
import Login from './pages/Login';

function App() {
  const [isAuthenticated, setIsAuthenticated] = useState(false);

  // Função chamada quando o usuário faz login
  const handleLogin = () => {
    setIsAuthenticated(true);
  };

  return (
    <>
    {isAuthenticated ? (
      <Layout>
        <Dashboard />
      </Layout>
    ) : (
      <Login onLogin={handleLogin} />
    )}
  </>
  );
}

export default App;