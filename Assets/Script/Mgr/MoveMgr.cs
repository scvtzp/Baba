using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MoveMgr : Singleton<MoveMgr>
{
    public List<List<List<Tile>>> objectPool = new List<List<List<Tile>>>();
    protected AudioSource audioSource; //�̵� ȿ����

    public void Awake()
    {
        base.Awake();

        audioSource = GetComponent<AudioSource>();

        //�ε����� ��ǥ�� Ȱ���Ұű� ������ 24*18 ũ���� null�� �̸� Ǯ ä��. 
        for (int i = 0; i < 22; ++i)
        {
            objectPool.Add(new List<List<Tile>>());
            for (int j = 0; j < 26; ++j)
            {
                objectPool[i].Add(new List<Tile>());
            }
        }
    }

    public bool Move(int StartX, int StartY, Vector3 Vec) //�̵� ��� ��ġx ,y , ����
    {
        if (audioSource != null)        {
            audioSource.Play();
        }


        StartX /= 24;
        StartY /= 24;

        //���������� ����. xy��ø �Ű� �Ƚ��ִ� ������ �׷����� ���� ����.
        if (Vec.x == 1)
        {
            if (StartX >= 24)
                return false;

            for (int i = StartX + 1; i < 26; ++i)
            {
                int StopCount = 0;
                if (i == 25)
                    return false;

                //xy������ ���·� z�� Ž���ϸ鼭 ��� ���ǿ� �����ϴ� ĭ������ Ȯ����.
                for (int k = 0; k < objectPool[StartY][i].Count; ++k)
                {

                    if (objectPool[StartY][i][k].TypeArray[(int)Whatis_Type.Push])
                    {
                        //�и��¾� ������ ��¼�� ����ĭ�� ��ĭ���� Ȯ���ؾ��ؼ� break;
                        break;
                    }

                    //��ĭ�̰ų� ��ħ������ ĭ�̸� Count+1
                    if (objectPool[StartY][i][k] == null || !objectPool[StartY][i][k].TypeArray[(int)Whatis_Type.Stop])
                        StopCount++;

                    //�� ���� ��ħ�� �Ұ����� ĭ�� ���� ������ �̵� ����.
                    else if (!objectPool[StartY][i][k].TypeArray[(int)Whatis_Type.Stop] || !objectPool[StartY][i][k].TypeArray[(int)Whatis_Type.Push])
                        return false;
                }

                //(z�� �ִ� ��� ĭ�� ��ħ������ ĭ || ��ĭ) �̵�����.
                if (StopCount == objectPool[StartY][i].Count || objectPool[StartY][i].Count == 0)
                {
                    //�ڿ������� ���. �տ��� ���� �и� �з����¾ָ� �ӽ������ؾ��ϱ� ����.
                    //�ᱹ ������ �Դٴ°� �� ĭ�� ��� ��ĭor��ħ���� ĭ�̱� ������. �߿����� ����.
                    //�� ��ĭ �������� ��� ������� ��� ������������ �Ǻ��ϰ� �� ĭ���� ����������.                  
                    for (int j = i; j > StartX; --j)
                    {
                        for (int k = 0; k < objectPool[StartY][j-1].Count; ++k) //�� ĭ�� �ݺ��ϸ� �� ĭ�� ��� �ִ��� Ȯ��.
                        {
                            //�о����� �и��¾ְų� / You(�̴¾�)�϶� �����.
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
                        //�и��¾� ������ ��¼�� ����ĭ�� ��ĭ���� Ȯ���ؾ��ؼ� break;
                        break;
                    }
                    if (objectPool[StartY][i][k] == null || !objectPool[StartY][i][k].TypeArray[(int)Whatis_Type.Stop])
                        StopCount++;

                    else if (!objectPool[StartY][i][k].TypeArray[(int)Whatis_Type.Stop] || !objectPool[StartY][i][k].TypeArray[(int)Whatis_Type.Push])
                        return false;

                }

                //(z�� �ִ� ��� ĭ�� ��ħ������ ĭ || ��ĭ) �̵�����.
                if (StopCount == objectPool[StartY][i].Count || objectPool[StartY][i].Count == 0)
                {
                    for (int j = i; j < StartX; ++j)
                    {
                        for (int k = 0; k < objectPool[StartY][j + 1].Count; ++k) //�� ĭ�� �ݺ��ϸ� �� ĭ�� ��� �ִ��� Ȯ��.
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
                //xy������ ���·� z�� Ž���ϸ鼭 ��� ���ǿ� �����ϴ� ĭ������ Ȯ����.
                for (int k = 0; k < objectPool[i][StartX].Count; ++k)
                {
                    if (objectPool[i][StartX][k].TypeArray[(int)Whatis_Type.Push])
                    {
                        //�и��¾� ������ ��¼�� ����ĭ�� ��ĭ���� Ȯ���ؾ��ؼ� break;
                        break;
                    }
                    //��ĭ�̰ų� ��ħ������ ĭ�̸� Count+1
                    if (objectPool[i][StartX][k] == null || !objectPool[i][StartX][k].TypeArray[(int)Whatis_Type.Stop])
                        StopCount++;

                    //�� ���� ��ħ�� �Ұ����� ĭ�� ���� ������ �̵� ����.
                    else if (!objectPool[i][StartX][k].TypeArray[(int)Whatis_Type.Stop] || !objectPool[i][StartX][k].TypeArray[(int)Whatis_Type.Push])
                        return false;
                }

                //(z�� �ִ� ��� ĭ�� ��ħ������ ĭ || ��ĭ) �̵�����.
                if (StopCount == objectPool[i][StartX].Count || objectPool[i][StartX].Count == 0)
                {
                    //�ڿ������� ���. �տ��� ���� �и� �з����¾ָ� �ӽ������ؾ��ϱ� ����.
                    //�ᱹ ������ �Դٴ°� �� ĭ�� ��� ��ĭor��ħ���� ĭ�̱� ������. �߿����� ����.
                    //�� ��ĭ �������� ��� ������� ��� ������������ �Ǻ��ϰ� �� ĭ���� ����������.                  
                    for (int j = i; j > StartY; --j)
                    {
                        for (int k = 0; k < objectPool[j - 1][StartX].Count; ++k) //�� ĭ�� �ݺ��ϸ� �� ĭ�� ��� �ִ��� Ȯ��.
                        {
                            //�о����� �и��¾ְų� / You(�̴¾�)�϶� �����.
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
                //xy������ ���·� z�� Ž���ϸ鼭 ��� ���ǿ� �����ϴ� ĭ������ Ȯ����.
                for (int k = 0; k < objectPool[i][StartX].Count; ++k)
                {
                    if (objectPool[i][StartX][k].TypeArray[(int)Whatis_Type.Push])
                    {
                        //�и��¾� ������ ��¼�� ����ĭ�� ��ĭ���� Ȯ���ؾ��ؼ� break;
                        break;
                    }

                    //��ĭ�̰ų� ��ħ������ ĭ�̸� Count+1
                    if (objectPool[i][StartX][k] == null || !objectPool[i][StartX][k].TypeArray[(int)Whatis_Type.Stop])
                        StopCount++;

                    //�� ���� ��ħ�� �Ұ����� ĭ�� ���� ������ �̵� ����.
                    else if (!objectPool[i][StartX][k].TypeArray[(int)Whatis_Type.Stop] || !objectPool[i][StartX][k].TypeArray[(int)Whatis_Type.Push])
                        return false;
                }

                //(z�� �ִ� ��� ĭ�� ��ħ������ ĭ || ��ĭ) �̵�����.
                if (StopCount == objectPool[i][StartX].Count || objectPool[i][StartX].Count == 0)
                {
                    //�ڿ������� ���. �տ��� ���� �и� �з����¾ָ� �ӽ������ؾ��ϱ� ����.
                    //�ᱹ ������ �Դٴ°� �� ĭ�� ��� ��ĭor��ħ���� ĭ�̱� ������. �߿����� ����.
                    //�� ��ĭ �������� ��� ������� ��� ������������ �Ǻ��ϰ� �� ĭ���� ����������.                  
                    for (int j = i; j < StartY; ++j)
                    {
                        for (int k = 0; k < objectPool[j + 1][StartX].Count; ++k) //�� ĭ�� �ݺ��ϸ� �� ĭ�� ��� �ִ��� Ȯ��.
                        {
                            //�о����� �и��¾ְų� / You(�̴¾�)�϶� �����.
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
    //    // ��������Ʈ ü�� �߰�
    //    SceneManager.sceneLoaded += OnSceneLoaded;
    //}

    //private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    objectPool.Clear();
    //    Awake();
    //}

    //void OnDisable()
    //{
    //    // ��������Ʈ ü�� ����
    //    SceneManager.sceneLoaded -= OnSceneLoaded;
    //}
}