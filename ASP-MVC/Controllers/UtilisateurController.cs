﻿// Controllers/UtilisateurController.cs
using BLL.Services;
using ASP_MVC.Models.Utilisateur;
using Microsoft.AspNetCore.Mvc;
using ASP_MVC.Mappers;
using Common.Repositories;
using ASP_MVC.Handlers.ActionFilters;
using BLL.Entities;

namespace ASP_MVC.Controllers
{
    public class UtilisateurController : Controller
    {
        private readonly IUtilisateurRepository<BLL.Entities.Utilisateur> _utilisateurRepository;

        // Injection de la dépendance pour l'interface IUtilisateurRepository
        public UtilisateurController(IUtilisateurRepository<BLL.Entities.Utilisateur> utilisateurRepository)
        {
            _utilisateurRepository = utilisateurRepository;
        }

        // Action pour afficher tous les utilisateurs
        public IActionResult Index()
        {
            // Utilisation de l'interface pour récupérer tous les utilisateurs et les mapper
            var utilisateurs = _utilisateurRepository.Get()
                .Select(utilisateur => utilisateur.ToListItem())  // Mapping à une vue simplifiée
                .ToList();

            // Retour de la vue avec la liste des utilisateurs
            return View(utilisateurs);
        }

        // Action pour afficher les détails d'un utilisateur
        public IActionResult Details(Guid id)
        {
            // Utilisation de l'interface pour récupérer un utilisateur par son ID et le mapper
            var utilisateur = _utilisateurRepository.Get(id).ToListItem();

            // Retour de la vue avec les détails de l'utilisateur
            return View(utilisateur);
        }

        // GET: UserController/Create

        [AnonymousNeeded]
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AnonymousNeeded]
        public ActionResult Create(UtilisateurCreateForm form)
        {
            try
            {
                if (!form.Consent) ModelState.AddModelError(nameof(form.Consent), "Vous devez lire et accepter les conditions générales d'utilisation.");
                if (!ModelState.IsValid) throw new ArgumentException();
                Guid id = _utilisateurRepository.Insert(form.ToBLL()); 
                return RedirectToAction(nameof(Details), new { id = id });
            }
            catch
            {
                return View();
            }
        }
    }
}
