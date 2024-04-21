using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TopDownShooter.Source.Gameplay.World.Factories;
using TopDownShooter.Source.Gameplay.World.Interfaces;

namespace TopDownShooter
{
    public class Enemy : Unit
    {
        public int bulletsPerEnrageWave, millisecondsBetweenEnrageWaves;
        public McTimer enrageTimer, enrageWaveTimer;
        private int currentRoutePointIndex = 0;
        public bool IsActive { get; set; }
        private Vector2 velocity;
        public bool IsAlive { get; private set; }

        public delegate void EnemyHitHandler(Enemy enemy);
        public event EnemyHitHandler EnemyHit;
        private TimeSpan shootingInterval;
        private TimeSpan lastShootingTime;
        public bool isBoss;
        private IBulletFactory bulletFactory;
        // Add Hit property
        public bool Hit { get; private set; }

        public Enemy(Texture2D sprite, Vector2 position, Vector2 velocity, Route route, GameTime gameTime, bool isBoss, IBulletFactory bulletFactory)
            : base(sprite, position, new Vector2(sprite.Width, sprite.Height), route)
        {
            pos = position;
            this.velocity = velocity;
            this.route = route;
            this.IsActive = true;
            this.IsAlive = true;
            this.Hit = false; // Initialize Hit to false
            this.shootingInterval = TimeSpan.FromSeconds(5);
            this.lastShootingTime = gameTime.TotalGameTime;
            enrageTimer = new McTimer(5000);
            enrageWaveTimer = new McTimer(0);
            millisecondsBetweenEnrageWaves = 500;
            bulletsPerEnrageWave = 20;
            this.isBoss = isBoss;
            this.bulletFactory = bulletFactory;
        }

        public override void OnHit(float damage)
        {
            this.IsAlive = false;
            this.IsActive = false;
            this.Hit = true; // Set Hit to true when hit

            // Additional logic for when the enemy is hit can be added here
            // For example, playing a death animation, sound, or spawning particles
            EnemyHit?.Invoke(this);
        }

        public override void Update(GameTime gameTime, Hero hero)
        {
            if (IsActive && route != null && route.routePoints.Count > 0)
            {
                RoutePoint currentPoint = route.routePoints[0];
                if (IsAtPoint(pos, currentPoint.point))
                {
                    route.routePoints.RemoveAt(0);
                    if (route.routePoints.Count == 0)
                    {
                        IsActive = false; // Deactivate if the route is completed
                        return;
                    }
                }
                else
                {
                    Vector2 direction = currentPoint.point - pos;
                    direction.Normalize(); // Ensure the direction is normalized
                    float speed = currentPoint.speed; // Use the speed defined in the route point
                    pos += direction * speed;
                }
            }

            // Check screen bounds
            if (pos.X < 0 || pos.Y < 0 || pos.X > Globals.screenWidth || pos.Y > Globals.screenHeight)
            {
                IsActive = false;
            }

            // Check collision with hero
            if (Globals.GetDistance(pos, hero.pos) < 15)
            {
                hero.OnHit(1);
                IsActive = false;
                IsAlive = false;
                Hit = true; // Set Hit to true when hit
            }

            // Enemy shooting
            if (gameTime.TotalGameTime - this.lastShootingTime > this.shootingInterval)
            {
                // Shoot a fireball
                Vector2 fireballPosition = new Vector2(pos.X + 40, pos.Y + 40);
                Vector2 targetPosition = new Vector2(hero.pos.X, hero.pos.Y);
                GameGlobals.PassProjectile(this.bulletFactory.GetBullet(fireballPosition, this, targetPosition));
                lastShootingTime = gameTime.TotalGameTime;
            }

            // Enrage
            enrageTimer.UpdateTimer();
            enrageWaveTimer.UpdateTimer();
            if (enrageTimer.Test() && enrageWaveTimer.Test() && isBoss)
            {
                for (int i = -bulletsPerEnrageWave; i < bulletsPerEnrageWave; i += 2)
                {
                    Vector2 fireballPosition = new Vector2(pos.X, pos.Y + 40);
                    Vector2 targetPosition = new Vector2(hero.pos.X + (i * 100), hero.pos.Y);
                    Texture2D enemyFireball_sprite = this.bulletFactory.GetSprite();
                    Route bulletRoute = this.bulletFactory.GetRoute(fireballPosition);
                    GameGlobals.PassProjectile(bulletFactory.GetEnrageBullet(fireballPosition, this, targetPosition));
                }
                enrageWaveTimer.Reset(millisecondsBetweenEnrageWaves);
            }

            base.Update(gameTime, hero);
        }

        public override void Draw(Vector2 OFFSET)
        {
            if (IsActive)
            {
                float rotation = (float)Math.PI; // π radians for 180-degree rotation
                Vector2 origin = new Vector2(sprite.Width / 2f, sprite.Height / 2f); // Center of the sprite

                Globals.spriteBatch.Draw(sprite,
                                         new Vector2(pos.X + OFFSET.X, pos.Y + OFFSET.Y),
                                         null, // Source rectangle (null for the whole texture)
                                         Color.White, // Color (no tint)
                                         rotation, // Rotation angle
                                         origin, // Origin of rotation
                                         1f, // Scale
                                         SpriteEffects.None, // Effects
                                         0f); // Layer depth
            }
        }

        public Rectangle Hitbox
        {
            get
            {
                Texture2D sprite = this.Sprite;
                if (sprite == null)
                {
                    throw new InvalidOperationException("Sprite is null in Enemy Hitbox getter.");
                }
                return new Rectangle((int)pos.X - sprite.Width / 2, (int)pos.Y - sprite.Height / 2, sprite.Width, sprite.Height);
            }
        }

        public Vector2 GetEnemyCurrentPosition()
        {
            return pos;
        }
    }
}
