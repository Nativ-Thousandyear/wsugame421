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
using System.Reflection.Metadata;
#endregion

namespace TopDownShooter
{
    public class QuantityDisplayBar
    {
        public Basic2d bar, barBKG;
        public int boarder;
        public Color color;

        public QuantityDisplayBar(Vector2 dimensions, int boarder, Color color)
        {
            this.boarder = boarder;
            this.color = color;
            this.bar = new Basic2d(Globals.content.Load<Texture2D>("2d\\Misc\\solid"),
                                   new Vector2(0, 0),
                                   new Vector2(dimensions.X - boarder * 2, dimensions.Y - boarder *2));
            this.barBKG = new Basic2d(Globals.content.Load<Texture2D>("2d\\Misc\\shade"),
                                      new Vector2(0, 0),
                                      new Vector2(dimensions.X, dimensions.Y));
        }

        public virtual void Update(float current, float max)
        {
            bar.dims = new Vector2(current/max*(barBKG.dims.X-boarder*2), bar.dims.Y);
        }

        public virtual void Draw(Vector2 OFFSET)
        {
            barBKG.Draw(OFFSET, new Vector2(0, 0), Color.Black);
            bar.Draw(OFFSET * new Vector2(boarder, boarder), new Vector2(0, 0), color);
        }
    }
}
