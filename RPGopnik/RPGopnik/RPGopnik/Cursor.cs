using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RPGopnik
{
    class Cursor
    {
        public enum CursorState { Normal, Choose }
        public static CursorState cursorstate = CursorState.Normal;
        
        public static void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();
            switch(cursorstate)
            {
                case CursorState.Normal:
                    spritebatch.Draw(ContentLoader.cursor_states.Normal, new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 32, 32), Color.White);
                    break;
                case CursorState.Choose:
                    spritebatch.Draw(ContentLoader.cursor_states.Choosing, new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 32, 32), Color.White);
                    break;
            }
            spritebatch.End();
        } 
    }
}
