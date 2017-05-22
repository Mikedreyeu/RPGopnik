﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RPGopnik
{
    class Camera
    {
        public Matrix transform;
        public Matrix inverseTransform;
        Vector2 pos;
        Viewport viewport;

        public Camera(Viewport viewp)
        {
            pos = Vector2.Zero;
            viewport = viewp;
        }

        public void Update(Map map, Vector2 playerPos)
        {
            pos = new Vector2(-playerPos.X - 15 + viewport.Width / 2, -playerPos.Y - 16 + (viewport.Height + 56) / 2);
            if (pos.X > 123)
            {
                pos.X = 123;
            }
            if (pos.X < -map.width - 126 + viewport.Width)
            {
                pos.X = -map.width - 126 + viewport.Width;
            }
            if (pos.Y > 56)
            {
                pos.Y = 56;
            }
            if (pos.Y < -map.height + viewport.Height)
            {
                pos.Y = -map.height + viewport.Height;
            }
            transform = Matrix.CreateScale(new Vector3(1, 1, 1)) * Matrix.CreateTranslation(pos.X, pos.Y, 0);
            inverseTransform = Matrix.Invert(transform);
        }
    }
}