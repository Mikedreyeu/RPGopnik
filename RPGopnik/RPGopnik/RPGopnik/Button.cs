using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RPGopnik
{
    class Button : Content
    {
        enum button_state { PRESSED, NONE, HOVERED };
        button_state bs = button_state.NONE;
        Texture2D hover_texture, pressed_texture;
        string text;
        Vector2 text_pos;
        public static SpriteFont font;
        Event ev;
        public Button(Rectangle rectangle, Texture2D main, Texture2D hover, Texture2D pressed, Event del, string text) : base(rectangle, main)
        {
            hover_texture = hover;
            pressed_texture = pressed;
            ev = del;
            this.text = text;
            text_pos = new Vector2(rect.Center.X - text.Length * 11, rect.Top + 3);
        }
        
        public override void Update(MouseState mouse)
        {
            if (rect.Contains(mouse.X, mouse.Y))
                if (mouse.LeftButton == ButtonState.Pressed)
                    bs = button_state.PRESSED;
                else
                {
                    if (bs == button_state.PRESSED)
                        ev();
                    bs = button_state.HOVERED;
                }
            else
                bs = button_state.NONE;
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            switch(bs)
            {
                case button_state.HOVERED:
                    spritebatch.Draw(hover_texture, rect, Color.White);
                    break;
                case button_state.PRESSED:
                    spritebatch.Draw(pressed_texture, rect, Color.White);
                    break;
                case button_state.NONE:
                    spritebatch.Draw(main_texture, rect, Color.White);
                    break;
            }
            spritebatch.DrawString(font, text, text_pos, Color.White);
        }
    }
}
