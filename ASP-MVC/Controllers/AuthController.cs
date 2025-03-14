﻿using ASP_MVC.Handlers;
using ASP_MVC.Handlers.ActionFilters;
using ASP_MVC.Models.Auth;
using BLL.Entities;
using Common.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ASP_MVC.Controllers
{
    public class AuthController : Controller
    {
        private IUtilisateurRepository<BLL.Entities.Utilisateur> _userService;
        private SessionManager _sessionManager;

        public AuthController(
            IUtilisateurRepository<Utilisateur> userService,
            SessionManager sessionManager
            )
        {
            _userService = userService;
            _sessionManager = sessionManager;
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(Login));
        }

        [AnonymousNeeded]
        public IActionResult Login()
        {
            /* Méthode compliquée de vérifier si l'utilisateur à le droit ou non d'accéder à la page...
            if(_sessionManager.ConnectedUser is not null)
            {
                return RedirectToAction("Index", "Home");
            }*/
            return View();
        }

        [HttpPost]
        [AnonymousNeeded]
		public IActionResult Login(AuthLoginForm model)
		{
			if (ModelState.IsValid)
			{
				var utilisateur = _userService.GetByEmailAndPassword(model.Email, model.Password);

				if (utilisateur != null)
				{
					// Enregistrer l'utilisateur dans la session
					_sessionManager.Login(new ConnectedUser
					{
						User_Id = utilisateur.Utilisateur_Id,
						Email = utilisateur.Email,
						Pseudo = utilisateur.Pseudo,
						ConnectedAt = DateTime.Now
					});
					return RedirectToAction("Index", "Home");
				}
				else
				{
					// Email ou mot de passe incorrect
					ModelState.AddModelError("", "Identifiants incorrects");
				}
			}

			return View(model);
		}

		[ConnectionNeeded]
        public IActionResult Logout()
        {
            return View();
        }

        [HttpPost]
        [ConnectionNeeded]
        public IActionResult Logout(IFormCollection form)
        {
            try
            {
                _sessionManager.Logout();
                return RedirectToAction(nameof(Login));
            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}
