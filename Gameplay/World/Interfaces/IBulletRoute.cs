using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownShooter.Source.Gameplay.World.Interfaces
{
    public interface IBulletRoute
    {
        Route CreateRoute(Vector2 start, float width, float height, int zigZagCount);
    }
}