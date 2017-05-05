using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RPGopnik
{
    class Content
    {
        protected Rectangle rect;
        protected Texture2D main_texture;

        public Content(Rectangle rectangle, Texture2D texture)
        {
            rect = rectangle;
            main_texture = texture;
        }

        public virtual void Update(MouseState mouse)
        {

        }
        
        public virtual void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(main_texture, rect, Color.White);
        }
    }
}
