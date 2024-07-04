<template>
    <div class="register-form">
      <h2>Criar Conta</h2>
      <form @submit.prevent="registerUser">
        <input type="text" v-model="formData.userName" placeholder="Nome de usuário" required>
        <input type="email" v-model="formData.email" placeholder="Email" required>
        <input type="text" v-model="formData.cpf" placeholder="CPF" required>
        <input type="password" v-model="formData.password" placeholder="Senha" required>
        <input type="password" v-model="formData.confirmPassword" placeholder="Confirmar Senha" required>
        <input type="text" v-model="formData.phoneNumber" placeholder="Telefone">
        <select v-model="formData.roleName" required>
          <option disabled value="">Selecione um papel</option>
          <option value="user">Usuário</option>
          <option value="admin">Administrador</option>
        </select>
        <button type="submit">Registrar</button>
      </form>
      <p v-if="registerError" class="error">{{ registerError }}</p>
      <router-link to="/login">Já tem uma conta? Faça login aqui.</router-link>
    </div>
  </template>
  
  <script>
  import { registerUser } from '../Services/apiService.js';
  
  export default {
    data() {
      return {
        formData: {
          userName: '',
          email: '',
          cpf: '',
          password: '',
          confirmPassword: '',
          phoneNumber: '',
          roleName: '',
          errorMessages: ''  // Campo adicionado para armazenar mensagens de erro
        },
        registerError: ''
      };
    },
    methods: {
      async registerUser() {
        try {
          // Limpa o campo 'ErrorMessages' para garantir que não haja texto visível ao usuário
          this.formData.errorMessages = '';
  
          await registerUser(this.formData);
          // Reset do formulário após o registro bem-sucedido, se necessário
          this.formData = {
            userName: '',
            email: '',
            cpf: '',
            password: '',
            confirmPassword: '',
            phoneNumber: '',
            roleName: '',
            errorMessages: ''
          };
          alert('Registro realizado com sucesso!');
        } catch (error) {
          // Manipula erros de registro
          if (error.response && error.response.data && error.response.data.errors) {
            this.registerError = error.response.data.errors.ErrorMessages[0];
          } else {
            this.registerError = 'Erro ao registrar. Verifique os dados e tente novamente.';
          }
        }
      }
    }
  };
  </script>
  
  <style scoped>
  .register-form {
    max-width: 400px;
    margin: 0 auto;
    padding: 20px;
    border: 1px solid #ccc;
    border-radius: 5px;
  }
  
  .register-form h2 {
    text-align: center;
  }
  
  .register-form form {
    display: flex;
    flex-direction: column;
  }
  
  .register-form input,
  .register-form select {
    margin-bottom: 10px;
    padding: 10px;
    border: 1px solid #ccc;
    border-radius: 3px;
  }
  
  .register-form button {
    padding: 10px;
    background-color: #007bff;
    color: white;
    border: none;
    border-radius: 3px;
    cursor: pointer;
  }
  
  .register-form button:hover {
    background-color: #0056b3;
  }
  
  .error {
    color: red;
    text-align: center;
    margin-top: 10px;
  }
  
  .router-link {
    text-align: center;
    margin-top: 10px;
  }
  </style>
  