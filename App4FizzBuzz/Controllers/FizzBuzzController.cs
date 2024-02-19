using App4FizzBuzz.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace App4FizzBuzz.Controllers
{
    public class FizzBuzzController : Controller
    {

        public IActionResult Index()
        {
            return View(new FizzBuzzViewModel());
        }

        [HttpPost]
        public ActionResult PlayFizzBuzz(FizzBuzzViewModel fizzBuzz)
        {
            if (!_ValidateFizzBuzz(fizzBuzz))
            {
                return View("Index", fizzBuzz);
            }

            List<string> results = new List<string>();
            for (int i = fizzBuzz.Start; i <= fizzBuzz.End; i++)
            {
                results.Add(this.CalculateFizzBuzz(i));
            }

            ViewBag.Results = results;
            return View("Index", fizzBuzz);
        }

        public string CalculateFizzBuzz(int number)
        {
            return _CalculateFizzBuzz(number);
        }

        private static string _CalculateFizzBuzz(int number)
        {
            if (number % 3 == 0 && number % 5 == 0)
            {
                return "FizzBuzz";
            }
            else if (number % 3 == 0)
            {
                return "Fizz";
            }
            else if (number % 5 == 0)
            {
                return "Buzz";
            }
            else
            {
                return number.ToString();
            }
        }

        public bool ValidateFizzBuzz(FizzBuzzViewModel fizzBuzz)
        {
            return _ValidateFizzBuzz(fizzBuzz);
        }

        private bool _ValidateFizzBuzz(FizzBuzzViewModel fizzBuzz)
        {
            if (fizzBuzz.Start < 1)
            {
                ModelState.AddModelError(nameof(fizzBuzz.Start), "Start must be greater than or equal to 1.");
            }

            if (fizzBuzz.End < 1)
            {
                ModelState.AddModelError(nameof(fizzBuzz.End), "End must be greater than or equal to 1.");
            }

            if (fizzBuzz.Start > 99999)
            {
                ModelState.AddModelError(nameof(fizzBuzz.Start), "Start must be less than or equal to 9999");
            }

            if (fizzBuzz.End > 99999)
            {
                ModelState.AddModelError(nameof(fizzBuzz.End), "End must be less than or equal to 9999");
            }

            if (fizzBuzz.Start >= fizzBuzz.End)
            {
                ModelState.AddModelError(nameof(fizzBuzz.End), "End must be greater than start.");
            }

            if (!ModelState.IsValid)
            {
                return false;
            }

            return true;
        }
    }
}
