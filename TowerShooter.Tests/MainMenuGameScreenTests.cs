using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Moq;
using TowerShooter.Screens;

namespace TowerShooter.Tests
{
    [TestClass]
    public class MainMenuGameScreenTests
    {
        private class TestHelper
        {
            public Mock<ITowerShooter> TowerShooter { get; set; } = new Mock<ITowerShooter>();
            public Mock<ISpriteBatch> SpriteBatch { get; set; } = new Mock<ISpriteBatch>();
            public Mock<ISoundEffect> SelectSoundEffect { get; set; } = new Mock<ISoundEffect>();
            public Mock<ISoundEffect> ChangeSelectSoundEffect { get; set; } = new Mock<ISoundEffect>();
            public Mock<IGameClock> GameClock { get; set; } = new Mock<IGameClock>();

            public MainMenuGameScreen GetMainMenuGameScreen()
            {
                TowerShooter.Setup(m => m.GraphicsDeviceManager.PreferredBackBufferWidth).Returns(800);
                TowerShooter.Setup(m => m.GraphicsDeviceManager.PreferredBackBufferHeight).Returns(600);

                TowerShooter.Setup(m => m.FlashlightComponent).Returns(new Mock<IFlashlightComponent>().Object);

                Mock<ISpriteFont> menuItemFontSpriteFont = new();
                menuItemFontSpriteFont.Setup(m => m.MeasureString("Start")).Returns(new Vector2(10, 5));
                menuItemFontSpriteFont.Setup(m => m.MeasureString("Exit")).Returns(new Vector2(5, 5));

                TowerShooter.Setup(m => m.ContentManager.LoadSpriteFont("MenuItemFont")).Returns(menuItemFontSpriteFont.Object);

                Mock<ISpriteFont> titleFontSpriteFont = new();
                titleFontSpriteFont.Setup(m => m.MeasureString("TowerShooter")).Returns(new Vector2(100, 100));
                TowerShooter.Setup(m => m.ContentManager.LoadSpriteFont("TitleFont")).Returns(titleFontSpriteFont.Object);

                TowerShooter.Setup(m => m.ContentManager.LoadSoundEffect("select")).Returns(SelectSoundEffect.Object);

                TowerShooter.Setup(m => m.ContentManager.LoadSoundEffect("changeselect")).Returns(ChangeSelectSoundEffect.Object);

                TowerShooter.Setup(m => m.ScreenManager.GetNewScreenInstance(ScreenType.MainMenu));

                TowerShooter.Setup(m => m.GameClock).Returns(GameClock.Object);

                return new MainMenuGameScreen(TowerShooter.Object);
            }
        }

        [TestMethod]
        public void Draw()
        {
            //Arrange
            TestHelper testhelper = new();
            Mock<ISpriteBatch> spriteBatch = testhelper.SpriteBatch;
            MainMenuGameScreen mainMenuGameScreen = testhelper.GetMainMenuGameScreen();

            //Act
            mainMenuGameScreen.LoadContent();
            mainMenuGameScreen.Draw(spriteBatch.Object, new GameTime());

            //Assert

            //verify draw game title
            spriteBatch.Verify(m => m.DrawString(It.IsAny<SpriteFont>(), "TowerShooter", new Vector2(350, 10), Color.White));

            //verify draw menu items
            spriteBatch.Verify(m => m.DrawString(It.IsAny<SpriteFont>(), "Start", new Vector2(395, 295), Color.Yellow));
            spriteBatch.Verify(m => m.DrawString(It.IsAny<SpriteFont>(), "Options", new Vector2(400, 310), Color.White));
            spriteBatch.Verify(m => m.DrawString(It.IsAny<SpriteFont>(), "Exit", new Vector2(398, 320), Color.White));
        }

        [TestMethod]
        public void UpdateTestPressEnterKeyAssertThatGameplayGameScreenIsCalled()
        {
            //Arrange
            TestHelper testHelper = new();

            Mock<ITowerShooter> towerShooter = testHelper.TowerShooter;

            towerShooter.Setup(m => m.InputManager.IsSingleKeyPress(Keys.Enter)).Returns(true);

            MainMenuGameScreen mainMenuGameScreen = testHelper.GetMainMenuGameScreen();

            //Act
            mainMenuGameScreen.LoadContent();
            mainMenuGameScreen.Update(new GameTime());

            //Assert

            //verify that the game screen is changed to gameplay screen
            towerShooter.Verify(m => m.ScreenManager.GetNewScreenInstance(ScreenType.Gameplay), Times.Once);
            //verify that the game clock is set to 8
            testHelper.GameClock.Verify(m => m.SetStartHour(8), Times.Once);
            //verify that select sound effect is played
            testHelper.SelectSoundEffect.Verify(m => m.Play(), Times.Once);
        }

        [TestMethod]
        public void UpdateTestPressDownVerifyThatOptionsScreenIsCalled()
        {
            //Arrange
            TestHelper testHelper = new();

            Mock<ITowerShooter> towerShooter = testHelper.TowerShooter;

            towerShooter.Setup(m => m.InputManager.IsSingleKeyPress(Keys.Down)).Returns(true);

            towerShooter.Setup(m => m.InputManager.IsSingleKeyPress(Keys.Enter)).Returns(true);

            MainMenuGameScreen mainMenuGameScreen = testHelper.GetMainMenuGameScreen();

            //Act
            mainMenuGameScreen.LoadContent();
            mainMenuGameScreen.Update(new GameTime());

            //Assert
            towerShooter.Verify(m => m.ScreenManager.GetNewScreenInstance(ScreenType.OptionsMenu), Times.Once);
            testHelper.ChangeSelectSoundEffect.Verify(m => m.Play(), Times.Once);
        }

        [TestMethod]
        public void UpdateTestPressDownTwiceVerifyThatTowerShooterExitIsCalled()
        {
            //Arrange
            TestHelper testHelper = new();
            Mock<ITowerShooter> towerShooter = testHelper.TowerShooter;

            //setup moq sequence first run only down key, second run down and enter key
            towerShooter.SetupSequence(m => m.InputManager.IsSingleKeyPress(Keys.Down))
                .Returns(true)
                .Returns(true);

            towerShooter.Setup(m => m.InputManager.IsSingleKeyPress(Keys.Enter)).Returns(true);

            MainMenuGameScreen mainMenuGameScreen = testHelper.GetMainMenuGameScreen();
            //Act
            mainMenuGameScreen.LoadContent();
            mainMenuGameScreen.Update(new GameTime());
            mainMenuGameScreen.Update(new GameTime());
            //Assert
            towerShooter.Verify(m => m.Exit(), Times.Once);
            testHelper.ChangeSelectSoundEffect.Verify(m => m.Play(), Times.Exactly(2));
        }

        [TestMethod]
        public void TestPressKeyUpAssertPlaySelectSound()
        {
            //Arrange
            TestHelper testHelper = new();
            Mock<ITowerShooter> towerShooter = testHelper.TowerShooter;
            towerShooter.Setup(m => m.InputManager.IsSingleKeyPress(Keys.Up)).Returns(true);
            MainMenuGameScreen mainMenuGameScreen = testHelper.GetMainMenuGameScreen();
            //Act
            mainMenuGameScreen.LoadContent();
            mainMenuGameScreen.Update(new GameTime());
            //Assert
            testHelper.ChangeSelectSoundEffect.Verify(m => m.Play(), Times.Once);
        }
    }
}