using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Whatis_Baba MyType = Whatis_Baba.End; //�� Ÿ��
    public bool[] TypeArray = new bool[(int)Whatis_Type.End]; //Ÿ�� onoff ���� �迭.

    //�̵���
    public Vector3 MoveCommand = new Vector3(0, 0, 0); //1234 �������� ǥ��. 0�϶��� �ֹ�����.
    protected bool IsMove = false; //�����̰� �ִ°�?
    private float MoveTimeCount = 0.0f;
    private Vector3 StartTrans = Vector3.zero;

    private void Awake()
    {
        MoveMgr.Instance.objectPool[(int)transform.position.y / 24][(int)transform.position.x / 24].Add(this);
        StartTrans = transform.position;

        ObjectMgr.Instance.objectPool.Add(this);
    }

    public void Update()
    {
        if (MoveCommand != new Vector3(0, 0, 0))
        {
            MoveTimeCount += Time.deltaTime;
            transform.position += MoveCommand * 24 * Time.deltaTime;

            if (GetComponent<BoxCollider2D>() != null)
                GetComponent<BoxCollider2D>().size = new Vector2 (1,1);
            if (GetComponent<CircleCollider2D>() != null)
                GetComponent<CircleCollider2D>().radius = 1;

            if (MoveTimeCount >= 0.1f)
            {
                MoveTimeCount = 0;
                transform.position = StartTrans + (MoveCommand * 24);
                IsMove = false;
                StartTrans = transform.position;
                MoveCommand = new Vector3(0, 0, 0);

                if (GetComponent<BoxCollider2D>() != null)
                    GetComponent<BoxCollider2D>().size = new Vector2(24, 24);
                if (GetComponent<CircleCollider2D>() != null)
                    GetComponent<CircleCollider2D>().radius = 13;


            }
        }
    }
    private void OnDestroy()
    {
        MoveMgr.Instance.objectPool[(int)transform.position.y / 24][(int)transform.position.x / 24] = null;
        ObjectMgr.Instance.objectPool.Remove(this);
    }
}
