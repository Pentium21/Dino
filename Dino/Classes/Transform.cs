using System;
using System.Drawing;

namespace Moon.Classes
{
    [Serializable]
    public class Transform
    {
        public PointF position;
        public Size size;

        public Transform(PointF pos, Size size)
        {
            this.position = pos;
            this.size = size;
        }
    }
}