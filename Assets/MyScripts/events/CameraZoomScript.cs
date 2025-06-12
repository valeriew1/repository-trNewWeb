using UnityEngine;

public class CameraZoomScript : MonoBehaviour
{

    [Header("Zoom Settings")]
    [SerializeField] private float zoomSize = 5f;          // Желаемый размер камеры (для ортографической)
    [SerializeField] private float zoomDuration = 2f;      // Длительность эффекта в секундах
    [SerializeField] private Transform zoomTarget;         // Объект, к которому приближаемся (игрок/шарик)
    //[SerializeField] private float timeSlowdownFactor = 0.2f; // Замедление времени (0.2 = 20% скорости)

    private Camera cam;                // Ссылка на компонент камеры
    private float originalSize;        // Исходный размер камеры
    private Vector3 originalPosition;  // Исходная позиция камеры
    private bool shouldZoom = false;   // Флаг активации эффекта
    private float zoomTimer = 0f;      // Таймер прогресса
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


