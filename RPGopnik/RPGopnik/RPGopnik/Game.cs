using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RPGopnik
{
    class Game
    {
        Map map;
        Camera camera;
        Character character;
        List<Artefact> artefacts;

        public Game(Map map, Viewport viewport, Character character, List<Artefact> artefacts)
        {
            this.artefacts = artefacts;
            camera = new Camera(viewport);
            this.map = map;
            this.character = character;
        }

        void Pick_up(Artefact artefact)
        {
            if (character.Rect.Intersects(new Rectangle((int)artefact.pos.X, (int)artefact.pos.Y, 20, 20)))
                character.inventory.Add(artefact, artefacts);
        }

        public void Update()
        {
            camera.Update(map, character.pos);
            character.Update(map.collisionObjects);
            artefacts.ForEach(Pick_up);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transform);

            map.Draw(spriteBatch, "Underlay");
            foreach (Artefact artefact in artefacts)
                artefact.Draw(spriteBatch);
            character.Draw(spriteBatch);
            map.Draw(spriteBatch, "Overlay");

            spriteBatch.End();
        }
    }
}
