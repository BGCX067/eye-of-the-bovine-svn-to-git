using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eye_of_the_Bovine
{
    public class Tile
    {
        public enum TileType {
            Passable,
            Impassable,
            Destructable,
            EnemySpawn,
            WhiteSpawn,
            Gap,
            Gate,
            Valve,
            InfectedCell
        };
        public Texture2D Photo { get; set; }

        public TileType Type { get; set; } 
        
        public Tile(Texture2D image, TileType _type) {
            Photo = image;
            Type = _type;
        }

    }
}
