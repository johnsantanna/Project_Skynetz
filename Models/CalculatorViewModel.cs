using System.ComponentModel.DataAnnotations;

namespace Project_Skynetz.Models
{
    public class CalculatorViewModel
    {
        [Required(ErrorMessage = "Selecione a origem")]
        public string OriginCode { get; set; }

        [Required(ErrorMessage = "Selecione o destino")]
        public string DestinationCode { get; set; }

        [Required(ErrorMessage = "Informe os minutos")]
        [Range(1, 999, ErrorMessage = "Entre 1 e 999 minutos")]
        public int TotalMinutes { get; set; }

        [Required(ErrorMessage = "Escolha um plano")]
        public int PlanId { get; set; }

        public decimal? PriceWithPlan { get; set; }
        public decimal? PriceWithoutPlan { get; set; }
        public string? PlanName { get; set; }
    }
}