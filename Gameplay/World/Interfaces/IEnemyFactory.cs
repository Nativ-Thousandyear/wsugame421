using Microsoft.Xna.Framework;
using System;
using TopDownShooter;

public interface IEnemyFactory
{
    Enemy CreateImp(Vector2 position, Vector2 velocity, Route route, GameTime gameTime);
    Enemy CreateL1Mob(Vector2 position, Vector2 velocity, Route route, int bossLevel, GameTime gameTime);
    Enemy CreateL2Boss(Vector2 position, Vector2 velocity, Route route, int bossLevel, GameTime gameTime);
    Enemy CreateL3Boss(Vector2 position, Vector2 velocity, Route route, int bossLevel, GameTime gameTime);
}
