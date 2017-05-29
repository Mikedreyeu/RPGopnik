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
            character.inventory.DrawInventory(spritebatch);
            DrawControls(spritebatch);
        }

        private void DrawControls(SpriteBatch spritebatch)
        {
            spritebatch.DrawString(ContentLoader.game_gui_content.hp_mana_font, "Spells:", new Vector2(1145, 33), Color.White, 0, Vector2.Zero, 0.7f, SpriteEffects.None, 0);
            Texture2D texture = null;
            string str = null;
            int pos = -1;
            for (int i = 0; i < 6; i++)
            {
                if (i == 0 && character.inventory.spells.addhp != null)
                {
                    texture = ContentLoader.game_gui_content.add_hp;
                    str = "1";
                    pos++;
                }
                else if (i == 1 && character.inventory.spells.heal != null)
                {
                    texture = ContentLoader.game_gui_content.heal;
                    str = "2";
                    pos++;
                }
                else if (i == 2 && character.inventory.spells.antidote != null)
                {
                    texture = ContentLoader.game_gui_content.antidote;
                    str = "3";
                    pos++;
                }
                else if (i == 3 && character.inventory.spells.revieve != null)
                {
                    texture = ContentLoader.game_gui_content.revive;
                    str = "4";
                    pos++;
                }
                else if (i == 4 && character.inventory.spells.shield != null)
                {
                    texture = ContentLoader.game_gui_content.shield;
                    str = "5";
                    pos++;
                }
                else if (i == 5 && character.inventory.spells.move != null)
                {
                    texture = ContentLoader.game_gui_content.move;
                    str = "6";
                    pos++;
                }
                if (texture != null && str != null)
                {
                    spritebatch.Draw(texture, new Rectangle(1150, 80 + 108 * pos, 64, 64), Color.White);
                    spritebatch.DrawString(ContentLoader.game_gui_content.hp_mana_font, str, new Vector2(1230, 86 + 108 * pos), Color.White);
                }
            }
        }
    }
}
