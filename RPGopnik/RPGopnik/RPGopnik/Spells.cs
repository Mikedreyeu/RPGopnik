using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGopnik
{
    class AddHp:Spell
    {
        public AddHp() : base(1, true, false, true, true)
        {
            type = Type.AddHp;
        }
        public override void Use(Character user, Person_Abstract character, uint power)
        {
            (user as Mage_Character).Curr_Mana = (user as Mage_Character).Curr_Mana - power;
            if (character.Curr_HP + power * 2 > character.Max_HP)
                character.Curr_HP = character.Max_HP;
            else
                character.Curr_HP = character.Curr_HP + power * 2;
        }
    }

    class Heal : Spell
    {
        public Heal() : base(20, true, true, false, true)
        {
            type = Type.Heal;
        }
        public override void Use(Character user, Person_Abstract character, uint power)
        {
            (user as Mage_Character).Curr_Mana = (user as Mage_Character).Curr_Mana - MinMana;
            if (character.Condition == (byte)Conditions.Sick)
                character.Condition = (byte)Conditions.Normal;
        }
    }

    class Antidote : Spell
    {
        public Antidote() : base(30, false, true, false, true)
        {
            type = Type.Antidote;
        }
        public override void Use(Character user, Person_Abstract character, uint power)
        {
            (user as Mage_Character).Curr_Mana = (user as Mage_Character).Curr_Mana - MinMana;
            if (character.Condition == (byte)Conditions.Poisoned)
                character.Condition = (byte)Conditions.Normal;
        }
    }

    class Revive : Spell
    {
        public Revive() : base(30, false, true, false, true)
        {
            type = Type.Revieve;
        }
        public override void Use(Character user, Person_Abstract character, uint power)
        {
            (user as Mage_Character).Curr_Mana = (user as Mage_Character).Curr_Mana - MinMana;
            if (character.Condition == (byte)Conditions.Dead)
            {
                character.Condition = (byte)Conditions.Normal;
                character.Curr_HP = 1;
            }
        }
    }

    class Shield : Spell
    {
        public Shield() : base(50, false, true, true, false)
        {
            type = Type.Shield;
        }
        public override void Use(Character user, Person_Abstract character, uint power)
        {
            
            (user as Mage_Character).Curr_Mana = (user as Mage_Character).Curr_Mana - (power / 50) * 50;
        }
    }

    class Move : Spell
    {
        public Move() : base(85, false, true, false, true)
        {
            type = Type.Move;
        }
        public override void Use(Character user, Person_Abstract character, uint power)
        {
            (user as Mage_Character).Curr_Mana = (user as Mage_Character).Curr_Mana - MinMana;
            if (character.Condition == (byte)Conditions.Paralyzed)
            {
                character.Condition = (byte)Conditions.Normal;
                character.Curr_HP = 1;
            }
        }
    }
}
