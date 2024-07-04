<template>
    <nav class="navbar">
      <div class="container">
        <h1>Livraria</h1>
        <ul>
          <li><router-link to="/">Home</router-link></li>
          <li><router-link to="/PostLivro">Cadastrar Livro</router-link></li>
          <li><router-link to="/PostAlunos">Cadastrar Aluno</router-link></li>
          <li><router-link to="/PostEmprestimo">Emprestimo</router-link></li>
 
        </ul>
      </div>
    </nav>
  </template>
  
  <script>
  export default {
    // Lógica do componente, se necessário
  };
  </script>
  
  <style scoped>
  .navbar {
    background-color: #007bff;
    color: white;
    padding: 10px 0;
  }
  
  .container {
    display: flex;
    justify-content: space-between;
    align-items: center;
    max-width: 800px;
    margin: 0 auto;
  }
  
  ul {
    list-style-type: none;
    display: flex;
    justify-content: center;
    align-items: center;
  }
  
  li {
    margin: 0 10px;
  }
  
  li a {
    color: white;
    text-decoration: none;
  }
  
  li a:hover {
    text-decoration: underline;
  }
  </style>
  