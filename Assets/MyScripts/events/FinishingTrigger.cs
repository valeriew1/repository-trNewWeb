using UnityEngine;
public class FinishingTrigger : MonoBehaviour
{
    [SerializeField] private CameraZoomScript cameraZoom;
    GameObject ball;
    private Rigidbody2D rbBall;
    private bool hasTriggered;
    void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Player");
        rbBall = ball.GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {       
        Canvas[] allCanvases = Resources.FindObjectsOfTypeAll<Canvas>();
        if (ball != null && other.CompareTag("Player")&& hasTriggered == false)
        {
            hasTriggered = true;
            foreach (Canvas canvas in allCanvases)
            {
                if (canvas.name == "CanvasLevel")
                {
                    canvas.gameObject.SetActive(false);
                }
                if (canvas.name == "WINCanvas")
                {
                    canvas.gameObject.SetActive(true); 
                }
            }
            cameraZoom.StartCameraZoomMethod();
        }
    }
}