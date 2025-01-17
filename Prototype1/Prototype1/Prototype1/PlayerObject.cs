using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

using Box2D.XNA;


namespace Prototype1
{
    class PlayerObject:GameObject
    {

        public int bounceCount;

        public int score;
        public SpriteFont scoreFont;
        public Vector2 scorePosition;
        public bool isScoring;

        public bool isFlicked;

        public float minVelocity;
        public Body ball;

   

        public PlayerObject(Texture2D tex):base( tex)
        {
            bounceCount = 0;
            score = 0;
            isScoring = false;
            scorePosition = new Vector2(position.X, position.Y + 20f);
            isFlicked = false;
            minVelocity = 50f;
           // radius = radius / 2;

            speed = 0.2f;
        }


        public void UpdatePV_Player(float ScaleFactor)
        {
            //base.UpdatePV();

            position = ball.GetPosition() / ScaleFactor;  // seting pos into pixel
           // position += velocity;

            rect = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            center = new Vector2(position.X + texture.Width / 2, position.Y + texture.Height / 2);


            scorePosition = new Vector2(position.X + radius, position.Y - 50f);

            velocity = ball.GetLinearVelocity();
            velocity -= friction * velocity;
            
            if ((velocity.Length() < minVelocity*ScaleFactor )&& isFlicked == true)
            {
                velocity = new Vector2(0, 0);
               // isFlicked = false;
            }


            position = position * ScaleFactor;

            ball.SetLinearVelocity ( velocity)  ;


        }




        public void DrawPlayer(SpriteBatch spriteBatch,float ScaleFactor)
        {
           // base.Draw(spriteBatch);


            spriteBatch.Draw(texture, ball.GetPosition() / ScaleFactor, null, Color.White, ball.Rotation,

                                 new Vector2(texture.Width / 2f, texture.Height / 2f), 1, SpriteEffects.None,0);


           // if(isFlicked)
                spriteBatch.DrawString(scoreFont, score.ToString(), scorePosition, Color.Orchid);
        }


        public Body CreateBall(World world, float ScaleFactor)
        {

            var bodyDef = new BodyDef();

            bodyDef.type = BodyType.Dynamic;

            var ballShape = new CircleShape();

            ballShape._radius = (texture.Width / 2f) * ScaleFactor;

            var ballFixture = new FixtureDef();

            ballFixture.friction = 0.0f; // no friction

            ballFixture.restitution = 1.0f; // give the ball a perfect bounce

            ballFixture.density = 1.0f;

            ballFixture.shape = ballShape;

            var ballBody = world.CreateBody(bodyDef);

            ballBody.CreateFixture(ballFixture);

            // ballBody.Position = new Vector2(((float)r.NextDouble() * 4.5f + .3f), (float)r.NextDouble() * 4.5f + .3f);

            //ballBodies.Add(ballBody);

            return ballBody;
        }



        

    }
}
