using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baba_Animation : MonoBehaviour
{
    private Animator Animator;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!GetComponent<Tile>().TypeArray[(int)Whatis_Type.You])
            return;
        if (Animator.GetInteger("Count_UDLR") == 0 && Input.GetKey("w"))
            Animator.SetInteger("Count_UDLR", 1);
        else if (Input.GetKeyUp("w"))
            Animator.SetInteger("Count_UDLR", 0);

        if (Animator.GetInteger("Count_UDLR") == 0 && Input.GetKey("s"))
            Animator.SetInteger("Count_UDLR", 2);
        else if (Input.GetKeyUp("s"))
            Animator.SetInteger("Count_UDLR", 0);

        if (Animator.GetInteger("Count_UDLR") == 0 && Input.GetKey("a"))
            Animator.SetInteger("Count_UDLR", 3);
        else if (Input.GetKeyUp("a"))
            Animator.SetInteger("Count_UDLR", 0);

        if (Animator.GetInteger("Count_UDLR") == 0 && Input.GetKey("d"))
            Animator.SetInteger("Count_UDLR", 4);
        else if (Input.GetKeyUp("d"))
            Animator.SetInteger("Count_UDLR", 0);

    }
}
