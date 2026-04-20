// Serviço de Pessoas — Aqui isolamos a comunicação com a API em um serviço + mapeamos os dados da api para o front(semelhante a um mapper)
import api from "@/api/axios";
import type { Person, PersonFormData } from "@/types";

const ENDPOINT = "/pessoas";

const mapToPerson = (data: any): Person => ({
  id: data.id,
  name: data.nome,
  age: data.idade,
});

const mapToApi = (data: PersonFormData) => ({
  nome: data.name,
  idade: data.age,
});

export const personService = {
  async getAll(): Promise<Person[]> {
    const { data } = await api.get(ENDPOINT);
    return data.map(mapToPerson);
  },

  async getById(id: string): Promise<Person> {
    const { data } = await api.get(`${ENDPOINT}/${id}`);
    return mapToPerson(data);
  },

  async create(payload: PersonFormData): Promise<Person> {
    const { data } = await api.post(ENDPOINT, mapToApi(payload));
    return mapToPerson(data);
  },

  async update(id: string, payload: PersonFormData): Promise<Person> {
    const { data } = await api.put(
      `${ENDPOINT}/${id}`,
      mapToApi(payload)
    );
    return mapToPerson(data);
  },

  async remove(id: string): Promise<void> {
    await api.delete(`${ENDPOINT}/${id}`);
  },
};