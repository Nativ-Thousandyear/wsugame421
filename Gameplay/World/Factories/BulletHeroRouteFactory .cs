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
    public class BulletHeroRouteFactory : IBulletFactory
    {
        private static BulletHeroRouteFactory instance;
        private float width, height;
        private int zigZagCount;
        Enemy owner;
        private BulletHeroRouteFactory()
        {
            this.width = Globals.screenWidth;
            this.height = Globals.screenHeight;
            this.zigZagCount = 100;
        }

        public static BulletHeroRouteFactory GetInstance()
        {
            // If the instance is null, create a new instance
            if (instance == null)
            {
                instance = new BulletHeroRouteFactory();
            }
            return instance;
        }

        public Projectile2d GetBullet(Vector2 fireballPosition, Unit bulletOwner, Vector2 targetPosition)
        {
            Texture2D heroFireball_sprite = this.GetSprite();
            Route bulletRoute = this.GetRoute(fireballPosition);
            Projectile2d bullet = new HeroFireball(heroFireball_sprite, fireballPosition, bulletOwner, targetPosition, bulletRoute);
            return bullet;
        }

        public Projectile2d GetEnrageBullet(Vector2 fireballPosition, Unit bulletOwner, Vector2 targetPosition)
        {
            return this.GetBullet(fireballPosition, bulletOwner, targetPosition);
        }

        public Route GetRoute(Vector2 bulletStartPosition)
        {
            BulletHeroRoute route = new BulletHeroRoute();
            return route.CreateRoute(bulletStartPosition, width, height, zigZagCount);
        }

        public Texture2D GetSprite()
        {
            Texture2D heroFireball_sprite = GameGlobals.spriteHandler.get_FireballHero_Sprite();
            return heroFireball_sprite;
        }
    }
}
