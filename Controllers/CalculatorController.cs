using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Skynetz.Data;
using Project_Skynetz.Models;

namespace Project_Skynetz.Controllers
{
    public class CalculatorController : Controller
    {
        private readonly SkynetzContext _context;
        
        public CalculatorController(SkynetzContext context)
        {
            _context = context;
        }
        
        public IActionResult Index()
        {
            var viewModel = new CalculatorViewModel();
            LoadDropdownData();
            return View(viewModel);
        }
        
        [HttpPost]
        public IActionResult Calculate(CalculatorViewModel model)
        {
            // Validações básicas antes de processar
            if (string.IsNullOrEmpty(model.OriginCode) || string.IsNullOrEmpty(model.DestinationCode))
            {
                ModelState.AddModelError("", "Selecione a origem e o destino.");
                LoadDropdownData();
                return View("Index", model);
            }
            
            if (model.TotalMinutes <= 0)
            {
                ModelState.AddModelError("", "O tempo deve ser maior que zero.");
                LoadDropdownData();
                return View("Index", model);
            }
            
            // Busca a tarifa da rota selecionada
            var selectedRate = _context.Rates
                .FirstOrDefault(r => r.OriginCode == model.OriginCode && r.DestinationCode == model.DestinationCode);
                
            if (selectedRate == null)
            {
                ModelState.AddModelError("", "Não existe tarifa cadastrada para essa rota.");
                LoadDropdownData();
                return View("Index", model);
            }
            
            var selectedPlan = _context.Plans.Find(model.PlanId);
            
            if (selectedPlan == null)
            {
                ModelState.AddModelError("", "Selecione um plano válido.");
                LoadDropdownData();
                return View("Index", model);
            }
            
            // Cálculo sem plano: simples multiplicação
            model.PriceWithoutPlan = selectedRate.PricePerMinute * model.TotalMinutes;
            
            // Cálculo com plano: verifica se está dentro dos minutos inclusos
            if (model.TotalMinutes <= selectedPlan.IncludedMinutes)
            {
                // Tempo dentro do plano = grátis
                model.PriceWithPlan = 0;
            }
            else
            {
                // Passou do limite: cobra excedente com acréscimo de 10%
                var extraMinutes = model.TotalMinutes - selectedPlan.IncludedMinutes;
                var priceWithSurcharge = selectedRate.PricePerMinute * 1.10m;
                model.PriceWithPlan = extraMinutes * priceWithSurcharge;
            }
            
            model.PlanName = selectedPlan.Name;
            
            LoadDropdownData();
            return View("Index", model);
        }
        
        private void LoadDropdownData()
        {
            ViewBag.Plans = _context.Plans.ToList();
            
            // Monta lista de DDDs disponíveis (origem + destino)
            var originCodes = _context.Rates.Select(r => r.OriginCode).Distinct();
            var destinationCodes = _context.Rates.Select(r => r.DestinationCode).Distinct();
            ViewBag.AreaCodes = originCodes.Union(destinationCodes).Distinct().OrderBy(c => c).ToList();
        }
    }
}