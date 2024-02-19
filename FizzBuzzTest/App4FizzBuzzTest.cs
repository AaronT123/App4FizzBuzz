using NUnit.Framework;
using App4FizzBuzz.Controllers;
using App4FizzBuzz.Models;
using Microsoft.AspNetCore.Mvc;

namespace FizzBuzzTest
{
    public class Tests
    {
        FizzBuzzController controller;

        [SetUp]
        public void Setup()
        {
            controller = new FizzBuzzController();
        }

        #region FizzBuzz Game Logic

        [TestCase(15, "FizzBuzz")]
        [TestCase(6, "Fizz")]
        [TestCase(10, "Buzz")]
        [TestCase(7, "7")]
        public void GetFizzBuzz_ReturnsExpectedResult(int number, string expectedResult)
        {
            string result = controller.CalculateFizzBuzz(number);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        #endregion

        #region Input Validation

        [TestCase(0, 5)]
        [TestCase(5, 0)]
        [TestCase(100000, 5)]
        [TestCase(5, 100000)]
        [TestCase(5, 5)]
        [TestCase(5, 3)]
        public void ValidateFizzBuzz_ReturnsFalseForInvalidInput(int start, int end)
        {
            var fizzBuzz = new FizzBuzzViewModel { Start = start, End = end };

            var result = controller.ValidateFizzBuzz(fizzBuzz);

            Assert.That(result, Is.False);
        }

        [TestCase(1, 10)]
        public void ValidateFizzBuzz_ReturnsTrueForValidInput(int start, int end)
        {
            var fizzBuzz = new FizzBuzzViewModel { Start = start, End = end };

            var result = controller.ValidateFizzBuzz(fizzBuzz);

            Assert.That(result, Is.True);
        }

        #endregion

        #region ViewResult

        // These tests make sure that ViewResults are returned as expected even with an invalid model

        [Test]
        public void Index_ReturnsViewResult()
        {
            IActionResult result = controller.Index();

            Assert.That(result, Is.InstanceOf<ViewResult>());
        }

        [Test]
        public void PlayFizzBuzz_ReturnsViewResult()
        {
            var fizzBuzz = new FizzBuzzViewModel();
            IActionResult result = controller.PlayFizzBuzz(fizzBuzz);
            Assert.That(result, Is.InstanceOf<ViewResult>());
        }

        [TestCase(1, 15)]
        [TestCase(0, 5)]
        [TestCase(-5, 5)]
        [TestCase(5, 0)]
        [TestCase(5, -5)]
        [TestCase(5, 3)]
        public void PlayFizzBuzz_ReturnsViewResultForVariousInputs(int start, int end)
        {
            var fizzBuzz = new FizzBuzzViewModel { Start = start, End = end };

            IActionResult result = controller.PlayFizzBuzz(fizzBuzz);

            Assert.That(result, Is.InstanceOf<ViewResult>());
        }

        #endregion
    }
}
