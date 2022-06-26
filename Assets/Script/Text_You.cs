using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Whatis_Type
{
    Stop, //겹치는가?
    You, //주인공인가?
    Win, //You와 겹치면 종료조건 발생
    Defeat,
    Push, //밀리나?
    Hot, //뜨거운가?
    Melt, //차가운가? (뜨거운거랑 겹치면 뜨거운것만 사라짐)
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
