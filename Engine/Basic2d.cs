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
    public class Basic2d
    {
        public Vector2 pos, dims;
        public Texture2D sprite;
        public float rot;

        //private Texture2D sprite;

        protected Texture2D Sprite
        {
            get { return sprite; }
        }

        public Basic2d(Texture2D texture, Vector2 pos, Vector2 dims)
        {
            this.pos = pos; // Assign the class field 'pos' with the constructor argument 'pos'
            this.dims = dims; // Assign the class field 'dims' with the constructor argument 'dims'
            sprite = texture; // Use the texture that's already passed in
        }

        public virtual void Update() 
        {
            
        }

        public virtual void Draw(Vector2 OFFSET)
        {

            if (sprite != null)
            {
                Globals.spriteBatch.Draw(sprite,
                                         new Rectangle((int)(pos.X + OFFSET.X), (int)(pos.Y + OFFSET.Y), (int)(dims.X), (int)(dims.Y)),
                                         null,
                                         Color.White,
                                         rot,
                                         new Vector2(sprite.Bounds.Width / 2, sprite.Bounds.Height / 2),
                                         new SpriteEffects(),
                                         0);
            }
        }

        public virtual void Draw(Vector2 OFFSET, Vector2 ORIGIN, Color COLOR)
        {
            if (sprite != null) 
            {
                Globals.spriteBatch.Draw(sprite,
                                         new Rectangle((int)(pos.X + OFFSET.X), (int)(pos.Y + OFFSET.Y), (int)(dims.X), (int)(dims.Y)),
                                         null,
                                         COLOR,
                                         rot,
                                         new Vector2(ORIGIN.X, ORIGIN.Y),
                                         new SpriteEffects(),
                                         0);
            }
        }
    }
}
