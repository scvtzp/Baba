using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Baba : Tile
{
    // Update is called once per frame
    private float MoveCoolTime = 48572393;
    public float Setting_MoveCoolTime = 0.2f;
    public Vector3 UDLR = new Vector3(1,0,0);

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        MoveCoolTime += Time.deltaTime;
       
        //�տ��� �ش� ������Ʈ�� you �Ӽ��� ������ �ִٸ� ����.
        if (TypeArray[(int)Whatis_Type.You] && MoveCommand == new Vector3(0, 0, 0) 
            && (x != 0 || y != 0) && MoveCoolTime >= Setting_MoveCoolTime)
        {
            MoveMgr.Instance.Move((int)transform.position.x, (int)transform.position.y, new Vector3(x, y, 0));
            MoveCoolTime = 0;

            ////���� ǥ��.
            UDLR = MoveCommand;
        }

        //move �̵� ó������.
        else if (TypeArray[(int)Whatis_Type.Move] && MoveCommand == new Vector3(0, 0, 0)
            && (x != 0 || y != 0) && MoveCoolTime >= Setting_MoveCoolTime)
        {
            if(!MoveMgr.Instance.Move((int)transform.position.x, (int)transform.position.y, UDLR))
            {
                UDLR.x *= -1;
                UDLR.y *= -1;
                MoveMgr.Instance.Move((int)transform.position.x, (int)transform.position.y, UDLR);
            }
            MoveCoolTime = 0;
        }    
        base.Update();



        //�ݱ�/���Ⱑ �Ѹ��̸� �ڵ� �ڻ�.
        if (TypeArray[(int)Whatis_Type.Open] && TypeArray[(int)Whatis_Type.Shut])
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //���� hot�̰� �ʰ� melt�϶� �� ����.
        if (TypeArray[(int)Whatis_Type.Hot] && collision.GetComponent<Baba>().TypeArray[(int)Whatis_Type.Melt])
        {
            Destroy(collision.gameObject);
        }
        //���� defeat�̰� �ʰ� you�϶� �� ����.
        else if (TypeArray[(int)Whatis_Type.Defeat] && collision.GetComponent<Baba>().TypeArray[(int)Whatis_Type.You])
        {
            Destroy(collision.gameObject);
        }
    }
}
