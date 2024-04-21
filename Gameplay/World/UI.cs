#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
#endregion

namespace TopDownShooter
{
    public class UI
    {
        public QuantityDisplayBar healthBar;
        public SpriteFont scoreFont;
        private Timer timer; // Updated timer variable name

        public UI(GraphicsDeviceManager graphics)
        {
            scoreFont = Globals.content.Load<SpriteFont>("2d\\Misc\\Arial16");
            healthBar = new QuantityDisplayBar(new Vector2(104, 16), 1, Color.Red);

            // Initialize the countdown timer
            TimeSpan initialTime = TimeSpan.FromMinutes(2);
            timer = new Timer(initialTime, scoreFont, graphics); // Updated timer variable name
            timer.Start(); // Start the timer or start it elsewhere as needed
        }

        public Timer GetTimer()
        {
            return timer;
        }

        public virtual void Update(World WORLD, GameTime gameTime)
        {
            // Update the UI elements only if the game is not paused
            if (!GameGlobals.isPaused)
            {
                healthBar.Update(WORLD.hero.health, WORLD.hero.healthMax);

                // Update the timer
                timer.Update(gameTime); // Updated timer variable name
            }
        }

        public virtual void Draw(World WORLD)
        {
            // Draw the score
            string scoreText = "Score: " + WORLD.score;
            Vector2 scorePosition = new Vector2(10, 10); // position adjustable
            Globals.spriteBatch.DrawString(scoreFont, scoreText, scorePosition, Color.White);

            // Draw the health bar
            healthBar.Draw(new Vector2(20, Globals.screenHeight - 40));

            // Draw the timer only if the game is not paused
            if (!GameGlobals.isPaused)
            {
                timer.Draw(Globals.spriteBatch); // Updated timer variable name
            }
        }
    }
}
