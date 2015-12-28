using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Eye_of_the_Bovine
{
    public class FloatRect
    {
        public Vector2 UpperLeft { get; set; }
        public Vector2 LowerRight { get; set; }
        Rectangle subRect = new Rectangle();

        public FloatRect() {
            UpperLeft = new Vector2();
            LowerRight = new Vector2();
        }

        public FloatRect( Vector2 upperLeft, Vector2 lowerRight) {
            UpperLeft = upperLeft;
            LowerRight = lowerRight;
        }

        public FloatRect(float x, float y, float width, float height) 
        {
            UpperLeft = new Vector2(x,y);
            LowerRight = new Vector2(x + width, y + height);
        }

        public Rectangle GenerateRectangle()
        {
            subRect.X = (int)UpperLeft.X;
            subRect.Y = (int)UpperLeft.Y;
            subRect.Width = (int)(LowerRight.X - UpperLeft.X);
            subRect.Height = (int)(UpperLeft.Y - LowerRight.Y);
            return subRect;
        }

        public bool CheckCollision(FloatRect otherRect) 
        {
            // If My upper left is less than his lower right
            if (UpperLeft.X < otherRect.LowerRight.X &&
                UpperLeft.Y < otherRect.LowerRight.Y &&
                // and my lower right is greater than his upper left
                LowerRight.X > otherRect.UpperLeft.X &&
                LowerRight.Y > otherRect.UpperLeft.Y)
                //we have collision
                return true;
            return false;
        }
    }
}
