using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class OrdinaryFigureController : MonoBehaviour
{
    [SerializeField] private GameObject th;
    [SerializeField] private LayerMask selectableLayer;
    [SerializeField] private bool isMoving = false;
    [SerializeField] private bool smoothMovement = true;
    [SerializeField] private bool keepFullObjectInView = true;
    private Vector3 offset;
    private Vector3 originalPosition;
    private float dragSpeed = 5.0f;
    private Bounds objectBounds;
    private Color selectedColor = Color.darkGreen;
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
            }
        }
        else
        {
            Collider2D collider = GetComponent<Collider2D>();
            if (collider != null)
            {
                objectBounds = collider.bounds;
            }
            else
            {
                objectBounds = new Bounds(transform.position, Vector3.zero);
            }
        }
    }    
    void Update()
    {
        if (isMoving)
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = transform.position.z;
            originalPosition = mouseWorldPos + offset;
            originalPosition = ClampPositionToCameraView(originalPosition);
            if (smoothMovement)
                transform.position = Vector3.Lerp(transform.position, originalPosition, dragSpeed * Time.deltaTime);
            else
                transform.position = originalPosition;
        }
    }
    void OnMouseDown()
    {
        isMoving = true;
        HighlightObject(th, true);
    }
    private void OnMouseUp()
    {
        isMoving = false;
        HighlightObject(th, false);
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
            renderer.material.color = selectedColor;
        }
        else if (highlight == false)
        {
            renderer.material = originMaterial;
            renderer.material.color = originColor;
        }
    }
}