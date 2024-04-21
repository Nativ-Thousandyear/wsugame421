using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks.Sources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using TopDownShooter.Source.Gameplay.World.Factories;
using TopDownShooter.Source.Gameplay.World.Interfaces;

namespace TopDownShooter
{
    public class SpriteHandler
    {
        private Texture2D heroSprite;
        private Texture2D impSprite;
        private Texture2D l1_boss_Sprite;
        private Texture2D l2_boss_Sprite;
        private Texture2D l3_boss_Sprite;
        private Texture2D fireballSprite;
        private Texture2D enemyFireballSprite;
        private Texture2D fireballImpSprite, fireballHeroSprite, fireballL2Sprite;

        public SpriteHandler()
        {
            // Initialize your properties here if needed
        }

        // Getter for heroSprite
        public Texture2D getHeroSprite()
        {
            return heroSprite;
        }

        // Setter for heroSprite
        public void setHeroSprite(Texture2D heroSprite)
        {
            this.heroSprite = heroSprite;
        }

        // Getter for impSprite
        public Texture2D getImpSprite()
        {
            return impSprite;
        }

        // Setter for impSprite
        public void setImpSprite(Texture2D impSprite)
        {
            this.impSprite = impSprite;
        }

        // Getter for l1_boss_Sprite
        public Texture2D getL1_boss_Sprite()
        {
            return l1_boss_Sprite;
        }

        // Setter for l1_boss_Sprite
        public void setL1_boss_Sprite(Texture2D l1_boss_Sprite)
        {
            this.l1_boss_Sprite = l1_boss_Sprite;
        }

        // Getter for l2_boss_Sprite
        public Texture2D getL2_boss_Sprite()
        {
            return l2_boss_Sprite;
        }

        // Setter for l2_boss_Sprite
        public void setL2_boss_Sprite(Texture2D l2_boss_Sprite)
        {
            this.l2_boss_Sprite = l2_boss_Sprite;
        }

        // Getter for l3_boss_Sprite
        public Texture2D getL3_boss_Sprite()
        {
            return l3_boss_Sprite;
        }

        // Setter for l3_boss_Sprite
        public void setL3_boss_Sprite(Texture2D l3_boss_Sprite)
        {
            this.l3_boss_Sprite = l3_boss_Sprite;
        }

        public Texture2D get_Fireball_Sprite()
        {
            return fireballSprite;
        }

        // Setter for l3_boss_Sprite
        public void set_Fireball_Sprite(Texture2D fireballSprite)
        {
            this.fireballSprite = fireballSprite;
        }

        public Texture2D get_EnemyFireball_Sprite()
        {
            return enemyFireballSprite;
        }

        // Setter for l3_boss_Sprite
        public void set_EnemyFireball_Sprite(Texture2D enemyFireballSprite)
        {
            this.enemyFireballSprite = enemyFireballSprite;
        }

        public Texture2D get_FireballImp_Sprite()
        {
            return fireballImpSprite;
        }

        // Setter for l3_boss_Sprite
        public void set_FireballImp_Sprite(Texture2D fireballImpSprite)
        {
            this.fireballImpSprite = fireballImpSprite;
        }

        public Texture2D get_FireballHero_Sprite()
        {
            return fireballHeroSprite;
        }

        // Setter for l3_boss_Sprite
        public void set_FireballHerp_Sprite(Texture2D fireballHeroSprite)
        {
            this.fireballHeroSprite = fireballHeroSprite;
        }

        public Texture2D get_FireballL2_Sprite()
        {
            return fireballL2Sprite;
        }

        // Setter for l3_boss_Sprite
        public void set_FireballL2_Sprite(Texture2D fireballL2Sprite)
        {
            this.fireballL2Sprite = fireballL2Sprite;
        }
    }

}