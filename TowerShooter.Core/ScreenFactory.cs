using System;
using TowerShooter.Exceptions;
using TowerShooter.Screens;
using TowerShooter.Screens.Interfaces;

namespace TowerShooter
{
    public class ScreenFactory : IScreenFactory
    {
        private readonly ITowerShooter towerShooter;

        public ScreenFactory(ITowerShooter towerShooter)
        {
            this.towerShooter = towerShooter;
        }

        public IGameScreen GetNewScreenInstance(ScreenType type)
        {
            IGameScreen gameScreen;

            switch (type)
            {
                case ScreenType.Gameplay:
                    gameScreen = new GameplayGameScreen(towerShooter);
                    towerShooter.GameplayGameScreen = (IGameplayGameScreen)gameScreen;
                    break;
                case ScreenType.MainMenu:
                    gameScreen = new MainMenuGameScreen(towerShooter);
                    break;
                case ScreenType.OptionsMenu:
                    gameScreen = new OptionsMenuGameScreen(towerShooter);
                    break;
                case ScreenType.GameoverMenu:
                    gameScreen = new GameoverMenuGameScreen(towerShooter);
                    break;
                default:
                    throw new InvalidGameScreenTypeException($"Could not determine screen to spawn from type {type}");
            }

            return gameScreen;
        }
    }
}