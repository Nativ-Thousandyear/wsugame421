#region Includes
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.MediaFoundation;
using TopDownShooter;
#endregion

namespace TopDownShooter
{
    public class ProjectileHandler
    {
        public List<Projectile2d> projectiles;
        public ProjectileHandler()
        {
            projectiles = new List<Projectile2d>();
        }

        public virtual void Update(Vector2 offset, List<Unit> units, List<Enemy> enemies, Main main, int score)
        {
            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                projectiles[i].Update(offset, units);

                if (projectiles[i].done)
                {
                    HandleEnemyKill(projectiles[i].HitEnemy, enemies, main, score);
                    projectiles.RemoveAt(i);
                }
            }
        }
        public void HandleEnemyKill(Enemy enemy, List<Enemy> enemies, Main main, int score)
        {
            if (enemy != null && enemy.Hit)
            {
                main.NumKilled++;
                score += 5; // Assuming each enemy hit adds 5 to the score
            }
            enemies.Remove(enemy);
        }

        public void Add(Projectile2d PROJECTILE)
        {
            projectiles.Add(PROJECTILE);
        }

        public virtual void Draw(Vector2 OFFSET)
        {
            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Draw(OFFSET);
            }
        }
    }
}
