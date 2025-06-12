using System;
using UnityEngine;
using UnityEngine.UI; //нужно для работы addlistener

public class StartButtonScript : MonoBehaviour
{
    //скрипт для кнопки start:
    //запускает процесс падения мячика (включает гравитацию)
    //либо позже изменить на: открывает opener (тогда можно не трогать гравитацию мячика)


    public Button startButt;

    GameObject ball;
    //public GameObject Opener;
    public float GravityScale = 1f; 


    //OpenerOpening opener = new OpenerOpening();

    private Rigidbody2D ballRb; // Для 2D

    void Start()
    {
        //startButt = Button.FindObjectOfType(GameObject);
        ball = GameObject.FindGameObjectWithTag("Player");
        startButt.onClick.AddListener(OnStartButtonClick);

        if (ball != null)
        {
            // Для 2D:
            ballRb = ball.GetComponent<Rigidbody2D>();

            if (ballRb != null)
            {
                ballRb.gravityScale = 0f; // Выключаем гравитацию при старте
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
            ballRb.gravityScale = GravityScale; // Включаем гравитацию
            //Debug.Log("Гравитация активирована!");

            //opener.OpenerMove();


        }

    }

    
}

// Метод для кнопки Start (вызывается через OnClick в инспекторе)
//public void OnTrigger()
//{
//    if (ballRb != null)
//    {
//        ballRb.gravityScale = GravityScale; // Включаем гравитацию
//        //Debug.Log("Гравитация активирована!");

//        //opener.OpenerMove();

//    }
//    //else
//    //{
//    //    //Debug.LogError("Rigidbody не найден на объекте Ball!");
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
