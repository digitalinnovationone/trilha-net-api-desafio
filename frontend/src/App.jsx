import { AuthProvider } from "./context/AuthContext";

import Home from "./pages/Home";
import { LanguageProvider } from '../src/context/LanguageContext.js'; // Import necessário
import "react-responsive-carousel/lib/styles/carousel.min.css";
import { ThemeProvider } from './context/ThemeContext'  // Import necessário

export default function App() {
  return (
    <ThemeProvider>  {/* Envolve todo o escopo */}
      <LanguageProvider>  {/* Envolve todo o escopo */}
        <AuthProvider>
          <Home/>
        </AuthProvider>
      </LanguageProvider>
    </ThemeProvider>
  );
}
