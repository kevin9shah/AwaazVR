import React from 'react';
import './MainContent.css';

const MainContent = () => {
    return (
        <div className="main-content">
            <div className="content-section">
                <div className="content-header">
                    <h1>Transform Your Presentations with VR Technology</h1>
                    <p className="content-subtitle">
                        Step into the future of presentation training with our cutting-edge virtual reality platform
                    </p>
                </div>

                <div className="features-grid">
                    {/* <div className="feature-card">
                        <div className="feature-icon">
                            <svg fill="none" viewBox="0 0 24 24" stroke="currentColor">
                                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M15 10l4.553-2.276A1 1 0 0121 8.618v6.764a1 1 0 01-1.447.894L15 14M5 18h8a2 2 0 002-2V8a2 2 0 00-2-2H5a2 2 0 00-2 2v8a2 2 0 002 2z" />
                            </svg>
                        </div>
                        <h3>Immersive Practice</h3>
                        <p>Practice your presentations in realistic virtual environments that simulate real-world scenarios.</p>
                    </div> */}

                    <div className="feature-card">
                        <div className="feature-icon">
                            <svg fill="none" viewBox="0 0 24 24" stroke="currentColor">
                                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M9 19v-6a2 2 0 00-2-2H5a2 2 0 00-2 2v6a2 2 0 002 2h2a2 2 0 002-2zm0 0V9a2 2 0 012-2h2a2 2 0 012 2v10m-6 0a2 2 0 002 2h2a2 2 0 002-2m0 0V5a2 2 0 012-2h2a2 2 0 012 2v14a2 2 0 01-2 2h-2a2 2 0 01-2-2z" />
                            </svg>
                        </div>
                        <h3>Real-time Analytics</h3>
                        <p>Get instant feedback on your presentation skills with detailed analytics and performance metrics.</p>
                    </div>

                    <div className="feature-card">
                        <div className="feature-icon">
                            <svg fill="none" viewBox="0 0 24 24" stroke="currentColor">
                                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M13 10V3L4 14h7v7l9-11h-7z" />
                            </svg>
                        </div>
                        <h3>AI-Powered Coaching</h3>
                        <p>Receive personalized coaching tips and suggestions powered by advanced AI technology.</p>
                    </div>
                </div>

                {/* <div className="cta-section">
          <h2>Ready to Elevate Your Presentation Skills?</h2>
          <p>Join thousands of professionals who have transformed their communication skills with VR training.</p>
        </div> */}
            </div>
        </div>
    );
};

export default MainContent;
