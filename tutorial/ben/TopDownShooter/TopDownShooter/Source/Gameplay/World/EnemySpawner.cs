using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;

namespace TopDownShooter.Gameplay.World
{
    public class EnemySpawner
    {
        public delegate void EnemyHitHandler(Enemy enemy);
        public event EnemyHitHandler EnemyHit;
        private List<Enemy> enemies;
        private TimeSpan heroSpawnTime, impSpawnTime;
        private TimeSpan lastHeroSpawn, lastImpSpawn;
        private Texture2D heroSprite, impSprite;
        private Game game;

        public EnemySpawner(Game game, Texture2D heroSprite, Texture2D impSprite)
        {
            this.game = game;
            this.heroSprite = heroSprite;
            this.impSprite = impSprite;
            enemies = new List<Enemy>();
            heroSpawnTime = TimeSpan.FromSeconds(1);
            impSpawnTime = TimeSpan.FromSeconds(1);
            lastHeroSpawn = lastImpSpawn = TimeSpan.Zero;
        }

        public void Update(GameTime gameTime)
        {

            // Spawn Hero
            if (gameTime.TotalGameTime - lastHeroSpawn > heroSpawnTime)
            {
                SpawnEnemy(heroSprite, new Vector2(0, 90)); // Moves downwards
                lastHeroSpawn = gameTime.TotalGameTime;
            }


            // Spawn Imp
            if (gameTime.TotalGameTime - lastImpSpawn > impSpawnTime)
            {
                SpawnEnemy(impSprite, new Vector2(0, 90)); // Moves downwards
                lastImpSpawn = gameTime.TotalGameTime;
            }


            // Update all enemies
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                enemies[i].Update(gameTime);
                if (!enemies[i].IsActive)
                {
                    enemies.RemoveAt(i);
                }
            }
        }

        private void SpawnEnemy(Texture2D sprite, Vector2 velocity)
        {

            if (sprite == null)
            {
                throw new InvalidOperationException("Attempted to spawn enemy with null sprite.");
            }
            Random rand = new Random();

            // Randomize the X-coordinate along the width of the screen
            int screenWidth = game.GraphicsDevice.Viewport.Width;
            float randomX = rand.Next(0, screenWidth);

            // Set the Y-coordinate to just above the top of the screen
            float spawnY = 10;  // Start just above the screen

            Vector2 spawnPosition = new Vector2(randomX, spawnY);

            Enemy newEnemy = new Enemy(sprite, spawnPosition, velocity);
            newEnemy.EnemyHit += (enemy) =>
            {
                // Raise the EnemySpawner's event when an enemy is hit
                EnemyHit?.Invoke(enemy);
            };
            enemies.Add(newEnemy);
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 offset = Vector2.Zero; // Determine the appropriate offset value

            foreach (var enemy in enemies)
            {
                enemy.Draw(offset);
            }
        }

        public int ActiveEnemyCount()
        {
            return enemies.Count(e => e.IsActive);
        }

        public List<Enemy> GetEnemies()
        {
            return enemies;
        }
    }
}
