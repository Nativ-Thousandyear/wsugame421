using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TopDownShooter
{
    public class Enemy : Unit
    {
        public bool IsActive { get; set; }
        private Vector2 velocity;
        public bool IsAlive { get; protected set; }

        public delegate void EnemyHitHandler(Enemy enemy);
        public event EnemyHitHandler EnemyHit;

        public Enemy(Texture2D sprite, Vector2 position, Vector2 velocity)
            : base(sprite, position, new Vector2(sprite.Width, sprite.Height))
        {
            this.velocity = velocity;
            this.IsActive = true;
            this.IsAlive = true;
        }

        public void OnHit()
        {
            this.IsAlive = false;
            this.IsActive = false;
            // Additional logic for when the enemy is hit can be added here
            // For example, playing a death animation, sound, or spawning particles

            EnemyHit?.Invoke(this);
        }

        public override void Update(GameTime gameTime)
        {
            if (IsActive)
            {
                pos += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (pos.X < 0 || pos.Y < 0 || pos.X > 800 || pos.Y > 600) // Check screen bounds
                {
                    IsActive = false;
                }
            }

            base.Update(gameTime); // Call the base Unit's Update method
        }

        public override void Draw(Vector2 OFFSET)
        {
            if (IsActive)
            {
                float rotation = (float)Math.PI; // π radians for 180 degree rotation
                Vector2 origin = new Vector2(sprite.Width / 2f, sprite.Height / 2f); // Center of the sprite

                Globals.spriteBatch.Draw(sprite,
                                         new Vector2(pos.X + OFFSET.X, pos.Y + OFFSET.Y),
                                         null, // Source rectangle (null for whole texture)
                                         Color.White, // Color (no tint)
                                         rotation, // Rotation angle
                                         origin, // Origin of rotation
                                         1f, // Scale
                                         SpriteEffects.None, // Effects
                                         0f); // Layer depth
            }
        }


        /*public override void Draw(Vector2 OFFSET)
        {
            if (IsActive)
            {

                base.Draw(OFFSET); // Call to base Unit Draw
            }
        }*/

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
    }
}


/*namespace TopDownShooter
{
    public class Enemy
    {
        public Vector2 Position { get; private set; }
        public bool IsActive { get; set; }
        private Texture2D sprite;
        private Vector2 velocity;

        public Enemy(Texture2D sprite, Vector2 position, Vector2 velocity)
        {
            this.sprite = sprite;
            this.Position = position;
            this.velocity = velocity;
            this.IsActive = true;
        }

        public Rectangle Hitbox
        {
            get
            {
                return new Rectangle((int)Position.X - sprite.Width / 2, (int)Position.Y - sprite.Height / 2, sprite.Width, sprite.Height);
            }
        }

        public void Update(GameTime gameTime)
        {
            // Update the position of the enemy based on its velocity
            Position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Debugging: Print out position and velocity
            Console.WriteLine($"Enemy Position: {Position}, Velocity: {velocity}");

            // Check if the enemy is off-screen or needs to be deactivated for some reason
            if (Position.X < 0 || Position.Y < 0 || Position.X > 800 || Position.Y > 600)
            {
                // Debugging: Print out a message when deactivating an enemy
                Console.WriteLine("Deactivating enemy due to being off-screen.");
                IsActive = false;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsActive)
            {
                // Calculate the origin (center of the sprite)
                Vector2 origin = new Vector2(sprite.Width / 2f, sprite.Height / 2f);

                // Set rotation to 180 degrees (π radians)
                float rotation = MathHelper.Pi;

                spriteBatch.Draw(sprite, Position, null, Color.White, rotation, origin, 1f, SpriteEffects.None, 0);
            }
        }
    }
}
*/