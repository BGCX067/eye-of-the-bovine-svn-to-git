using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Eye_of_the_Bovine
{
    public class Bacterium : GameObject
    {
        public static PlayerIndex[] PlayerIndexs = { PlayerIndex.One, PlayerIndex.Two, PlayerIndex.Three, PlayerIndex.Four };

        public enum Plasmid { None, Catalyst, Enzyme, ATPcarrier, Offensive, Toxin, Endo, Marker}
        public int Action { 
            get { 
                return action; 
            } 
            set { 
                action = value; 
            } 
        }
        int action;

        private PlayerIndex currentPlayerIndex;
        public PlayerIndex CurrentPlayerIndex
        {
            get { return currentPlayerIndex; }
        }
        private int healthpoints;
        private bool live;
        public bool damaged;
        public bool IsAlive{
            get{
                return live;
            }
        }

        public FloatRect Bounding
        {
            get
            {
                return new FloatRect(Position.X, Position.Y, 1.0f, 1.0f);
            }
        }

        public Bacterium(Texture2D image, Vector2 location, int health, 
            PlayerIndex _playerIndex) : base(image,location) {
            healthpoints = health;
            Action = (int)Plasmid.None;
            live = true;
            currentPlayerIndex = _playerIndex;
        }
        
        public void Kill()
        {
            live = false;
            App.GetInstance().Sound.WAVS[5].Play();
        }

        public void TakeDamage()
        {
            healthpoints--;
            App.GetInstance().Sound.WAVS[6].Play();
            damaged = true;
            if (healthpoints <= 0){
                Kill();
            }
        }

        public void Update(GameTime gameTime, World level)
        {
            // get player input
            GamePadState gpState = GamePad.GetState(CurrentPlayerIndex);
            Velocity = new Vector2(gpState.ThumbSticks.Left.X * (gameTime.ElapsedGameTime.Milliseconds / 200.0f),
                -gpState.ThumbSticks.Left.Y * (gameTime.ElapsedGameTime.Milliseconds / 200.0f));

            // Get the tiles around the player, and check collision
            // get this cells row and collumn
            int col = (int)(Position.X + 0.5f);
            int row = (int)(Position.Y + 0.5f);
            //Position = newPosition;
            // we do this 8 times
            // I hate my self for writing this code!
            // HATE!! HATE!! HATE!!
            // upper right corner
            if ( Velocity.Y < 0)
            {
                if (row - 1 >= 0)
                {
                    Tile t = level.Tiles[col, row - 1];
                    if (t.Type != Tile.TileType.Passable &&
                        true == Bounding.CheckCollision(new FloatRect(col, row - 1, 1.0f, 1.0f)))
                        velocity.Y = 0;
                    //if (col - 1 >= 0)
                    //{
                    //    Tile leTile = level.Tiles[col - 1, row - 1];
                    //    if (leTile.Type != Tile.TileType.Passable &&
                    //        true == Bounding.CheckCollision(new FloatRect(col - 1.0f, row - 1.0f, 1.0f, 1.0f)))
                    //    {
                    //        velocity.Y = 0;
                    //    }
                    //}
                    //if (col + 1 < level.Width)
                    //{
                    //    Tile leTile = level.Tiles[col + 1, row - 1];
                    //    if (leTile.Type != Tile.TileType.Passable &&
                    //        true == Bounding.CheckCollision(new FloatRect(col + 1, row - 1, 1.0f, 1.0f)))
                    //        velocity.Y = 0;
                    //}
                }

            }
            else if (Velocity.Y > 0)
            {
                    {
                        Tile t = level.Tiles[col, row + 1];
                        if (t.Type != Tile.TileType.Passable &&
                            true == Bounding.CheckCollision(new FloatRect(col, row + 1, 1.0f, 1.0f)))
                            velocity.Y = 0;
                    }
                    /*
                    if (col - 1 >= 0)
                    {
                        Tile leTile = level.Tiles[col - 1, row + 1];
                        if (leTile.Type != Tile.TileType.Passable &&
                            true == Bounding.CheckCollision(new FloatRect(col - 1.0f, row + 1.0f, 1.0f, 1.0f)))
                        {
                            velocity.Y = 0;
                        }
                    }
                    if (col + 1 < level.Width)
                    {
                        Tile leTile = level.Tiles[col + 1, row + 1];
                        if (leTile.Type != Tile.TileType.Passable &&
                            true == Bounding.CheckCollision(new FloatRect(col + 1.0f, row + 1.0f, 1.0f, 1.0f)))
                        {
                            velocity.Y = 0;
                        }
                    }
                     * */
            }
            if (Velocity.X < 0 && col - 1 >= 0)
            {
                Tile t = level.Tiles[col - 1, row];
                if (t.Type != Tile.TileType.Passable &&
                    true == Bounding.CheckCollision(new FloatRect(col - 1, row, 1.0f, 1.0f)))
                {
                    velocity.X = 0;

                }
            } else if (Velocity.X > 0 )
            {
                Tile t = level.Tiles[col + 1, row];
                if (t.Type != Tile.TileType.Passable &&
                    true == Bounding.CheckCollision(new FloatRect(col + 1, row, 1.0f, 1.0f)))
                    velocity.X = 0;
            }

            Position += Velocity;
            if (gpState.Buttons.A == ButtonState.Pressed) {
                Perform(action, col, row);
            }
            
        }

        private void Perform(int act, int col, int row)
        {
            Tile[] around = new Tile[8];
            around[0] = App.GetInstance().Level.Tiles[col, row - 1];
            around[1] = App.GetInstance().Level.Tiles[col, row + 1];
            around[2] = App.GetInstance().Level.Tiles[col - 1, row];
            around[3] = App.GetInstance().Level.Tiles[col + 1, row];
            around[4] = App.GetInstance().Level.Tiles[col - 1, row - 1];
            around[5] = App.GetInstance().Level.Tiles[col + 1, row + 1];
            around[6] = App.GetInstance().Level.Tiles[col - 1, row + 1];
            around[7] = App.GetInstance().Level.Tiles[col + 1, row - 1];
            Plasmid[] powers = new Plasmid[8];
            switch (act)
            {
                case (int)Plasmid.None:
                    for (int surrounds = 0; surrounds < 8; surrounds++)
                    {

                    }
                    break;
                case (int)Plasmid.Catalyst:
                    for (int check = 0; check < 8; check++)
                    {
                        if (around[check].Type == Tile.TileType.Gap)
                        {
                            around[check].Photo = App.GetInstance().TileSwitch[0];
                        }
                    }
                    break;
                case(int)Plasmid.Enzyme:
                    for (int check = 0; check < 4; check++)
                    {
                        if (around[check].Type == Tile.TileType.Destructable)
                        {
                            around[check].Photo = App.GetInstance().TileSwitch[1];
                        }
                    }
                    break;
                case(int)Plasmid.ATPcarrier:
                    for (int check = 0; check < 4; check++)
                    {
                        if (around[check].Type == Tile.TileType.Gap)
                        {
                            around[check].Photo = App.GetInstance().TileSwitch[2];
                        }
                    }
                    break;
                case(int)Plasmid.Offensive:
                    break;
                case(int)Plasmid.Toxin:
                    break;
                case(int)Plasmid.Endo:
                    break;
                case(int)Plasmid.Marker:
                    break;
                default:
                    break;
            }
        }
    }
}
