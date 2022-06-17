using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeColor : MonoBehaviour
{
    SpriteRenderer sr;
    public GameObject go;

    void Start()
    {
        sr = go.GetComponent<SpriteRenderer>();
    } 

    void Update()
    {
        // 유니티에서 제공하는 11가지 색상값
        if (Input.GetKeyDown("r"))
            sr.material.color = Color.red; // 빨간색
        if (Input.GetKeyDown("g"))
            sr.material.color = Color.green; // 녹색
        if (Input.GetKeyDown("b"))
            sr.material.color = Color.blue; // 파란색

        if (Input.GetKeyDown("m"))
            sr.material.color = Color.magenta; // 자홍색
        if (Input.GetKeyDown("y"))
            sr.material.color = Color.yellow; // 노란색
        if (Input.GetKeyDown("c"))
            sr.material.color = Color.cyan; // 청록색

        if (Input.GetKeyDown("w"))
            sr.material.color = Color.white; // 화이트
        if (Input.GetKeyDown("k"))
            sr.material.color = Color.black; // 검정색
        if (Input.GetKeyDown("e"))
            sr.material.color = Color.gray; // 회색
        if (Input.GetKeyDown("u"))
            sr.material.color = Color.clear; // 투명


        // 원하는 색상 RGB + A(투명도) 값을 지정
        if (Input.GetKeyDown("i"))
            sr.material.color = new Color(0.3f, 0.4f, 0.7f); // 0에서 1 사이값 (Red, Green, Blue)설정

        if (Input.GetKeyDown("o"))
            sr.material.color = new Color(0.3f, 0.4f, 0.7f, 0.2f); // 0에서 1 사이값 (Red, Green, Blue, Alpha)설정

        if (Input.GetKeyDown("p"))
            sr.material.color = new Color(90 / 255f, 142 / 255f, 72 / 255f); // 0에서 255 사이값 (Red, Green, Blue)설정
    }
}