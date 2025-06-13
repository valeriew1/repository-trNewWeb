using System;
using UnityEngine;
using UnityEngine.UI;
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
                ballRb.gravityScale = 0f;
            }
        }
    }
    void OnStartButtonClick() 
    {
        if (ballRb != null)
        {
            ballRb.gravityScale = GravityScale;
        }
    }    
}
