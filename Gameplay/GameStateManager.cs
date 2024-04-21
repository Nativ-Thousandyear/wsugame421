#region Includes
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.MediaFoundation;
using TopDownShooter;
#endregion

namespace TopDownShooter
{
    public class GameStateManager
    {
        private GameState currentState;

        public GameStateManager()
        {
            currentState = GameState.Playing; // Start the game in the playing state
        }

        public GameState CurrentState
        {
            get { return currentState; }
            set { currentState = value; }
        }
    }
}

