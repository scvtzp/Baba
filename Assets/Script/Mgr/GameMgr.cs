using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameMgr : Singleton<GameMgr>
{
    public string NextSceneName = "";
    public bool WinCheck = false;
    private float Timer = 0;
    public GameObject Wintext;

    void Update()
    {
        if (WinCheck)
        {
            Timer += Time.deltaTime;
            if(Timer >= 3)
            {
                //SceneManager.LoadScene("WorldmapScene");
                SceneManager.LoadScene(NextSceneName);
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
