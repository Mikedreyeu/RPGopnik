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

        }

        public void Draw(SpriteBatch spritebatch)
        {
            foreach(ArtefactCount pair in artefacts)
            {
                pair.artefact.Draw(spritebatch);
            }
        }
    }
}
