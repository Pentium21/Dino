using System;
using System.Collections.Generic;
using System.Drawing;

namespace Moon.Classes
{
    [Serializable]
    public class GameController
    {
        public static Image spritesheet;
        public static List<Road> roads;
        public static List<Obstacle> cactuses;

        public static int dangerSpawn = 10;
        public static int countDangerSpawn = 0;


        public static void Init()
        {
            roads = new List<Road>();
            cactuses = new List<Obstacle>();
            spritesheet = Properties.Resources.sprite;
            GenerateRoad();
        }

        public static void MoveMap()
        {
            for(int i = 0; i < roads.Count; i++)
            {
                roads[i].transform.position.X -= 4;
                if (roads[i].transform.position.X + roads[i].transform.size.Width < 0)
                {
                    roads.RemoveAt(i);
                    GetNewRoad();
                }
            }
            for (int i = 0; i < cactuses.Count; i++)
            {
                cactuses[i].transform.position.X -= 4;
                if (cactuses[i].transform.position.X + cactuses[i].transform.size.Width < 0)
                {
                    cactuses.RemoveAt(i);
                }
            }

        }

        public static void GetNewRoad()
        {
            Road road = new Road(new PointF(0 + 100 * 9, 200), new Size(100, 17));
            roads.Add(road);
            countDangerSpawn++;

            if (countDangerSpawn >= dangerSpawn)
            {
                Random r = new Random();
                dangerSpawn = r.Next(5, 9);
                countDangerSpawn = 0;
                int obj = r.Next(0, 2);
                switch (obj)
                {
                    case 0:
                        Obstacle cactus = new Obstacle(new PointF(0 + 100 * 9, 150), new Size(50, 50));
                        cactuses.Add(cactus);
                        break;
                }
            }
        }
        public static void GenerateRoad()
        {
            for(int i = 0; i < 10; i++)
            {
                Road road = new Road(new PointF(0 + 100 * i, 200), new Size(100, 17));
                roads.Add(road);
                countDangerSpawn++;
            }
        }

        public static void DrawObjets(Graphics g)
        {
            for(int i = 0; i < roads.Count; i++)
            {
                roads[i].DrawSprite(g);
            }
            for (int i = 0; i < cactuses.Count; i++)
            {
                cactuses[i].DrawSprite(g);
            }
        }
    }
}
