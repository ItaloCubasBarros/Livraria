<template>
    <div class="post-aluno">
      <h2>Cadastrar Aluno</h2>
      <form @submit.prevent="postAluno">
        <input type="text" v-model="aluno.Nome" placeholder="Nome do aluno" required>
        <input type="text" v-model="aluno.Cpf" placeholder="CPF do aluno" required>
        <input type="text" v-model="aluno.email" placeholder="Email do aluno" required>
        <input type="text" v-model="aluno.telefone" placeholder="Telefone do aluno" required>
        <input type="text" v-model="aluno.endereco" placeholder="Endereço do aluno" required>
        <input type="text" v-model="aluno.sexo" placeholder="Sexo: M ou F" required>
  
     
  
        <button type="submit">Cadastrar Aluno</button>
      </form>
      <p v-if="postError" class="error">{{ postError }}</p>
      <p v-if="postSuccess" class="success">{{ postSuccess }}</p>
  

   
    </div>
  </template>
  
  <script>
  import { GetLivros, PostAluno, GetAlunos } from '../Services/apiService.js'; // Importe as funções necessárias do apiService.js
  
  export default {
    data() {
      return {
        aluno: {
          Nome: '',
          Cpf: '',
          email: '',
          telefone: '',
          endereco: '',
          sexo: '',
          idLivro: ''
        },
        livros: [],
        alunos: [],
        postError: '',
        postSuccess: ''
      };
    },
    async created() {
      try {
        // Busca os livros ao carregar a página
        this.livros = await GetLivros();
  
        // Busca os alunos cadastrados
        this.alunos = await GetAlunos();
      } catch (error) {
        console.error('Erro ao buscar livros ou alunos:', error);
      }
    },
    methods: {
      async postAluno() {
        try {
          await PostAluno(this.aluno);
          this.postSuccess = 'Aluno cadastrado com sucesso!';
          this.clearForm();
  
          // Atualiza a lista de alunos após cadastrar um novo aluno
          this.alunos = await GetAlunos();
        } catch (error) {
          this.postError = 'Erro ao cadastrar o aluno. Verifique os dados e tente novamente.';
          console.error('Erro ao cadastrar aluno:', error);
        }
      },
      clearForm() {
        this.aluno.Nome = '';
        this.aluno.Cpf = '';
        this.aluno.email = '';
        this.aluno.telefone = '';
        this.aluno.endereco = '';
        this.aluno.sexo = '';
        this.aluno.idLivro = '';
      },
      async getLivroTitulo(idLivro) {
        const livro = this.livros.find(l => l.id === idLivro);
        return livro ? livro.titulo : 'Livro não encontrado';
      }
    }
  };
  </script>
  
  <style scoped>
  .post-aluno {
    max-width: 400px;
    margin: 0 auto;
    padding: 20px;
    border: 1px solid #ccc;
    border-radius: 5px;
  }
  
  .post-aluno h2 {
    text-align: center;
  }
  
  .post-aluno form {
    display: flex;
    flex-direction: column;
  }
  
  .post-aluno input,
  .post-aluno select {
    margin-bottom: 10px;
    padding: 10px;
    border: 1px solid #ccc;
    border-radius: 3px;
  }
  
  .post-aluno button {
    padding: 10px;
    background-color: #007bff;
    color: white;
    border: none;
    border-radius: 3px;
    cursor: pointer;
  }
  
  .post-aluno button:hover {
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
  