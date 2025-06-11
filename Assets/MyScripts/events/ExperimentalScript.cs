//using System.Security.Cryptography.X509Certificates;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ExperimentalScript : MonoBehaviour
{
    public GameObject th;
    private Renderer rend;
    public bool isMoving = false;
    //private Vector2 TergetPosition;
    private Vector3 offset;
    //private Vector2 mousePosition;

    private Vector3 targetPosition;
    private float dragSpeed = 5.0f;
    private Bounds objectBounds;

    private bool isMouseDragging = false;
    public LayerMask selectableLayer;
    public bool smoothMovement = true;
    public bool keepFullObjectInView = true;

    //[Header("Ограничения")]
    //public bool lockX = false;
    //public bool lockY = false;
    ////public bool allowScaling = true;
    ////public float minScale = 0.5f;
    ////public float maxScale = 2f;
    //public bool useMovementBounds = false; // Ограничение зоны перемещения
    //public Rect movementBounds;            // Границы перемещения (если useMovementBounds=true)
    //public bool enableMouseControl = true;
    
    //private GameObject chosenObj;

    //private Rigidbody2D rb;
    //public static bool queriestHitTriggers;
    //void OnMouseDown()
    //{
    //    offset = th.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //}
    //void OnMouseDrag()
    //{
    //    Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
    //    th.transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
    //}


    //public UnityEngine.Events.UnityEvent onClickEvent;
    //public bool isClickable = true;

    //public GameObject figure;
    //private Collider2D Col2D;
    //SpriteRenderer spriteRenderer;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //rend = th.GetComponent<Renderer>();
       
        targetPosition = transform.position;

        // Получаем границы объекта (учитывая все дочерние элементы)
        CalculateObjectBounds();

        //{
        //rb = th.GetComponent<Rigidbody2D>();
        //    spriteRenderer = figure.GetComponent<SpriteRenderer>();
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
            // Если нет рендереров, используем коллайдер
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
    
    // Update is called once per frame
    void Update()
    {
        //mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //mousePosition = Mouse.current.position.ReadValue();
        ////mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
        ////mousePosition.z = 0;
        //th.transform.position = mousePosition;

        //if (isClickable && Input.GetMouseButtonDown(0))
        //{
        //    onClickEvent.Invoke();

        //if (Input.GetKeyDown(multiSelectKey)) isMultiSelectMode = true;
        //if (Input.GetKeyUp(multiSelectKey)) isMultiSelectMode = false;

        //if (Input.GetMouseButtonDown(0)) { chooseObject(); }
        //if (enableTouchControl && Input.touchCount > 0) { ProcessTouchInput(); } // Обработка тачпада/пальцев
        //if (enableMouseControl)
        //{
        //    ProcessMouseInput(); // Обработка мыши
        //}

        //
        //if (Input.GetMouseButton(0))
        //    OnMouseDown();


        if (isMoving)
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = transform.position.z;
            targetPosition = mouseWorldPos + offset;

            // Ограничиваем позицию в пределах видимости камеры
            targetPosition = ClampPositionToCameraView(targetPosition);
        }

        if (smoothMovement)
            transform.position = Vector3.Lerp(transform.position, targetPosition, dragSpeed * Time.deltaTime);
        else
            transform.position = targetPosition;
    }
    


    //private void OnMouseEnter()
    //{
    //    //figure.GetComponent<SpriteRenderer>().color = Color.yellow;
    //}
    //private void OnMouseExit()
    //{
    //    //figure.GetComponent<SpriteRenderer>().color = spriteRenderer.color;
    //}
    void OnMouseDown()
    {

        isMoving = true;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = transform.position.z;
        offset = transform.position - mouseWorldPos;

        //isMoving = true;
        ////th.GetComponent<SpriteRenderer>().color -= Color.yellow*Time.deltaTime;
        ////rend.material.color -= Color.white * Time.deltaTime;
        //mousePosition = Mouse.current.position.ReadValue();
        //Vector2 stPoint = th.transform.position;
        //if (isMoving) th.transform.position = Vector2.MoveTowards(stPoint, mousePosition, 10f);
        //th.transform.position = mousePosition;
        //mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //if (isClickable && Input.GetMouseButtonDown(0))
        //{
        //    onClickEvent.Invoke();


        //}
    }

    private void OnMouseUp()
    {
        isMoving = false;
    }

    //void ProcessMouseInput()
    //{
    //    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    //    if (Input.GetMouseButtonDown(0)) // Нажатие ЛКМ
    //    {
    //        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, selectableLayer);

    //        if (hit.collider != null)
    //        {
    //            th = hit.collider.gameObject;
    //            offset = th.transform.position - (Vector3)mousePosition;
    //            isMouseDragging = true;
    //        }
    //    }

    //    if (Input.GetMouseButtonUp(0)) // Отпускание ЛКМ
    //    {
    //        isMouseDragging = false;
    //        //th = null;
    //    }

    //    if (isMouseDragging && th != null) // Перетаскивание
    //    {
    //        Vector3 newPosition = mousePosition + (Vector2)offset;
    //        ApplyConstraints(ref newPosition);
    //        th.transform.position = newPosition;
    //    }


    //}

    private Vector3 ClampPositionToCameraView(Vector3 targetPos)
    {
        if (Camera.main == null) return targetPos;

        // Получаем границы видимой области камеры в мировых координатах
        float cameraHeight = Camera.main.orthographicSize * 2;
        float cameraWidth = cameraHeight * Camera.main.aspect;

        Vector3 cameraMin = Camera.main.transform.position - new Vector3(cameraWidth / 2, cameraHeight / 2, 0);
        Vector3 cameraMax = Camera.main.transform.position + new Vector3(cameraWidth / 2, cameraHeight / 2, 0);

        // Если нужно учитывать размеры объекта
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
            // Просто ограничиваем позицию центра объекта
            targetPos.x = Mathf.Clamp(targetPos.x, cameraMin.x, cameraMax.x);
            targetPos.y = Mathf.Clamp(targetPos.y, cameraMin.y, cameraMax.y);
        }

        return targetPos;
    }

    void ApplyConstraints(ref Vector3 position)
    {
    //    if (lockX) position.x = th.transform.position.x; // Фиксация X
    //    if (lockY) position.y = th.transform.position.y; // Фиксация Y
    //    position.z = th.transform.position.z; // Сохраняем Z

    //    if (useMovementBounds) // Ограничение зоны перемещения
    //    {
    //        position.x = Mathf.Clamp(position.x, movementBounds.xMin, movementBounds.xMax);
    //        position.y = Mathf.Clamp(position.y, movementBounds.yMin, movementBounds.yMax);
    //    }
    }




}