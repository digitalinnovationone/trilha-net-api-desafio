import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App.jsx";
import "./index.css";

import { AuthProvider } from "./context/AuthContext";
import { LanguageProvider } from '../src/context/LanguageContext.js'; // Import necessário
import { ThemeProvider } from './context/ThemeContext'  // Import necessário

ReactDOM.createRoot(document.getElementById("root")).render(
    <React.StrictMode>
        <AuthProvider>
            <ThemeProvider>
                <LanguageProvider>  {/* Envolve todo o escopo */}
                        {/* Envolve todo o escopo */}
                    <App />
                </LanguageProvider>
            </ThemeProvider>
        </AuthProvider>
    </React.StrictMode>
);
