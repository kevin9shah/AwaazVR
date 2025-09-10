import React from 'react';
import Header from './Header';
import UploadForm from './UploadForm';
import Footer from './Footer';
import './App.css';

function App() {
  return (
    <div className="app-container">
      <Header />
      <main className="app-main">
        <div className="upload-wrapper">
          <UploadForm />
        </div>
      </main>
      <Footer />
    </div>
  );
}

export default App;
