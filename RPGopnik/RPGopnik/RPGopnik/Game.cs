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

        public Game(Map map, Character character)
        {
            camera = new Camera();
            this.map = map;
            this.character = character;
        }

        public void Update()
        {
            camera.Update(map, character.X, character.Y);
            character.Update();   
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
