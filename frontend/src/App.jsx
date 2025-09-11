import React from 'react';
import Header from './Header';
import MainContent from './MainContent';
import UploadForm from './UploadForm';
import Footer from './Footer';
import './App.css';

function App() {
  return (
    <div className="app-container">
      <Header />
      <main className="app-main">
        <div className="main-layout">
          <MainContent />
          <div className="upload-section">
            <UploadForm />
          </div>
        </div>
      </main>
      <Footer />
    </div>
  );
}

export default App;
