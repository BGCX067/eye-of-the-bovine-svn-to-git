using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Eye_of_the_Bovine
{
    public class Virus : Enemy
    {
        Texture2D viralPic;
        Vector2 currentLocation, destination, trajectory;
        Bacterium target;
        Rectangle bound, targetCollision;
        TimeSpan damageTiem = new TimeSpan();
        GameTime currentTiem = new GameTime();
        float distanceToTarget, speed = 0.06f;
        Game world;
        bool attached = false, existant = true;
        int damageTicks = 0;
        Random chance = new Random(DateTime.Now.Millisecond);
        public Virus(Game app, Texture2D image, Vector2 location)
            : base(app, image, location)
        {
            world = app;
            viralPic = image;
            bound.Width = viralPic.Width;
            bound.Height = viralPic.Height;
            currentLocation = location;
            distanceToTarget = 110.0f;
        }
        private Vector2 FindTarget()
        {
            for (int a = 0; a < world.players.Count<Bacterium>(); a++)
            {
                if (world.players[a] == null)
                    break;
                else if (Math.Abs(currentLocation.X - world.players[a].Position.X) +
                Math.Abs(currentLocation.Y - world.players[a].Position.Y) < distanceToTarget)
                {
                    target = world.players[a];
                    distanceToTarget = Math.Abs(currentLocation.X - world.players[a].Position.X) + Math.Abs(currentLocation.Y - world.players[a].Position.Y);
                }
            }
            if (target != null)
                return target.Position;
            else
                return Vector2.Zero;
        }
        public void Draw(SpriteBatch s)
        {
            if (existant == true)
                s.Draw(viralPic, bound, Color.White);
        }
        public void Update()
        {
            if (existant == true)
            {
                bound.X = (int)((currentLocation.X - world.camPosition.X) * (float)world.tileSize);
                bound.Y = (int)((currentLocation.Y - world.camPosition.Y) * (float)world.tileSize);

                if (target != null)
                {
                    targetCollision = target.Bounding.GenerateRectangle();
                    if (Math.Abs(currentLocation.X - target.Position.X) < 0.4f &&
                        Math.Abs(currentLocation.Y - target.Position.Y) < 0.4f &&
                        attached == false)
                    {
                        attached = true;
                        damageTiem = TimeSpan.FromSeconds(DateTime.Now.Second) + TimeSpan.FromSeconds(1.8);
                        target.TakeDamage();
                    }
                }
                if ((chance.Next(100) >= 60 || target == null || destination == null) && attached == false)
                    destination = FindTarget();
                if (attached == true)
                {
                    currentLocation = target.Position;
                    if (TimeSpan.FromSeconds(DateTime.Now.Second) >= damageTiem)
                    {
                        target.TakeDamage();
                        damageTicks++;
                        damageTiem = TimeSpan.FromSeconds(DateTime.Now.Second) + TimeSpan.FromSeconds(1.8);
                    }
                }
                else if (target != null && destination != null)
                {
                    trajectory = destination - currentLocation;
                    trajectory.Normalize();
                    currentLocation += trajectory * speed;
                }
            }
            if (damageTicks >= 2)
                existant = false;
        }
    }
}
