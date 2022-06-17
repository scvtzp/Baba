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
        // ����Ƽ���� �����ϴ� 11���� ����
        if (Input.GetKeyDown("r"))
            sr.material.color = Color.red; // ������
        if (Input.GetKeyDown("g"))
            sr.material.color = Color.green; // ���
        if (Input.GetKeyDown("b"))
            sr.material.color = Color.blue; // �Ķ���

        if (Input.GetKeyDown("m"))
            sr.material.color = Color.magenta; // ��ȫ��
        if (Input.GetKeyDown("y"))
            sr.material.color = Color.yellow; // �����
        if (Input.GetKeyDown("c"))
            sr.material.color = Color.cyan; // û�ϻ�

        if (Input.GetKeyDown("w"))
            sr.material.color = Color.white; // ȭ��Ʈ
        if (Input.GetKeyDown("k"))
            sr.material.color = Color.black; // ������
        if (Input.GetKeyDown("e"))
            sr.material.color = Color.gray; // ȸ��
        if (Input.GetKeyDown("u"))
            sr.material.color = Color.clear; // ����


        // ���ϴ� ���� RGB + A(����) ���� ����
        if (Input.GetKeyDown("i"))
            sr.material.color = new Color(0.3f, 0.4f, 0.7f); // 0���� 1 ���̰� (Red, Green, Blue)����

        if (Input.GetKeyDown("o"))
            sr.material.color = new Color(0.3f, 0.4f, 0.7f, 0.2f); // 0���� 1 ���̰� (Red, Green, Blue, Alpha)����

        if (Input.GetKeyDown("p"))
            sr.material.color = new Color(90 / 255f, 142 / 255f, 72 / 255f); // 0���� 255 ���̰� (Red, Green, Blue)����
    }
}