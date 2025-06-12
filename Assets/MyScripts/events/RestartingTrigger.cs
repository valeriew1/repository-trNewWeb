using UnityEngine;

public class RestartingTrigger : MonoBehaviour
{
    GameObject ball;
    GameObject ballSTARTLOC;
    private Vector2 worldPosSTARTBall;
    private Rigidbody2D rbBall;
    void Start()
    {

        ball = GameObject.FindGameObjectWithTag("Player");
        ballSTARTLOC = GameObject.Find("PlayerStartPoint");
        rbBall = ball.GetComponent<Rigidbody2D>();

        //êîîðäèíàòû íà÷àëüíîé ïîçèöèè
        Transform ballSTARTLOC_transform = ballSTARTLOC.transform; //êýøèðóåì 
        worldPosSTARTBall = ballSTARTLOC_transform.position; //ïîëó÷èëè ãëîáàëüíûå êîîðäèíàòû
        //transform.position = worldPosSTARTBall; - íå çäåñü, íèæå áóäåò

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (ball != null && other.CompareTag("Player") )
        {
            ball.transform.position = worldPosSTARTBall; //ïåðåìåñòèëè
            rbBall.gravityScale = 0f;
            rbBall.mass = 0f;
            rbBall.linearVelocity = Vector2.zero; //îñòàíîâèëè, ÷òîáû äàëüøå íå ëåòàë ïî êðóãó

        }
        
    }
}
