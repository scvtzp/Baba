using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Whatis_Baba MyType = Whatis_Baba.End; //내 타입
    public bool[] TypeArray = new bool[(int)Whatis_Type.End]; //타입 onoff 관리 배열.

    //이동용
    public Vector3 MoveCommand = new Vector3(0, 0, 0); //1234 동서남북 표시. 0일때는 주문없음.
    protected bool IsMove = false; //움직이고 있는가?
    private float MoveTimeCount = 0.0f;
    private Vector3 StartTrans = Vector3.zero;

    private void Awake()
    {
        for(int i = 0; i < ObjectMgr.Instance.RuleList.Count; ++i)
        {
            if(ObjectMgr.Instance.RuleList[i].Key == MyType)
            {
                TypeArray[(int)ObjectMgr.Instance.RuleList[i].Value] = true;
            }
        }

        MoveMgr.Instance.objectPool[(int)transform.position.y / 24][(int)transform.position.x / 24].Add(this);
        StartTrans = transform.position;

        ObjectMgr.Instance.objectPool.Add(this);
    }

    public void Update()
    {
        if (MoveCommand != new Vector3(0, 0, 0))
        {
            if(GetComponent<Text_Baba>() != null || GetComponent<Text_You>() != null || GetComponent<Text_Is>() != null)
            {
                if (GetComponent<BoxCollider2D>() != null)
                    GetComponent<BoxCollider2D>().size = new Vector2 (1,1);
                if (GetComponent<CircleCollider2D>() != null)
                    GetComponent<CircleCollider2D>().radius = 1;
            }

            MoveTimeCount += Time.deltaTime;
            transform.position += MoveCommand * 24 * Time.deltaTime;


            if (MoveTimeCount >= 0.03f)
            {
                MoveTimeCount = 0;
                transform.position = StartTrans + (MoveCommand * 24);
                IsMove = false;
                StartTrans = transform.position;
                MoveCommand = new Vector3(0, 0, 0);

                if (GetComponent<Text_Baba>() != null || GetComponent<Text_You>() != null || GetComponent<Text_Is>() != null)
                {
                    if (GetComponent<BoxCollider2D>() != null)
                        GetComponent<BoxCollider2D>().size = new Vector2(24, 24);
                    if (GetComponent<CircleCollider2D>() != null)
                        GetComponent<CircleCollider2D>().radius = 13;
                }
            }
        }
    }
    private void OnDestroy()
    {
        for(int i = 0; i < MoveMgr.Instance.objectPool[(int)transform.position.y / 24][(int)transform.position.x / 24].Count; ++i)
        {
            if(MoveMgr.Instance.objectPool[(int)transform.position.y / 24][(int)transform.position.x / 24][i] == this)
            {
                MoveMgr.Instance.objectPool[(int)transform.position.y / 24][(int)transform.position.x / 24].RemoveAt(i);
            }
        }

        for(int i = 0; i < ObjectMgr.Instance.HasRuleList.Count; ++i)
        {
            if(ObjectMgr.Instance.HasRuleList[i].Key == MyType)
            {
                Instantiate(ObjectMgr.Instance.HasRuleList[i].Value, GetComponent<Transform>().position, Quaternion.Euler(0,0,0));
            }
        }

        //MoveMgr.Instance.objectPool[(int)transform.position.y / 24][(int)transform.position.x / 24] = null;
        ObjectMgr.Instance.objectPool.Remove(this);
    }
}
