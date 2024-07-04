<template>
  
  <div class="emprestimo">
  
    <h2>Registrar Empréstimo</h2>
    <!-- Formulário de registro de empréstimo -->
    <form @submit.prevent="registrarEmprestimo">
      <label for="alunoId">Aluno:</label>
      <select v-model="emprestimo.alunoId" required>
        <option v-for="aluno in alunos" :key="aluno.id" :value="aluno.id">{{ aluno.nome }}</option>
      </select>
      <label for="livroId">Livro:</label>
      <select v-model="emprestimo.livroId" required>
        <option v-for="livro in livros" :key="livro.id" :value="livro.id">{{ livro.titulo }}</option>
      </select>
      <button type="submit">Registrar Empréstimo</button>
    </form>

    <!-- Mensagens de sucesso e erro -->
    <p v-if="postError" class="error">{{ postError }}</p>
    <p v-if="postSuccess" class="success">{{ postSuccess }}</p>

    <hr>

    <!-- Lista de empréstimos -->
    <h2>Lista de Empréstimos</h2>
    <ul v-if="emprestimos.length > 0">
      <li v-for="emprestimo in emprestimos" :key="emprestimo.id">
        <p><strong>Aluno:</strong> {{ emprestimo.aluno.nome }}</p>
        <p><strong>Livro:</strong> {{ emprestimo.livro.titulo }}</p>
        <p><strong>Data:</strong> {{ formatarData(emprestimo.data) }}</p>
        <p><strong>Hora:</strong> {{ emprestimo.hora }}</p>
        <button @click="deletarEmprestimo(emprestimo.id)">Deletar</button>
      </li>
    </ul>
    <p v-else>Nenhum empréstimo registrado.</p>
  </div>
</template>

<script>
import { GetAlunos, GetLivros, PostEmprestimo, GetEmprestimos, DeleteEmprestimo } from '../Services/apiService.js';
import Navbar from './Navbar.vue';

export default {
  data() {
    return {
      emprestimo: {
        alunoId: '',
        livroId: ''
      },
      alunos: [],
      livros: [],
      emprestimos: [],
      postError: '',
      postSuccess: ''
    };
  },
  async created() {
    try {
      this.alunos = await GetAlunos();
      this.livros = await GetLivros();
      this.carregarEmprestimos();
    } catch (error) {
      console.error('Erro ao buscar alunos, livros ou empréstimos:', error);
    }
  },
  methods: {
    async registrarEmprestimo() {
      try {
        // Busque os objetos completos de aluno e livro com base nos IDs selecionados
        const alunoSelecionado = this.alunos.find(aluno => aluno.id === this.emprestimo.alunoId);
        const livroSelecionado = this.livros.find(livro => livro.id === this.emprestimo.livroId);

        // Verifique se encontrou os alunos e livros correspondentes
        if (!alunoSelecionado || !livroSelecionado) {
          throw new Error('Aluno ou livro não encontrados.');
        }

        // Crie o objeto completo de empréstimo para enviar ao backend
        const emprestimoData = {
          aluno: alunoSelecionado,
          livro: livroSelecionado
        };

        // Faça a requisição POST usando o objeto completo de empréstimo
        await PostEmprestimo(emprestimoData);
        this.postSuccess = 'Empréstimo registrado com sucesso!';
        this.clearForm();
        this.carregarEmprestimos(); // Atualiza a lista de empréstimos após registro
      } catch (error) {
        this.postError = 'Erro ao registrar o empréstimo. Verifique os dados e tente novamente.';
        console.error('Erro ao registrar empréstimo:', error);
      }
    },
    async carregarEmprestimos() {
      try {
        this.emprestimos = await GetEmprestimos();
      } catch (error) {
        console.error('Erro ao carregar empréstimos:', error);
      }
    },
    async deletarEmprestimo(id) {
      try {
        await DeleteEmprestimo(id);
        this.postSuccess = 'Empréstimo deletado com sucesso!';
        this.carregarEmprestimos(); // Atualiza a lista de empréstimos após deleção
      } catch (error) {
        console.error('Erro ao deletar empréstimo:', error);
        this.postError = 'Erro ao deletar o empréstimo. Tente novamente mais tarde.';
      }
    },
    clearForm() {
      this.emprestimo.alunoId = '';
      this.emprestimo.livroId = '';
    },
    formatarData(data) {
      if (!data) return '';
      const dataObj = new Date(data);
      return dataObj.toLocaleDateString();
    }
  }
};
</script>

<style scoped>
/* Estilos semelhantes aos formulários anteriores */
.emprestimo {
  max-width: 400px;
  margin: 0 auto;
  padding: 20px;
  border: 1px solid #ccc;
  border-radius: 5px;
}

.emprestimo h2 {
  text-align: center;
}

.emprestimo form {
  display: flex;
  flex-direction: column;
}

.emprestimo select {
  margin-bottom: 10px;
  padding: 10px;
  border: 1px solid #ccc;
  border-radius: 3px;
}

.emprestimo button {
  padding: 10px;
  background-color: #007bff;
  color: white;
  border: none;
  border-radius: 3px;
  cursor: pointer;
}

.emprestimo button:hover {
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

.emprestimo ul {
  list-style-type: none;
  padding: 0;
}

.emprestimo li {
  padding: 5px;
  border-bottom: 1px solid #ccc;
}
</style>
