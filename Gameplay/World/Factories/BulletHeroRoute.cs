using Microsoft.Xna.Framework;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDownShooter.Source.Gameplay.World.Interfaces;

namespace TopDownShooter.Source.Gameplay.World.Factories
{
    public class BulletHeroRoute : IBulletRoute
    {
        public Route CreateRoute(Vector2 start, float width, float height, int zigZagCount)
        {
            var routePoints = new List<RoutePoint>();
            return new Route(routePoints);
        }

    }
}