
//Página de Pessoas — com funções CRUD completas.
import { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { useAppStore } from "@/store/useAppStore";
import { personSchema, type PersonSchemaType } from "@/utils/schemas";
import Modal from "@/components/Modal";
import { FormInput } from "@/components/FormFields";
import EmptyState from "@/components/EmptyState";
import { Users, Plus, Pencil, Trash2 } from "lucide-react";
import type { Person } from "@/types";
import { toast } from "sonner";

const PersonsPage = () => {
  const { persons, addPerson, updatePerson, deletePerson, loadingPersons } =
    useAppStore();
  const [modalOpen, setModalOpen] = useState(false);
  const [editing, setEditing] = useState<Person | null>(null);
  const [confirmDelete, setConfirmDelete] = useState<string | null>(null);

  const {
    register,
    handleSubmit,
    reset,
    formState: { errors, isSubmitting },
  } = useForm<PersonSchemaType>({
    resolver: zodResolver(personSchema),
  });

const fetchPersons = useAppStore((state) => state.fetchPersons);

useEffect(() => {
  fetchPersons();
}, [fetchPersons]);

  const openCreate = () => {
    setEditing(null);
    reset({ name: "", age: undefined as unknown as number });
    setModalOpen(true);
  };

  const openEdit = (person: Person) => {
    setEditing(person);
    reset({ name: person.name, age: person.age });
    setModalOpen(true);
  };

  const onSubmit = async (data: PersonSchemaType) => {
    if (editing) {
      await updatePerson(editing.id, data);
      toast.success("Pessoa atualizada com sucesso!");
    } else {
      await addPerson(data);
      toast.success("Pessoa criada com sucesso!");
    }
    setModalOpen(false);
    reset();
  };

  const handleDelete = async (id: string) => {
    await deletePerson(id);
    toast.success("Exclusão realizada com sucesso!");
    setConfirmDelete(null);
  };

  return (
    <div>
      <div className="flex items-center justify-between mb-6">
        <h1 className="page-title mb-0">Pessoas</h1>
        <button
          onClick={openCreate}
          className="flex items-center gap-2 px-4 py-2.5 bg-primary text-primary-foreground rounded-lg text-sm font-medium hover:opacity-90 transition-opacity"
        >
          <Plus size={16} />
          Nova Pessoa
        </button>
      </div>

      {persons.length === 0 ? (
        <EmptyState
          icon={<Users size={24} />}
          title="Nenhuma pessoa cadastrada"
          description="Comece adicionando as pessoas que fazem parte do seu controle financeiro."
          action={
            <button
              onClick={openCreate}
              className="text-sm text-primary font-medium hover:underline"
            >
              Adicionar pessoa
            </button>
          }
        />
      ) : (
        <div className="data-table">
          <table className="w-full">
            <thead>
              <tr className="border-b border-border">
                <th className="text-left px-4 py-3 text-xs font-semibold text-muted-foreground uppercase tracking-wider">
                  Nome
                </th>
                <th className="text-left px-4 py-3 text-xs font-semibold text-muted-foreground uppercase tracking-wider">
                  Idade
                </th>
                <th className="text-right px-4 py-3 text-xs font-semibold text-muted-foreground uppercase tracking-wider">
                  Ações
                </th>
              </tr>
            </thead>
            <tbody>
              {persons.map((person) => (
                <tr
                  key={person.id}
                  className="border-b border-border last:border-0 hover:bg-muted/50 transition-colors"
                >
                  <td className="px-4 py-3">
                    <div className="flex items-center gap-3">
                      <div className="w-8 h-8 rounded-full bg-primary/10 flex items-center justify-center text-primary font-semibold text-sm">
                        {person.name.charAt(0).toUpperCase()}
                      </div>
                      <span className="font-medium text-foreground text-sm">
                        {person.name}
                      </span>
                    </div>
                  </td>
                  <td className="px-4 py-3 text-sm text-muted-foreground">
                    {person.age} anos
                    {person.age < 18 && (
                      <span className="ml-2 px-2 py-0.5 bg-warning/10 text-warning text-xs rounded-full font-medium">
                        Menor
                      </span>
                    )}
                  </td>
                  <td className="px-4 py-3 text-right">
                    <div className="flex items-center justify-end gap-1">
                      <button
                        onClick={() => openEdit(person)}
                        className="p-2 text-muted-foreground hover:text-primary rounded-lg hover:bg-muted transition-colors"
                      >
                        <Pencil size={16} />
                      </button>
                      <button
                        onClick={() => setConfirmDelete(person.id)}
                        className="p-2 text-muted-foreground hover:text-destructive rounded-lg hover:bg-destructive/10 transition-colors"
                      >
                        <Trash2 size={16} />
                      </button>
                    </div>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}

      {/* Modal criar/editar pessoa */}
      <Modal
        open={modalOpen}
        onClose={() => setModalOpen(false)}
        title={editing ? "Editar Pessoa" : "Nova Pessoa"}
      >
        <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
          <FormInput
            label="Nome"
            placeholder="Nome da pessoa"
            error={errors.name?.message}
            {...register("name")}
          />
          <FormInput
            label="Idade"
            type="number"
            placeholder="Idade"
            error={errors.age?.message}
            {...register("age", { valueAsNumber: true })}
          />
          <div className="flex justify-end gap-2 pt-2">
            <button
              type="button"
              onClick={() => setModalOpen(false)}
              className="px-4 py-2.5 text-sm font-medium text-muted-foreground hover:text-foreground rounded-lg border border-border hover:bg-muted transition-colors"
            >
              Cancelar
            </button>
            <button
              type="submit"
              disabled={isSubmitting}
              className="px-4 py-2.5 text-sm font-medium bg-primary text-primary-foreground rounded-lg hover:opacity-90 transition-opacity disabled:opacity-50"
            >
              {isSubmitting ? "Salvando..." : editing ? "Atualizar" : "Criar"}
            </button>
          </div>
        </form>
      </Modal>

      {/* Modal de confirmação de exclusão */}
      <Modal
        open={confirmDelete !== null}
        onClose={() => setConfirmDelete(null)}
        title="Confirmar Exclusão"
      >
        <p className="text-sm text-muted-foreground mb-4">
          Tem certeza que deseja excluir esta pessoa? Todas as transações
          associadas também serão removidas.
        </p>
        <div className="flex justify-end gap-2">
          <button
            onClick={() => setConfirmDelete(null)}
            className="px-4 py-2.5 text-sm font-medium text-muted-foreground hover:text-foreground rounded-lg border border-border hover:bg-muted transition-colors"
          >
            Cancelar
          </button>
          <button
            onClick={() => confirmDelete && handleDelete(confirmDelete)}
            disabled={loadingPersons}
            className="px-4 py-2.5 text-sm font-medium bg-destructive text-destructive-foreground rounded-lg hover:opacity-90 transition-opacity disabled:opacity-50"
          >
            Excluir
          </button>
        </div>
      </Modal>
    </div>
  );
};

export default PersonsPage;
