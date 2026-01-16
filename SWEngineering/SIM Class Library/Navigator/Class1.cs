using System;
using System.Linq;
using System.Collections.Generic;
using SIM;

namespace Navigator
{
    public class Make_Path
    {
        private int numReachedObjectives = 0;

        private RobotMovementInterface robot;
        private Minimap miniMap;

        public Make_Path(ref RobotMovementInterface target)
        {
            robot = target;
        }

        /*
         * Minimap 클래스의 객체 miniMap를 생성하고 초기화한다.
         */
        public void initialize(int sizeX, int sizeY, int posX, int posY, int direction)
        {
            miniMap = new Minimap();

            miniMap.makeMap(sizeX, sizeY);
            miniMap.setPosition(posX, posY, direction);
        }

        /*
         * miniMap에 Objective를 추가한다.
         */
        public void setObjective(int x, int y)
        {
            miniMap.setObjective(x, y);
        }

        public void setItem(int x, int y, int target)
        {
            miniMap.editMap(x, y, target);
        }

        /*
         * MiniMap에서 지도 정보를 읽는다.
         * 장애물을 피해서 다음 Objective로 가는 경로를 생성한다.
         * 현재 로봇의 방향과 다음 이동 방향이 일치하는지 확인한다.
         * 일치하지 않으면 SIM의 RobotMovementInterface에 rotate 명령을 내리고 처음부터다시 진행한다.
         */
        public void pathFind()
        {
            // { x좌표, y좌표, 경로 비용, 누적 비용, 예측 비용 }
            var start = new int[6] { miniMap.getPosX(), miniMap.getPosY(), 0, 0, 0, 0 };  //시작 좌표
            var target = new int[6] { miniMap.getObjectiveX(numReachedObjectives), miniMap.getObjectiveY(numReachedObjectives), 0, 0, 0, 0 }; // 목표 좌표

            while (true)
            {
                // 열린 리스트, 닫힌 리스트 초기화
                int[] current = null;
                var openList = new List<int[]>();
                var closedList = new List<int[]>();
                int g = 0;
                int end = 0;

                // 시작 지점을 열린 리스트에 추가
                openList.Add(start);

                while (openList.Count > 0)
                {
                    // 최소 비용인 값을 저장함  
                    var lowest = openList.Min(l => l[2]);
                    current = openList.First(l => l[2] == lowest);

                    // 현재 지점을 닫힌 리스트에 추가
                    closedList.Add(current);

                    // 현재 지점을 열린 리스트에서 제거
                    openList.Remove(current);

                    // 닫힌 리스트에 추가된 좌표가 목표 좌료랑 같으면 루프 탈출
                    if (closedList.FirstOrDefault(l => l[0] == target[0] && l[1] == target[1]) != null)
                        break;

                    // 이동경로 계산
                    List<int[]> safe_zones = new List<int[]>();
                    int x = current[0];
                    int y = current[1];

                    int near;
                    near = miniMap.readMap(x, y - 1);
                    makeSafeZones(near, x, y - 1, ref safe_zones, openList);
                    near = miniMap.readMap(x, y + 1);
                    makeSafeZones(near, x, y + 1, ref safe_zones, openList);
                    near = miniMap.readMap(x - 1, y);
                    makeSafeZones(near, x - 1, y, ref safe_zones, openList);
                    near = miniMap.readMap(x + 1, y);
                    makeSafeZones(near, x + 1, y, ref safe_zones, openList);



                    g = current[3] + 1;

                    foreach (var safe_zone in safe_zones)
                    {
                        // 탐색된 지점이 닫힌 리스트에 있으면 넘어감
                        if (closedList.FirstOrDefault(l => l[0] == safe_zone[0]
                            && l[1] == safe_zone[1]) != null)
                            continue;

                        // 없으면 열린 리스트에 추가
                        if (openList.FirstOrDefault(l => l[0] == safe_zone[0]
                            && l[1] == safe_zone[1]) == null)
                        {
                            // 누적비용
                            safe_zone[3] = g;

                            // 추가 비용 계산
                            int deltaX = target[0] - safe_zone[0];
                            deltaX = (deltaX > 0 ? deltaX : -deltaX);
                            int deltaY = target[1] - safe_zone[1];
                            deltaY = (deltaY > 0 ? deltaY : -deltaY);
                            safe_zone[4] = deltaX + deltaY;

                            safe_zone[2] = safe_zone[3] + safe_zone[4];

                            for (int i = 0; i < closedList.Count(); i++)
                            {
                                if (closedList[i][0] == current[0] && closedList[i][1] == current[1])
                                    safe_zone[5] = i;
                            }

                            // 열린 리스트에 추가
                            openList.Insert(0, safe_zone);
                        }
                        else
                        {
                            // 현재 비용과 추가된 지점의 비용을 비교
                            if (g + safe_zone[4] < safe_zone[2])
                            {
                                safe_zone[3] = g;
                                safe_zone[2] = safe_zone[3] + safe_zone[4];
                                for (int i = 0; i < closedList.Count(); i++)
                                {
                                    if (closedList[i][0] == current[0] && closedList[i][1] == current[1])
                                        safe_zone[5] = i;
                                }
                            }
                        }
                    }
                }

                for (int i = 0; i < closedList.Count(); i++)
                {
                    if (closedList[i][0] == current[0] && closedList[i][1] == current[1])
                        end = i;
                }
                while (closedList[end][5] != 0)
                {
                    end = closedList[end][5];
                }

                // 이동 방향이 현재 방향과 같은지 판정하고 아니면 회전
                // 이동 방향이 현재 방향과 같은지 판정하고 아니면 회전
                int dir = miniMap.getDirection();
                if (dir == 1)
                    if (closedList[end][0] == miniMap.getPosX() - 1)
                        break;
                if (dir == 2)
                    if (closedList[end][1] == miniMap.getPosY() + 1)
                        break;
                if (dir == 3)
                    if (closedList[end][0] == miniMap.getPosX() + 1)
                        break;
                if (dir == 4)
                    if (closedList[end][1] == miniMap.getPosY() - 1)
                        break;

                SensorReport sensor = robot.rotate();
                mapUpdate(ref sensor);
            }
        }

