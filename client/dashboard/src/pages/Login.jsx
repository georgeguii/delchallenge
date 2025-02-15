import { useState } from 'react';

const Login = ({ onLogin }) => {
  const [accountNumber, setAccountNumber] = useState('');
  const [error, setError] = useState('');

  const handleLoginClick = () => {
    if (accountNumber.trim() === '') {
      return;
    }
    setError('');
    onLogin();
  }

  return (
    <div className="login-container">
      <div className="login-container">
      <input
        type="text"
        placeholder="Número da conta"
        className="account-input"
        value={accountNumber}
        onChange={(e) => setAccountNumber(e.target.value)}
      />
      {error && <p className="error-message">{error}</p>}
      <button onClick={handleLoginClick} className="login-button">Acessar sua conta</button>
      <button className="internal-access-button">Acesso interno</button>
    </div>
    </div>
    
  );
};

export default Login;
