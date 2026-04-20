// Serviço de Categorias — Aqui isolamos a comunicação com a API em um serviço + mapeamos os dados da api para o front(semelhante a um mapper)
import api from "@/api/axios";
import type { Category, CategoryFormData, CategoryPurpose } from "@/types";

const ENDPOINT = "/categorias";

const purposeMap: Record<number, CategoryPurpose> = {
  0: "despesa",
  1: "receita",
  2: "ambas",
};

const reversePurposeMap: Record<CategoryPurpose, number> = {
  despesa: 0,
  receita: 1,
  ambas: 2,
};

const mapToCategory = (data: any): Category => ({
  id: data.id,
  description: data.descricao,
  purpose: purposeMap[data.finalidade],
});

const mapToApi = (data: CategoryFormData) => ({
  descricao: data.description,
  finalidade: reversePurposeMap[data.purpose],
});

export const categoryService = {
  async getAll(): Promise<Category[]> {
    const { data } = await api.get(ENDPOINT);
    return data.map(mapToCategory);
  },

  async create(payload: CategoryFormData): Promise<Category> {
    const { data } = await api.post(ENDPOINT, mapToApi(payload));
    return mapToCategory(data);
  },
};