#region Includes
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace TopDownShooter
{
    public class Projectile2d : Basic2d
    {
        public Vector2 direction;
        public McTimer timer;
        public float speed;
        public Unit owner;
        public bool done;
        public bool IsHit { get; set; }


        // Updated property name for consistency
        public Enemy HitEnemy { get; private set; }

        public Rectangle Hitbox
        {
            get
            {
                return new Rectangle((int)pos.X, (int)pos.Y, (int)dims.X, (int)dims.Y);
            }
        }

       

        public Projectile2d(Texture2D texture, Vector2 pos, Vector2 dims, Unit owner, Vector2 target)
            : base(texture, pos, dims)
        {
            done = false;
            speed = 5.0f;
            this.owner = owner;

            // Direction is the normalized vector between target and shooter
            direction = target - this.owner.pos;
            direction.Normalize();

            // To turn projectile in the direction traveling
            rot = Globals.RotateTowards(this.pos, new Vector2(target.X, target.Y));

            timer = new McTimer(12000);
        }

        public virtual void Update(Vector2 OFFSET, List<Unit> UNITS)
        {
            if (!GameGlobals.isPaused)
            {
                pos += direction * speed;
                timer.UpdateTimer();

                if (timer.Test() || HitSomething(UNITS))
                {
                    done = true;
                }
            }
        }
        

        public virtual bool HitSomething(List<Unit> units)
        {
            for (int i = 0; i < units.Count; i++)
            {
                if (Globals.GetDistance(pos, units[i].pos) < 35.0f && owner.GetType() != typeof(Enemy))
                {
                    IsHit = true;
                    HitEnemy = units[i] as Enemy; // Updated property name
                    units[i].OnHit(1); // Call the new method to handle the enemy being hit
                    return true;
                }
            }

            return false;
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
