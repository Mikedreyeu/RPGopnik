using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RPGopnik
{
    class Menu
    {
        public static Background background;
        List<Content> content_list;

        public Menu(List<Content> content_list)
        {
            this.content_list = content_list;
        }

        public void Update()
        {
            foreach (Content ct in content_list)
                ct.Update(Mouse.GetState());
        }
        
        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();
            background.Draw(spritebatch);
            foreach(Content ct in content_list)
                ct.Draw(spritebatch);
            spritebatch.End();
        }
    }
}
