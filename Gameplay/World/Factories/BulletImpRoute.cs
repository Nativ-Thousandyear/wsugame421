using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDownShooter.Source.Gameplay.World.Interfaces;

namespace TopDownShooter.Source.Gameplay.World.Factories
{
    public class BulletImpRoute : IBulletRoute
    {
        int bulletCounts = 0;
        public BulletImpRoute() {
            this.bulletCounts = 100;   
        }
        public Route CreateRoute(Vector2 start, float width, float height, int zigZagCount)
        {
            var routePoints = new List<RoutePoint>();

            Random random = new Random();

            for (int i = 0; i < bulletCounts; i++)
            {
                float x = (float)random.NextDouble() * width; // Random x-coordinate within the screen width
                float y = (float)random.NextDouble() * height; // Random y-coordinate within the screen height
                Vector2 position = new Vector2(x, y);
                routePoints.Add(new RoutePoint(position, 1.0f)); // Adjust speed as needed
            }

            return new Route(routePoints);
        }
    }
}