using System;
using UnityEngine;
using UnityEngine.UI; //нужно для работы addlistener

public class StartButtonScript : MonoBehaviour
{
    public Button startButt;
    GameObject ball;
    public float GravityScale = 1f; 
    private Rigidbody2D ballRb;

    void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Player");
        startButt.onClick.AddListener(OnStartButtonClick);

        if (ball != null)
        {
            ballRb = ball.GetComponent<Rigidbody2D>();
            if (ballRb != null)
            {
                ballRb.gravityScale = 0f; // ��������� ���������� ��� ������
                //ball.SetActive(false);
                
            }
        }
    }

    void OnStartButtonClick() 
    {
        if (ballRb != null)
        {
            //ball.SetActive(true);
            ballRb.gravityScale = GravityScale; // �������� ����������
            //Debug.Log("���������� ������������!");

            //opener.OpenerMove();


        }
    }    
}
