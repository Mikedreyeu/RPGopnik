using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RPGopnik
{
    class Pivas : Artefact
    {
        public Pivas(Size size, Vector2 pos) : base((uint)size, false, false, false, pos)
        {
            switch(size)
            {
                case Size.Big:
                    type = Type.BigHP;
                    break;
                case Size.Middle:
                    type = Type.MedHP;
                    break;
                case Size.Little:
                    type = Type.SmaHP;
                    break;
            }
        }

        public override void Use(Character user, Person_Abstract character, uint power)
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
        public Boyarishnik(Size size, Vector2 pos) : base((uint)size, false, false, false, pos)
        {
            switch (size)
            {
                case Size.Big:
                    type = Type.BigMP;
                    break;
                case Size.Middle:
                    type = Type.MedMP;
                    break;
                case Size.Little:
                    type = Type.SmaMP;
                    break;
            }
        }

        public override void Use(Character user, Person_Abstract character, uint power)
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
        DateTime last_update;
        public Rose(uint power, Vector2 pos) : base(power, true, true, true, pos)
        {
            type = Type.Rose;
        }

        public override void Use(Character user, Person_Abstract character, uint power)
        {
            if (power != 0)
            {
                if (power >= character.Curr_HP)
                {
                    character.Curr_HP = 0;
                    character.Condition = (byte)Conditions.Dead;
                }
                else
                    character.Curr_HP -= power;
            }
            this.power -= power;
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(ContentLoader.game_content.rose, new Rectangle((int)pos.X, (int)pos.Y, 20, 20), Color.White);
        }
    }

    class Colesa : Artefact
    {
        public Colesa(Vector2 pos): base(0, false, false, true, pos)
        {
            type = Type.Colesa;
        }

        public override void Use(Character user, Person_Abstract character, uint power)
        {
            if (character.Condition == (byte)Conditions.Poisoned)
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
        DateTime last_update;
        public Balanda(uint power, Vector2 pos):base(power, true, true, true, pos)
        {
            type = Type.Balanda;
        }

        public override void Use(Character user, Person_Abstract character, uint power)
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
            this.power -= power;
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(ContentLoader.game_content.balanda, new Rectangle((int)pos.X, (int)pos.Y, 20, 20), Color.White);
        }
    }

    class PlayBoy : Artefact
    {
        public PlayBoy(Vector2 pos):base(0, false, false, true, pos)
        {
            type = Type.PlayBoy;
        }
        public override void Use(Character user, Person_Abstract character, uint power)
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
