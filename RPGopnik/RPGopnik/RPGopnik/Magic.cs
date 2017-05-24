using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace RPGopnik
{
    interface IMagic
    {
        void Use(Character user);
        void Use(Character user, Character character);
        void Use(Character user, uint power);
        void Use(Character user, Character character, uint power);
    }

    abstract class Artefact : IMagic
    {
        public Vector2 pos;
        public enum Size { Little = 10, Middle = 25, Big = 50 }
        protected uint power;
        protected bool renewable;
        public void Use(Character user)
        {
            Use(user, user, 1);
        }
        public void Use(Character user, Character character)
        {
            Use(user, character, 1);
        }
        public void Use(Character user, uint power)
        {
            Use(user, user, power);
        }
        public virtual void Use(Character user, Character character, uint power) { }

        public virtual void Draw(SpriteBatch spritebatch) { }
    }

    class Pivas : Artefact
    {
        public Pivas(Size size, Vector2 pos)
        {
            this.pos = pos;
            power = (uint)size;
            renewable = false;
        }

        public override void Use(Character user, Character character, uint power)
        {
            if (character.Curr_HP + this.power > character.Max_HP)
                character.Curr_HP = character.Max_HP;
            else
                character.Curr_HP = character.Curr_HP + this.power;
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(ContentLoader.game_content.beer, new Rectangle((int)pos.X, (int)pos.Y, 20, 20), Color.White);
        }
    }

    class Boyarishnik : Artefact
    {
        static public Texture2D texture;
        public Boyarishnik(Size size, Vector2 pos)
        {
            this.pos = pos;
            power = (uint)size;
            renewable = false;
        }

        public override void Use(Character user, Character character, uint power)
        {
            if (character is Mage_Character)
            {
                if ((character as Mage_Character).Curr_Mana + this.power > (character as Mage_Character).Max_Mana)
                    (character as Mage_Character).Curr_Mana = (character as Mage_Character).Max_Mana;
                else
                    (character as Mage_Character).Curr_Mana = (character as Mage_Character).Curr_Mana + this.power;
            }
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, new Rectangle((int)pos.X, (int)pos.Y, 20, 20), Color.White);
        }
    }
}
