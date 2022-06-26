using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameMgr : Singleton<GameMgr>
{
    public string NowSceneName = "";
    public string NextSceneName = "";
    public bool WinCheck = false;
    private float Timer = 0;
    public GameObject Wintext;

    void Update()
    {
        //R버튼 = 리스타트
        if(Input.GetKey("r"))
        {
            SceneManager.LoadScene(NowSceneName);
        }

        if (WinCheck)
        {
            Timer += Time.deltaTime;
            if(Timer >= 3)
            {
                MoveMgr.Instance.Awake();
                ObjectMgr.Instance.Awake();
                SceneManager.LoadScene(NextSceneName);
                Timer = 0;
                WinCheck = false;

                switch (NextSceneName)
                {
                    case "1-2Scene":
                        NowSceneName = NextSceneName;
                        NextSceneName = "1-3Scene";
                        break;
                    case "1-3Scene":
                        NowSceneName = NextSceneName;
                        NextSceneName = "MenuScene";
                        break;
                }
            }
        }
    }
    public void WinTrigger()
    {
        Wintext.GetComponent<TextMeshProUGUI>().enabled = true;
        GetComponent<AudioSource>().Play();
        WinCheck = true;
    }
}
