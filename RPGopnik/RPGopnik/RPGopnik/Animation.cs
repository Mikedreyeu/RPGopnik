using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RPGopnik
{
    class Animation
    {
        int interval;
        Texture2D texture;
        int frame_width, frame_height;
        int num_frame;
        int frame_now;
        DateTime last_changeframe;


        public Animation(int interval, Texture2D texture, int frame_width, int frame_height, int num_frame)
        {
            this.last_changeframe = DateTime.Now;
            this.interval = interval;
            this.texture = texture;
            this.frame_width = frame_width;
            this.frame_height = frame_height;
            this.num_frame = num_frame;
            frame_now = 0;
        }

        public void Update()
        {
            if(DateTime.Now - last_changeframe > TimeSpan.FromMilliseconds(interval))
            {
                frame_now++;
                if (frame_now == num_frame)
                    frame_now = 0;
                last_changeframe = DateTime.Now;
            }
        }

        public void Draw(SpriteBatch spritebatch, Vector2 pos, Direction direction)
        {
            spritebatch.Draw(texture, new Rectangle((int)pos.X, (int)pos.Y, frame_width, frame_height), new Rectangle(frame_width * frame_now, frame_height * (int)direction, frame_width, frame_height), Color.White);
        }
    }
}
