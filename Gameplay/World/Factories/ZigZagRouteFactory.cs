using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDownShooter.Source.Gameplay.World.Interfaces;

namespace TopDownShooter.Source.Gameplay.World.Factories
{
    public class ZigZagRouteFactory : IEnemyRouteFactory
    {
        public Route CreateRoute(Vector2 startPosition, float screenWidth, float screenHeight, int routeSteps)
        {
            // Adjust the width parameter as needed to control the amplitude of the zig zag
            float zigZagWidth = screenWidth * 0.5f; // Example: half the width of the screen

            return Route.CreateZigZagRoute(startPosition, zigZagWidth, screenHeight, routeSteps);
        }
    }
}