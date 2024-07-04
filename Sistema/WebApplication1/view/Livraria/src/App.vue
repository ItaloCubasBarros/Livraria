<template>
  <div id="app">
    <router-view/>
  </div>
</template>

<script>
import { createApp } from 'vue';
import router from './Routes/router'; // Certifique-se de importar corretamente o router

export default {
  name: 'App',
  setup() {
    // Certifique-se de criar a aplicação Vue com o router configurado
    const app = createApp({});
    app.use(router);
    return { app };
  }
};
</script>

<style scoped>
/* Estilos específicos para o componente App.vue */
#app {
  font-family: Arial, Helvetica, sans-serif;
}
</style>
