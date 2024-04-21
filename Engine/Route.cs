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
#endregion

namespace TopDownShooter
{
    public class Route
    {
        public List<RoutePoint> routePoints;

        public Route(List<RoutePoint> routePoints)
        {
            this.routePoints = routePoints;
        }

        public static Route CreateZigZagRoute(Vector2 start, float width, float height, int zigZagCount)
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

        public static Route CreateVerticalRoute(Vector2 start, float height, int steps)
        {
            var routePoints = new List<RoutePoint>();
            float verticalStep = height / steps;

            Vector2 nextPoint = start;
            for (int i = 0; i <= steps; i++)
            {
                nextPoint.Y += verticalStep;

                routePoints.Add(new RoutePoint(nextPoint, 1.0f)); // Adjust speed as needed
            }

            return new Route(routePoints);
        }

        public static Route CreateRandomStepRoute(Vector2 start, float width, float height, int steps)
        {
            var routePoints = new List<RoutePoint>();
            float verticalStep = height / steps;
            Random rand = new Random();

            Vector2 nextPoint = start;
            for (int i = 0; i <= steps; i++)
            {
                // Random horizontal movement - you can adjust the multiplier to control the range
                float horizontalMovement = ((float)rand.NextDouble() - 0.5f) * (width / 4); // Random step within a quarter of the screen width

                nextPoint.X += horizontalMovement;

                // Ensure the X-coordinate stays within the screen bounds
                nextPoint.X = Math.Max(0, Math.Min(nextPoint.X, width));

                // Vertical movement with a small chance of a back step
                float verticalMovement = verticalStep;
                if (rand.NextDouble() < 0.3) // 30% chance of a back step
                {
                    verticalMovement *= -1; // Move backward
                }
                nextPoint.Y += verticalMovement;

                // Ensure the Y-coordinate increases overall, preventing too much backward movement
                nextPoint.Y = Math.Max(nextPoint.Y, 0);

                routePoints.Add(new RoutePoint(nextPoint, 1.0f)); // Adjust speed as needed
            }

            return new Route(routePoints);
        }

        public virtual void Update()
        {
        }

        public virtual bool HasRoute()
        {
            if (routePoints.Any())
            {
                return true;
            }
            return false;
        }

        public virtual void Draw(Vector2 OFFSET)
        {
        }
    }
}
