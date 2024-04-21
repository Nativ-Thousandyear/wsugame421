#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
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
    public class GameGlobals
    {
        
        public static PassObject PassProjectile, PassBoss_L1;
        public static SpriteHandler spriteHandler;
        public static GameState gameState; // Add a variable to store the game state
        public static bool isPaused;
    }
}
