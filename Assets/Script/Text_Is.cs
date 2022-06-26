using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text_Is : Tile
{
    //상하좌우 단어들 저장
    public GameObject Left;
    public GameObject Right;
    public GameObject Up;
    public GameObject Down;

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //1. 상하좌우 어디에 들어왔는지 확인
        if (collision.GetComponent<Text_You>() != null || collision.GetComponent<Text_Baba>() != null)
        {
            //우항에는 baba, you 둘다 들어올수 있음.
            if (collision.transform.position.x > transform.position.x)
            {
                Right = collision.gameObject;
            }
            else if (collision.transform.position.y < transform.position.y)
            {
                Down = collision.gameObject;
            }

            //좌항에는 baba만 들어올수 있음.
            else if (collision.GetComponent<Text_Baba>() != null)
            {
                if (collision.transform.position.x < transform.position.x)
                {
                    Left = collision.gameObject;
                }
                else if (collision.transform.position.y > transform.position.y)
                {
                    Up = collision.gameObject;
                }
            }
        }

        //2. 좌우or상하 다 차있다면 규칙에 추가.
        if (Left != null && Right != null)
        {
            //경우의수 2개 구분
            //1. baba -> baba (wall -> rock)
            if (Right.GetComponent<Text_Baba>() != null)
            {
                //그냥 지금 있는애들 애니메이션+you내부 설정 초기화 하면 됨.
            }
            //2. baba -> you (wall is stop)
            else if (Right.GetComponent<Text_You>() != null)
            {
                ObjectMgr.Instance.NewRule(new KeyValuePair<Whatis_Baba, Whatis_Type>(Left.GetComponent<Text_Baba>().Type, Right.GetComponent<Text_You>().Type));
            }
        }
        if (Up != null && Down != null)
        {
            if (Down.GetComponent<Text_Baba>() != null)
            {
                //그냥 지금 있는애들 애니메이션+you내부 설정 초기화 하면 됨.
            }
            else if (Down.GetComponent<Text_You>() != null)
            {
                ObjectMgr.Instance.NewRule(new KeyValuePair<Whatis_Baba, Whatis_Type>(Up.GetComponent<Text_Baba>().Type, Down.GetComponent<Text_You>().Type));
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //x에 차이가 없을때 -> 즉, y값에 차이가 있다 -> up down 중에 수정.
        if (collision.transform.position.x - transform.position.x > -1 
            && collision.transform.position.x - transform.position.x < 1)
        {
            if(Up!= null && Down != null && Down.GetComponent<Text_You>() != null)
                ObjectMgr.Instance.DeleteRule(new KeyValuePair<Whatis_Baba, Whatis_Type>(Up.GetComponent<Text_Baba>().Type, Down.GetComponent<Text_You>().Type));

            if (collision.transform.position.y > transform.position.y)
                Up = null;
            else
                Down = null;
        }

        else if (collision.transform.position.y - transform.position.y > -1
                && collision.transform.position.y - transform.position.y < 1)

        {
            if (Left != null && Right != null && Right.GetComponent<Text_You>() != null)
                ObjectMgr.Instance.DeleteRule(new KeyValuePair<Whatis_Baba, Whatis_Type>(Left.GetComponent<Text_Baba>().Type, Right.GetComponent<Text_You>().Type));

            if (collision.transform.position.x > transform.position.x)
                Right = null;
            else
                Left = null;
        }

    }

}
