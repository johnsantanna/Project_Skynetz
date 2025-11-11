using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace Project_Skynetz.ModelBinders
{
    public class DecimalModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            var modelName = bindingContext.ModelName;
            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

            if (valueProviderResult == ValueProviderResult.None)
                return Task.CompletedTask;

            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

            var value = valueProviderResult.FirstValue;

            if (string.IsNullOrEmpty(value))
                return Task.CompletedTask;

            // sceita formato brasileiro primeiro (virgula como separador decimal)
            if (decimal.TryParse(value, NumberStyles.Any, new CultureInfo("pt-BR"), out decimal result))
            {
                bindingContext.Result = ModelBindingResult.Success(result);
                return Task.CompletedTask;
            }

            // tenta formato internacional (ponto como separador)
            if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
            {
                bindingContext.Result = ModelBindingResult.Success(result);
                return Task.CompletedTask;
            }

            // se nenhum formato funcionou, retorna erro de validacao
            bindingContext.ModelState.TryAddModelError(
                modelName,
                "O valor deve ser um número válido.");

            return Task.CompletedTask;
        }
    }
}
