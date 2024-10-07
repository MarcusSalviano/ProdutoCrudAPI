using System.ComponentModel.DataAnnotations;

namespace ProdutoCrudAPI.Application.Dtos;
public class DataValidadeValidationAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        // Verifica se o valor não é nulo e é do tipo DateTime
        if (value is DateTime dataValidade)
        {
            // Verifica se a data de validade é maior que a data atual
            if (dataValidade.Date > DateTime.Today)
            {
                return true;
            }
        }

        return false;
    }
}