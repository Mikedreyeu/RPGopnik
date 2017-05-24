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
        Character character;
        List<Artefact> artefacts;
        SpriteFont hp_mana_font; //просто впихнул, скорее всего, в дальнейшем будет в классе с GUI

        public Game(Map map, Viewport viewport, Character character, List<Artefact> artefacts)
        {
            this.artefacts = artefacts;
            camera = new Camera(viewport);
            this.map = map;
            this.character = character;

        }

        public void LoadContent(ContentManager Content)         // просто впихнул, скорее всего, в дальнейшем будет в классе с GUI
        {                                                       //
            hp_mana_font = Content.Load<SpriteFont>("bt_font"); //
        }                                                       //

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

            spriteBatch.Begin();
            ContentLoader.game_gui_content.foundation.Draw(spriteBatch);
            ///////////////////////////////// скорее всего, в дальнейшем будет в классе с GUI ////////////////////////////////////////////////
            spriteBatch.DrawString(hp_mana_font, character.Curr_HP + "/" + character.Max_HP, new Vector2(360, 10), Color.White);           //
            spriteBatch.DrawString(hp_mana_font, character.XP.ToString(), new Vector2(620, 10), Color.White);                              //
            // маг spriteBatch.DrawString(map.hp_mana_font,  + "/" + , new Vector2(camera.pos.X + 430, camera.pos.Y + 10), Color.White);    //
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            spriteBatch.End();
        }
    }
}
