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
using TopDownShooter.Source.Gameplay.World;
using TopDownShooter.Source.Gameplay.World.Interfaces;
#endregion

namespace TopDownShooter
{
    public class Fireball : Projectile2d
    {
        Route bulletRoute;
        public Fireball(Texture2D texture, Vector2 pos, Unit owner, Vector2 target, Route bulletRoute)
        : base(texture, pos, new Vector2(20, 20), owner, target)
        {
            this.bulletRoute = bulletRoute;
        }

        public override void Update(Vector2 OFFSET, List<Unit> UNITS)
        {
            base.Update(OFFSET, UNITS);
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
