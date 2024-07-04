<template>
  <div class="home">
    <Navbar />
    <div class="container">
      <h1>Bem-vindo à nossa livraria!</h1>
      <div class="livros">
        <template v-if="livros.length > 0">
          <div class="livro-card" v-for="livro in livros" :key="livro.id">
            <h2>{{ livro.titulo }}</h2>
            <p>Autor: {{ livro.autor }}</p>
            <p>Gênero: {{ livro.genero }}</p>
            <!-- Botão de exclusão -->
            <button @click="deletarLivro(livro.id)">Excluir</button>
          </div>
        </template>
        <template v-else>
          <p>Nenhum livro encontrado.</p>
        </template>
      </div>
    </div>
  </div>
</template>

  
<script>
import Navbar from '@/components/Navbar.vue'; // Importe o componente Navbar
import { GetLivros, DeleteLivro } from '../Services/apiService.js'; // Importe a função DeleteLivro

export default {
  name: 'Home',
  components: {
    Navbar
  },
  data() {
    return {
      livros: []
    };
  },
  async created() {
    try {
      // Busca os livros ao carregar a página
      this.livros = await GetLivros();
    } catch (error) {
      console.error('Erro ao buscar livros:', error);
    }
  },
  methods: {
    async deletarLivro(livroId) {
      try {
        await DeleteLivro(livroId);
        this.livros = this.livros.filter(livro => livro.id !== livroId);
        console.log('Livro excluído com sucesso!');
        // Adicione uma mensagem de sucesso ou atualizações visuais conforme necessário
      } catch (error) {
        console.error('Erro ao excluir livro:', error);
        // Trate o erro, exiba uma mensagem de erro para o usuário, etc.
      }
    }
  }
};
</script>

  
  <style scoped>
  .home {
    font-family: Arial, sans-serif;
  }
  
  .container {
    max-width: 800px;
    margin: 20px auto;
    padding: 20px;
    border: 1px solid #ccc;
    border-radius: 5px;
    text-align: center;
  }
  
  .livros {
    display: grid;
    grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
    gap: 20px;
    margin-top: 20px;
  }
  
  .livro-card {
    background-color: #f0f0f0;
    padding: 10px;
    border: 1px solid #ccc;
    border-radius: 5px;
  }
  
  .livro-card h2 {
    margin-bottom: 10px;
  }
  
  .livro-card p {
    margin: 5px 0;
  }
  </style>
  