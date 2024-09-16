using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MoveMgr : Singleton<MoveMgr>
{
    public List<List<List<Tile>>> objectPool = new List<List<List<Tile>>>();
    protected AudioSource audioSource; //이동 효과음

    public void Awake()
    {
        base.Awake();

        audioSource = GetComponent<AudioSource>();

        //인덱스를 좌표로 활용할거기 때문에 24*18 크기의 null로 미리 풀 채움. 
        for (int i = 0; i < 22; ++i)
        {
            objectPool.Add(new List<List<Tile>>());
            for (int j = 0; j < 26; ++j)
            {
                objectPool[i].Add(new List<Tile>());
            }
        }
    }

    public bool Move(int StartX, int StartY, Vector3 Vec) //이동 출발 위치x ,y , 방향
    {
        if (audioSource != null)        {
            audioSource.Play();
        }


        StartX /= 24;
        StartY /= 24;

        //오른쪽으로 갈때. xy중첩 신경 안써주는 이유는 그럴일이 없기 때문.
        if (Vec.x == 1)
        {
            if (StartX >= 24)
                return false;

            for (int i = StartX + 1; i < 26; ++i)
            {
                int StopCount = 0;
                if (i == 25)
                    return false;

                //xy고정된 상태로 z쭉 탐색하면서 모두 조건에 부합하는 칸들인지 확인함.
                for (int k = 0; k < objectPool[StartY][i].Count; ++k)
                {

                    if (objectPool[StartY][i][k].TypeArray[(int)Whatis_Type.Push])
                    {
                        //밀리는애 나오면 어쩌피 다음칸이 빈칸인지 확인해야해서 break;
                        break;
                    }

                    //빈칸이거나 겹침가능한 칸이면 Count+1
                    if (objectPool[StartY][i][k] == null || !objectPool[StartY][i][k].TypeArray[(int)Whatis_Type.Stop])
                        StopCount++;

                    //그 전에 겹침이 불가능한 칸이 먼저 나오면 이동 정지.
                    else if (!objectPool[StartY][i][k].TypeArray[(int)Whatis_Type.Stop] || !objectPool[StartY][i][k].TypeArray[(int)Whatis_Type.Push])
                        return false;
                }

                //(z에 있는 모든 칸이 겹침가능한 칸 || 빈칸) 이동시작.
                if (StopCount == objectPool[StartY][i].Count || objectPool[StartY][i].Count == 0)
                {
                    //뒤에서부터 당김. 앞에서 부터 밀면 밀려나는애를 임시저장해야하기 때문.
                    //결국 여까지 왔다는건 이 칸은 모두 빈칸or겹침가능 칸이기 때문에. 중요하지 않음.
                    //그 전칸 기준으로 어떤게 당겨지고 어떤게 남아있을지를 판별하고 이 칸으로 흡수해줘야함.                  
                    for (int j = i; j > StartX; --j)
                    {
                        for (int k = 0; k < objectPool[StartY][j-1].Count; ++k) //전 칸을 반복하며 그 칸에 어떤게 있는지 확인.
                        {
                            //밀었을때 밀리는애거나 / You(미는애)일때 당겨줌.
                            if (objectPool[StartY][j-1][k].TypeArray[(int)Whatis_Type.Push] 
                                || objectPool[StartY][j - 1][k].TypeArray[(int)Whatis_Type.You]
                                || objectPool[StartY][j - 1][k].TypeArray[(int)Whatis_Type.Move])
                            {
                                objectPool[StartY][j - 1][k].MoveCommand = new Vector3(1, 0, 0);
                                objectPool[StartY][j].Add(objectPool[StartY][j - 1][k]);
                                objectPool[StartY][j - 1].RemoveAt(k);
                            }
                        }
                    }
                    break;
                }
            }
        }

        else if (Vec.x == -1)
        {
            if (StartX <= 0)
                return false;

            for (int i = StartX - 1; i >= -1; --i)
            {
                int StopCount = 0;
                if (i == -1)
                    return false;

                for (int k = 0; k < objectPool[StartY][i].Count; ++k)
                {
                    if (objectPool[StartY][i][k].TypeArray[(int)Whatis_Type.Push])
                    {
                        //밀리는애 나오면 어쩌피 다음칸이 빈칸인지 확인해야해서 break;
                        break;
                    }
                    if (objectPool[StartY][i][k] == null || !objectPool[StartY][i][k].TypeArray[(int)Whatis_Type.Stop])
                        StopCount++;

                    else if (!objectPool[StartY][i][k].TypeArray[(int)Whatis_Type.Stop] || !objectPool[StartY][i][k].TypeArray[(int)Whatis_Type.Push])
                        return false;

                }

                //(z에 있는 모든 칸이 겹침가능한 칸 || 빈칸) 이동시작.
                if (StopCount == objectPool[StartY][i].Count || objectPool[StartY][i].Count == 0)
                {
                    for (int j = i; j < StartX; ++j)
                    {
                        for (int k = 0; k < objectPool[StartY][j + 1].Count; ++k) //전 칸을 반복하며 그 칸에 어떤게 있는지 확인.
                        {
                            if (objectPool[StartY][j + 1][k].TypeArray[(int)Whatis_Type.Push] 
                                || objectPool[StartY][j + 1][k].TypeArray[(int)Whatis_Type.You] 
                                || objectPool[StartY][j + 1][k].TypeArray[(int)Whatis_Type.Move])
                            {
                                objectPool[StartY][j + 1][k].MoveCommand = new Vector3(-1, 0, 0);
                                objectPool[StartY][j].Add(objectPool[StartY][j + 1][k]);
                                objectPool[StartY][j + 1].RemoveAt(k);
                            }
                        }
                    }
                    break;
                }
            }
        }

        else if(Vec.y == 1 && StartY < 23)
        {
            for (int i = StartY + 1; i < 24; ++i)
            {
                int StopCount = 0;
                //xy고정된 상태로 z쭉 탐색하면서 모두 조건에 부합하는 칸들인지 확인함.
                for (int k = 0; k < objectPool[i][StartX].Count; ++k)
                {
                    if (objectPool[i][StartX][k].TypeArray[(int)Whatis_Type.Push])
                    {
                        //밀리는애 나오면 어쩌피 다음칸이 빈칸인지 확인해야해서 break;
                        break;
                    }
                    //빈칸이거나 겹침가능한 칸이면 Count+1
                    if (objectPool[i][StartX][k] == null || !objectPool[i][StartX][k].TypeArray[(int)Whatis_Type.Stop])
                        StopCount++;

                    //그 전에 겹침이 불가능한 칸이 먼저 나오면 이동 정지.
                    else if (!objectPool[i][StartX][k].TypeArray[(int)Whatis_Type.Stop] || !objectPool[i][StartX][k].TypeArray[(int)Whatis_Type.Push])
                        return false;
                }

                //(z에 있는 모든 칸이 겹침가능한 칸 || 빈칸) 이동시작.
                if (StopCount == objectPool[i][StartX].Count || objectPool[i][StartX].Count == 0)
                {
                    //뒤에서부터 당김. 앞에서 부터 밀면 밀려나는애를 임시저장해야하기 때문.
                    //결국 여까지 왔다는건 이 칸은 모두 빈칸or겹침가능 칸이기 때문에. 중요하지 않음.
                    //그 전칸 기준으로 어떤게 당겨지고 어떤게 남아있을지를 판별하고 이 칸으로 흡수해줘야함.                  
                    for (int j = i; j > StartY; --j)
                    {
                        for (int k = 0; k < objectPool[j - 1][StartX].Count; ++k) //전 칸을 반복하며 그 칸에 어떤게 있는지 확인.
                        {
                            //밀었을때 밀리는애거나 / You(미는애)일때 당겨줌.
                            if (objectPool[j - 1][StartX][k].TypeArray[(int)Whatis_Type.Push] 
                                || objectPool[j - 1][StartX][k].TypeArray[(int)Whatis_Type.You] 
                                || objectPool[j - 1][StartX][k].TypeArray[(int)Whatis_Type.Move])
                            {
                                objectPool[j - 1][StartX][k].MoveCommand = new Vector3(0, 1, 0);
                                objectPool[j][StartX].Add(objectPool[j - 1][StartX][k]);
                                objectPool[j - 1][StartX].RemoveAt(k);
                            }
                        }
                    }
                    break;
                }
            }
        }

        else if (Vec.y == -1 && StartY > 0)
        {
            for (int i = StartY - 1; i >= 0; --i)
            {
                int StopCount = 0;
                //xy고정된 상태로 z쭉 탐색하면서 모두 조건에 부합하는 칸들인지 확인함.
                for (int k = 0; k < objectPool[i][StartX].Count; ++k)
                {
                    if (objectPool[i][StartX][k].TypeArray[(int)Whatis_Type.Push])
                    {
                        //밀리는애 나오면 어쩌피 다음칸이 빈칸인지 확인해야해서 break;
                        break;
                    }

                    //빈칸이거나 겹침가능한 칸이면 Count+1
                    if (objectPool[i][StartX][k] == null || !objectPool[i][StartX][k].TypeArray[(int)Whatis_Type.Stop])
                        StopCount++;

                    //그 전에 겹침이 불가능한 칸이 먼저 나오면 이동 정지.
                    else if (!objectPool[i][StartX][k].TypeArray[(int)Whatis_Type.Stop] || !objectPool[i][StartX][k].TypeArray[(int)Whatis_Type.Push])
                        return false;
                }

                //(z에 있는 모든 칸이 겹침가능한 칸 || 빈칸) 이동시작.
                if (StopCount == objectPool[i][StartX].Count || objectPool[i][StartX].Count == 0)
                {
                    //뒤에서부터 당김. 앞에서 부터 밀면 밀려나는애를 임시저장해야하기 때문.
                    //결국 여까지 왔다는건 이 칸은 모두 빈칸or겹침가능 칸이기 때문에. 중요하지 않음.
                    //그 전칸 기준으로 어떤게 당겨지고 어떤게 남아있을지를 판별하고 이 칸으로 흡수해줘야함.                  
                    for (int j = i; j < StartY; ++j)
                    {
                        for (int k = 0; k < objectPool[j + 1][StartX].Count; ++k) //전 칸을 반복하며 그 칸에 어떤게 있는지 확인.
                        {
                            //밀었을때 밀리는애거나 / You(미는애)일때 당겨줌.
                            if (objectPool[j + 1][StartX][k].TypeArray[(int)Whatis_Type.Push] 
                                || objectPool[j + 1][StartX][k].TypeArray[(int)Whatis_Type.You] 
                                || objectPool[j + 1][StartX][k].TypeArray[(int)Whatis_Type.Move])
                            {
                                objectPool[j + 1][StartX][k].MoveCommand = new Vector3(0, -1, 0);
                                objectPool[j][StartX].Add(objectPool[j + 1][StartX][k]);
                                objectPool[j + 1][StartX].RemoveAt(k);
                            }
                        }
                    }
                    break;
                }
            }
        }

        for(int i = 0; i < objectPool[StartY+(int)Vec.y][StartX + (int)Vec.x].Count; ++i)
        {
            if(objectPool[StartY + (int)Vec.y][StartX + (int)Vec.x][i].TypeArray[(int)Whatis_Type.Win])
            {
                GameMgr.Instance.WinTrigger();
            }
        }

        return true;
    }
    //void OnEnable()
    //{
    //    // 델리게이트 체인 추가
    //    SceneManager.sceneLoaded += OnSceneLoaded;
    //}

    //private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    objectPool.Clear();
    //    Awake();
    //}

    //void OnDisable()
    //{
    //    // 델리게이트 체인 제거
    //    SceneManager.sceneLoaded -= OnSceneLoaded;
    //}
}