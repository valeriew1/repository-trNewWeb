using UnityEngine;

public class NonOrdinaryFigureController : MonoBehaviour
{
    [SerializeField] private GameObject th;
    [SerializeField] private LayerMask selectableLayer;
    [SerializeField] private bool isMoving = false;
    [SerializeField] private bool smoothMovement = true;
    [SerializeField] private float dragSpeed = 5.0f;
    [SerializeField] private bool isRotating = false;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private bool invertRotation = false;
    [SerializeField] private float minAngle = -45f;
    [SerializeField] private float maxAngle = 45f;
    [SerializeField] private bool keepFullObjectInView = true;
    private bool ShiftPressed;
    private Vector3 center;
    private Vector3 mouseWorldPos;
    private Vector3 originalPosition;
    private Vector3 mouseStartPosition;
    private Bounds objectBounds;
    private Color selectedColor = Color.darkGreen;
    private Color RotationColor = Color.greenYellow; 
    private Material originMaterial;
    private Color originColor;

    void Start()
    {
        originalPosition = transform.position;
        CalculateObjectBounds();
    }
    private void CalculateObjectBounds()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        if (renderers.Length > 0)
        {
            objectBounds = renderers[0].bounds;
            for (int i = 1; i < renderers.Length; i++)
            {
                objectBounds.Encapsulate(renderers[i].bounds);
                center = renderers[i].bounds.center;
            }            
        }
        else
        {
            Collider2D collider = GetComponent<Collider2D>();
            if (collider != null)
            {
                objectBounds = collider.bounds;
                center = collider.bounds.center;
            }
            else
            {
                objectBounds = new Bounds(transform.position, Vector3.zero);
            }
        }
    }
    void Update()
    {
        ShiftPressed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        if (isMoving)
        {
            mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = transform.position.z;
            originalPosition = mouseWorldPos;
            originalPosition = ClampPositionToCameraView(originalPosition);
            if (smoothMovement)
                transform.position = Vector3.Lerp(transform.position, originalPosition, dragSpeed * Time.deltaTime);
            else
                transform.position = originalPosition;
            if (ShiftPressed == true)
            {
                isRotating = true;
                mouseStartPosition = Input.mousePosition;
                Rotate();
            }
            else if (ShiftPressed == false) { isRotating = false; }
        }
    }
    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMoving = true;
            HighlightObject(th, true);
        }
    }
    private void OnMouseUp()
    {
        isMoving = false;
        isRotating = false;
        HighlightObject(th, false);
    }
    private void Rotate() 
    {
        if (isRotating == true)
        {
            Vector3 currentMouseRotationPosition = Input.mousePosition;
            float deltaX = (currentMouseRotationPosition.x - originalPosition.x) * rotationSpeed * Time.deltaTime;
            float rotationAmount = invertRotation? -deltaX : deltaX;
            transform.Rotate(0, 0, rotationAmount / 50);
        }
    }
    private Vector3 ClampPositionToCameraView(Vector3 targetPos)
    {
        if (Camera.main == null) return targetPos;
        float cameraHeight = Camera.main.orthographicSize * 2;
        float cameraWidth = cameraHeight * Camera.main.aspect;
        Vector3 cameraMin = Camera.main.transform.position - new Vector3(cameraWidth / 2, cameraHeight / 2, 0);
        Vector3 cameraMax = Camera.main.transform.position + new Vector3(cameraWidth / 2, cameraHeight / 2, 0);
        if (keepFullObjectInView)
        {
            float objectWidth = objectBounds.size.x / 2;
            float objectHeight = objectBounds.size.y / 2;

            targetPos.x = Mathf.Clamp(targetPos.x,
                                    cameraMin.x + objectWidth,
                                    cameraMax.x - objectWidth);
            targetPos.y = Mathf.Clamp(targetPos.y,
                                    cameraMin.y + objectHeight,
                                    cameraMax.y - objectHeight);
        }
        else
        {
            targetPos.x = Mathf.Clamp(targetPos.x, cameraMin.x, cameraMax.x);
            targetPos.y = Mathf.Clamp(targetPos.y, cameraMin.y, cameraMax.y);
        }
        return targetPos;
    }
    private void HighlightObject(GameObject obj, bool highlight)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer == null) return;
        if (highlight == true)
        {
            originMaterial = renderer.material;
            originColor = renderer.material.color;
            if (ShiftPressed == false) renderer.material.color = selectedColor;
            else if (ShiftPressed == true) renderer.material.color = RotationColor;
        }
        else if (highlight == false)
        {
            renderer.material = originMaterial;
            renderer.material.color = originColor;
        }
    }
}
