using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Drawing;


namespace TopDownShooter
{
    public class Main : Game
    {
        private GameState gameState;
        private GameMenu gameMenu;
        public SpriteBatch spriteBatch; // Add this line
        private GraphicsDeviceManager graphics;
        public int NumKilled { get; set; }



        protected override void Initialize()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            gameState = GameState.MainMenu; // Set the initial game state

            gameState = GameState.MainMenu;
            gameMenu = new GameMenu(Content, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight); // Pass the ContentManager and screen dimensions
            gameMenu.AddOption("PLAY", () => gameState = GameState.Playing);
            gameMenu.AddOption("EXIT", () => Exit());

            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice); // Initialize the spriteBatch
            gameMenu.LoadContent(); // Load content for the game menu
        }

        protected override void Update(GameTime gameTime)
        {
            switch (gameState)
            {
                case GameState.MainMenu:
                    // Handle menu input
                    if (Keyboard.GetState().IsKeyDown(Keys.Up))
                        gameMenu.MoveSelectionUp();
                    else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                        gameMenu.MoveSelectionDown();
                    else if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                        gameMenu.ExecuteSelectedOption();
                    break;

                case GameState.Playing:
                    // Update game logic
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                        gameState = GameState.Paused;
                    break;

                case GameState.Paused:
                    // Handle pause menu input
                    if (Keyboard.GetState().IsKeyDown(Keys.Up))
                        gameMenu.MoveSelectionUp();
                    else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                        gameMenu.MoveSelectionDown();
                    else if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                        gameMenu.ExecuteSelectedOption();
                    break;

                // Add handling for other game states (e.g., GameOver, Victory) if needed

                default:
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.CornflowerBlue);

            switch (gameState)
            {
                case GameState.MainMenu:
                case GameState.Paused:
                    spriteBatch.Begin();
                    gameMenu.Draw((Microsoft.Xna.Framework.Graphics.SpriteBatch)spriteBatch);

                    spriteBatch.End();
                    break;

                case GameState.Playing:
                    // Draw game elements
                    break;

                // Add drawing logic for other game states if needed

                default:
                    break;
            }

            base.Draw(gameTime);
        }
    }

}