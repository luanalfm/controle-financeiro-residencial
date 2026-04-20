//Utilização do axios para conectar-se aos endpoints da api e intercepctar erros 
import axios from "axios";

const api = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL || "https://localhost:7008/api",
  headers: {
    "Content-Type": "application/json",
  },
  timeout: 10000,
});

// Interceptor da resposta para retornar o erro
api.interceptors.response.use(
  (response) => response,
  (error) => {
    const message =
      error.response?.data?.message ||
      error.message ||
      "Erro inesperado na comunicação com o servidor.";

    return Promise.reject(new Error(message));
  }
);

export default api;
