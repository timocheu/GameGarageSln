using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using GameGarage.Models;
using GameGarage.Controllers;
using Xunit;

namespace GameGarage.Tests;

public class HomeControllerTests {
    [Fact]
    public void Can_Use_Repository() {
        Mock<IGarageRepository> mock = new Mock<IGarageRepository>();
        mock.Setup(m => m.Games).Returns((new Game[] {
                    new Game { Id = 1, Name = "Dota" },
                    new Game { Id = 2, Name = "LoL" }
                    }).AsQueryable<Game>());

        HomeController controller = new HomeController(mock.Object);

        IEnumerable<Game>? result =
            (controller.Index() as ViewResult)?.ViewData.Model
            as IEnumerable<Game>;

        Game[] gameArray = result?.ToArray() ?? Array.Empty<Game>();
        Assert.True(gameArray.Length == 2);
        Assert.Equal("Dota", gameArray[0].Name);
        Assert.Equal("LoL", gameArray[1].Name); 
    }
}
