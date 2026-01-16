using System;

namespace GUI
{
    public class Interface
    {
        private int[,] map;
        private bool isComplete;
        private int sizeX;
        private int sizeY;
        private int posX;
        private int posY;
        private int direction;

        private MapEditor mapEditor;
        private Navigator.Make_Path pathFinder;

        // 경로탐색, 회전, 이동까지 모두 수행한다.
        public void doNext()
        {
            pathFinder.pathFind();
            Navigator.MapReport report = pathFinder.move();
            posX = report.getPosX();
            posY = report.getPosY();
            direction = report.getDirection();

            //지도 갱신
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    map[i, j] = report.readMap(i, j);
                }
            }
        }

        public void selectMap(int x, int y)
        {
            mapEditor.setTarget(x, y);
        }

        public void editMap(int obj)
        {
            if (obj != 1)
                mapEditor.editMap(obj);
            else
                mapEditor.setObjective(obj);
        }

        public void setMapSize(int x, int y)
        {
            sizeX = x;
            sizeY = y;

            map = new int[x, y];
        }

        // 맵생성 및 초기화
        public void initialize(int posX, int posY, int direction)
        {
            this.posX = posX;
            this.posY = posY;
            this.direction = direction;

            mapEditor = new MapEditor();

            mapEditor.makeMap(sizeX, sizeY, this.posX, this.posY, this.direction);

            pathFinder = mapEditor.getPathFinder();
        }

        public int readMap(int x, int y)
        {
            return map[x, y];
        }

        public int getPosX()
        {
            return posX;
        }

        public int getPosY()
        {
            return posY;
        }
        
        public int getDirection()
        {
            return direction;
        }

        public int getIsDone()
        {
            if (pathFinder.evalAllReached() == 1)
                return 1;
            else
                return 0;
        }
    }

    public class MapEditor
    {
        private int targetX;
        private int targetY;

        private SIM.RobotMovementInterface robot;
        private Navigator.Make_Path pathFinder;

        public MapEditor()
        {
            robot = new SIM.RobotMovementInterface();
        }

        public ref Navigator.Make_Path getPathFinder()
        {
            return ref pathFinder;
        }

        public void setTarget(int x, int y)
        {
            targetX = x;
            targetY = y;
        }

        // 장애물, 컬러블롭 생성
        public void editMap(int obj)
        {
            robot.editMap(targetX, targetY, obj);
            if (obj != 3)
                pathFinder.setItem(targetX, targetY, obj);
        }

        // 목표지점 생성
        public void setObjective(int obj)
        {
            pathFinder.setObjective(targetX, targetY);
            robot.editMap(targetX, targetY, obj);
        }

        //맵 초기화
        public void makeMap(int sizeX, int sizeY, int posX, int posY, int direction)
        {
            robot.generateMap(sizeX, sizeY);
            robot.setPosition(posX, posY, direction);

            pathFinder = new Navigator.Make_Path(ref robot);
            pathFinder.initialize(sizeX, sizeY, posX, posY, direction);
        }
    }
}