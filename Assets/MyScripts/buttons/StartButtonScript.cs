using System;
using UnityEngine;
using UnityEngine.UI;
public class StartButtonScript : MonoBehaviour
{
    //скрипт дл€ кнопки start:
    //запускает процесс падени€ м€чика (включает гравитацию)
    //либо позже изменить на: открывает opener (тогда можно не трогать гравитацию м€чика)


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
            // ƒл€ 2D:
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

// ћетод дл€ кнопки Start (вызываетс€ через OnClick в инспекторе)
//public void OnTrigger()
//{
//    if (ballRb != null)
//    {
//        ballRb.gravityScale = GravityScale; // ¬ключаем гравитацию
//        //Debug.Log("√равитаци€ активирована!");

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
