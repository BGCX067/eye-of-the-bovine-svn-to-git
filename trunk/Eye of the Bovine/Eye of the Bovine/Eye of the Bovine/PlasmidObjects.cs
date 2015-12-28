using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Eye_of_the_Bovine
{
    public class PlasmidObjects
    {
        private Texture2D photo;
        private Vector2 position;
        private int acts;
        private bool pickedup;
        public PlasmidObjects(Texture2D image, Vector2 location, int kind){
            photo = image;
            position = location;
            acts = kind;
            pickedup = false;
        }
    }
}
