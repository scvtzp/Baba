using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text_Has : MonoBehaviour
{
    //�����¿� �ܾ�� ����
    public GameObject Left;
    public GameObject Right;
    public GameObject Up;
    public GameObject Down;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //1. �����¿� ��� ���Դ��� Ȯ��
        if (collision.GetComponent<Text_Baba>() != null)
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
            else if (collision.transform.position.x < transform.position.x)
            {
                Left = collision.gameObject;
            }
            else if (collision.transform.position.y > transform.position.y)
            {
                Up = collision.gameObject;
            }
        }

        //2. �¿�or���� �� ���ִٸ� ��Ģ�� �߰�.
        if (Left != null && Right != null)
        {
            ObjectMgr.Instance.NewHasRule(new KeyValuePair<Whatis_Baba, GameObject>(Left.GetComponent<Text_Baba>().Type, Right.GetComponent<Text_Baba>().ForHasObj));
        }
        if (Up != null && Down != null)
        {
            ObjectMgr.Instance.NewHasRule(new KeyValuePair<Whatis_Baba, GameObject>(Up.GetComponent<Text_Baba>().Type, Down.GetComponent<Text_Baba>().ForHasObj));
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.position.y != transform.position.y)
        {
            if (Up != null && Down != null && Down.GetComponent<Text_You>() != null)
                ObjectMgr.Instance.DeleteHasRule(new KeyValuePair<Whatis_Baba, GameObject>(Up.GetComponent<Text_Baba>().Type, Down.GetComponent<Text_Baba>().ForHasObj));

            if (collision.transform.position.y > transform.position.y)
                Up = null;
            else
                Down = null;
        }
        else if (collision.transform.position.x != transform.position.x)
        {
            if (Left != null && Right != null && Right.GetComponent<Text_You>() != null)
                ObjectMgr.Instance.DeleteHasRule(new KeyValuePair<Whatis_Baba, GameObject>(Left.GetComponent<Text_Baba>().Type, Right.GetComponent<Text_Baba>().ForHasObj));

            if (collision.transform.position.x > transform.position.x)
                Right = null;
            else
                Left = null;
        }

    }
}
