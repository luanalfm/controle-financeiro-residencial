//Página de Categorias, com as funções criar e listar.
import { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { useAppStore } from "@/store/useAppStore";
import { categorySchema, type CategorySchemaType } from "@/utils/schemas";
import Modal from "@/components/Modal";
import { FormInput, FormSelect } from "@/components/FormFields";
import EmptyState from "@/components/EmptyState";
import { Tags, Plus } from "lucide-react";
import { toast } from "sonner";

const purposeLabels: Record<string, string> = {
  despesa: "Despesa",
  receita: "Receita",
  ambas: "Ambas",
};

const purposeBadgeStyles: Record<string, string> = {
  despesa: "bg-expense/10 text-expense",
  receita: "bg-income/10 text-income",
  ambas: "bg-primary/10 text-primary",
};

const CategoriesPage = () => {
  const { categories, addCategory } = useAppStore();
  const [modalOpen, setModalOpen] = useState(false);

  const {
    register,
    handleSubmit,
    reset,
    formState: { errors, isSubmitting },
  } = useForm<CategorySchemaType>({
    resolver: zodResolver(categorySchema),
  });

  const fetchCategories = useAppStore((state) => state.fetchCategories);
  
  useEffect(() => {
    fetchCategories();
  }, [fetchCategories]);

  const onSubmit = async (data: CategorySchemaType) => {
    await addCategory(data);
    toast.success("Categoria criada com sucesso!");
    setModalOpen(false);
    reset();
  };

  return (
    <div>
      <div className="flex items-center justify-between mb-6">
        <h1 className="page-title mb-0">Categorias</h1>
        <button
          onClick={() => {
            reset({ description: "", purpose: undefined as unknown as "despesa" });
            setModalOpen(true);
          }}
          className="flex items-center gap-2 px-4 py-2.5 bg-primary text-primary-foreground rounded-lg text-sm font-medium hover:opacity-90 transition-opacity"
        >
          <Plus size={16} />
          Nova Categoria
        </button>
      </div>

      {categories.length === 0 ? (
        <EmptyState
          icon={<Tags size={24} />}
          title="Nenhuma categoria cadastrada"
          description="Crie categorias para classificar suas receitas e despesas."
          action={
            <button
              onClick={() => setModalOpen(true)}
              className="text-sm text-primary font-medium hover:underline"
            >
              Adicionar categoria
            </button>
          }
        />
      ) : (
        <div className="data-table">
          <table className="w-full">
            <thead>
              <tr className="border-b border-border">
                <th className="text-left px-4 py-3 text-xs font-semibold text-muted-foreground uppercase tracking-wider">
                  Descrição
                </th>
                <th className="text-left px-4 py-3 text-xs font-semibold text-muted-foreground uppercase tracking-wider">
                  Propósito
                </th>
              </tr>
            </thead>
            <tbody>
              {categories.map((cat) => (
                <tr
                  key={cat.id}
                  className="border-b border-border last:border-0 hover:bg-muted/50 transition-colors"
                >
                  <td className="px-4 py-3 text-sm font-medium text-foreground">
                    {cat.description}
                  </td>
                  <td className="px-4 py-3">
                    <span
                      className={`px-2.5 py-1 text-xs font-medium rounded-full ${
                        purposeBadgeStyles[cat.purpose]
                      }`}
                    >
                      {purposeLabels[cat.purpose]}
                    </span>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}

      <Modal
        open={modalOpen}
        onClose={() => setModalOpen(false)}
        title="Nova Categoria"
      >
        <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
          <FormInput
            label="Descrição"
            placeholder="Ex: Alimentação"
            error={errors.description?.message}
            {...register("description")}
          />
          <FormSelect
            label="Propósito"
            placeholder="Selecione..."
            error={errors.purpose?.message}
            options={[
              { value: "despesa", label: "Despesa" },
              { value: "receita", label: "Receita" },
              { value: "ambas", label: "Ambas" },
            ]}
            {...register("purpose")}
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
              {isSubmitting ? "Salvando..." : "Criar"}
            </button>
          </div>
        </form>
      </Modal>
    </div>
  );
};

export default CategoriesPage;
