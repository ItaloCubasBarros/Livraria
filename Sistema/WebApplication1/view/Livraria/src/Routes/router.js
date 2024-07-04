import { createRouter, createWebHistory } from 'vue-router';
import Home from '@/components/Home.vue'; // Importe a página Home
import Register from '@/components/Register.vue';
import Login from '@/components/Login.vue';
import PostLivros from '@/components/PostLivros.vue';
import PostAlunos from '@/components/PostAlunos.vue';
import PostEmprestimo from '@/components/PostEmprestimo.vue';

const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: '/',
      name: 'Home',
      component: Home
    },
    {
      path: '/register',
      name: 'Register',
      component: Register
    },
    {
      path: '/login',
      name: 'Login',
      component: Login
    },
    {
        path: '/PostLivro',
        name: 'PostLivros',
        component: PostLivros
    },
    {
        path: '/PostAlunos',
        name: 'PostAlunos',
        component: PostAlunos
    },
    {
        path: '/PostEmprestimo',
        name: 'PostEmprestimo',
        component: PostEmprestimo
    }
  ]
});

export default router;
