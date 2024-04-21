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
    public class Projectile2d : Basic2d
    {
        public float speed;
        public Unit owner;
        public Vector2 direction;
        public bool done;
        public McTimer timer;

        public Rectangle Hitbox
        {
            get
            {
                return new Rectangle((int)pos.X, (int)pos.Y, (int)dims.X, (int)dims.Y);
            }
        }

        public bool IsHit { get; private set; }

        public Projectile2d(Texture2D texture, Vector2 pos, Vector2 dims, Unit owner, Vector2 target)
        : base(texture, pos, dims)
        {
            done = false;
            speed = 5.0f;
            this.owner = owner;
            direction = target - this.owner.pos; // Assuming 'pos' is a field in 'owner'
            direction.Normalize();
            timer = new McTimer(1200);
            rot = Globals.RotateTowards(this.pos, new Vector2(target.X, target.Y)); // Use 'this.pos' to refer to the projectile's position
        }

        public virtual void Update(Vector2 OFFSET, List<Unit> UNITS)
        {
            pos += direction * speed;
            timer.UpdateTimer();

            if (timer.Test() || HitSomething(UNITS))
            {
                done = true;
            }
        }

        public virtual bool HitSomething(List<Unit> units)
        {
            for (int i = 0; i < UNITS.Count; i++)
            {
                if (Globals.GetDistance(pos, UNITS[i].pos) < UNITS[i].hitDist)
                {
                    UNITS[i].GetHit();
                    return true;
                }
            }
            return false;
        }

        /* public virtual bool HitSomething(List<Unit> units)
         {
             foreach (var unit in units)
             {
                 if (unit is Enemy enemy && enemy.IsActive && enemy.Hitbox.Intersects(this.Hitbox))
                 {
                     IsHit = true;
                     return true;
                 }
             }
             return false;
         }*/

        /*public virtual bool HitSomething(List<Unit> UNITS)
        {
            return false;
        }*/

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
