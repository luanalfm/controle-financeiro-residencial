//Componentes de formulário padrão com o suporte do React Hook Form com estilização do tailwind
import { forwardRef } from "react";
import { cn } from "@/lib/utils";

//FormInput

interface FormInputProps extends React.InputHTMLAttributes<HTMLInputElement> {
  label: string;
  error?: string;
}

export const FormInput = forwardRef<HTMLInputElement, FormInputProps>(
  ({ label, error, className, ...props }, ref) => (
    <div className="space-y-1.5">
      <label className="text-sm font-medium text-foreground">{label}</label>
      <input
        ref={ref}
        className={cn(
          "w-full px-3 py-2.5 rounded-lg border bg-background text-foreground placeholder:text-muted-foreground text-sm transition-colors focus:outline-none focus:ring-2 focus:ring-ring focus:border-transparent",
          error ? "border-destructive" : "border-input",
          className
        )}
        {...props}
      />
      {error && <p className="text-xs text-destructive">{error}</p>}
    </div>
  )
);
FormInput.displayName = "FormInput";

//FormSelect

interface FormSelectProps extends React.SelectHTMLAttributes<HTMLSelectElement> {
  label: string;
  error?: string;
  options: { value: string; label: string }[];
  placeholder?: string;
}

export const FormSelect = forwardRef<HTMLSelectElement, FormSelectProps>(
  ({ label, error, options, placeholder, className, ...props }, ref) => (
    <div className="space-y-1.5">
      <label className="text-sm font-medium text-foreground">{label}</label>
      <select
        ref={ref}
        className={cn(
          "w-full px-3 py-2.5 rounded-lg border bg-background text-foreground text-sm transition-colors focus:outline-none focus:ring-2 focus:ring-ring focus:border-transparent",
          error ? "border-destructive" : "border-input",
          className
        )}
        {...props}
      >
        {placeholder && (
          <option value="" className="text-muted-foreground">
            {placeholder}
          </option>
        )}
        {options.map((opt) => (
          <option key={opt.value} value={opt.value}>
            {opt.label}
          </option>
        ))}
      </select>
      {error && <p className="text-xs text-destructive">{error}</p>}
    </div>
  )
);
FormSelect.displayName = "FormSelect";
