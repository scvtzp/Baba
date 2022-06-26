using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text_Is : Tile
{
    //�����¿� �ܾ�� ����
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
        //1. �����¿� ��� ���Դ��� Ȯ��
        if (collision.GetComponent<Text_You>() != null || collision.GetComponent<Text_Baba>() != null)
        {
            //���׿��� baba, you �Ѵ� ���ü� ����.
            if (collision.transform.position.x > transform.position.x)
            {
                Right = collision.gameObject;
            }
            else if (collision.transform.position.y < transform.position.y)
            {
                Down = collision.gameObject;
            }

            //���׿��� baba�� ���ü� ����.
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

        //2. �¿�or���� �� ���ִٸ� ��Ģ�� �߰�.
        if (Left != null && Right != null)
        {
            //����Ǽ� 2�� ����
            //1. baba -> baba (wall -> rock)
            if (Right.GetComponent<Text_Baba>() != null)
            {
                //�׳� ���� �ִ¾ֵ� �ִϸ��̼�+you���� ���� �ʱ�ȭ �ϸ� ��.
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
                //�׳� ���� �ִ¾ֵ� �ִϸ��̼�+you���� ���� �ʱ�ȭ �ϸ� ��.
            }
            else if (Down.GetComponent<Text_You>() != null)
            {
                ObjectMgr.Instance.NewRule(new KeyValuePair<Whatis_Baba, Whatis_Type>(Up.GetComponent<Text_Baba>().Type, Down.GetComponent<Text_You>().Type));
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //x�� ���̰� ������ -> ��, y���� ���̰� �ִ� -> up down �߿� ����.
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
