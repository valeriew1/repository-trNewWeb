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
    void Start()
    {
        cam = GetComponent<Camera>();
        originalSize = cam.orthographicSize;
        originalPosition = transform.position;
    }
    void Update()
    {
        if (shouldZoom == true && zoomTimer < zoomDuration)
        {
            zoomTimer += Time.fixedDeltaTime;
            progress = Mathf.Clamp01(zoomTimer / zoomDuration);
            cam.orthographicSize = Mathf.Lerp(originalSize, zoomSize, progress);
            targetPos = new Vector3(
                zoomTarget.position.x,
                zoomTarget.position.y,
                originalPosition.z
            );
            transform.position = Vector3.Lerp(originalPosition, targetPos, progress);
        }
    }
    public void StartCameraZoomMethod() 
    {
        if (zoomTarget == null)
        {
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