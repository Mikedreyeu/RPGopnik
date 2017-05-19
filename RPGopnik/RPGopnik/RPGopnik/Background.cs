using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RPGopnik
{
    class Background
    {
        Texture2D bg_texture;
        Rectangle bg_rect;
        public Background(Texture2D background)
        {
            bg_texture = background;
            bg_rect = new Rectangle(0, 0, 1280, 720);
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(bg_texture, bg_rect, Color.White);
        }
    }
}
