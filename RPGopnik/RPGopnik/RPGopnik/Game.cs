using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RPGopnik
{
    class Game
    {
        Map map;
        Camera camera;
        List<Enemy> list;
        GUI gui;
        public Character character;
        List<Artefact> artefacts;

        public Game(Map map, List<Enemy> list, Viewport viewport, Character character, List<Artefact> artefacts)
        {
            this.artefacts = artefacts;
            this.list = list;
            camera = new Camera(viewport);
            this.map = map;
            this.character = character;
            gui = new GUI(viewport, character as Mage_Character);
            character.XP = 420;
        }

        public void LoadContent(ContentManager Content)
        {                                                       

        }                                                       

        void Pick_up(Artefact artefact)
        {
            if (character.Rect.Intersects(new Rectangle((int)artefact.pos.X, (int)artefact.pos.Y, 20, 20)))
            {
                character.inventory.Add(artefact);
                artefacts.Remove(artefact);
            }
        }

        public void Update(GameTime gameTime)
        {
            camera.Update(map, character.pos);
            foreach (Enemy enemy in list)
            {
                enemy.Update(gameTime, character, map.collisionObjects);
                character.Update(camera, map.collisionObjects, enemy);
            }
            artefacts.ForEach(Pick_up);

            if (character.Rect.Intersects(map.spellLearningArea) && Keyboard.GetState().IsKeyDown(Keys.F))
            {
                Game1.game_state = gs.SLSCREEN;
            }

            if (ContentLoader.game_content.baryga.Condition == (byte)Conditions.Dead &&
               ContentLoader.game_content.kolshik.Condition == (byte)Conditions.Dead &&
               ContentLoader.game_content.enemy.Condition == (byte)Conditions.Dead &&
               ContentLoader.game_content.petuh.Condition == (byte)Conditions.Dead &&
               character.pos.X > 640 && character.pos.X < 800 && character.pos.Y > 1460)
                Game1.game_state = gs.MAIN;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transform);
            map.Draw(spriteBatch, "Underlay");
            foreach (Enemy enemy in list)
                enemy.Draw(spriteBatch, enemy);
            foreach (Artefact artefact in artefacts)
                artefact.Draw(spriteBatch);
            character.Draw(spriteBatch);
            map.Draw(spriteBatch, "Overlay");
            spriteBatch.End();

            spriteBatch.Begin();
            ContentLoader.game_gui_content.foundation.Draw(spriteBatch);
            gui.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
