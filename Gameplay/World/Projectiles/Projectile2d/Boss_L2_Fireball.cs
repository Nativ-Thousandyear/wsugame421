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
    public class Boss_L2_Fireball : Projectile2d
    {
        private Route bulletRoute;
        private Boolean IsActive;
        Vector2 target;
        public Boss_L2_Fireball(Texture2D texture, Vector2 pos, Unit owner, Vector2 target, Route bulletRoute)
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

            timer = new McTimer(5000);
        }

        public override void Update(Vector2 OFFSET, List<Unit> UNITS)
        {
            if (this.bulletRoute != null && this.bulletRoute.routePoints.Count > 0 && this.IsActive)
            {

                RoutePoint currentPoint = this.bulletRoute.routePoints[0];

                if (IsAtPoint(pos, currentPoint.point))
                {
                    this.bulletRoute.routePoints.RemoveAt(0);
                    if (this.bulletRoute.routePoints.Count == 0)
                    {
                        IsActive = false; // Deactivate if the route is completed
                 
                    }
                }

                Vector2 direction = this.target - currentPoint.point;
                direction.Normalize(); // Ensure the direction is normalized
                float speed = currentPoint.speed; // Use the speed defined in the route point
                pos += direction * speed;

                timer.UpdateTimer();

                if (timer.Test() || HitSomething(UNITS))
                {
                    done = true;
                }
            }
            else
            {
                base.Update(OFFSET, UNITS);
            }
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
