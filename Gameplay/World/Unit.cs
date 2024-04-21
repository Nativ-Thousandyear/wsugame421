#region Includes
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    public class Unit : Basic2d
    {
        public float speed, health, healthMax, hitDist;
        public Route route;
        public bool dead;

        public Unit(Texture2D texture, Vector2 pos, Vector2 dims, Route route)
        : base(texture, pos, dims)
        {
            speed = 2.0f;
            health = 1;
            healthMax = health;
            hitDist = 35.0f;
            this.route = route;
        }

        public virtual void Update(GameTime gameTime, Hero hero)
        {
            // Check if the game is paused before updating
            if (!GameGlobals.isPaused)
            {
                base.Update();
                // Add unit update logic here...
            }
        }

        public virtual void OnHit(float damage)
        {
            health -= damage;
            if (health <= 0)
            {
                dead = true;
            }
        }

        public virtual bool HasRoute(Route route)
        {
            if (route.HasRoute())
            {
                return true;
            }
            return false;
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
