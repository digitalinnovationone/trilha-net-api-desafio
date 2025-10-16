// LanguageContext.js
import { createContext, useState, useContext } from 'react';
import i18n from '../i18n'

const LanguageContext = createContext();

export const LanguageProvider = ({ children }) => {
  const [language, setLanguage] = useState(i18n.language || 'pt');

  const changeLanguage = (lng) => {
    i18n.changeLanguage(lng);
    setLanguage(lng);
  };

  return (
    <LanguageContext.Provider value={{ language, changeLanguage }}>
      {children}
    </LanguageContext.Provider>
  );
};

// Hook para usar o contexto facilmente
export const useLanguage = () => useContext(LanguageContext);