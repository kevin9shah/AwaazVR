import React, { useState } from 'react';
import './CodeDisplay.css';

const CodeDisplay = ({ code }) => {
    const [copied, setCopied] = useState(false);

    const handleCopy = () => {
        navigator.clipboard.writeText(code);
        setCopied(true);
        setTimeout(() => setCopied(false), 2000);
    };

    const handleListen = () => {
        // Placeholder for listen functionality
        console.log('Listen button clicked');
    };

    return (
        <div className="code-display-container">
            <div className="code-display-header">
                <h2>Generated Code</h2>
                <div className="code-display-actions">
                    <button onClick={handleListen} className="listen-btn" disabled={!code}>Listen</button>
                    <button onClick={handleCopy} className="copy-btn" disabled={!code}>
                        {copied ? 'Copied!' : 'Copy'}
                    </button>
                </div>
            </div>
            <pre className="code-block">
                {code ? <code>{code}</code> : <code className="placeholder-code">Your generated code will appear here...</code>}
            </pre>
        </div>
    );
};

export default CodeDisplay;