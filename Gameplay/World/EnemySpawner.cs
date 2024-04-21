using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks.Sources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using TopDownShooter.Source.Gameplay.World.Factories;
using TopDownShooter.Source.Gameplay.World.Interfaces;


namespace TopDownShooter
{
    public class EnemySpawner
    {
        private IEnemyFactory enemyFactory;
        private World world;
        private TimeSpan heroSpawnTime, impSpawnTime, l1MobSpawnTime, l2BossSpawnTime, l3BossSpawnTime;
        private TimeSpan lastHeroSpawn, lastImpSpawn, lastL1MobSpawn, lastL2BossSpawn, lastL3BossSpawn;
        private Texture2D heroSprite, impSprite, l1_boss_Sprite, l2_boss_Sprite, l3_boss_Sprite;
        public delegate void EnemyHitHandler(Enemy enemy);
        public event EnemyHitHandler EnemyHit;
        private List<Enemy> enemies;
        private Timer gameTimer;
        private bool bossL2Spawned = false;
        private bool bossL3Spawned = false;

        public EnemySpawner(World world, IEnemyFactory enemyFactory, Timer timer)
        {
            this.enemyFactory = enemyFactory;
            this.world = world;
            this.gameTimer = timer;
            enemies = new List<Enemy>();
            impSpawnTime = TimeSpan.FromSeconds(.5);
            l1MobSpawnTime = TimeSpan.FromSeconds(.5);
            lastHeroSpawn = lastImpSpawn = lastL1MobSpawn = lastL2BossSpawn = lastL3BossSpawn = TimeSpan.Zero;
        }

        public void Update(GameTime gameTime, World world, Hero hero)
        {
            if (!GameGlobals.isPaused)
            {
                double timeLeft = gameTimer.GetTimeLeftInSeconds();

                TimeLeftDependentSpawner(timeLeft, world, gameTime);
                UpdateEnemies(gameTime, hero);
            }
        }

        private void TimeLeftDependentSpawner(double timeLeft, World world, GameTime gameTime)
        {
            if (timeLeft <= 70)
            {
                if (!bossL3Spawned)
                {
                    SpawnL3Boss(world, gameTime);
                    bossL3Spawned = true;
                }
            }
            else if (timeLeft <= 90 && bossL2Spawned)
            {
                TrySpawnEnemy(lastL1MobSpawn, l1MobSpawnTime, () => SpawnL1Mob(world, gameTime), ref lastL1MobSpawn, gameTime);
            }
            else if (timeLeft <= 110)
            {
                if (!bossL2Spawned)
                {
                    SpawnL2Boss(world, gameTime);
                    bossL2Spawned = true;
                }
            }
            else
            {
                TrySpawnEnemy(lastImpSpawn, impSpawnTime, () => SpawnImp(world, gameTime), ref lastImpSpawn, gameTime);
            }
        }

        private void UpdateEnemies(GameTime gameTime, Hero hero)
        {
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                enemies[i].Update(gameTime, hero);
                if (!enemies[i].IsActive)
                {
                    enemies.RemoveAt(i);
                }
            }
        }

        private void TrySpawnEnemy(TimeSpan lastSpawnTime, TimeSpan spawnInterval, Action spawnMethod, ref TimeSpan lastSpawnUpdateTime, GameTime gameTime)
        {
            if (gameTime.TotalGameTime - lastSpawnTime > spawnInterval)
            {
                spawnMethod();
                lastSpawnUpdateTime = gameTime.TotalGameTime;
            }
        }