        private void makeSafeZones(int near, int x, int y, ref List<int[]> safe_zones, List<int[]> openList)
        {
            if (near == 0 || near == 1 || near == 3)
            {
                int[] node = openList.Find(l => l[0] == x && l[1] == y);
                if (node == null) safe_zones.Add(new int[6] { x, y, 0, 0, 0, 0 });
                else safe_zones.Add(node);
            }
        }

        /*
         * SIM의 RobotMovementInterface의 move를 호출한다.
         * 센서 정보를 리턴받고 MiniMap을 갱신한다.
         * MapReport를 생성한다.
         * MiniMap에서 지도를 읽고 MapReport에 옮겨 적는다.
         * MapReport를 반환한다.
         */
        public MapReport move()
        {
            SensorReport sensor = robot.move();

            mapUpdate(ref sensor);

            int sizeX = miniMap.getSizeX();
            int sizeY = miniMap.getSizeY();
            int posX = miniMap.getPosX();
            int posY = miniMap.getPosY();
            int direction = miniMap.getDirection();
            MapReport report = new MapReport(sizeX, sizeY, posX, posY, direction);

            if (posX == miniMap.getObjectiveX(numReachedObjectives) && posY == miniMap.getObjectiveY(numReachedObjectives))
                numReachedObjectives++;

            for(int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    report.editMap(i, j, miniMap.readMap(i, j));
                }
            }

            return report;
        }

