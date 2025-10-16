// i18n.js
import i18n from 'i18next';
import { initReactI18next } from 'react-i18next';

// Importando arquivos de tradução
import translationEN from './locales/en.json';
import translationPT from './locales/pt.json';

// Configuração dos recursos (languages)
const resources = {
  en: {
    translation: translationEN.translation
  },
  pt: {
    translation: translationPT.translation
  }
};

// Inicializando i18next
i18n
  .use(initReactI18next) // Passando o i18n para o react-i18next
  .init({
    resources,
    lng: 'pt', // idioma padrão
    fallbackLng: 'en', // idioma de fallback caso a tradução não exista
    interpolation: {
      escapeValue: false // React já protege contra XSS
    }
  });

export default i18n;
