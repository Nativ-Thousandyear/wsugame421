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
using TopDownShooter.Source.Gameplay.World.Factories;
using TopDownShooter.Source.Gameplay.World.Interfaces;
#endregion

namespace TopDownShooter
{
    public class Hero : Unit
    {
        private Texture2D fireballTexture;
        private float standardSpeed;
        public float tacticalSpeed;

        private IBulletFactory bulletFactory;
        public Hero(Texture2D sprite, Texture2D fireballTexture, Vector2 position, Vector2 dimensions, Route route)
        : base(sprite, position, dimensions, route)
        {
            this.fireballTexture = fireballTexture;
            standardSpeed = 2.0f;
            tacticalSpeed = standardSpeed;
            health = 5;
            bulletFactory = BulletHeroRouteFactory.GetInstance();
        }

        public override void Update()
        {
            // Check if LeftShift is pressed and adjust standardSpeed
            float actualSpeed = Globals.keyboard.GetPress("LeftShift") ? standardSpeed * 2.3f : standardSpeed;

            if (HasRoute(this.route))
            {
                RoutePoint routePoint = route.routePoints[0];

                // If arrived at point, remove point
                if (IsAtPoint(pos, routePoint.point))
                {
                    route.routePoints.RemoveAt(0);
                }
                else
                {
                    Vector2 direction = routePoint.point - this.pos;
                    direction.Normalize();
                    pos += direction * routePoint.speed;
                }
            }
            else
            {
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
            }

            rot = Globals.RotateTowards(pos, new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y));

            if (Globals.mouse.LeftClick())
            {
                // Shoot a fireball
                Vector2 fireballPosition = new Vector2(pos.X, pos.Y);
                Vector2 targetPosition = new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y);
                GameGlobals.PassProjectile(bulletFactory.GetBullet(fireballPosition, this, targetPosition));
            }

            base.Update();
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
