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
    public class RoutePoint
    {
        public Vector2 point;
        public float speed;
        public RoutePoint(Vector2 point, float speed)
        {
            this.point = point;
            this.speed = speed;
        }

        public virtual void Update()
        {
        }

        public virtual void Draw(Vector2 OFFSET)
        {
        }
    }
}