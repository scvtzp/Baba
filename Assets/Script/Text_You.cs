using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Whatis_Type
{
    Stop, //��ġ�°�?
    You, //���ΰ��ΰ�?
    Win, //You�� ��ġ�� �������� �߻�
    Defeat,//You�� ��ġ�� you ����.
    Push, //�и���?

    Hot, //�߰ſ?
    Melt, //�����? (�߰ſ�Ŷ� ��ġ�� �߰ſ�͸� �����)

    Move, //You�� �̵� �� �ٶ󺸴� �������� �ڵ� �̵�.

    Shut, //����
    Open, //���� (���� ��ġ�� �����)

    End
}

public class Text_You : Tile
{
    public Whatis_Type Type;

    void Update()
    {
        base.Update();
    }
}
