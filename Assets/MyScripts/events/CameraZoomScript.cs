using UnityEngine;

public class CameraZoomScript : MonoBehaviour
{

    [Header("Zoom Settings")]
    [SerializeField] private float zoomSize = 5f;
    [SerializeField] private float zoomDuration = 2f;
    [SerializeField] private Transform zoomTarget;
    private Camera cam;
    private float originalSize;
    private Vector3 originalPosition;
    private bool shouldZoom = false;
    private float zoomTimer = 0f;
    private Vector3 targetPos;
    private float progress;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = GetComponent<Camera>();
        originalSize = cam.orthographicSize;
        originalPosition = transform.position;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (shouldZoom == true && zoomTimer < zoomDuration)
        {
            // Обновляем таймер
            /*zoomTimer += Time.unscaledDeltaTime; */// Используем unscaled, если замедлено время
            zoomTimer += Time.fixedDeltaTime;
            progress = Mathf.Clamp01(zoomTimer / zoomDuration);

            // Плавное изменение размера камеры
            cam.orthographicSize = Mathf.Lerp(originalSize, zoomSize, progress);

            // Плавное перемещение к цели (сохраняем Z-координату)
            targetPos = new Vector3(
                zoomTarget.position.x,
                zoomTarget.position.y,
                originalPosition.z
            );
            transform.position = Vector3.Lerp(originalPosition, targetPos, progress);

            // Замедление времени для драматизма
            //Time.timeScale = Mathf.Lerp(1f, timeSlowdownFactor, progress);
        }
    }



    public void StartCameraZoomMethod() 
    {
        if (zoomTarget == null)
        {
            //Debug.LogError("Zoom target not assigned!");
            return;
        }
        shouldZoom = true;
        zoomTimer = 0f;
    }

    public void ResetCamera()
    {
        shouldZoom = false;
        cam.orthographicSize = originalSize;
        transform.position = originalPosition;
        Time.timeScale = 1f;
    }

}


