using System;
using UnityEngine;
using UnityEngine.UI;
public class StartButtonScript : MonoBehaviour
{
    //������ ��� ������ start:
    //��������� ������� ������� ������ (�������� ����������)
    //���� ����� �������� ��: ��������� opener (����� ����� �� ������� ���������� ������)


    public Button startButt;

    GameObject ball;
    //public GameObject Opener;
    public float GravityScale = 1f; 
    private Rigidbody2D ballRb;
    void Start()
    {
        //startButt = Button.FindObjectOfType(GameObject);
        ball = GameObject.FindGameObjectWithTag("Player");
        startButt.onClick.AddListener(OnStartButtonClick);

        if (ball != null)
        {
            // ��� 2D:
            ballRb = ball.GetComponent<Rigidbody2D>();

            if (ballRb != null)
            {
                ballRb.gravityScale = 0f; 
            }
        }
        

        //playerActivator = GetComponent< Rigidbody2D)>;
    }

    void OnStartButtonClick() 
    {
        if (ballRb != null)
        {
            ballRb.gravityScale = GravityScale; 
        }

    }

    
}

// ����� ��� ������ Start (���������� ����� OnClick � ����������)
//public void OnTrigger()
//{
//    if (ballRb != null)
//    {
//        ballRb.gravityScale = GravityScale; // �������� ����������
//        //Debug.Log("���������� ������������!");

//        //opener.OpenerMove();

//    }
//    //else
//    //{
//    //    //Debug.LogError("Rigidbody �� ������ �� ������� Ball!");
//    //}
//}

//}
//// Start is called once before the first execution of Update after the MonoBehaviour is created
//void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }
//}
