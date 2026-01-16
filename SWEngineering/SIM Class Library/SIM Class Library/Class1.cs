using System;

namespace SIM
{
    public class RobotMovementInterface
    {
        private int sizeX;
        private int sizeY;
        private int posX;
        private int posY;
        private int direction;      // up: 1, right: 2, down: 3, left: 4

        private CompleteMap map;

        // 맵 생성 및 초기화
        public void generateMap(int x, int y)
        {
            sizeX = x;
            sizeY = y;
            map = new CompleteMap();
            map.makeMap(sizeX, sizeY);
        }

        // 맵 수정
        public void editMap(int x, int y, int target)
        {
            map.editMap(x, y, target);
        }

        
        public void setPosition(int x, int y, int dir)
        {
            posX = x;
            posY = y;
            direction = dir;

            map.setPosition(posX, posY, direction);
        }

        /*
         * 로봇을 회전하고 주위 4방향의 센서 정보를 반환한다.
         * 위험지역은 앞에만 나온다.
         */
        public SensorReport rotate()
        {
            int forward, backward, left, right;

            posX = map.getPosX();
            posY = map.getPosY();
            direction = map.getDirection();

            // 회전
            if (direction != 4)
                direction++;
            else
                direction = 1;

            map.setPosition(posX, posY, direction);
            
            // 방향에 따른 위치 변화
            switch(direction)
            {
                case 1:
                    forward = map.readMap(posX - 1, posY);
                    backward = map.readMap(posX + 1, posY);
                    left = map.readMap(posX, posY - 1);
                    right = map.readMap(posX, posY + 1);
                    break;
                case 2:
                    forward = map.readMap(posX, posY + 1);
                    backward = map.readMap(posX, posY - 1);
                    left = map.readMap(posX - 1, posY);
                    right = map.readMap(posX + 1, posY);
                    break;
                case 3:
                    forward = map.readMap(posX + 1, posY);
                    backward = map.readMap(posX - 1, posY);
                    left = map.readMap(posX, posY + 1);
                    right = map.readMap(posX, posY - 1);
                    break;
                case 4:
                    forward = map.readMap(posX, posY - 1);
                    backward = map.readMap(posX, posY + 1);
                    left = map.readMap(posX + 1, posY);
                    right = map.readMap(posX - 1, posY);
                    break;
                default:
                    forward = 0;
                    backward = 0;
                    left = 0;
                    right = 0;
                    break;
            }

            // forward가 아니면 위험지역은 모르게한다.
            backward = (backward == 2 ? 0 : backward);
            left = (left == 2 ? 0 : left);
            right = (right == 2 ? 0 : right);

            SensorReport report = new SensorReport(forward, backward, left, right, posX, posY, direction);

            return report;
        }

        /*
         * 로봇을 앞으로 이동하고 주위 4방향 센서정보를 반환한다.
         * 무작위로 두칸 이동할 수 있다.
         * 무작위로 앞에 위험지역이 생길 수 있다.
         */
        public SensorReport move()
        {
            int forward, backward, left, right;

            posX = map.getPosX();
            posY = map.getPosY();
            direction = map.getDirection();

            Random rand = new Random();


            //방향에 따른 위치면화, 이동하되 무작위로 2칸이동한다.
            switch (direction)
            {
                case 1:
                    if ((rand.Next() % 10) == 1)
                        posX = posX - 2;
                    else
                        posX = posX - 1;
                    break;
                case 2:
                    if ((rand.Next() % 10) == 1)
                        posY = posY + 2;
                    else
                        posY = posY + 1;
                    break;
                case 3:
                    if ((rand.Next() % 10) == 1)
                        posX = posX + 2;
                    else
                        posX = posX + 1;
                    break;
                case 4:
                    if ((rand.Next() % 10) == 1)
                        posY = posY - 2;
                    else
                        posY = posY - 1;
                    break;
            }

            //무작위로 앞에 위험지역 생성
            switch (direction)
            {
                case 1:
                    if ((rand.Next() % 10) == 1)
                        map.editMap(posX - 1, posY, 2);
                    break;
                case 2:
                    if ((rand.Next() % 10) == 1)
                        map.editMap(posX, posY + 1, 2);
                    break;
                case 3:
                    if ((rand.Next() % 10) == 1)
                        map.editMap(posX + 1, posY, 2);
                    break;
                case 4:
                    if ((rand.Next() % 10) == 1)
                        map.editMap(posX, posY - 1, 2);
                    break;
            }

            map.setPosition(posX, posY, direction);

            // 방향에 따른 위치변화
            switch (direction)
            {
                case 1:
                    forward = map.readMap(posX - 1, posY);
                    backward = map.readMap(posX + 1, posY);
                    left = map.readMap(posX, posY - 1);
                    right = map.readMap(posX, posY + 1);
                    break;
                case 2:
                    forward = map.readMap(posX, posY + 1);
                    backward = map.readMap(posX, posY - 1);
                    left = map.readMap(posX - 1, posY);
                    right = map.readMap(posX + 1, posY);
                    break;
                case 3:
                    forward = map.readMap(posX + 1, posY);
                    backward = map.readMap(posX - 1, posY);
                    left = map.readMap(posX, posY + 1);
                    right = map.readMap(posX, posY - 1);
                    break;
                case 4:
                    forward = map.readMap(posX, posY - 1);
                    backward = map.readMap(posX, posY + 1);
                    left = map.readMap(posX + 1, posY);
                    right = map.readMap(posX - 1, posY);
                    break;
                default:
                    forward = 0;
                    backward = 0;
                    left = 0;
                    right = 0;
                    break;
            }
            
            // forward가 아니면 위험지역은 모르게 한다.
            backward = (backward == 2 ? 0 : backward);
            left = (left == 2 ? 0 : left);
            right = (right == 2 ? 0 : right);

            SensorReport report = new SensorReport(forward, backward, left, right, posX, posY, direction);

            return report;
        }
    }

    public class CompleteMap
    {
        private int posX;
        private int posY;
        private int direction;
        private int[,] map;         // 0: empty, 1: Objective, 2: Danger, 3: Color Blob, -1: out of border

        public int getPosX()
        {
            return posX - 1;
        }

        public int getPosY()
        {
            return posY - 1;
        }

        public void setPosition(int x, int y, int dir)
        {
            posX = x + 1;
            posY = y + 1;
            direction = dir;
        }

        public int getDirection()
        {
            return direction;
        }

        public void editMap(int x, int y, int target)
        {
            map[x + 1, y + 1] = target;
        }

        public int readMap(int x, int y)
        {
            return map[x + 1, y + 1];
        }

        public void makeMap(int x, int y)
        {
            // 맵을 만들고 모서리를 1(벽)으로 초기화
            map = new int[x + 2, y + 2];
            for (int i = 0; i < x + 2; i++)
            {
                for (int j = 0; j < y + 2; j++)
                {
                    if (i == 0 || i == x + 1 || j == 0 || j == y + 1)
                        map[i, j] = 2;
                }
            }
        }
    }

    public class SensorReport
    {
        private int forward;
        private int backward;
        private int left;
        private int right;
        private int posX;
        private int posY;
        private int direction;

        public SensorReport(int forward, int backward, int left, int right, int posX, int posY, int direction)
        {
            this.forward = forward;
            this.backward = backward;
            this.left = left;
            this.right = right;
            this.posX = posX;
            this.posY = posY;
            this.direction = direction;
        }

        public int getForward()
        {
            return forward;
        }

        public int getBack()
        {
            return backward;
        }

        public int getLeft()
        {
            return left;
        }

        public int getRight()
        {
            return right;
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
    }
}