        private void SpawnImp(World world, GameTime gameTime)
        {
            Random rand = new Random();

            // Randomize the X-coordinate along the width of the screen
            float randomX = rand.Next(0, Globals.screenWidth);

            // Define the starting position at the top of the screen
            Vector2 position = new Vector2(randomX, 0);

            Vector2 velocity = new Vector2(0, 1); // Example: moving downwards

            // Define screenHeight and routeSteps
            float screenHeight = Globals.screenHeight; // Assuming screenHeight is defined in Globals
            int routeSteps = 10; // Example value, adjust as needed

            // Instantiate the RandomStepRouteFactory and create a route
            IEnemyRouteFactory routeFactory = new RandomStepRouteFactory();
            Route randomStepRoute = routeFactory.CreateRoute(position, Globals.screenWidth, screenHeight, routeSteps);

            // Create the Imp using the factory with the route
            Enemy imp = enemyFactory.CreateImp(position, velocity, randomStepRoute, gameTime);

            // Subscribe to any necessary events, e.g., EnemyHit
            imp.EnemyHit += (enemy) =>
            {
                world.score += 5; // Update the score or any other game state
                                  // Additional logic when an enemy is hit, if required
            };

            enemies.Add(imp);
        }

        private void SpawnL1Mob(World world, GameTime gameTime)
        {
            Random rand = new Random();

            // Randomize the X-coordinate along the width of the screen
            float randomX = rand.Next(0, Globals.screenWidth);

            // Define the starting position at the top of the screen
            Vector2 position = new Vector2(randomX, 0);

            Vector2 velocity = new Vector2(0, 1); // Example: moving downwards

            // Define the route for the L1 Boss using the zigzag route factory from the world
            int routeSteps = 10; // Example value, adjust as needed
            Route bossRoute = world.ZigZagRouteFactory.CreateRoute(position, Globals.screenWidth, Globals.screenHeight, routeSteps);

            // Create the L1 Boss using the factory with the route
            Enemy newBoss = enemyFactory.CreateL1Mob(position, velocity, bossRoute, 1, gameTime);

            // Subscribe to any necessary events, e.g., EnemyHit
            newBoss.EnemyHit += (enemy) =>
            {
                world.score += 5; // Update the score or any other game state
                                  // Additional logic when an enemy is hit, if required
            };

            enemies.Add(newBoss);
        }


        private void SpawnL2Boss(World world, GameTime gameTime)
        {
            if(!bossL2Spawned)
            {
                Random rand = new Random();

                // Randomize the X-coordinate along the width of the screen
                float randomX = rand.Next(0, Globals.screenWidth);

                // Define the starting position at the top of the screen
                Vector2 position = new Vector2(randomX, 0);

                Vector2 velocity = new Vector2(0, 1); // Example: moving downwards

                // Define the route for the L1 Boss
                int routeSteps = 10; // Example value, adjust as needed
                Route bossRoute = world.ZigZagRouteFactory.CreateRoute(position, Globals.screenWidth, Globals.screenHeight, routeSteps);

                // Create the L1 Boss using the factory with the route
                Enemy newBoss = enemyFactory.CreateL2Boss(position, velocity, bossRoute, 1, gameTime);

                // Subscribe to any necessary events, e.g., EnemyHit
                newBoss.EnemyHit += (enemy) =>
                {
                    world.score += 5; // Update the score or any other game state
                                      // Additional logic when an enemy is hit, if required
                };

                enemies.Add(newBoss);
            }
            
        }

        private void SpawnL3Boss(World world, GameTime gameTime)
        {
            Random rand = new Random();

            // Randomize the X-coordinate along the width of the screen
            float randomX = rand.Next(0, Globals.screenWidth);

            // Define the starting position at the top of the screen
            Vector2 position = new Vector2(randomX, 0);

            Vector2 velocity = new Vector2(0, 1); // Example: moving downwards

            // Define the route for the L1 Boss
            int routeSteps = 10; // Example value, adjust as needed
            Route bossRoute = world.ZigZagRouteFactory.CreateRoute(position, Globals.screenWidth, Globals.screenHeight, routeSteps);

            // Create the L1 Boss using the factory with the route
            Enemy newBoss = enemyFactory.CreateL3Boss(position, velocity, bossRoute, 1, gameTime);

            // Subscribe to any necessary events, e.g., EnemyHit
            newBoss.EnemyHit += (enemy) =>
            {
                world.score += 5; // Update the score or any other game state
                                  // Additional logic when an enemy is hit, if required
            };

            enemies.Add(newBoss);
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