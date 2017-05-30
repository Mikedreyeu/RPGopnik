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
        Character owner;
        Vector2 mouse;
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

        public Spells spells;
        private Backpack artefacts;
        public Inventory(Character owner)
        {
            this.owner = owner;
            artefacts = new Backpack();
            spells = new Spells();
        }

        private void SetUsingObject(IMagic Object, Keys key, Character owner)
        {
            if (!(Object is Spell) || (Object as Spell).MinMana <= (owner as Mage_Character).Curr_Mana)
            {
                prev_powerup = DateTime.Now;
                if (Object is Spell)
                    power = (Object as Spell).MinMana;
                else
                    power = 0;
                if (Object.choosable)
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

        public void Update(Camera camera, Character owner)
        {
            mouse = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            mouse = Vector2.Transform(mouse, camera.inverseTransform);
            artefacts.Update();
            Choose(owner);
            if (UsingObject != null)
                Using(owner);
        }

        private void Using(Character owner)
        {
            if (Mouse.GetState().RightButton != ButtonState.Pressed)
            {
                if (UsingObject.Item1.choosable)
                {
                    if (UsingObject.Item1.powerable && Keyboard.GetState().IsKeyDown(UsingObject.Item2) && (DateTime.Now - prev_powerup).Milliseconds > 300)
                    {
                        if (UsingObject.Item1 is Spell)
                        {
                            if (power < (owner as Mage_Character).Curr_Mana)
                            {
                                power++;
                                prev_powerup = DateTime.Now;
                            }
                        }
                        else
                            if (power < (UsingObject.Item1 as Artefact).power)
                        {
                            power++;
                            prev_powerup = DateTime.Now;
                        }
                    }
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        if (owner.Rect.Contains((int)mouse.X, (int)mouse.Y))
                        {
                            UsingObject.Item1.Use(owner, owner, power);
                            UsingObject = null;
                            Cursor.cursorstate = Cursor.CursorState.Normal;
                        }
                    }
                }
                else
                {
                    if (UsingObject.Item1.powerable && Keyboard.GetState().IsKeyDown(UsingObject.Item2))
                    {
                        if ((DateTime.Now - prev_powerup).Milliseconds > 300)
                        {
                            if (UsingObject.Item1 is Spell)
                            {
                                if (power < (owner as Mage_Character).Curr_Mana)
                                {
                                    power++;
                                    prev_powerup = DateTime.Now;
                                }
                            }
                            else
                                if (power < (UsingObject.Item1 as Artefact).power)
                            {
                                power++;
                                prev_powerup = DateTime.Now;
                            }
                        }
                    }
                    else
                    {
                        UsingObject.Item1.Use(owner, owner, power);
                        UsingObject = null;
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
            switch (type)
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

        public void Use(Artefact.Type type)
        {
            Artefact artefact = artefacts.Find(type);
            switch(artefact.type)
            {
                case Artefact.Type.BigHP:
                    ContentLoader.game_gui_content.BigHP.ChangePosition(new Vector2(-50, -50));
                    break;
                case Artefact.Type.MedHP:
                    ContentLoader.game_gui_content.MedHP.ChangePosition(new Vector2(-50, -50));
                    break;
                case Artefact.Type.SmaHP:
                    ContentLoader.game_gui_content.SmaHP.ChangePosition(new Vector2(-50, -50));
                    break;
                case Artefact.Type.BigMP:
                    ContentLoader.game_gui_content.BigMP.ChangePosition(new Vector2(-50, -50));
                    break;
                case Artefact.Type.MedMP:
                    ContentLoader.game_gui_content.MedMP.ChangePosition(new Vector2(-50, -50));
                    break;
                case Artefact.Type.SmaMP:
                    ContentLoader.game_gui_content.SmaMP.ChangePosition(new Vector2(-50, -50));
                    break;
                case Artefact.Type.Balanda:
                    ContentLoader.game_gui_content.Balanda.ChangePosition(new Vector2(-50, -50));
                    break;
                case Artefact.Type.Rose:
                    ContentLoader.game_gui_content.Rose.ChangePosition(new Vector2(-50, -50));
                    break;
                case Artefact.Type.Colesa:
                    ContentLoader.game_gui_content.Colesa.ChangePosition(new Vector2(-50, -50));
                    break;
                case Artefact.Type.PlayBoy:
                    ContentLoader.game_gui_content.PlayBoy.ChangePosition(new Vector2(-50, -50));
                    break;
            }
            SetUsingObject(artefact, Keys.Q, owner);
            if(!artefact.renewable)
                artefacts.Remove(artefact);
        }

        public void Use(Spell spell)
        {

        }

        public void DrawInventory(SpriteBatch spritebatch)
        {
            artefacts.Draw(spritebatch);
        }

        public void Draw(SpriteBatch spritebatch, Character owner)
        {
            if (UsingObject != null && UsingObject.Item1.powerable)
                spritebatch.DrawString(ContentLoader.game_content.PowerFont, "Power: " + power, new Vector2(owner.pos.X - 35, owner.pos.Y - 20), Color.White);
        }
    }
}
