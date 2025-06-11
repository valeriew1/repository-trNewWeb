using UnityEngine;

public class RestartingTrigger : MonoBehaviour
{
    //если мячик улетает куда не надо, не достигая finish точки,
    //уровень перезапускается по этому триггеру (прикреплен к стенам - dead-zone)

    GameObject ball;
    GameObject ballSTARTLOC;
    private Vector2 worldPosSTARTBall;
    private Rigidbody2D rbBall;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        ball = GameObject.FindGameObjectWithTag("Player");
        ballSTARTLOC = GameObject.Find("PlayerStartPoint");
        rbBall = ball.GetComponent<Rigidbody2D>();

        //координаты начальной позиции
        Transform ballSTARTLOC_transform = ballSTARTLOC.transform; //кэшируем 
        worldPosSTARTBall = ballSTARTLOC_transform.position; //получили глобальные координаты
        //transform.position = worldPosSTARTBall; - не здесь, ниже будет

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (ball != null && other.CompareTag("Player") )
        {
            ball.transform.position = worldPosSTARTBall; //переместили
            rbBall.gravityScale = 0f;
            rbBall.mass = 0f;
            rbBall.linearVelocity = Vector2.zero; //остановили, чтобы дальше не летал по кругу

        }
        
    }
}
