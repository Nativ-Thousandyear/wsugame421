#region Includes
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public class Hero : Unit
    {
        public Hero(string PATH, Vector2 POS, Vector2 DIMS) : base(PATH, POS, DIMS)
        {
            this.fireballTexture = fireballTexture; // Set the fireball texture
            standardSpeed = 2.0f;
            tacticalSpeed = standardSpeed;
        }

        public override void Update(Vector2 OFFSET)
        {
            // Check if LeftShift is pressed and adjust standardSpeed
            float actualSpeed = Globals.keyboard.GetPress("LeftShift") ? standardSpeed * 2.3f : standardSpeed;

            if (Globals.keyboard.GetPress("A"))
            {
                pos = new Vector2(pos.X - actualSpeed, pos.Y);
            }

            if (Globals.keyboard.GetPress("D"))
            {
                pos = new Vector2(pos.X + actualSpeed, pos.Y);
            }

            if (Globals.keyboard.GetPress("W"))
            {
                pos = new Vector2(pos.X, pos.Y - actualSpeed);
            }

            if (Globals.keyboard.GetPress("S"))
            {
                pos = new Vector2(pos.X, pos.Y + actualSpeed);
            }

            rot = Globals.RotateTowards(pos, new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y));

            if (Globals.mouse.LeftClick())
            {
                Vector2 fireballPosition = new Vector2(pos.X, pos.Y);
                Vector2 targetPosition = new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y);

                GameGlobals.PassProjectile(new Fireball(fireballTexture, fireballPosition, this, targetPosition));
            }

            base.Update(OFFSET);
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