        private void mapUpdate(ref SensorReport sensor)
        {
            int posX = sensor.getPosX();
            int posY = sensor.getPosY();
            int direction = sensor.getDirection();  // 방향 정보 :  // up: 1, right: 2, down: 3, left: 4

            // 센서 정보:
            // 0: empty, 1: Objective, 2: Danger, 3: Color Blob, -1: out of border
            int forward = sensor.getForward();
            int backward = sensor.getBack();
            int left = sensor.getLeft();
            int right = sensor.getRight();

            int[] forwardCord;
            int[] backwardCord;
            int[] leftCord;
            int[] rightCord;

            // 방향에 따라 다른 위치
            switch (direction)
            {
                case 1:
                    forwardCord = new int[2] { posX - 1, posY };
                    backwardCord = new int[2] { posX + 1, posY};
                    leftCord = new int[2] { posX, posY - 1 };
                    rightCord = new int[2] { posX, posY + 1 };
                    break;
                case 2:
                    forwardCord = new int[2] { posX, posY + 1 };
                    backwardCord = new int[2] { posX, posY - 1 };
                    leftCord = new int[2] { posX - 1, posY };
                    rightCord = new int[2] { posX + 1, posY };
                    break;
                case 3:
                    forwardCord = new int[2] { posX + 1, posY };
                    backwardCord = new int[2] { posX - 1, posY };
                    leftCord = new int[2] { posX, posY + 1 };
                    rightCord = new int[2] { posX, posY - 1 };
                    break;
                case 4:
                    forwardCord = new int[2] { posX, posY - 1 };
                    backwardCord = new int[2] { posX, posY + 1 };
                    leftCord = new int[2] { posX + 1, posY };
                    rightCord = new int[2] { posX - 1, posY };
                    break;
                default:
                    forwardCord = new int[2];
                    backwardCord = new int[2];
                    leftCord = new int[2];
                    rightCord = new int[2];
                    break;
            }

            miniMap.setPosition(posX, posY, direction);
            if (forward == 2 || forward == 3)
                miniMap.editMap(forwardCord[0], forwardCord[1], forward);
            if (backward == 3)
                miniMap.editMap(backwardCord[0], backwardCord[1], backward);
            if (left == 3)
                miniMap.editMap(leftCord[0], leftCord[1], left);
            if (right == 3)
                miniMap.editMap(rightCord[0], rightCord[1], right);
        }

        // 모든 목표를 탐색하였는지?
        public int evalAllReached()
        {
            if (miniMap.getNumObjective() == this.numReachedObjectives)
                return 1;
            else
                return 0;
        }
    }

    public class Minimap
    {
        private int[,] map;
        private int posX, posY;
        private int sizeX, sizeY;
        private int direction;
        private int numObjectives = 0;
        private int[] objectiveX, objectiveY;

        public void makeMap(int x, int y)
        {
            sizeX = x;
            sizeY = y;

            // 맵을 만들고 모서리를 -1(out of border)로 초기화
            map = new int[sizeX + 2, sizeY + 2];
            for (int i = 0; i < x + 2; i++)
            {
                for (int j = 0; j < y + 2; j++)
                {
                    if (i == 0 || i == sizeX + 1 || j == 0 || j == sizeY + 1)
                        map[i, j] = 2;
                }
            }
            objectiveX = new int[0];
            objectiveY = new int[0];
        }

        public void setPosition(int x, int y, int dir)
        {
            posX = x + 1;
            posY = y + 1;
            direction = dir;
        }

        public void setObjective(int x, int y)
        {
            numObjectives++;
            Array.Resize(ref objectiveX, numObjectives);
            Array.Resize(ref objectiveY, numObjectives);
            objectiveX[numObjectives - 1] = x + 1;
            objectiveY[numObjectives - 1] = y + 1;
            map[x + 1, y + 1] = 1;
        }

        public void editMap(int x, int y, int target)
        {
            map[x + 1, y + 1] = target;
        }

        public int getObjectiveX(int target)
        {
            return objectiveX[target] - 1;
        }

        public int getObjectiveY(int target)
        {
            return objectiveY[target] - 1;
        }

        public int getNumObjective()
        {
            return numObjectives;
        }

        public int readMap(int x, int y)
        {
            return map[x + 1, y + 1];
        }

        public int getPosX()
        {
            return posX - 1;
        }

        public int getPosY()
        {
            return posY - 1;
        }

        public int getSizeX()
        {
            return sizeX;
        }

        public int getSizeY()
        {
            return sizeY;
        }

        public int getDirection()
        {
            return direction;
        }
    }

    public class MapReport
    {
        private int[,] map;
        private int posX, posY;
        private int direction;
        

        /*
         * MapReport를 초기화
         */
        public MapReport(int sizeX, int sizeY, int posX, int posY, int dir)
        {
            map = new int[sizeX, sizeY];
            this.posX = posX;
            this.posY = posY;
            direction = dir;
        }

        /*
         * map을 초기화
         */
        public void editMap(int x, int y, int target)
        {
            map[x, y] = target;
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
    }
}

