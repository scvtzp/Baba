using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_Animation : MonoBehaviour
{
    private Animator Animator;
    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Vector3[] Vec = new[] { new Vector3(0, 1, 0), new Vector3(0, -1, 0), new Vector3(-1, 0, 0), new Vector3(1, 0, 0) };
        bool[] UDLR = new bool[] {false, false , false , false }; 
        for(int i = 0; i <4; ++i)
        {
            if(MoveMgr.Instance.objectPool[(int)(transform.position.y / 24 + Vec[i].y)][(int)(transform.position.x / 24 + Vec[i].x)].Count != 0
                && MoveMgr.Instance.objectPool[(int)(transform.position.y/24 + Vec[i].y)][(int)(transform.position.x/24 + Vec[i].x)] != null 
                && MoveMgr.Instance.objectPool[(int)(transform.position.y/24 + Vec[i].y)][(int)(transform.position.x/24 + Vec[i].x)][0].MyType == Whatis_Baba.Wall)
            {
                UDLR[i] = true;
            }
        }

        if (!UDLR[0] && !UDLR[1] && !UDLR[2] && !UDLR[3])
        {
            return;
        }
        string AniName = "Wall_";
        if (UDLR[0])
            AniName += "U";
        if (UDLR[1])
            AniName += "D";
        if (UDLR[2])
            AniName += "L";
        if (UDLR[3])
            AniName += "R";

        Animator.Play(AniName);


        //if (UDLR[0] && UDLR[1] && UDLR[2] && UDLR[3])
        //{
        //    ;
        //}
        //else if (UDLR[0] && UDLR[2] && UDLR[3])
        //    Animator.Play("Wall_ULR");
        //else if (UDLR[1] && UDLR[2] && UDLR[3])
        //    Animator.Play("Wall_DLR");

        //else if (UDLR[1] && UDLR[2])
        //    Animator.Play("Wall_DL");
        //else if (UDLR[1] && UDLR[3])
        //    Animator.Play("Wall_DR");

        //else if (UDLR[0] && UDLR[2])
        //    Animator.Play("Wall_UL");
        //else if (UDLR[0] && UDLR[3])
        //    Animator.Play("Wall_UR");

        //else if (UDLR[0] && UDLR[1])
        //    Animator.Play("Wall_UD");
        //else if (UDLR[2] && UDLR[3])
        //    Animator.Play("Wall_LR");
        //else
        //{
        //    //³ÀµÐ´Ù = ¾Ë¾Æ¼­ wall_single·Î °¨;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
