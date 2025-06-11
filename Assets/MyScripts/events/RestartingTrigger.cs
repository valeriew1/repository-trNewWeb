using UnityEngine;

public class RestartingTrigger : MonoBehaviour
{
    //���� ����� ������� ���� �� ����, �� �������� finish �����,
    //������� ��������������� �� ����� �������� (���������� � ������ - dead-zone)

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

        //���������� ��������� �������
        Transform ballSTARTLOC_transform = ballSTARTLOC.transform; //�������� 
        worldPosSTARTBall = ballSTARTLOC_transform.position; //�������� ���������� ����������
        //transform.position = worldPosSTARTBall; - �� �����, ���� �����

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (ball != null && other.CompareTag("Player") )
        {
            ball.transform.position = worldPosSTARTBall; //�����������
            rbBall.gravityScale = 0f;
            rbBall.mass = 0f;
            rbBall.linearVelocity = Vector2.zero; //����������, ����� ������ �� ����� �� �����

        }
        
    }
}
