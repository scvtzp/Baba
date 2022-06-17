using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMgr : Singleton<GameMgr>
{
    public string NextSceneName = "";
    public bool WinCheck = false;
    private float Timer = 0;

    void Update()
    {
        if(NextSceneName != "")
        {
            SceneManager.LoadScene(NextSceneName);
        }
        if(WinCheck)
        {
            Timer += Time.deltaTime;
            if(Timer >= 3)
            {
                NextSceneName = "WorldmapScene";
            }
        }
    }
    public void WinTrigger()
    {
        WinCheck = true;
    }
}
