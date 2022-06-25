using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Whatis_Type
{
    Stop, //겹치는가?
    You, //넌가?
    Win, //겹치면 이기나?
    Defeat,
    Push, //밀리나?
    Hot,
    Melt,
    Sink,
    Shut, //닫힘
    Open, //열림 (둘이 겹치면 사라짐)

    End
}

public class Text_You : Tile
{
    public Whatis_Type Type;

    void Update()
    {
        base.Update();
    }
}
