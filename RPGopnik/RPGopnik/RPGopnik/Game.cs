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
        Enemy enemy;
        Camera camera;
        public Game(Map map, Enemy enemy)
        {
            camera = new Camera();
            this.map = map;
            this.enemy = enemy;
        }

        public void Update()
        {
            camera.Update(map);
            enemy.Update(Mouse.GetState());   
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transform);

            map.Draw(spriteBatch, "Underlay");
            enemy.Draw(spriteBatch);
            map.Draw(spriteBatch, "Overlay");

            spriteBatch.End();
        }
    }
}
