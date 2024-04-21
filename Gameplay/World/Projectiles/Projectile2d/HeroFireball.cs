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
using TopDownShooter;
#endregion

namespace TopDownShooter
{
    public class HeroFireball : Projectile2d
    {
        private Route bulletRoute;
        private Boolean IsActive;
        Vector2 target;
        public HeroFireball(Texture2D texture, Vector2 pos, Unit owner, Vector2 target, Route bulletRoute)
        : base(texture, pos, new Vector2(30, 30), owner, target)
        {
            this.bulletRoute = bulletRoute;
            this.IsActive = true;

            done = false;
            speed = 5.0f;
            this.owner = owner;
            this.target = target;

            // Direction is the normalized vector between target and shooter
            direction = target - this.owner.pos;
            direction.Normalize();

            // To turn projectile in the direction traveling
            rot = Globals.RotateTowards(this.pos, new Vector2(target.X, target.Y));

            timer = new McTimer(30000);
        }

        public override void Update(Vector2 OFFSET, List<Unit> UNITS)
        {
            base.Update(OFFSET, UNITS);
        }

        public virtual bool IsAtPoint(Vector2 pos, Vector2 point)
        {
            if (Math.Abs(pos.X - point.X) < 5 &&
                Math.Abs(pos.Y - point.Y) < 5)
            {
                return true;
            }
            return false;
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }

    }
}
