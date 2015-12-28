using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Eye_of_the_Bovine
{
    public class GameObject
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        protected Vector2 velocity;
        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public GameObject(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
            Velocity = new Vector2(0);
        }
    }
}
