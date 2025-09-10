import React from 'react';
import './Header.css';

const Header = () => {
  return (
    <header className="header">
      <div className="header-container">
        <div className="header-inner">
          <div className="logo">
            <h1>AWAAZ</h1>
          </div>

          <nav className="nav-desktop">
            <div className="nav-links">
              <a href="#">Home</a>
              <a href="#">About</a>
              <a href="#">Contact</a>
            </div>
          </nav>

          <div className="nav-mobile">
            <button>
              <svg className="menu-icon" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M4 6h16M4 12h16M4 18h16" />
              </svg>
            </button>
          </div>
        </div>
      </div>
    </header>
  );
};

export default Header;
