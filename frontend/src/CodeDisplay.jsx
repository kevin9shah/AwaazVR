import React, { useState } from 'react';

const CodeDisplay = ({ code }) => {
    const [copied, setCopied] = useState(false);

    const handleCopy = () => {
        navigator.clipboard.writeText(code);
        setCopied(true);
        setTimeout(() => setCopied(false), 2000);
    };

    // const handleListen = () => {
    //     // Placeholder for listen functionality
    //     console.log('Listen button clicked');
    // };

    return (
        <>
            <style>{`
                .code-display-container {
                    background: rgba(0, 217, 255, 0.05);
                    border: 1px solid rgba(0, 217, 255, 0.2);
                    border-radius: 10px;
                    padding: 2rem;
                    margin-top: 2rem;
                }

                .code-display-header {
                    display: flex;
                    justify-content: space-between;
                    align-items: center;
                    margin-bottom: 1rem;
                }

                .code-display-header h2 {
                    font-size: 1.5rem;
                    font-weight: 600;
                    color: #ffffff;
                }

                .code-display-actions {
                    display: flex;
                    gap: 1rem;
                }

                .listen-btn, .copy-btn {
                    background: transparent;
                    border: 1px solid #00d9ff;
                    color: #00d9ff;
                    padding: 0.5rem 1rem;
                    border-radius: 5px;
                    cursor: pointer;
                    transition: all 0.3s ease;
                }

         

                .code-block {
                    background: #011627;
                    border-radius: 5px;
                    padding: 1rem;
                    overflow-x: auto;
                    color: #d6deeb;
                    font-family: 'Source Code Pro', monospace;
                }

                .placeholder-code {
                    color: #a7cde8;
                    font-style: italic;
                }

                pre code {
                    display: block;
                    white-space: pre-wrap;
                    word-break: break-word;
                }
            `}</style>

            <div className="code-display-container">
                <div className="code-display-header">
                    <h2>Generated Code</h2>
                    <div className="code-display-actions">
                        {/* <button onClick={handleListen} className="listen-btn" disabled={!code}>Listen</button> */}
                        <button onClick={handleCopy} className="copy-btn" disabled={!code}>
                            {copied ? 'Copied!' : 'Copy'}
                        </button>
                    </div>
                </div>
                <pre className="code-block">
                    {code 
                        ? <code>{code}</code> 
                        : <code className="placeholder-code">Your generated code will appear here...</code>}
                </pre>
            </div>
        </>
    );
};

export default CodeDisplay;
