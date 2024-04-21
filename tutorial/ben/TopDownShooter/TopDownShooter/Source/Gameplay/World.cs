#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Reflection.Metadata;
#endregion

namespace TopDownShooter
{
    public class World
    {
        public List<Enemy> enemies;
        public Vector2 offset;
        public Hero hero;
        public List<Projectile2d> projectiles = new List<Projectile2d>();
        public List<Mob> mobs = new List<Mob>();
        public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

        public World()
        {
            Texture2D heroTexture = content.Load<Texture2D>("2d\\Hero");
            fireballTexture = content.Load<Texture2D>("2d\\Projectiles\\Fireball");
            Vector2 startPosition = new Vector2(300, 300); // As per your commented-out code
            Vector2 dimensions = new Vector2(48, 48); // As per your commented-out code

            hero = new Hero(heroTexture, fireballTexture, startPosition, dimensions);
            //hero = new Hero(heroTexture, startPosition, dimensions);
            this.enemies = enemies;
            GameGlobals.PassProjectile = AddProjectile;
            GameGlobals.PassMob = AddMob;

            offset = new Vector2(0, 0);

            spawnPoints.Add(new SpawnPoint("2d\\Misc\\circle", new Vector2(50, 50), new Vector2(35, 35)));
            spawnPoints.Add(new SpawnPoint("2d\\Misc\\circle", new Vector2(Globals.screenWidth/2, 50), new Vector2(35, 35)));
            spawnPoints.Add(new SpawnPoint("2d\\Misc\\circle", new Vector2(Globals.screenWidth - 50, 50), new Vector2(35, 35)));

            spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(500);
            spawnPoints[spawnPoints.Count - 2].spawnTimer.AddToTimer(1000);
        }
        public virtual void Update()
        {
            hero.Update(offset);

            for (int i = 0; i < spawnPoints.Count; i++)
            {
                spawnPoints[i].Update(offset);
            }

            foreach (var projectile in projectiles)
            {
                projectile.Update(Vector2.Zero, enemies.Cast<Unit>().ToList());
            }


            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Update(offset, mobs.ToList<Unit>());

                if (projectiles[i].done)
                {
                    projectiles.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < mobs.Count; i++)
            {
                mobs[i].Update(offset, hero);

                if (mobs[i].dead)
                {
                    mobs.RemoveAt(i);
                    i--;
                }
            }
        }

        public virtual void AddMob(object INFO)
        {
            mobs.Add((Mob) INFO);
        }

        public virtual void AddProjectile(object INFO)
        {
            projectiles.Add((Projectile2d) INFO);
        }

        public virtual void Draw(Vector2 OFFSET)
        {
            hero.Draw(OFFSET);

            for (int i = 0; i < projectiles.Count;i++)
            {
                projectiles[i].Draw(offset);
            }

            for (int i = 0; i < spawnPoints.Count; i++)
            {
                spawnPoints[i].Draw(offset);
            }

            for (int i = 0; i < mobs.Count; i++)
            {
                mobs[i].Draw(offset);
            }
        }
    }
}
