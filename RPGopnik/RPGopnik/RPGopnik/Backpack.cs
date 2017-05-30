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
    class Backpack
    {
        private class ArtefactCount
        {
            public Artefact artefact;
            public uint number;

            public ArtefactCount(Artefact artefact)
            {
                this.artefact = artefact;
                number = 0;
            }
        }
        private List<Button> ButtonList;
        private List<ArtefactCount> artefacts;

        public Backpack()
        {
            artefacts = new List<ArtefactCount>(10);
            ButtonList = new List<Button>();
        }

        private ArtefactCount find_equal(Artefact artefact)
        {
            foreach (var tuple in artefacts)
                if (artefact.GetType() == tuple.artefact.GetType())
                    return tuple;
            return null;
        }

        private ArtefactCount find_equal_bottle(Artefact artefact)
        {
            foreach (var tuple in artefacts)
                if (artefact.GetType() == tuple.artefact.GetType() && artefact.power == tuple.artefact.power)
                    return tuple;
            return null;
        }

        public void Add(Artefact artefact)
        {
            if(!(artefact is Pivas) && !(artefact is Boyarishnik))
            {
                var equal = find_equal(artefact);
                if (equal == null)
                {
                    artefacts.Add(new ArtefactCount(artefact));
                    equal = artefacts.Last();
                    equal.artefact.pos = new Vector2(30 + ((artefacts.Count - 1) % 2) * 70, 200 + ((artefacts.Count - 1) / 2) * 70);
                }
                if (equal.artefact.renewable)
                    equal.artefact.power = equal.artefact.max_power;
                else
                    equal.number++;
            }
            else
            {
                var equal = find_equal_bottle(artefact);
                if (equal == null)
                {
                    artefacts.Add(new ArtefactCount(artefact));
                    equal = artefacts.Last();
                    equal.artefact.pos = new Vector2(30 + ((artefacts.Count - 1) % 2) * 70, 200 + ((artefacts.Count - 1) / 2) * 70);
                }
                equal.number++;
            }
        }

        public void Remove(Artefact artefact)
        {
            if (!(artefact is Pivas) && !(artefact is Boyarishnik))
            {
                var equal = find_equal(artefact);
                if (equal != null)
                {
                    if (equal.artefact.renewable)
                        artefacts.Remove(equal);
                    else
                    {
                        equal.number--;
                        if (equal.number == 0)
                            artefacts.Remove(equal);
                    }
                }
            }
            else
            {
                var equal = find_equal_bottle(artefact);
                if (equal != null)
                {
                    equal.number--;
                    if (equal.number == 0)
                        artefacts.Remove(equal);
                }
            }
        }

        public void Update()
        {
            ContentLoader.game_gui_content.BigHP.Update(Mouse.GetState());
            ContentLoader.game_gui_content.MedHP.Update(Mouse.GetState());
            ContentLoader.game_gui_content.SmaHP.Update(Mouse.GetState());
            ContentLoader.game_gui_content.BigMP.Update(Mouse.GetState());
            ContentLoader.game_gui_content.MedMP.Update(Mouse.GetState());
            ContentLoader.game_gui_content.SmaMP.Update(Mouse.GetState());
            ContentLoader.game_gui_content.Rose.Update(Mouse.GetState());
            ContentLoader.game_gui_content.Balanda.Update(Mouse.GetState());
            ContentLoader.game_gui_content.Colesa.Update(Mouse.GetState()); ;
            ContentLoader.game_gui_content.PlayBoy.Update(Mouse.GetState());
        }

        public Artefact Find(Artefact.Type type)
        {
            foreach(ArtefactCount pair in artefacts)
                if (pair.artefact.type == type)
                    return pair.artefact;
            return null;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            int i = 1;
            foreach(ArtefactCount pair in artefacts)
            {
                switch(pair.artefact.type)
                {
                    case Artefact.Type.BigHP:
                        ContentLoader.game_gui_content.BigHP.Draw(spritebatch, new Vector2(30 + ((i - 1) % 2) * 70, 200 + ((i - 1) / 2) * 70), pair.number.ToString());
                        break;
                    case Artefact.Type.MedHP:
                        ContentLoader.game_gui_content.MedHP.Draw(spritebatch, new Vector2(30 + ((i - 1) % 2) * 70, 200 + ((i - 1) / 2) * 70), pair.number.ToString());
                        break;
                    case Artefact.Type.SmaHP:
                        ContentLoader.game_gui_content.SmaHP.Draw(spritebatch, new Vector2(30 + ((i - 1) % 2) * 70, 200 + ((i - 1) / 2) * 70), pair.number.ToString());
                        break;
                    case Artefact.Type.BigMP:
                        ContentLoader.game_gui_content.BigMP.Draw(spritebatch, new Vector2(30 + ((i - 1) % 2) * 70, 200 + ((i - 1) / 2) * 70), pair.number.ToString());
                        break;
                    case Artefact.Type.MedMP:
                        ContentLoader.game_gui_content.MedMP.Draw(spritebatch, new Vector2(30 + ((i - 1) % 2) * 70, 200 + ((i - 1) / 2) * 70), pair.number.ToString());
                        break;
                    case Artefact.Type.SmaMP:
                        ContentLoader.game_gui_content.SmaMP.Draw(spritebatch, new Vector2(30 + ((i - 1) % 2) * 70, 200 + ((i - 1) / 2) * 70), pair.number.ToString());
                        break;
                    case Artefact.Type.Balanda:
                        ContentLoader.game_gui_content.Balanda.Draw(spritebatch, new Vector2(30 + ((i - 1) % 2) * 70, 200 + ((i - 1) / 2) * 70), pair.artefact.power.ToString() + "/" + pair.artefact.max_power.ToString());
                        break;
                    case Artefact.Type.Rose:
                        ContentLoader.game_gui_content.Rose.Draw(spritebatch, new Vector2(30 + ((i - 1) % 2) * 70, 200 + ((i - 1) / 2) * 70), pair.artefact.power.ToString() + "/" + pair.artefact.max_power.ToString());
                        break;
                    case Artefact.Type.Colesa:
                        ContentLoader.game_gui_content.Colesa.Draw(spritebatch, new Vector2(30 + ((i - 1) % 2) * 70, 200 + ((i - 1) / 2) * 70), pair.number.ToString());
                        break;
                    case Artefact.Type.PlayBoy:
                        ContentLoader.game_gui_content.PlayBoy.Draw(spritebatch, new Vector2(30 + ((i - 1) % 2) * 70, 200 + ((i - 1) / 2) * 70), pair.number.ToString());
                        break;
                }
                i++;
            }
        }
    }
}
