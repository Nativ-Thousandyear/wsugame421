using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using TopDownShooter.Source.Gameplay.World.Interfaces;
using TopDownShooter.Source.Gameplay.World.Factories;
using System.Linq;
using TopDownShooter;
using Microsoft.Xna.Framework.Input;


namespace TopDownShooter
{
    public class World
    {
        private IEnemyFactory enemyFactory;
        private IEnemyRouteFactory randomStepRouteFactory;
        private IEnemyRouteFactory zigZagRouteFactory;
        private IEnemyRouteFactory verticalRouteFactory;
        private EnemySpawner enemySpawner;
        public List<Enemy> enemies;
        public Vector2 offset;
        public int score;
        public Hero hero;
        internal Main main; // Reference to the Main class
        public Timer timer;
        public ProjectileHandler projectileHandler;
        private List<Projectile2d> projectiles;


        // Public getter for RandomStepRouteFactory
        public IEnemyRouteFactory RandomStepRouteFactory
        {
            get { return randomStepRouteFactory; }
        }

        // Public getter for ZigZagRouteFactory
        public IEnemyRouteFactory ZigZagRouteFactory
        {
            get { return zigZagRouteFactory; }
        }

        // Public getter for VerticalRouteFactory
        public IEnemyRouteFactory VerticalRouteFactory
        {
            get { return verticalRouteFactory; }
        }

        public World(ContentManager content, Main main, Timer timer)
        {
            score = 0;
            GameGlobals.PassProjectile = AddProjectile;
            this.main = main;

            Texture2D fireballTexture = content.Load<Texture2D>("2d\\Projectiles\\Fireball");
            Texture2D enemyFireballSprite = content.Load<Texture2D>("2d\\Projectiles\\enemy_fireball");
            Texture2D l1_boss_Sprite = content.Load<Texture2D>("2d\\L1");
            Texture2D l2_boss_Sprite = content.Load<Texture2D>("2d\\L2");
            Texture2D l3_boss_Sprite = content.Load<Texture2D>("2d\\L3");
            Texture2D heroSprite = content.Load<Texture2D>("2d\\Hero");
            Texture2D impSprite = content.Load<Texture2D>("2d\\Imp");
            Texture2D fireballImpSprite = content.Load<Texture2D>("2d\\Projectiles\\BulletImp");
            Texture2D fireballHeroSprite = content.Load<Texture2D>("2d\\Projectiles\\HeroFireball");
            Texture2D fireballL2Sprite = content.Load<Texture2D>("2d\\Projectiles\\BulletL2");


            SpriteHandler spriteHandler = new SpriteHandler();
            spriteHandler.setHeroSprite(heroSprite);
            spriteHandler.setImpSprite(impSprite);
            spriteHandler.set_Fireball_Sprite(fireballTexture);
            spriteHandler.set_EnemyFireball_Sprite(enemyFireballSprite);
            spriteHandler.set_FireballImp_Sprite(fireballImpSprite);
            spriteHandler.set_FireballHerp_Sprite(fireballHeroSprite);
            spriteHandler.set_FireballL2_Sprite(fireballL2Sprite);
            GameGlobals.spriteHandler = spriteHandler;

            projectileHandler = new ProjectileHandler();

            Vector2 startPosition = new Vector2(300, 300);
            Vector2 dimensions = new Vector2(48, 48);

            List<RoutePoint> routePoints = new List<RoutePoint>();
            routePoints.Add(new RoutePoint(new Vector2(300, 300), 3));
            routePoints.Add(new RoutePoint(new Vector2(50, 50), 3));
            routePoints.Add(new RoutePoint(new Vector2(600, 600), 7));
            routePoints.Add(new RoutePoint(new Vector2(500, 500), 1));
            routePoints.Add(new RoutePoint(new Vector2(300, 300), 9));
            Route route = new Route(routePoints);

            hero = new Hero(heroSprite, fireballTexture, startPosition, dimensions, route);

            enemyFactory = new StandardEnemyFactory(impSprite, l1_boss_Sprite, l2_boss_Sprite, l3_boss_Sprite);
            randomStepRouteFactory = new RandomStepRouteFactory();
            zigZagRouteFactory = new ZigZagRouteFactory();
            verticalRouteFactory = new VerticalRouteFactory();

            this.enemySpawner = new EnemySpawner(this, enemyFactory, timer);
            this.enemies = enemySpawner.GetEnemies();

            offset = new Vector2(0, 0);
            GameGlobals.gameState = GameState.Playing; // Start the game in the playing state
            GameGlobals.isPaused = false;
            projectiles = new List<Projectile2d>();

        }

        public virtual void Update(GameTime gameTime)
        {
            // Check if the game is paused
            if (!GameGlobals.isPaused)
            {
                hero.Update();
                enemySpawner.Update(gameTime, this, hero);
                projectileHandler.Update(offset, enemies.Cast<Unit>().ToList(), enemies, main, score);
            }

            // Handle pause input
            if (GameGlobals.gameState == GameState.Playing &&
                (Globals.keyboard.GetPress("escape") || GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed))
            {
                GameGlobals.isPaused = !GameGlobals.isPaused; // Toggle pause state
                if (GameGlobals.isPaused)
                {
                    GameGlobals.gameState = GameState.Paused; // Update game state to paused
                }
                else
                {
                    GameGlobals.gameState = GameState.Playing; // Update game state to playing
                }
            }
        }
        public void AddProjectile(object projectile)
        {
            // Cast the object to Projectile2d and add it to the list of projectiles
            projectiles.Add((Projectile2d)projectile);
        }



        public virtual void Draw(Vector2 OFFSET)
        {
            // Draw only if the game is not paused
            if (!GameGlobals.isPaused)
            {
                hero.Draw(OFFSET);
                enemySpawner.Draw(Globals.spriteBatch);
                projectileHandler.Draw(OFFSET);
            }
        }
    }
}