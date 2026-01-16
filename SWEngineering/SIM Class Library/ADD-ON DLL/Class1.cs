using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIM_Class_Library;

namespace Navigator
{
       public class MapEditor
        {
            private int[] tx, ty; // 목표지점 3개의 좌표배열
            private int[,] map;
            private int sizeX, sizeY, posX, posY, direction;

            
            public int[] setObjectiveX //목표지점의 x좌표 배열을 받고 줌
            {
                get { return tx; }
                set { tx = value; }
            }

            public int[] setObjectiveY //목표지점의 y좌표 배열을 받고 줌
        {
                get { return ty; }
                set { ty = value; }
        }

            public int[,] makeMap   //맵을 리턴
            {
                get { return map; }
            }

            public int initializeX //시작위치의 x좌표를 받고 줌
            {
                get { return posX; }
                set { posX = value; }
            }

            public int initializeY //시작위치의 y좌표를 받고 줌
        {
                get { return posY; }
                set { posY = value; }
            }
        }
        public class MapReport
        {
            private int[,] map;
            public int posX, posY, direction;

            public int[,] reportmap //탐색완료 후 미니맵의 맵정보를 리턴
        {
                get {
                    MiniMap mm = new MiniMap();
                    map = mm.readMap;
                    return map; }
            }
            public int[,] readMap //맵을 받고 줌
            {
                get { return map; }
                set { map = value; }
            }
            public int getPosX  // 현재 x위치를 받고 줌
            {
                get { return posX; }
                set { posX = value; }
            }
            public int getPosY  // 현재 y위치를 받고 줌
        {
                get { return posY; }
                set { posY = value; }
            }
            public int getDirection  // 현재 방향을 받고 줌
        {
                get { return direction; }
                set { direction = value; }
            }

        }

    public class Make_Path
    {
        private int[] tx, ty; // 목표지점 3개의 좌표배열
        private int[,] objective;
        private int posX, posY, dx, dy, sizeX, sizeY, direction, nextMoveDirection, numReachedObjectives;
        private RobotMovementInterface robot;

        
        public Make_Path(RobotMovementInterface target)
        {
            robot = target;
        }
           
            public int setObjectiveX //맵 에디터로부터 목표지점의 x좌표 배열을 받음
            {
                set{ MapEditor me = new MapEditor();
                tx = me.setObjectiveX; }
            }

            public int setObjectiveY //맵 에디터로부터 목표지점의 y좌표 배열을 받음
        {
                set{MapEditor me = new MapEditor();
                    ty = me.setObjectiveY;}
                }

            public SensorReport move
            {  set{ }
               
            }
            public void pathFind() //경로 찾기
            {
                if(posX != dx)
                {
                posX++;
                }
                if (posY != dy)
                {
                posY--;
                }
                if(posX == dx || posY == dy)
                {
                Console.WriteLine("finish");
                }
        }

            public int initializeX  // 맵 에디터로부터 초기위치의 x좌표 설정
            {
                get { return posX; }
                set {  MapEditor me = new MapEditor();
                    posX = me.initializeX;}
            }

            public int initializeY // 맵 에디터로부터 초기위치의 y좌표 설정
            {
                get { return posY; }
                set { MapEditor me = new MapEditor();
                    posY = me.initializeY; }
            }
        

        }

        public class MiniMap
        {
            private int[] objX, objY;
            private int[,] map;
            private int posX, posY, direction, numObjectives;

            public void makeMap() //맵에디터로부터 맵을 받음
            {
                MapEditor me = new MapEditor();
                map = me.makeMap;
            }


            public int[,] readMap //맵정보를 받고 줌
            {
                get { return map; }
                set { map = value; }
            }
        
            public int[] getObjectiveX //맵 에디터로부터 목표지점의 x좌표 배열을 받음
        {
               
                set{ objX = value; }
            }

            public int[] getObjectiveY //맵 에디터로부터 목표지점의 y좌표 배열을 받음
        {
                set { objY = value; }
            }

        }

        public class Navigator
        {
            static void Main(string[] args)
            {
            }
        
    }

   
}
