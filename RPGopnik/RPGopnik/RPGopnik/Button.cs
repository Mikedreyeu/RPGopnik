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
        Rectangle infoBoxRectangle;
        string text, infoHeader, infoContent;
        bool withInfoBox;
        Vector2 text_pos;
        public static SpriteFont font;
        Event ev;
        public Button(Rectangle rectangle, Texture2D main, Texture2D hover, Texture2D pressed, Event del, string text) : base(rectangle, main)
        {
            hover_texture = hover;
            pressed_texture = pressed;
            ev = del;
            this.text = text;
            text_pos = new Vector2(rect.Center.X - text.Length * 12, rect.Top + 3);
            withInfoBox = false;
        }

        public Button(Rectangle rectangle, Texture2D main, Texture2D hover, Texture2D pressed, Event del, string text, string infoHeader, string infoContent) : base(rectangle, main)
        {
            hover_texture = hover;
            pressed_texture = pressed;
            ev = del;
            this.text = text;
            text_pos = new Vector2(rect.Center.X - text.Length * 12, rect.Top + 3);
            this.infoContent = infoContent;
            this.infoHeader = infoHeader;
            infoBoxRectangle = new Rectangle(180, rect.Y - 95, 285, 190);
            withInfoBox = true;
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
            switch (bs)
            {
                case button_state.HOVERED:
                    spritebatch.Draw(hover_texture, rect, Color.White);
                    if (withInfoBox == true)
                    {
                        DrawInfoBox(spritebatch);
                    }
                    break;
                case button_state.PRESSED:
                    spritebatch.Draw(pressed_texture, rect, Color.White);
                    if (withInfoBox == true)
                    {
                        DrawInfoBox(spritebatch);
                    }
                    break;
                case button_state.NONE:
                    spritebatch.Draw(main_texture, rect, Color.White);
                    break;
            }
            spritebatch.DrawString(font, text, text_pos, Color.White);
        }

        private string FormatInfoBoxContent(string infbCont)
        {
            int lastSpaceIndex = infbCont.IndexOf(" ");
            int currSI;
            int indN = 0;
            while (lastSpaceIndex != -1)
            {
                currSI = infbCont.IndexOf(" ", lastSpaceIndex + 1);
                if (lastSpaceIndex < 33 + indN && currSI >= 33 + indN)
                {
                    infbCont = infbCont.Insert(lastSpaceIndex + 1, "\n");
                    indN = lastSpaceIndex + 1;
                }
                lastSpaceIndex = currSI;
            }
            return infbCont;
        }

        private void DrawInfoBox(SpriteBatch spritebatch)
        {
            spritebatch.Draw(ContentLoader.button_content.infoBox, infoBoxRectangle, Color.White);
            spritebatch.DrawString(ContentLoader.game_gui_content.hp_mana_font, infoHeader, new Vector2(infoBoxRectangle.X + 30, infoBoxRectangle.Y + 20), new Color(70, 41, 14), 0, Vector2.Zero, 0.4f, SpriteEffects.None, 0);
            spritebatch.DrawString(ContentLoader.game_gui_content.hp_mana_font, FormatInfoBoxContent(infoContent), new Vector2(infoBoxRectangle.X + 27, infoBoxRectangle.Y + 45), new Color(70, 41, 14), 0, Vector2.Zero, 0.3f, SpriteEffects.None, 0);
        }
    }
}
