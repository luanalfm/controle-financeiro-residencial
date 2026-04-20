//Componente de loading, para caso haja alguma demora ou delay no carregamento das informações do backend
import { Loader2 } from "lucide-react";

interface LoadingSpinnerProps {
  message?: string;
}

const LoadingSpinner = ({ message = "Carregando..." }: LoadingSpinnerProps) => (
  <div className="flex items-center justify-center gap-2 py-12 text-muted-foreground">
    <Loader2 size={20} className="animate-spin" />
    <span className="text-sm">{message}</span>
  </div>
);

export default LoadingSpinner;
