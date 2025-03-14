using ASP_MVC.Models;
using Common.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BLL.Entities;  // Assurez-vous d'ajouter ce `using` pour acc�der � BLL.Entities.Jeu

namespace ASP_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IJeuRepository<BLL.Entities.Jeu> _jeuRepository;

        // Constructor fusionn� pour les deux d�pendances
        public HomeController(ILogger<HomeController> logger, IJeuRepository<BLL.Entities.Jeu> jeuRepository)
        {
            _logger = logger;
            _jeuRepository = jeuRepository;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Action pour la page d'accueil avec un param�tre de recherche
        public IActionResult Index(string searchQuery)
        {
            // Si une recherche a �t� effectu�e
            var jeux = string.IsNullOrEmpty(searchQuery)
                ? _jeuRepository.Get()  // Retourner tous les jeux si pas de recherche
                : _jeuRepository.Get()
                      .Where(j => j.Nom.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))  // Recherche par nom uniquement
                      .ToList();
            return View(jeux); // Passer la liste des jeux � la vue
        }
    }
}

