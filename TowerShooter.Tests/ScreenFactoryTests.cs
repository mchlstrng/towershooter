using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TowerShooter.Exceptions;
using TowerShooter.Screens;

namespace TowerShooter.Tests
{
    [TestClass]
    public class ScreenFactoryTests
    {
        [TestMethod]
        public void GetNewScreenInstance_GamePlayGameScreen()
        {
            //Arrange
            Mock<ITowerShooter> towerShooter = new();
            towerShooter.SetupAllProperties();
            ScreenFactory screenFactory = new(towerShooter.Object);

            //Act
            Screens.Interfaces.IGameScreen result = screenFactory.GetNewScreenInstance(ScreenType.Gameplay);

            //Assert
            Assert.AreEqual(result.GetType(), typeof(GameplayGameScreen));
        }

        [TestMethod]
        public void GetNewScreenInstance_MainMenuGameScreen()
        {
            //Arrange
            Mock<ITowerShooter> towerShooter = new();
            towerShooter.SetupAllProperties();
            ScreenFactory screenFactory = new(towerShooter.Object);

            //Act
            Screens.Interfaces.IGameScreen result = screenFactory.GetNewScreenInstance(ScreenType.MainMenu);

            //Assert
            Assert.AreEqual(result.GetType(), typeof(MainMenuGameScreen));
        }

        [TestMethod]
        public void GetNewScreenInstance_OptionsMenuGameScreen()
        {
            //Arrange
            Mock<ITowerShooter> towerShooter = new();
            towerShooter.SetupAllProperties();
            ScreenFactory screenFactory = new(towerShooter.Object);

            //Act
            Screens.Interfaces.IGameScreen result = screenFactory.GetNewScreenInstance(ScreenType.OptionsMenu);

            //Assert
            Assert.AreEqual(result.GetType(), typeof(OptionsMenuGameScreen));
        }

        [TestMethod]
        public void GetNewScreenInstance_GameOverGameScreen()
        {
            //Arrange
            Mock<ITowerShooter> towerShooter = new();
            towerShooter.SetupAllProperties();
            ScreenFactory screenFactory = new(towerShooter.Object);

            //Act
            Screens.Interfaces.IGameScreen result = screenFactory.GetNewScreenInstance(ScreenType.GameoverMenu);

            //Assert
            Assert.AreEqual(result.GetType(), typeof(GameoverMenuGameScreen));
        }

        [TestMethod]
        public void GetNewScreenInstance_InvalidGameScreen()
        {
            //Arrange
            Mock<ITowerShooter> towerShooter = new();
            towerShooter.SetupAllProperties();
            ScreenFactory screenFactory = new(towerShooter.Object);

            var invalidScreenType = (ScreenType)(-1);

            //Act + Assert
            Assert.ThrowsException<InvalidGameScreenTypeException>(() => screenFactory.GetNewScreenInstance(invalidScreenType));
        }

    }
}