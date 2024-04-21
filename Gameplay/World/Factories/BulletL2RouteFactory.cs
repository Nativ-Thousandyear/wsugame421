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
    public class BulletL2RouteFactory : IBulletFactory
    {
        private static BulletL2RouteFactory instance;
        private float width, height;
        private int zigZagCount;
        Enemy owner;
        private BulletL2RouteFactory()
        {
            this.width = Globals.screenWidth;
            this.height = Globals.screenHeight;
            this.zigZagCount = 100;
        }

        public static BulletL2RouteFactory GetInstance()
        {
            // If the instance is null, create a new instance
            if (instance == null)
            {
                instance = new BulletL2RouteFactory();
            }
            return instance;
        }

        public Projectile2d GetBullet(Vector2 fireballPosition, Unit bulletOwner, Vector2 targetPosition)
        {
            Texture2D enemyFireball_sprite = this.GetSprite();
            Route bulletRoute = this.GetRoute(fireballPosition);
            Projectile2d bullet = new Fireball(enemyFireball_sprite, fireballPosition, bulletOwner, targetPosition, bulletRoute);
            return bullet;
        }

        public Projectile2d GetEnrageBullet(Vector2 fireballPosition, Unit bulletOwner, Vector2 targetPosition)
        {
            Texture2D enemyFireball_sprite = this.GetSprite();
            IBulletRoute route = new BulletL2BossRoute();
            Route bulletRoute = route.CreateRoute(fireballPosition, width, height, zigZagCount);
            Projectile2d bullet = new Boss_L2_Fireball(enemyFireball_sprite, fireballPosition, bulletOwner, targetPosition, bulletRoute);
            return bullet;
        }

        public Route GetRoute(Vector2 bulletStartPosition)
        {
            IBulletRoute route = new BulletHeroRoute();
            return route.CreateRoute(bulletStartPosition, width, height, zigZagCount);
        }

        public Texture2D GetSprite()
        {
            Texture2D enemyFireball_sprite = GameGlobals.spriteHandler.get_FireballL2_Sprite();
            return enemyFireball_sprite;
        }
    }
}
