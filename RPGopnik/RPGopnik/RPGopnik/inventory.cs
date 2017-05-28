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
    class Inventory
    {
        uint power;
        DateTime prev_powerup;
        private Tuple<IMagic, Keys> UsingObject;
        private bool is_number(Keys key)
        {
            if ((uint)key >= 49 && (uint)key <= 54)
                return true;
            return false;
        }

        public struct Spells
        {
            public AddHp addhp;
            public Move move;
            public Revive revieve;
            public Heal heal;
            public Antidote antidote;
            public Shield shield;
        }

        private Spells spells;
        private List<Artefact> artefacts;
        public Inventory()
        {
            artefacts = new List<Artefact>();
            spells = new Spells();
        }

        private void SetUsingObject(IMagic Object, Keys key, Character owner)
        {
            if (!(Object is Spell) || (Object as Spell).MinMana <= (owner as Mage_Character).Curr_Mana)
            {
                prev_powerup = DateTime.Now;
                power = 0;
                Cursor.cursorstate = Cursor.CursorState.Choose;
                UsingObject = new Tuple<IMagic, Keys>(Object, key);
            }
        }

        private void Choose(Character owner)
        {
            if (Keyboard.GetState().GetPressedKeys().Count(is_number) > 0 && UsingObject == null)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.D1) && spells.addhp != null)
                    SetUsingObject(new AddHp(), Keys.D1, owner);
                if (Keyboard.GetState().IsKeyDown(Keys.D2) && spells.heal != null)
                    SetUsingObject(new Heal(), Keys.D2, owner);
                if (Keyboard.GetState().IsKeyDown(Keys.D3) && spells.antidote != null)
                    SetUsingObject(new Antidote(), Keys.D3, owner);
                if (Keyboard.GetState().IsKeyDown(Keys.D4) && spells.move != null)
                    SetUsingObject(new Move(), Keys.D4, owner);
                if (Keyboard.GetState().IsKeyDown(Keys.D5) && spells.revieve != null)
                    SetUsingObject(new Revive(), Keys.D5, owner);
                if (Keyboard.GetState().IsKeyDown(Keys.D6) && spells.shield != null)
                    SetUsingObject(new Shield(), Keys.D6, owner);
            }
        }

        public void Update(Character owner)
        {
            Choose(owner);
            if (UsingObject != null)
                Using(owner);
        }

        private void Using(Character owner)
        {
            if (Mouse.GetState().RightButton != ButtonState.Pressed)
            {
                if (UsingObject.Item1 is Spell)
                {
                    if(Keyboard.GetState().IsKeyDown(UsingObject.Item2) && power <= (owner as Mage_Character).Curr_Mana && (DateTime.Now - prev_powerup).Milliseconds > 300)
                    {
                        power++;
                        prev_powerup = DateTime.Now;
                    }
                    if(Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        if (owner.Rect.Contains(Mouse.GetState().X- 157, Mouse.GetState().Y - 80))
                        {
                            UsingObject.Item1.Use(owner, owner, power);
                            UsingObject = null;
                            Cursor.cursorstate = Cursor.CursorState.Normal;
                        }
                    }
                }
            }
            else
            {
                UsingObject = null;
                Cursor.cursorstate = Cursor.CursorState.Normal;
                return;
            }
        }

        public void Add(Artefact artefact)
        {
            artefacts.Add(artefact);
        }

        public void Learn(Spell.Type type)
        {
            switch(type)
            {
                case Spell.Type.AddHp:
                    spells.addhp = new AddHp();
                    break;
                case Spell.Type.Antidote:
                    spells.antidote = new Antidote();
                    break;
                case Spell.Type.Heal:
                    spells.heal = new Heal();
                    break;
                case Spell.Type.Move:
                    spells.move = new Move();
                    break;
                case Spell.Type.Revieve:
                    spells.revieve = new Revive();
                    break;
                case Spell.Type.Shield:
                    spells.shield = new Shield();
                    break;
            }
        }

        public void Throw(Artefact artefact)
        {
            artefacts.Remove(artefact);
        }

        public void Give(Artefact artefact, Character to)
        {
            to.inventory.Add(artefact);
            Throw(artefact);
        }

        public void Use(Artefact artefact)
        {

        }

        public void Use(Spell spell)
        {

        }

        public void Draw(SpriteBatch spritebatch, Character owner)
        {
            if(UsingObject != null)
                spritebatch.DrawString(ContentLoader.game_content.PowerFont, "Power: " + power, new Vector2(owner.pos.X - 35, owner.pos.Y - 20), Color.White);
        }
    }
}
