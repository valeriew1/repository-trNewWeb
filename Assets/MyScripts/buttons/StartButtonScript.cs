using System;
using UnityEngine;
using UnityEngine.UI; //����� ��� ������ addlistener

public class StartButtonScript : MonoBehaviour
{
    //������ ��� ������ start:
    //��������� ������� ������� ������ (�������� ����������)
    //���� ����� �������� ��: ��������� opener (����� ����� �� ������� ���������� ������)


    public Button startButt;

    GameObject ball;
    //public GameObject Opener;
    public float GravityScale = 1f; 


    //OpenerOpening opener = new OpenerOpening();

    private Rigidbody2D ballRb; // ��� 2D

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
                ballRb.gravityScale = 0f; // ��������� ���������� ��� ������
                //ball.SetActive(false);
                
            }
        }
        

        //playerActivator = GetComponent< Rigidbody2D)>;
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
