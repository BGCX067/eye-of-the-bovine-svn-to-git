using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Eye_of_the_Bovine
{
    public class Plasmid : GameObject
    {
        public Bacterium.Plasmid Type { get; set; }
        public Plasmid(Texture2D image, Vector2 location,
            Bacterium.Plasmid kind)
        : base(image,location){
            Type = kind;
        }

        public void Pick(ref Bacterium picking){  }
    }
}
