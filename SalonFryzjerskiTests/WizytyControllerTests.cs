using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.EntityFrameworkCore;
using SalonFryzjerski.Controllers;
using SalonFryzjerski.Data;

namespace SalonFryzjerskiTests
{
    [TestClass]
    public class WizytyControllerTests
    {
        [TestMethod]
        public void IndexTest1()
        {
            Mock<SalonFryzjerskiContext> contextMock = new Mock<SalonFryzjerskiContext>();

            List<Wizyta> wizyty = new List<Wizyta>()
            {

                new Wizyta()
                {
                    Id = 1,
                    Data = new DateTime(2023,03,09),
                    RodzajId = 1,
                    UserId = "123",
                    Ocena = 4,
                    Rodzaj = new Rodzaj(){ Id = 1, Nazwa = "Strzy¿enie", Cena = 34, Wizyty = null }
                }
            };

            contextMock.Setup(c => c.Wizyty).ReturnsDbSet(wizyty);

            WizytyController wc = new WizytyController(contextMock.Object);

            IActionResult result = wc.Index().GetAwaiter().GetResult();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void CreateTest()
        {
            Mock<SalonFryzjerskiContext> contextMock = new Mock<SalonFryzjerskiContext>();
            WizytyController wc = new WizytyController(contextMock.Object);

            contextMock.Setup(s=>s.Rodzaje).ReturnsDbSet(new List<Rodzaj>());

            IActionResult result = wc.Create();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void DetailsTest1()
        {
            Mock<SalonFryzjerskiContext> contextMock = new Mock<SalonFryzjerskiContext>();

            Rodzaj rodzaj = new Rodzaj() { Id = 1, Nazwa = "Strzy¿enie", Cena = 34, Wizyty = null };

            List<Wizyta> wizyty = new List<Wizyta>()
            {

                new Wizyta()
                {
                    Id = 1,
                    Data = new DateTime(2023,03,09),
                    RodzajId = 1,
                    UserId = "123",
                    Ocena = 4,
                    Rodzaj = rodzaj
                }
            };

            contextMock.Setup(c => c.Wizyty).ReturnsDbSet(wizyty);


            List<Rodzaj> rodzaje = new List<Rodzaj>()
            {
                rodzaj
            };

            contextMock.Setup(c => c.Rodzaje).ReturnsDbSet(rodzaje);

            WizytyController wc = new WizytyController(contextMock.Object);

            IActionResult result = wc.Details(1).GetAwaiter().GetResult();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void DetailsTest2()
        {
            Mock<SalonFryzjerskiContext> contextMock = new Mock<SalonFryzjerskiContext>();

            Rodzaj rodzaj = new Rodzaj() { Id = 1, Nazwa = "Strzy¿enie", Cena = 34, Wizyty = null };

            List<Wizyta> wizyty = new List<Wizyta>()
            {

                new Wizyta()
                {
                    Id = 1,
                    Data = new DateTime(2023,03,09),
                    RodzajId = 1,
                    UserId = "123",
                    Ocena = 4,
                    Rodzaj = rodzaj
                }
            };

            contextMock.Setup(c => c.Wizyty).ReturnsDbSet(wizyty);

            List<Rodzaj> rodzaje = new List<Rodzaj>()
            {
                rodzaj
            };

            contextMock.Setup(c => c.Rodzaje).ReturnsDbSet(rodzaje);

            WizytyController wc = new WizytyController(contextMock.Object);

            IActionResult result = wc.Details(2).GetAwaiter().GetResult();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}