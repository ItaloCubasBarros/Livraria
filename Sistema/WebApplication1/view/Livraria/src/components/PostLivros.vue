<template>
    <div class="post-livro">
      <h2>Postar Livro</h2>
      <form @submit.prevent="postLivro">
        <input type="text" v-model="livro.titulo" placeholder="Título" required>
        <input type="text" v-model="livro.autor" placeholder="Autor" required>
        <input type="text" v-model="livro.genero" placeholder="Gênero" required>
        <button type="submit">Postar Livro</button>
      </form>
      <p v-if="postError" class="error">{{ postError }}</p>
      <p v-if="postSuccess" class="success">{{ postSuccess }}</p>
    </div>
  </template>
  
  <script>
  import { PostLivros } from '../Services/apiService.js'; // Importe a função PostLivros
  
  export default {
    data() {
      return {
        livro: {
          titulo: '',
          autor: '',
          genero: ''
        },
        postError: '',
        postSuccess: ''
      };
    },
    methods: {
      async postLivro() {
        try {
          await PostLivros(this.livro);
          this.postSuccess = 'Livro postado com sucesso!';
          this.clearForm();
        } catch (error) {
          this.postError = 'Erro ao postar o livro. Verifique os dados e tente novamente.';
          console.error('Erro ao postar livro:', error);
        }
      },
      clearForm() {
        this.livro.titulo = '';
        this.livro.autor = '';
        this.livro.genero = '';
      }
    }
  };
  </script>
  
  <style scoped>
  .post-livro {
    max-width: 400px;
    margin: 0 auto;
    padding: 20px;
    border: 1px solid #ccc;
    border-radius: 5px;
  }
  
  .post-livro h2 {
    text-align: center;
  }
  
  .post-livro form {
    display: flex;
    flex-direction: column;
  }
  
  .post-livro input {
    margin-bottom: 10px;
    padding: 10px;
    border: 1px solid #ccc;
    border-radius: 3px;
  }
  
  .post-livro button {
    padding: 10px;
    background-color: #007bff;
    color: white;
    border: none;
    border-radius: 3px;
    cursor: pointer;
  }
  
  .post-livro button:hover {
    background-color: #0056b3;
  }
  
  .error {
    color: red;
    text-align: center;
    margin-top: 10px;
  }
  
  .success {
    color: green;
    text-align: center;
    margin-top: 10px;
  }
  </style>
  