using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Baba : Tile
{
    // Update is called once per frame
    private float MoveCoolTime = 48572393;
    public float Setting_MoveCoolTime = 0.2f;

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");


        MoveCoolTime += Time.deltaTime;
       
        //�տ��� �ش� ������Ʈ�� you �Ӽ��� ������ �ִٸ� ����.
        if (TypeArray[(int)Whatis_Type.You] && MoveCommand == new Vector3(0, 0, 0) && (x != 0 || y != 0) && MoveCoolTime >= Setting_MoveCoolTime)
        {
            MoveMgr.Instance.Move((int)transform.position.x, (int)transform.position.y, new Vector3(x, y, 0));
            MoveCoolTime = 0;
        }

        //�ݱ�/���Ⱑ �Ѹ��̸� �ڵ� �ڻ�.
        if (TypeArray[(int)Whatis_Type.Open] && TypeArray[(int)Whatis_Type.Shut])
        {
            Destroy(this.gameObject);
        }


        base.Update();
    }
}
