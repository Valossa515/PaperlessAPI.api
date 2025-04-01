using FluentValidation;
using PaperlessAPI.api.Borders.Dtos.Requests;

namespace PaperlessAPI.api.Handlers.Validators
{
    public class DynamicReportEntityRequestValidator : AbstractValidator<DynamicReportEntityRequest>
    {
        public DynamicReportEntityRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("O nome do relatório é obrigatório.")
                .MaximumLength(100).WithMessage("O nome do relatório não pode exceder 100 caracteres.");

            RuleFor(x => x.ColumnHeaders)
                .NotEmpty().WithMessage("O cabeçalho do relatório é obrigatório.")
                .Must(headers => headers.Any()).WithMessage("O cabeçalho do relatório deve conter pelo menos um item.");

            RuleFor(x => x.Rows)
                .NotEmpty().WithMessage("As linhas do relatório são obrigatórias.")
                .Must(rows => rows.Any()).WithMessage("O relatório deve conter pelo menos uma linha.")
                .Must((request, rows) => rows.All(row => row.Count() == request.ColumnHeaders.Count()))
                .WithMessage("Todas as linhas do relatório devem ter o mesmo número de colunas que o cabeçalho.");

            RuleForEach(x => x.ColumnHeaders)
                .NotEmpty().WithMessage("O cabeçalho do relatório não pode conter itens vazios.")
                .MaximumLength(100).WithMessage("O cabeçalho do relatório não pode exceder 100 caracteres.");

            RuleForEach(x => x.Rows)
                .NotEmpty().WithMessage("As linhas do relatório não podem conter itens vazios.");
        }
    }
}