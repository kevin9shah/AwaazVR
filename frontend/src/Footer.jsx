import React from 'react';
import './Footer.css';

const Footer = () => {
  const currentYear = new Date().getFullYear();

  return (
    <footer className="footer">
      <div className="footer-container">
        <div className="footer-top">
          <div className="footer-left">
            <p>© {currentYear} AWAAZ VR. All rights reserved.</p>
          </div>
          <div className="footer-right">
            <a href="#">Privacy Policy</a>
            <a href="#">Terms of Service</a>
            <a href="#">Support</a>
          </div>
        </div>
        <div className="footer-bottom">
          <p>Empowering better presentations through immersive VR technology</p>
        </div>
      </div>
    </footer>
  );
};

export default Footer;
