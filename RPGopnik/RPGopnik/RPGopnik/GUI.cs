using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RPGopnik
{
    class GUI
    {
        Character character;
        Mage_Character mageCharacter;
        Viewport viewp;
        public GUI(Viewport viewp, Character character)
        {
            this.character = character;
            this.viewp = viewp;
        }

        public GUI(Viewport viewp, Mage_Character mageCharacter)
        {
            this.mageCharacter = mageCharacter;
            this.viewp = viewp;
            character = mageCharacter;
        }

        public void Update()
        {
            MouseState mousestate = Mouse.GetState();
        }

        public void Draw(SpriteBatch spritebatch)
        {
            if (mageCharacter != null)
            {
                spritebatch.DrawString(ContentLoader.game_gui_content.hp_mana_font, mageCharacter.Curr_Mana.ToString(), new Vector2(590 - mageCharacter.Curr_Mana.ToString().Length * 25, 10), Color.White);
                spritebatch.DrawString(ContentLoader.game_gui_content.hp_mana_font, "/" + 100, new Vector2(590, 10), Color.White);
            }
            spritebatch.DrawString(ContentLoader.game_gui_content.hp_mana_font, character.Curr_HP.ToString(), new Vector2(270 - character.Curr_HP.ToString().Length * 25, 10), Color.White);
            spritebatch.DrawString(ContentLoader.game_gui_content.hp_mana_font, "/" + character.Max_HP, new Vector2(270, 10), Color.White);
            spritebatch.DrawString(ContentLoader.game_gui_content.hp_mana_font, character.XP.ToString(), new Vector2(940 - character.XP.ToString().Length * 12, 10), Color.White);

            
        }
    }
}
