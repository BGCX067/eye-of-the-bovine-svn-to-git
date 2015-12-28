using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eye_of_the_Bovine
{
    public struct Sprite
    {
        public Texture2D Texture;
        public Vector2 Position;

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Texture != null)
                spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
    public class Layer
    {
        public Layer(Camera camera)
        {
            _camera = camera;
            Parallax = Vector2.One;
            Sprites = new List<Sprite>();
        }

        public Vector2 Parallax { get; set; }
        public List<Sprite> Sprites { get; private set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, _camera.GetViewMatrix(Parallax));
            foreach (Sprite sprite in Sprites)
                sprite.Draw(spriteBatch);
            spriteBatch.End();
        }

        private readonly Camera _camera;

        public Vector2 WorldToScreen(Vector2 worldPosition)
        {
            return Vector2.Transform(worldPosition, _camera.GetViewMatrix(Parallax));
        }

        public Vector2 ScreenToWorld(Vector2 screenPosition)
        {
            return Vector2.Transform(screenPosition, Matrix.Invert(_camera.GetViewMatrix(Parallax)));
        }
    }
}
