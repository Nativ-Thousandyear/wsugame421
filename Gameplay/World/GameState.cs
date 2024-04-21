using System;

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
    public enum GameState
    {
        Playing,
        Paused,
        GameOver,
        Victory,
        MainMenu
    }

}

