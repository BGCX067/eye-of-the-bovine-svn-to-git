using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Eye_of_the_Bovine
{
    public class Enemy
    {protected Texture2D photo;
        protected Vector2 position;
        protected App ownerapp;
        protected int damage;
        public int Damage {
            get
            {
                return damage;
            }
        }
        public Enemy(App owner, Texture2D image, Vector2 location, int doesdamage)
        {
            ownerapp = owner;
            photo = image;
            position = location;
            damage = doesdamage;
        }
        public Enemy(Game owner, Texture2D image, Vector2 location)
        {
            photo = image;
            position = location;
        }

        public void Move(float amount) {
           position.X= position.X + amount;
           position.Y= position.Y + amount;
        }

        public void Move(float X, float Y)
        {
            position.X = position.X + X;
            position.Y = position.Y + Y;
        }
    }
}
