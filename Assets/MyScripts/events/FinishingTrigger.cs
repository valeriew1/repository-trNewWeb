using UnityEngine;


public class FinishingTrigger : MonoBehaviour
{
    //когда мяч касается триггерной зоны (finish)
    //уровень закончен - появляется надпись "level succeed"
    //камера приближается к объекту
    //появляются какие-то эффекты

    [SerializeField] private CameraZoomScript cameraZoom;

    GameObject ball;
    private Rigidbody2D rbBall;
    //GameObject canvas1Obj;
    //GameObject canvas2Obj;
    private bool hasTriggered;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Player");
        rbBall = ball.GetComponent<Rigidbody2D>();
        //canvas1Obj = GameObject.Find("Canvas");
        //canvas2Obj = GameObject.Find("WINCanvas");

        //if (canvas1Obj != null && canvas2Obj != null) 
        //{ 
        //    Canvas canvas1 = canvas1Obj.GetComponent<Canvas>();
        //    Canvas canvas2 = canvas2Obj.GetComponent<Canvas>();
        //}

        //Canvas[] allCanvases = Resources.FindObjectsOfTypeAll<Canvas>();

        //foreach (Canvas canvas in allCanvases)
        //{
        //    if (canvas.name == "CanvasLevel")
        //    {
        //        //Debug.Log("Найден Canvas: " + canvas.name);
        //        canvas.gameObject.SetActive(false); 
        //        //break;
        //    }
        //    if (canvas.name == "WINCanvas")
        //    {
        //        //Debug.Log("Найден Canvas: " + canvas.name);
        //        canvas.gameObject.SetActive(true);
        //        //break;
        //    }
        //}
    }



    // Update is called once per frame
    void Update()
    {
        
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
                    //Debug.Log("Найден Canvas: " + canvas.name);
                    canvas.gameObject.SetActive(false);
                    //break;
                }
                if (canvas.name == "WINCanvas")
                {
                    //Debug.Log("Найден Canvas: " + canvas.name);
                    canvas.gameObject.SetActive(true);
                    //break;
                }
            }
            //canvas1.SetActive(false);
            //canvas2.SetActive(true);
            cameraZoom.StartCameraZoomMethod();
        }
    }

}
