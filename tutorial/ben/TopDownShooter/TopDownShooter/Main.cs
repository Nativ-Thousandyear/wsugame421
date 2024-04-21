//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;
//using System.Drawing;
//using TopDownShooter.Source.Gameplay;
//using TopDownShooter.Source.Engine;
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
using SharpDX.Direct2D1;
using TopDownShooter.Gameplay.World;

namespace TopDownShooter
{
    public class Main : Game
    {
        private EnemySpawner enemySpawner;
        private GraphicsDeviceManager graphics;
        //private SpriteBatch _spriteBatch;
        Basic2d cursor;
        private World world;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //IsMouseVisible = true;
        }

        private void OnEnemyHit(Enemy enemy)
        {
            // Increment the score by 5 points for each enemy hit
            score += 5;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Globals.screenWidth = 1000;
            Globals.screenHeight = 700;

            graphics.PreferredBackBufferWidth = Globals.screenWidth;
            graphics.PreferredBackBufferHeight = Globals.screenHeight;
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Initialize global content manager and sprite batch
            Globals.content = this.Content;
            Globals.spriteBatch = new Microsoft.Xna.Framework.Graphics.SpriteBatch(GraphicsDevice);

            scoreFont = Content.Load<SpriteFont>("2d\\Misc\\Arial16");
            //fireballTexture = Content.Load<Texture2D>("2d\\Projectiles\\Fireball");
            // Load enemy sprite and initialize the EnemySpawner;
            Texture2D heroSprite = Content.Load<Texture2D>("2d\\Hero");
            Texture2D impSprite = Content.Load<Texture2D>("2d\\Imp");

            enemySpawner = new EnemySpawner(this, heroSprite, impSprite);
            this.enemySpawner.EnemyHit += OnEnemyHit;
            //enemySpawner = new EnemySpawner(this, enemySprite);

            // Load the cursor texture
            Texture2D cursorTexture = Content.Load<Texture2D>("2d\\Misc\\CursorArrow");

            // Create the cursor object
            cursor = new Basic2d(cursorTexture, new Vector2(0, 0), new Vector2(28, 28));


            // Initialize input controls
            Globals.keyboard = new McKeyboard();
            Globals.mouse = new McMouseControl();

            // Create the world instance
            world = new World(this.Content, enemySpawner.GetEnemies());
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Update the enemy spawner
            enemySpawner.Update(gameTime);
            // TODO: Add your update logic here
            Globals.gameTime = gameTime;
            Globals.keyboard.Update();
            Globals.mouse.Update();


            if (world != null)
            {
                world.Update();
            }

            Globals.keyboard.UpdateOld();
            Globals.mouse.UpdateOld();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Globals.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            // Draw world and gameplay elements
            world.Draw(Vector2.Zero);

            // Draw the enemies
            enemySpawner.Draw(Globals.spriteBatch);

            // Draw the cursor last so it appears on top of everything else
            cursor.Draw(new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y), new Vector2(0, 0));

            // Draw the score
            string scoreText = $"Score: {score}";
            Vector2 scorePosition = new Vector2(10, 10); // position adjustable
            Globals.spriteBatch.DrawString(scoreFont, scoreText, scorePosition, Color.White);

            Globals.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
