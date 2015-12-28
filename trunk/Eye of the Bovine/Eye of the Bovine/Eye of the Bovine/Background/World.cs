using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Eye_of_the_Bovine
{
    public class World
    {
        
        public int Width { get; set; }
        public int Height { get; set; }
        public List<Vector2> playerSpawns = new List<Vector2>();
        public Tile[,] Tiles { get; set; }

        public World() {
            // default player spawn
        }

        public void LoadLevel(StreamReader reader)
        {
            // Get my content manager
            ContentManager content = App.GetInstance().Content;
            string line = null;
            // Create a list of file strings
            List<string> fileStrings = new List<string>();
            // read to the end of the file
            while (null != (line = reader.ReadLine()))
            {
                fileStrings.Add(line);
            }
            // set width and height
            Width = fileStrings[0].Length;
            Height = fileStrings.Count; 
            Tiles = new Tile[Width, Height];

            for ( int row = 0; row < fileStrings.Count; row++) {
                line = fileStrings[row];
                // terate through the characters in the line
                for ( int col = 0; col < line.Length; col++) 
                {
                    char c = line.ToCharArray()[col];
                    Tile currTile = null;
                    switch (c)
                    {
                        case '1':
                            currTile = new Tile(content.Load<Texture2D>("Assets/floor8"),
                                Tile.TileType.Passable);
                            break;
                        case '0':
                            currTile = new Tile(content.Load<Texture2D>("Assets/Wall"),
                                Tile.TileType.Impassable);
                            break;
                        case 'd':
                        case 'D':
                            currTile = new Tile(content.Load<Texture2D>("Assets/Enzyme Wall"),
                                Tile.TileType.Destructable);
                            break;
                        case 's':
                        case 'S':
                            currTile = new Tile(content.Load<Texture2D>("Assets/Viral Node"),
                                Tile.TileType.EnemySpawn);
                            break;
                        case'w':
                        case'W':
                            currTile = new Tile(content.Load<Texture2D>("Assets/WhiteyNode"), 
                                Tile.TileType.WhiteSpawn);
                                break;
                        case 'h':
                        case 'H':
                            currTile = new Tile(content.Load<Texture2D>("Assets/Gap"),
                                Tile.TileType.Gap);
                            break;
                        case 'g':
                        case 'G':
                            currTile = new Tile(content.Load<Texture2D>("Assets/Gate Closed"),
                                Tile.TileType.Gate);
                            break;
                        case 'v':
                        case 'V':
                            currTile = new Tile(content.Load<Texture2D>("Assets/Valve shut"),
                                Tile.TileType.Valve);
                            break;
                        case 'i':
                        case 'I':
                            currTile = new Tile(content.Load<Texture2D>("Assets/floor8"),
                                Tile.TileType.InfectedCell);
                            break;
                        case 'p':
                        case 'P':
                            playerSpawns.Add(new Vector2(col, row));
                            currTile = new Tile(content.Load<Texture2D>("Assets/startplate"),
                                Tile.TileType.Passable);
                            break;
                            

                        default:
                            currTile = new Tile(content.Load<Texture2D>("Assets/floor8"),
                                Tile.TileType.Passable);
                            break;
                    }
                    Tiles[col, row] = currTile;
                }
            }

            // We have our Tiles!!!
            // Huzza!
        }

        public void Draw(int col, int row,Tile part, SpriteBatch spriteBatch){
            spriteBatch.Draw(part.Photo, new Vector2((row * part.Photo.Width), (col * part.Photo.Height)), Color.White);
        }
    }
}
