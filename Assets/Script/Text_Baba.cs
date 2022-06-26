using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Whatis_Baba
{
    Baba,
    Rock,
    Flag,
    Skull,
    Jelly,
    Key,
    Door,
    
    Tile,
    Wall,
    Grass,
    Water,
    Lava,
    Ice,

    Text,
    Empty,
    All,
    Box,
    End
}

public class Text_Baba : Tile
{
    public Whatis_Baba Type;
    public GameObject ForHasObj;

    //결국 필요한건 너가 누군질 알아야함.
    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
}
