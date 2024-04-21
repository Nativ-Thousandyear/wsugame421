using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownShooter.Source.Gameplay.World.Interfaces
{
    public interface IEnemyRouteFactory
    {
        Route CreateRoute(Vector2 startPosition, float screenWidth, float screenHeight, int routeSteps);
    }
}