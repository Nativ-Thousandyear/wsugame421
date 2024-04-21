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
    public class BulletL1BossRoute : IBulletRoute
    {
        public Route CreateRoute(Vector2 start, float width, float height, int zigZagCount)
        {
            var routePoints = new List<RoutePoint>();

            // Increase the horizontalStep for more pronounced zigzag movement
            // adjust the multiplier (e.g., 0.5f) to control the amplitude of the zigzag
            float horizontalStep = width * 0.5f; // half the width of the screen
            float verticalStep = height / zigZagCount;

            Vector2 nextPoint = start;
            for (int i = 0; i < zigZagCount; i++)
            {
                // Alternate between moving right and left with larger steps
                nextPoint.X += (i % 2 == 0) ? horizontalStep : -horizontalStep;

                // Ensure that the X-coordinate stays within the screen bounds
                nextPoint.X = Math.Max(0, Math.Min(nextPoint.X, width));

                nextPoint.Y += verticalStep;

                routePoints.Add(new RoutePoint(nextPoint, 1.0f)); // Adjust speed as needed
            }

            return new Route(routePoints);
        }

    }
}