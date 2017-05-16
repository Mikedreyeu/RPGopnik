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

        public Game(Map map, Viewport viewport, Character character)
        {
            camera = new Camera(viewport);
            this.map = map;
            this.character = character;
        }

        public void Update()
        {
            camera.Update(map, character.pos);
            character.Update(map.collisionObjects);   
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transform);

            map.Draw(spriteBatch, "Underlay");
            character.Draw(spriteBatch);
            map.Draw(spriteBatch, "Overlay");

            spriteBatch.End();
        }
    }
}
