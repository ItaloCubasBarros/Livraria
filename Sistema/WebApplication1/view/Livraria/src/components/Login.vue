<template>
    <div class="login-form">
      <h2>Entrar</h2>
      <form @submit.prevent="loginUser">
        <input type="email" v-model="formData.email" placeholder="Email" required>
        <input type="password" v-model="formData.password" placeholder="Senha" required>
        <button type="submit">Entrar</button>
      </form>
      <p v-if="loginError" class="error">{{ loginError }}</p>
    </div>
  </template>
  
  <script>
  import { loginUser } from '../Services/apiService.js';
  
  export default {
    data() {
      return {
        formData: {
          email: '',
          password: ''
        },
        loginError: ''
      };
    },
    methods: {
      async loginUser() {
        try {
          await loginUser(this.formData);
          
          // Limpa os dados do formulário após o login bem-sucedido
          this.formData.email = '';
          this.formData.password = '';
          
          // Redireciona para a página inicial após o login
          this.$router.push({ name: 'Home' });
          
          // Exibe mensagem de sucesso (pode usar um componente de alerta ou toast, por exemplo)
          alert('Login realizado com sucesso!');
        } catch (error) {
          // Manipula erros de login
          if (error.response && error.response.data && error.response.data.errors) {
            this.loginError = error.response.data.errors.ErrorMessages[0];
          } else {
            this.loginError = 'Erro ao tentar fazer login. Verifique suas credenciais e tente novamente.';
          }
        }
      }
    }
  };
  </script>
  
  <style scoped>
  /* Estilos para o formulário de login, se necessário */
  .login-form {
    max-width: 400px;
    margin: 0 auto;
    padding: 20px;
    border: 1px solid #ccc;
    border-radius: 5px;
  }
  
  .login-form h2 {
    text-align: center;
  }
  
  .login-form form {
    display: flex;
    flex-direction: column;
  }
  
  .login-form input {
    margin-bottom: 10px;
    padding: 10px;
    border: 1px solid #ccc;
    border-radius: 3px;
  }
  
  .login-form button {
    padding: 10px;
    background-color: #007bff;
    color: white;
    border: none;
    border-radius: 3px;
    cursor: pointer;
  }
  
  .login-form button:hover {
    background-color: #0056b3;
  }
  
  .error {
    color: red;
    text-align: center;
    margin-top: 10px;
  }
  </style>
  