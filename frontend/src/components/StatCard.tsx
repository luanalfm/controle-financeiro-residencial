//Componente para exibir as métricas das consultas no Dashboard/tela principal
import { cn, formatCurrency } from "@/lib/utils";

interface StatCardProps {
  title: string;
  value: number;
  icon: React.ReactNode;
  variant: "income" | "expense" | "balance";
}

const variantStyles = {
  income: "text-income",
  expense: "text-expense",
  balance: "text-primary",
};

const StatCard = ({ title, value, icon, variant }: StatCardProps) => (
  <div className="stat-card">
    <div className="flex items-center justify-between mb-3">
      <span className="text-sm font-medium text-muted-foreground">{title}</span>
      <div className={cn("w-8 h-8 rounded-lg flex items-center justify-center", variantStyles[variant])}>
        {icon}
      </div>
    </div>
    <p className={cn("text-2xl font-bold", variantStyles[variant])}>
      {formatCurrency(value)}
    </p>
  </div>
);

export default StatCard;
