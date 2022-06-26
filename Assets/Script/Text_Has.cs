using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text_Has : MonoBehaviour
{
    //상하좌우 단어들 저장
    public GameObject Left;
    public GameObject Right;
    public GameObject Up;
    public GameObject Down;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //1. 상하좌우 어디에 들어왔는지 확인
        if (collision.GetComponent<Text_Baba>() != null)
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
            else if (collision.transform.position.x < transform.position.x)
            {
                Left = collision.gameObject;
            }
            else if (collision.transform.position.y > transform.position.y)
            {
                Up = collision.gameObject;
            }
        }

        //2. 좌우or상하 다 차있다면 규칙에 추가.
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
