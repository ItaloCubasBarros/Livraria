import { createApp } from 'vue';
import App from './App.vue';
import router from './Routes/router'; // Certifique-se de importar corretamente o router

createApp(App)
  .use(router) // Use o router configurado
  .mount('#app');
