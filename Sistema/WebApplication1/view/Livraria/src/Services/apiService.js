// apiService.js

import axios from 'axios';

const apiClient = axios.create({
  baseURL: 'https://localhost:7217',
  headers: {
    'Content-Type': 'application/json'
  }
});

export const registerUser = async (userData) => {
  try {
    const response = await apiClient.post('/api/Auth/Register', userData);
    return response.data;
  } catch (error) {
    throw error;
  }
};

export const loginUser = async (credentials) => {
  try {
    const response = await apiClient.post('/api/Auth/Authenticate', credentials);
    return response.data;
  } catch (error) {
    throw error;
  }
};

export const GetLivros = async (credentials) => {
    try {
      const response = await apiClient.get('/api/Livro', credentials);
      return response.data;
    } catch (error) {
      throw error;
    }
  };

  export const GetAlunoById = async (alunoId) => {
    try {
      const response = await apiClient.get(`/api/Aluno/${alunoId}`);
      return response.data;
    } catch (error) {
      throw error;
    }
  };

  export const DeleteLivro = async (livroId) => {
    try {
      const response = await apiClient.delete(`/api/Livro/${livroId}`);
      return response.data; // Pode retornar algo se necessário
    } catch (error) {
      if (error.response && error.response.status === 404) {
        throw new Error('Livro não encontrado.');
      } else if (error.response && error.response.data && error.response.data.title) {
        throw new Error(`Erro ao excluir livro: ${error.response.data.title}`);
      } else {
        throw new Error('Erro desconhecido ao excluir o livro.');
      }
    }
  };

  export const DeleteEmprestimo = async (emprestimoId) => {
    try {
      const response = await apiClient.delete(`/api/Emprestimo/${emprestimoId}`);
      return response.data; // Pode retornar algo se necessário
    } catch (error) {
      if (error.response && error.response.status === 404) {
        throw new Error('Empréstimo não encontrado.');
      } else if (error.response && error.response.data && error.response.data.title) {
        throw new Error(`Erro ao excluir empréstimo: ${error.response.data.title}`);
      } else {
        throw new Error('Erro desconhecido ao excluir o empréstimo.');
      }
    }
  };
  
  export const GetLivroById = async (livroId) => {
    try {
      const response = await apiClient.get(`/api/Livro/${livroId}`);
      return response.data;
    } catch (error) {
      throw error;
    }
  };
''  

  

  export const PostLivros = async (livroData) => {
    try {
        const response = await apiClient.post('/api/Livro', livroData);
        return response.data;
    } catch (error) {
        throw error;
    }
};

  export const PostAluno = async (alunoData) => {
    try {
        const response = await apiClient.post('/api/Aluno', alunoData);
        return response.data;
    } catch (error) {
        throw error;
    }
};

export const PostEmprestimo = async (emprestimoData) => {
  try {
    const response = await apiClient.post('/api/Emprestimo', emprestimoData); // Corrigido de EmprestimoDataData para emprestimoData
    return response.data;
  } catch (error) {
    throw error;
  }
};








  



  export const GetEmprestimos = async () => {
    try {
      const response = await apiClient.get('/api/Emprestimo');
      return response.data;
    } catch (error) {
      throw error;
    }
  };


  export const GetAlunos = async (credentials) => {
    try {
      const response = await apiClient.get('/api/Aluno', credentials);
      return response.data;
    } catch (error) {
      throw error;
    }
  };
