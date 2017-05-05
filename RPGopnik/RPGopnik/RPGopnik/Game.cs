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
        public Game(Map map, Enemy enemy)
        {
            this.map = map;
            this.enemy = enemy;
        }

        public void Update()
        {
            enemy.Update(Mouse.GetState());   
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            map.Draw(spriteBatch, "Underlay");
            //player.draw();
            spriteBatch.Begin();
            enemy.Draw(spriteBatch);
            spriteBatch.End();
            map.Draw(spriteBatch, "Overlay");
        }
    }
}
