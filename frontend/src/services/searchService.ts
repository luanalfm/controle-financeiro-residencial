// Serviço de Consultas — Aqui isolamos a comunicação com a API em um serviço 
import api from "@/api/axios";

const ENDPOINT = "/consultas";

export interface TotaisPorPessoaResponse {
  porPessoa: {
    pessoaId: string;
    nome: string;
    totalReceitas: number;
    totalDespesas: number;
    saldo: number;
  }[];
  totalGeral: {
    totalReceitas: number;
    totalDespesas: number;
    saldoLiquido: number;
  };
}

export interface TotaisPorCategoriaResponse {
  porCategoria: {
    descricaoCategoria: string;
    totalReceitas: number;
    totalDespesas: number;
    saldo: number;
  }[];
  totalGeral: {
    totalReceitas: number;
    totalDespesas: number;
    saldoLiquido: number;
  };
}

export const consultasService = {
  async getTotaisPorPessoa(): Promise<TotaisPorPessoaResponse> {
    const { data } = await api.get(`${ENDPOINT}/totais-por-pessoa`);
    return data;
  },

  async getTotaisPorCategoria(): Promise<TotaisPorCategoriaResponse> {
    const { data } = await api.get(`${ENDPOINT}/totais-por-categoria`);
    return data;
  },
};