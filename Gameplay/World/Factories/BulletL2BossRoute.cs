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
    public class BulletL2BossRoute : IBulletRoute
    {
        private int numTurns, numPointsPerTurn;
        public BulletL2BossRoute()
        {
            numPointsPerTurn = 3;
            numTurns = 6;
        }
        public Route CreateRoute(Vector2 start, float width, float height, int zigZagCount)
        {
            var routePoints = new List<RoutePoint>();

            // Calculate the angle increment per point
            float angleIncrement = 360f / (numTurns * numPointsPerTurn);

            // Start angle
            float angle = 0f;
            float radius = 300;
            Vector2 center = Vector2.Zero;
            // Iterate through each turn
            for (int turn = 0; turn < numTurns; turn++)
            {
                // Iterate through each point in the turn
                center.X = start.X;
                center.Y = start.Y + 200;

                for (int i = 0; i < numPointsPerTurn; i++)
                {
                    // Calculate the position of the current point on the spiral
                    float radians = angle * (float)Math.PI / 180; // Convert angle to radians
                    float x = center.X + radius * (float)Math.Cos(radians);
                    float y = center.Y + radius * (float)Math.Sin(radians) + turn * height;


                    // Add the route point to the list
                    routePoints.Add(new RoutePoint(new Vector2(x, y), 1.0f)); // Adjust speed as needed

                    // Increment the angle
                    angle += angleIncrement;
                }

                // Increase the radius for the next turn to create the spiral effect
                radius += height * 0.1f; // Adjust the multiplier as needed
            }

            return new Route(routePoints);
        }

    }
}