using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownShooter
{
    public class Boss_L1 : Unit
    {


        public Boss_L1(Texture2D texture, Vector2 pos, Route route)
        : base(texture, pos, new Vector2(5,5), route)
        {
            speed = 2.0f;
        }

        public virtual void Update(Vector2 offset, Hero hero)
        {
            AI(hero); 
            base.Update();
        }

        public virtual void AI(Hero hero) {
            pos += Globals.CurvyMovement(hero.pos, pos, speed, 3.0f, 1.5f);
            rot = Globals.RotateTowards(pos, hero.pos);
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
