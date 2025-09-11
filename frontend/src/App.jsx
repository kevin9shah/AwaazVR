import React, { useState } from 'react';
import Header from './Header';
import MainContent from './MainContent';
import UploadForm from './UploadForm';
import CodeDisplay from './CodeDisplay';
import Footer from './Footer';
import './App.css';

function App() {
  const [generatedCode, setGeneratedCode] = useState('');

  const handleCodeGeneration = (code) => {
    setGeneratedCode(code);
  };

  return (
    <div className="app-container">
      <Header />
      <main className="app-main">
        <div className="main-layout">
          <MainContent />
          <div className="upload-section">
            {!generatedCode ? (
              <UploadForm onCodeGeneration={handleCodeGeneration} />
            ) : (
              <CodeDisplay code={generatedCode} />
            )}
          </div>
        </div>
      </main>
      <Footer />
    </div>
  );
}

export default App;