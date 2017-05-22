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
        List<Artefact> artefacts;
        public Inventory()
        {
            artefacts = new List<Artefact>();
        }

        public void Update()
        {
            MouseState mousestate = Mouse.GetState();
        }

        public void Add(Artefact artefact, List<Artefact> artefacts)
        {
            this.artefacts.Add(artefact);
            artefacts.Remove(artefact);
        }

        private void Add(Artefact artefact)
        {
            artefacts.Add(artefact);
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

        public void Draw(SpriteBatch spritebatch)
        {

        }
    }
}
