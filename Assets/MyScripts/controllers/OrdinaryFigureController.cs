

using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


////let's move some staff!)
////при нажатии на фигуру позволяет переместить ее из поля дополнительных фигур в игровое поле
////двигается клавиатурой, но нельзя вращать!



public class OrdinaryFigureController : MonoBehaviour
{
    [SerializeField] private GameObject th;
    //private Renderer rend;

    [SerializeField] private LayerMask selectableLayer;

    [SerializeField] private bool isMoving = false;
    [SerializeField] private bool smoothMovement = true;
    [SerializeField] private bool keepFullObjectInView = true;

    private Vector3 offset;
    private Vector3 originalPosition;

    private float dragSpeed = 5.0f;
    private Bounds objectBounds;

    //private bool isMouseDragging = false;

    private Color selectedColor = Color.darkGreen;
    private Material originMaterial;
    private Color originColor;

    //public UnityEngine.Events.UnityEvent onClickEvent;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //rend = th.GetComponent<Renderer>();

        originalPosition = transform.position;

        // Получаем границы объекта 
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

    
    void Update()
    {


        if (isMoving)
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = transform.position.z;
            originalPosition = mouseWorldPos + offset;

            // Ограничиваем позицию в пределах видимости камеры
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

    //public void ResetPosition() 
    //{
    //    isMoving = false;
    //    transform.position = originalPosition;
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
            // ограничиваем позицию центра объекта
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




//void ApplyConstraints(ref Vector3 position)
//{
//    //    if (lockX) position.x = th.transform.position.x; // Фиксация X
//    //    if (lockY) position.y = th.transform.position.y; // Фиксация Y
//    //    position.z = th.transform.position.z; // Сохраняем Z

//    //    if (useMovementBounds) // Ограничение зоны перемещения
//    //    {
//    //        position.x = Mathf.Clamp(position.x, movementBounds.xMin, movementBounds.xMax);
//    //        position.y = Mathf.Clamp(position.y, movementBounds.yMin, movementBounds.yMax);
//    //    }
//}


//void ApplyConstraints(ref Vector3 position)
//{
//    //    if (lockX) position.x = th.transform.position.x; // Фиксация X
//    //    if (lockY) position.y = th.transform.position.y; // Фиксация Y
//    //    position.z = th.transform.position.z; // Сохраняем Z

//    //    if (useMovementBounds) // Ограничение зоны перемещения
//    //    {
//    //        position.x = Mathf.Clamp(position.x, movementBounds.xMin, movementBounds.xMax);
//    //        position.y = Mathf.Clamp(position.y, movementBounds.yMin, movementBounds.yMax);
//    //    }
//}


//using UnityEngine;


////let's move some staff!)
////при нажатии на фигуру позволяет переместить ее из поля дополнительных фигур в игровое поле
////двигается клавиатурой, но нельзя вращать!


//public class OrdinaryFigureController : MonoBehaviour
//{
//    public float moveSpeed = 5f;
//    public float rotationSpeed = 120f;

//    public GameObject figure;
//    private GameObject selectedfig;
//    //private Rigidbody2D rbfig;
//    //private BoxCollider2D bcfig;

//    //private Color selectedColor = Color.darkGreen;
//    //SpriteRenderer spriteRenderer;

//    private Vector2 originalPosition;


//    private void Start()
//    {
//        //rbfig = this.figure.GetComponent<Rigidbody2D>();
//        //bcfig = this.figure.GetComponent<BoxCollider2D>();
//        //spriteRenderer = figure.GetComponent<SpriteRenderer>();
//        originalPosition = figure.transform.position;

//        //selectedfig = rbfig.GetComponent<GameObject>();
//    }

//    private void Update()
//    {

//    }


//    //private void OnMouseDown()
//    //{
//    //    if (figure != null)
//    //    {
//    //        rbfig = figure.GetComponent<Rigidbody2D>();
//    //        selectedfig = rbfig.GetComponent<GameObject>();
//    //        selectedfig = bcfig.GetComponent<GameObject>();
//    //        //spriteRenderer.color = selectedColor;
//    //        FigFixedUpdate();
//    //    }
//    //}

//    private void FigFixedUpdate()
//    {
//        float moveVertical = Input.GetAxis("Vertical");
//        Vector2 moveVert = transform.forward * moveVertical * moveSpeed * Time.deltaTime;
//        selectedfig.transform.position = moveVert;
//        rbfig.MovePosition(rbfig.position + moveVert);



//    }

//}










//    //public GameObject selectedObject;
//    ////public GameObject[] selectableObjects;

//    //////private Rigidbody rb;
//    //private Vector3 movement;
//    //private int currentSelectionIndex = 0;
//    ////public GameObject selectedObject;
//    //private KeyCode selectionKey = KeyCode.Tab;

//    //void Start()
//    //{
//    //    //rb = GetComponent<Rigidbody>();

//    //    spriteRenderer = selectedObject.GetComponent<SpriteRenderer>();

//    //    // Инициализация выбранного объекта
//    //    if (selectableObjects.Length > 0)
//    //    {
//    //        SelectObject(0);
//    //    }
//    //}

//    //private void Update()
//    //{
//    //    // Выбор объекта
//    //    if (Input.GetKeyDown(selectionKey))
//    //    {
//    //        CycleSelection();
//    //    }

//    //    // Перемещение выбранного объекта
//    //    if (selectedObject != null)
//    //    {
//    //        HandleMovementInput();
//    //        MoveSelectedObject();
//    //    }
//    //}

//    //private void CycleSelection()
//    //{
//    //    if (selectableObjects.Length == 0) return;

//    //    currentSelectionIndex = (currentSelectionIndex + 1) % selectableObjects.Length;
//    //    SelectObject(currentSelectionIndex);
//    //}


//    //private void SelectObject(int index)
//    //    {
//    //        // Снимаем выделение с предыдущего объекта (если нужно)
//    //        if (selectedObject != null)
//    //        {
//    //        // Здесь можно добавить код для визуального отличия выбранного объекта
//    //        // Например, изменить цвет материала или включить/выключить подсветку
//    //        //spriteRenderer = selectedObject.GetComponent<SpriteRenderer>();
//    //        spriteRenderer.color = selectedColor;
//    //        }

//    //        // Выбираем новый объект
//    //        selectedObject = selectableObjects[index];

//    //        // Здесь можно добавить код для визуального выделения нового объекта
//    //        Debug.Log($"Selected object: {selectedObject.name}");
//    //    }


//    //private void HandleMovementInput()
//    //{
//    //    // Сбрасываем вектор движения
//    //    movement = Vector3.zero;

//    //    // Получаем ввод с клавиатуры
//    //    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
//    //    {
//    //        movement.z = 1f; // Вперёд
//    //    }
//    //    if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
//    //    {
//    //        movement.z = -1f; // Назад
//    //    }
//    //    if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
//    //    {
//    //        movement.x = -1f; // Влево
//    //    }
//    //    if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
//    //    {
//    //        movement.x = 1f; // Вправо
//    //    }

//    //    // Нормализуем вектор, чтобы диагональное движение не было быстрее
//    //    if (movement.magnitude > 0)
//    //    {
//    //        movement.Normalize();
//    //    }
//    //}
//    //private void MoveSelectedObject()
//    //{
//    //    // Перемещаем объект с учётом скорости и времени кадра
//    //    selectedObject.transform.Translate(movement * moveSpeed * Time.deltaTime);
//    //}




//    //// Update is called once per frame
//    //void Update()
//    //{
//    //    //Move on vertical input
//    //    float moveVertical = Input.GetAxis("Vertical");
//    //    Vector3 movementVer = transform.forward * moveVertical * moveSpeed * Time.fixedDeltaTime;
//    //    selectedObject.transform.Translate(movement * moveSpeed * Time.deltaTime);
//    //    //rb.MovePosition(rb.position + movementVer);
//    //    // Move on horizontal input.
//    //    float moveHorizontal = Input.GetAxis("Horizontal") * moveSpeed * Time.fixedDeltaTime;
//    //    Vector3 movementHor = transform.forward * moveHorizontal * moveSpeed * Time.fixedDeltaTime;
//    //    //rb.MovePosition(rb.position + movementHor);


//    //}

//    //private void OnEnable()
//    //{
//    //    ////Move on vertical input
//    //    //float moveVertical = Input.GetAxis("Vertical");
//    //    //Vector3 movementVer = transform.forward * moveVertical * moveSpeed * Time.fixedDeltaTime;
//    //    //rb.MovePosition(rb.position + movementVer);
//    //    //// Move on horizontal input.
//    //    //float moveHorizontal = Input.GetAxis("Horizontal") * moveSpeed * Time.fixedDeltaTime;
//    //    //Vector3 movementHor = transform.forward * moveHorizontal * moveSpeed * Time.fixedDeltaTime;
//    //    //rb.MovePosition(rb.position + movementHor);


//    //    //float turn = Input.GetAxis("Horizontal") * rotationSpeed * Time.fixedDeltaTime;
//    //    //Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
//    //    //rb.MoveRotation(rb.rotation * turnRotation);
//    //}





