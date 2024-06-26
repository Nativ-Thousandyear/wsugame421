﻿#region Includes
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
#endregion

namespace TopDownShooter
{
    public class SpawnPoint : Basic2d
    {
        public bool dead;
        public float hitDist;
        public McTimer spawnTimer = new McTimer(2200);

        public SpawnPoint(string PATH, Vector2 POS, Vector2 DIMS) : base(PATH, POS, DIMS)
        {
            dead = false;
            hitDist = 35.0f;
        }

        public override void Update(Vector2 OFFSET)
        {
            spawnTimer.UpdateTimer();

            if (spawnTimer.Test())
            {
                SpawnMob();
                spawnTimer.ResetToZero();
            }

            base.Update(OFFSET);
        }

        public virtual void GetHit()
        {
            dead = true;
        }

        public virtual void SpawnMob()
        {
            GameGlobals.PassMob(new Imp(new Vector2(pos.X, pos.Y)));
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
