using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TiledSharp;

namespace RPGopnik
{
    class Map
    {
        TmxMap map;
        Texture2D tileset;

        int tileWidth;
        int tileHeight;
        int tilesetTilesWide;
        int tilesetTilesHigh;

        public Map(string tmxFile)
        {
            map = new TmxMap(tmxFile);
        }

        public void LoadContent(ContentManager Content)
        {
            tileset = Content.Load<Texture2D>(@"TileSets\" + map.Tilesets[0].Name.ToString());

            tileWidth = map.Tilesets[0].TileWidth;
            tileHeight = map.Tilesets[0].TileHeight;
            tilesetTilesWide = tileset.Width / tileWidth;
            tilesetTilesHigh = tileset.Height / tileHeight;
        }

        public void Draw(SpriteBatch spritebatch, string layerPos)
        {
            spritebatch.Begin();
            for (int j = 0; j < map.Layers.Count; j++)
            {
                if (map.Layers[j].Name.Contains("Overlay") && layerPos == "Underlay")
                    continue;
                if (!map.Layers[j].Name.Contains("Overlay") && layerPos == "Overlay")
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

            spritebatch.End();
        }
    }
}
