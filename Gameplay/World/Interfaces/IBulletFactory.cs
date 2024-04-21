using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownShooter.Source.Gameplay.World.Interfaces
{
    public interface IBulletFactory
    {
        Texture2D GetSprite();
        Route GetRoute(Vector2 enemyPos);
        Projectile2d GetBullet( Vector2 fireballPosition, Unit bulletOwner, Vector2 targetPosition);
        Projectile2d GetEnrageBullet(Vector2 fireballPosition, Unit bulletOwner, Vector2 targetPosition);
    }
}