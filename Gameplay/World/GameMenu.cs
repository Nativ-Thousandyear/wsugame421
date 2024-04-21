using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace TopDownShooter
{
    public class GameMenu
    {
        private ContentManager content;
        private int screenWidth, screenHeight;
        private string[] menuOptions;
        private int selectedOptionIndex;

        public GameMenu(ContentManager content, int screenWidth, int screenHeight)
        {
            this.content = content;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            this.menuOptions = new string[] { "PLAY", "EXIT" };
            this.selectedOptionIndex = 0;
        }

        public void LoadContent()
        {
            // Load content for menu items, textures, fonts, etc.
        }

        public void MoveSelectionUp()
        {
            selectedOptionIndex = (selectedOptionIndex - 1 + menuOptions.Length) % menuOptions.Length;
        }

        public void MoveSelectionDown()
        {
            selectedOptionIndex = (selectedOptionIndex + 1) % menuOptions.Length;
        }

        public void ExecuteSelectedOption()
        {
            string selectedOption = menuOptions[selectedOptionIndex];
            if (selectedOption == "PLAY")
            {
                // Handle the PLAY option (e.g., start the game)
                // For now, let's just print a message to the console
                Console.WriteLine("Starting the game...");
            }
            else if (selectedOption == "EXIT")
            {
                // Handle the EXIT option (e.g., exit the game)
                // For now, let's just exit the application
                Environment.Exit(0);
            }
        }
        public void AddOption(string option, Action action)
        {
            // Add a new option to the menu
            string[] newOptions = new string[menuOptions.Length + 1];
            menuOptions.CopyTo(newOptions, 0);
            newOptions[newOptions.Length - 1] = option;
            menuOptions = newOptions;
        }

        public void Update(GameTime gameTime)
        {
            // Update menu logic, handle input, etc.
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw menu items, textures, fonts, etc.
            Vector2 position = new Vector2(screenWidth / 2, screenHeight / 2);
            Vector2 offset = new Vector2(0, 30); // Spacing between options

            // Draw menu options
            for (int i = 0; i < menuOptions.Length; i++)
            {
                Color color = (i == selectedOptionIndex) ? Color.Yellow : Color.White;
                Vector2 optionPosition = position + i * offset;
                spriteBatch.DrawString(Globals.defaultFont, menuOptions[i], optionPosition, color);
            }
        }
    }
}
