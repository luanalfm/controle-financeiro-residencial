// Serviço de Transações — Aqui isolamos a comunicação com a API em um serviço + mapeamos os dados da api para o front(semelhante a um mapper)
import api from "@/api/axios";
import type { Transaction, TransactionFormData, TransactionType } from "@/types";

const ENDPOINT = "/transacoes";

const typeMap: Record<number, TransactionType> = {
  0: "despesa",
  1: "receita",
};

const reverseTypeMap: Record<TransactionType, number> = {
  despesa: 0,
  receita: 1,
};

const mapToTransaction = (data: any): Transaction => ({
  id: data.id,
  description: data.descricao,
  amount: data.valor,
  type: typeMap[data.tipo],
  categoryId: data.categoriaId,
  personId: data.pessoaId,
});

const mapToApi = (data: TransactionFormData) => ({
  descricao: data.description,
  valor: data.amount,
  tipo: reverseTypeMap[data.type],
  categoriaId: data.categoryId,
  pessoaId: data.personId,
});

export const transactionService = {
  async getAll(): Promise<Transaction[]> {
    const { data } = await api.get(ENDPOINT);
    return data.map(mapToTransaction);
},

  async create(payload: TransactionFormData): Promise<Transaction> {
    const { data } = await api.post(ENDPOINT, mapToApi(payload));
    return mapToTransaction(data);
  },
};