using System;
using TowerShooter.Screens.Interfaces;

namespace TowerShooter
{
    public interface IScreenFactory
    {
        IGameScreen GetNewScreenInstance(ScreenType type);
    }
}