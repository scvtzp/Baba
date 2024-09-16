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
            MoveMgr.Instance.Awake();
            ObjectMgr.Instance.Awake();

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

                Wintext.GetComponent<TextMeshProUGUI>().enabled = false;

                switch (NextSceneName)
                {
                    case "1-2Scene":
                        NowSceneName = NextSceneName;
                        NextSceneName = "1-3Scene";
                        break;
                    case "1-3Scene":
                        NowSceneName = NextSceneName;
                        NextSceneName = "1-4Scene";
                        break;
                    case "1-4Scene":
                        NowSceneName = NextSceneName;
                        NextSceneName = "MenuScene";
                        break;
                    case "MenuScene":
                        NowSceneName = NextSceneName;
                        NextSceneName = "1-1Scene";
                        break;
                    case "1-1Scene":
                        NowSceneName = NextSceneName;
                        NextSceneName = "1-2Scene";
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
