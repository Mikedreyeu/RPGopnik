using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TiledSharp;
using System.Collections.Generic;

namespace RPGopnik
{
    class Map
    {
        Texture2D tileset;
        TmxMap map;
        public Texture2D game_interface;
        public SpriteFont hp_mana_font;

        public List<Rectangle> collisionObjects;

        public int width;
        public int height;

        int tileWidth;
        int tileHeight;
        int tilesetTilesWide;
        int tilesetTilesHigh;

        public Map(string tmxFile)
        {
            map = new TmxMap(tmxFile);
            width = map.Width * map.TileWidth;
            height = map.Height * map.TileHeight;

            collisionObjects = new List<Rectangle>();
            foreach (var o in map.ObjectGroups["Collision"].Objects)
                collisionObjects.Add(new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height));
        }

        public void LoadContent(ContentManager Content)
        {
            tileset = Content.Load<Texture2D>(@"TileSets\" + map.Tilesets[0].Name.ToString());

            tileWidth = map.Tilesets[0].TileWidth;
            tileHeight = map.Tilesets[0].TileHeight;
            tilesetTilesWide = tileset.Width / tileWidth;
            tilesetTilesHigh = tileset.Height / tileHeight;

            hp_mana_font = Content.Load<SpriteFont>("bt_font");
            game_interface = Content.Load<Texture2D>("interface");
        }

        public void Draw(SpriteBatch spritebatch, string layerPos)
        {
            for (int j = 0; j < map.Layers.Count; j++)
            {
                if (map.Layers[j].Name.Contains("Overlay") && layerPos == "Underlay"
                    || !map.Layers[j].Name.Contains("Overlay") && layerPos == "Overlay")
                    continue;
                for (int i = 0; i < map.Layers[j].Tiles.Count; i++)
                {
                    int gid = map.Layers[j].Tiles[i].Gid; // gid - global tile ID

                    if (gid != 0)
                    {
                        int tileFrame = gid - 1;
                        int column = tileFrame % tilesetTilesWide;
                        int row = (int)Math.Floor((double)tileFrame / (double)tilesetTilesWide);

                        float x = (i % map.Width) * map.TileWidth;
                        float y = (float)Math.Floor(i / (double)map.Width) * map.TileHeight;

                        Rectangle tilesetRec = new Rectangle(tileWidth * column, tileHeight * row, tileWidth, tileHeight);

                        spritebatch.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);
                    }
                }
            }
        }
    }
}