using System;
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
        

        public Camera()
        {
            pos = Vector2.Zero;
        }

        public void Update(Map map)
        {
            if (pos.X > 0)
            {
                pos.X = 0;
            }
            if (pos.X < -map.width + 800)
            {
                pos.X = -map.width + 800;
            }
            if (pos.Y > 0)
            {
                pos.Y = 0;
            }
            if (pos.Y < -map.height + 600)
            {
                pos.Y = -map.height + 600;
            }
            transform = Matrix.CreateScale(new Vector3(1, 1, 1)) * Matrix.CreateTranslation(pos.X, pos.Y, 0);
            inverseTransform = Matrix.Invert(transform);
        }
    }
}