using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SecretNumber.Models;

namespace SecretNumber.Controllers
{
    public class HomeController : Controller
    {
        private SecretNumberModel SecretNumber
        {
            get
            {
                return (SecretNumberModel)Session["SecretNumber"];
            }
            set
            {
                Session["SecretNumber"] = value;
            }
        }

        public ActionResult Index()
        {
            SecretNumber = new SecretNumberModel();
            return View(SecretNumber);
        }

        [HttpPost]
        public ActionResult Index(FormCollection formData)
        {
            if (!Session.IsNewSession)
            {
                if (!TryUpdateModel(SecretNumber, formData))
                {
                    ModelState.AddModelError(String.Empty, "Ett fel inträffade.");
                }

                if (ModelState.IsValid)
                {
                    SecretNumber.MakeGuess(SecretNumber.Guess);
                }
            }
            
            return View(SecretNumber);
        }
    }
}