using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMgr : Singleton<ObjectMgr>
{
    public List<Tile> objectPool = new List<Tile>();
    public List<KeyValuePair<Whatis_Baba, Whatis_Type>> RuleList = new();
    public List<KeyValuePair<Whatis_Baba, GameObject>> HasRuleList = new();

    public void Awake()
    {
        base.Awake();
        objectPool.Clear();
        RuleList.Clear();
        HasRuleList.Clear();
    }

    public void NewRule(KeyValuePair<Whatis_Baba, Whatis_Type> Pair)
    {
        RuleList.Add(Pair);
        for (int i = 0; i < objectPool.Count; ++i)
        {
            Baba Obj = objectPool[i].GetComponent<Baba>();
            if(Obj != null)
            {
                if(Obj.MyType == Pair.Key)
                {
                    Obj.TypeArray[(int)Pair.Value] = true;
                }
            }
        }
    }
    public void DeleteRule(KeyValuePair<Whatis_Baba, Whatis_Type> Pair)
    {
        RuleList.Remove(Pair);
        for (int i = 0; i < objectPool.Count; ++i)
        {
            Baba Obj = objectPool[i].GetComponent<Baba>();
            if (Obj != null)
            {
                if (Obj.MyType == Pair.Key)
                {
                    Obj.TypeArray[(int)Pair.Value] = false;
                }
            }
        }
    }
    public void NewHasRule(KeyValuePair<Whatis_Baba, GameObject> Pair)
    {
        HasRuleList.Add(Pair);
    }
    public void DeleteHasRule(KeyValuePair<Whatis_Baba, GameObject> Pair)
    {
        HasRuleList.Remove(Pair);
    }


}
