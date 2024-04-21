using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDownShooter.Source.Gameplay.World.Interfaces;

namespace TopDownShooter.Source.Gameplay.World.Factories
{
    public class BulletL1RouteFactory : IBulletFactory
    {
        private static BulletL1RouteFactory instance;
        private float width, height;
        private int zigZagCount;
        Enemy owner;
        private BulletL1RouteFactory()
        {
            this.width = Globals.screenWidth;
            this.height = Globals.screenHeight;
            this.zigZagCount = 100;
        }

        public static BulletL1RouteFactory GetInstance()
        {
            // If the instance is null, create a new instance
            if (instance == null)
            {
                instance = new BulletL1RouteFactory();
            }
            return instance;
        }

        public Projectile2d GetBullet(Vector2 fireballPosition, Unit bulletOwner, Vector2 targetPosition)
        {
            Texture2D enemyFireball_sprite = this.GetSprite();
            Route bulletRoute = this.GetRoute(fireballPosition);
            Projectile2d bullet = new Boss_L1_Fireball(enemyFireball_sprite, fireballPosition, bulletOwner, targetPosition, bulletRoute);
            return bullet;
        }

        public Projectile2d GetEnrageBullet(Vector2 fireballPosition, Unit bulletOwner, Vector2 targetPosition)
        {
            Texture2D enemyFireball_sprite = this.GetSprite();
            Route bulletRoute = this.GetRoute(fireballPosition);
            Projectile2d bullet = new Fireball(enemyFireball_sprite, fireballPosition, bulletOwner, targetPosition, bulletRoute);
            return bullet;
        }

        public Route GetRoute(Vector2 bulletStartPosition)
        {
            BulletL1BossRoute route = new BulletL1BossRoute();
            return route.CreateRoute(bulletStartPosition, width, height, zigZagCount);
        }

        public Texture2D GetSprite()
        {
            Texture2D enemyFireball_sprite = GameGlobals.spriteHandler.get_EnemyFireball_Sprite();
            return enemyFireball_sprite;
        }
    }
}
