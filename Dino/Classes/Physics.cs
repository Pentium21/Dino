using System;
using System.Drawing;

namespace Moon.Classes
{
    public class Physics
    {
        public Transform transform;
        float gravity;
        float a;

        public bool isJumping;
        public bool isCrouching;

        public Physics(PointF position, Size size)
        {
            transform = new Transform(position, size);
            gravity = 0;
            a = 0.4f;
            isJumping = false;
            isCrouching = false;
        }

        public void ApplyPhysics()
        {
            CalculatePhysics();
        }

        public void CalculatePhysics()
        {
            if(transform.position.Y<150 || isJumping)
            {
                transform.position.Y += gravity;
                gravity += a;
            }
            if (transform.position.Y > 150)
                isJumping = false;
        }

        public bool Collide()
        {
            for(int i = 0; i < GameController.cactuses.Count; i++)
            {
                var cactus = GameController.cactuses[i];
                PointF delta = new PointF();
                delta.X = (transform.position.X + transform.size.Width / 2) - (cactus.transform.position.X + cactus.transform.size.Width / 2);
                delta.Y = (transform.position.Y + transform.size.Height / 2) - (cactus.transform.position.Y + cactus.transform.size.Height / 2);
                if (Math.Abs(delta.X) <= transform.size.Width / 2 + cactus.transform.size.Width / 2)
                {
                    if (Math.Abs(delta.Y) <= transform.size.Height / 2 + cactus.transform.size.Height / 2)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void AddForce()
        {
            if (!isJumping)
            {
                isJumping = true;
                gravity = -10;
            }
        }
    }
}
