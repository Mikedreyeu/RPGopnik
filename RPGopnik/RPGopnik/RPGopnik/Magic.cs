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
            spritebatch.Draw(ContentLoader.game_content.boyarishnik, new Rectangle((int)pos.X, (int)pos.Y, 20, 20), Color.White);
        }
    }

    class Rose : Artefact
    {
        uint max_power;
        DateTime last_update;
        public Rose(uint power, Vector2 pos)
        {
            this.pos = pos;
            this.power = power;
            max_power = power;
            renewable = true;
        }

        public override void Use(Character user, Character character, uint power)
        {
            if(power != 0)
            {
                if (power >= character.Curr_HP)
                {
                    character.Curr_HP = 0;
                    character.Condition = (byte)Conditions.Dead;
                }
                else
                    character.Curr_HP -= power;
            }
        }

        public void Update()
        {
            if(power != max_power)
            {
                if((DateTime.Now - last_update).TotalMinutes > 1)
                {
                    power++;
                    last_update = DateTime.Now;
                }
            }
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(ContentLoader.game_content.rose, new Rectangle((int)pos.X, (int)pos.Y, 20, 20), Color.White);
        }
    }

    class Colesa : Artefact
    {
        public Colesa(Vector2 pos)
        {
            this.pos = pos;
            this.renewable = false;
        }

        public override void Use(Character user, Character character, uint power)
        {
            if(character.Condition == (byte)Conditions.Poisoned)
            {
                character.Condition = (byte)Conditions.Normal;
            }
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(ContentLoader.game_content.colesa, new Rectangle((int)pos.X, (int)pos.Y, 20, 20), Color.White);
        }
    }

    class Balanda : Artefact
    {
        uint max_power;
        DateTime last_update;
        public Balanda(uint power, Vector2 pos)
        {
            this.pos = pos;
            this.power = power;
            max_power = power;
            renewable = true;
        }

        public override void Use(Character user, Character character, uint power)
        {
            if (power != 0)
            {
                if (power >= character.Curr_HP)
                {
                    character.Curr_HP = 0;
                    character.Condition = (byte)Conditions.Dead;
                }
                else
                {
                    character.Condition = (byte)Conditions.Poisoned;
                    character.Curr_HP -= power;
                }
            }
        }

        public void Update()
        {
            if (power != max_power)
            {
                if ((DateTime.Now - last_update).TotalMinutes > 1)
                {
                    power++;
                    last_update = DateTime.Now;
                }
            }
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(ContentLoader.game_content.balanda, new Rectangle((int)pos.X, (int)pos.Y, 20, 20), Color.White);
        }
    }

    class PlayBoy : Artefact
    {
        public PlayBoy(Vector2 pos)
        {
            this.pos = pos;
        }
        public override void Use(Character user, Character character, uint power)
        {
            if (character.Condition != (byte)Conditions.Dead)
            {
                character.Condition = (byte)Conditions.Paralyzed;
            }
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(ContentLoader.game_content.playBoy, new Rectangle((int)pos.X, (int)pos.Y, 20, 20), Color.White);
        }
    }
}
