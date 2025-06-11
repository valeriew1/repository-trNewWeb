using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

//[RequireComponent(typeof(Camera))]


//при нажатии на add можно выбрать объект и тогда он переносится в центральную зону

public class AddButtonScript : MonoBehaviour
{

    [SerializeField] private GameObject[] AddObjects;
    [SerializeField] private GameObject centralObject;
    [SerializeField] private Button AddButt;

    private GameObject chosenObj;
    private int currentAddIndex = 0;

    private bool activator = false;



    //private KeyCode selectionKey = KeyCode.Tab;
    //private KeyCode addingKey = KeyCode.KeypadEnter;

   

    private Color selectedColor = Color.darkGreen;
    private Material originMaterial;
    private Color originColor;


    private void Start()
    {
        AddButt.onClick.AddListener(OnAddButtonClick);
    }

    private void Update()
    {
        if (activator == true)
        {
            if (AddObjects.Length > 0)
            {
                AddObject();
                
                activator = false;
            }
        }
    }

    private void AddObject() 
    {
        for (int i = 0; i < AddObjects.Length; i++)
        {
            currentAddIndex = i;
            chosenObj = AddObjects[currentAddIndex];
            
            if (chosenObj != null)
            {
                Vector3 vector3 = new Vector3(i-2, i-2, 0);
                chosenObj.transform.position = vector3;
                
            }

            if (currentAddIndex == AddObjects.Length - 1)
            {
                currentAddIndex = 0;
                break;
            }
        }
    }

    private void OnAddButtonClick()
    {
        activator = true;
        
    }

}


//private void CycleSelection() //цикл выбора
//{
//    if (AddObjects.Length == 0) return;

//    currentAddIndex = (currentAddIndex + 1) % AddObjects.Length;


//    //ChosingObj(currentAddIndex);
//}

//private void HighlightObject(GameObject obj, bool highlight)
//{
//    Renderer renderer = obj.GetComponent<Renderer>();
//    if (renderer == null) return;

//    if (highlight == true)
//    {
//        originMaterial = renderer.material;
//        originColor = renderer.material.color;
//        renderer.material.color = selectedColor;
//    }
//    else if (highlight == false)
//    {
//        renderer.material = originMaterial;
//        renderer.material.color = originColor;
//    }
//}


//chosenObj = AddObjects[currentAddIndex];

//if (Keyboard.current.tabKey.isPressed)
//{

//    CycleSelection();
//    if (currentAddIndex - 1 >= 0) HighlightObject(AddObjects[currentAddIndex - 1], false);

//}
////if (Input.GetKeyDown(addingKey))
//if (Keyboard.current.enterKey.isPressed)
//{
//    AddObject();
//}




//private void AddObject() //3.перемещение
//{
//    if (activator == true)
//    {
//        //chosenObj = AddObjects[currentAddIndex];
//        chosenObj.transform.position = centralObject.transform.position;
//        activator = false;
//    }
//    //}
//    //chosenObj.transform.position = centralObject.transform.position;
//    //activator = false;
//}

//private void OnAddButtonClick() //1.нажатие кнопки
//{
//    activator = true;

//    //chosenObj.transform.position = centralObject.transform.position;
//}



//private void ChosingObj(int Index) //2.выбор(переключение) объекта по табу
//{

//    if (activator == true)
//    {
//        //chosenObj = AddObjects[currentAddIndex];
//        HighlightObject(chosenObj, true);
//    }
//    //else if (chosenObj != AddObjects[currentAddIndex]) HighlightObject(chosenObj, false);



//}





//private Vector3 offset;
//private int? fingerId = null;
//private bool isMouseDragging = false;

//[Header("Настройки управления")]
//public bool enableMouseControl = true; // Включение управления мышью
//public bool enableTouchControl = true; // Включение управления тачпадом/пальцами
//public LayerMask selectableLayer;

//[Header("Ограничения")]
//public bool lockX = false;
//public bool lockY = false;
////public bool allowScaling = true;
////public float minScale = 0.5f;
////public float maxScale = 2f;
//public bool useMovementBounds = false; // Ограничение зоны перемещения
//public Rect movementBounds;            // Границы перемещения (если useMovementBounds=true)

//private float initialDistance;
//private Vector2 initialScale;
//private bool isMultiSelectMode = false;
//private KeyCode multiSelectKey = KeyCode.LeftShift;


//SpriteRenderer spriteRenderer;





//if (Input.GetKeyDown(multiSelectKey)) isMultiSelectMode = true;
//if (Input.GetKeyUp(multiSelectKey)) isMultiSelectMode = false;

//if (Input.GetMouseButtonDown(0)) { chooseObject(); }
//if (enableTouchControl && Input.touchCount > 0) { ProcessTouchInput(); } // Обработка тачпада/пальцев
//else if (enableMouseControl)
//{
//    //ProcessMouseInput(); // Обработка мыши
//}

//if (Input.GetKeyDown(selectionKey))




//void ProcessTouchInput()
//{
//    foreach (Touch touch in Input.touches) //перебор касаний
//    {
//        Vector2 touchPosition = MainCamera.ScreenToWorldPoint(touch.position);

//        switch (touch.phase)
//        {
//            case TouchPhase.Began:
//                if (fingerId == null) //палец не обрабатывается
//                {
//                    RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero, Mathf.Infinity, selectableLayer);

//                    if (hit.collider != null)  // Если коснулись объекта
//                    {


//                        chosenObj = hit.collider.gameObject;
//                        offset = chosenObj.transform.position - (Vector3)touchPosition;
//                        fingerId = touch.fingerId; //запоминаем айди пальца
//                        initialDistance = 0f;
//                        initialScale = chosenObj.transform.localScale;
//                    }
//                }
//                break;

//            case TouchPhase.Moved:
//                if (fingerId == touch.fingerId && chosenObj != null)
//                {
//                    Vector3 newPosition = touchPosition + (Vector2)offset;
//                    //ApplyConstraints(ref newPosition);
//                    chosenObj.transform.position = newPosition;
//                }
//                break;

//            case TouchPhase.Ended:
//            case TouchPhase.Canceled:
//                if (fingerId == touch.fingerId)
//                {
//                    fingerId = null;
//                    chosenObj = null;
//                }
//                break;
//        }
//    }

//}

//void ProcessMouseInput()
//{
//    Vector2 mousePosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);

//    if (Input.GetMouseButtonDown(0)) // Нажатие ЛКМ
//    {
//        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, selectableLayer);

//        if (hit.collider != null)
//        {
//            chosenObj = hit.collider.gameObject;
//            offset = chosenObj.transform.position - (Vector3)mousePosition;
//            isMouseDragging = true;
//        }
//    }

//    if (Input.GetMouseButtonUp(0)) // Отпускание ЛКМ
//    {
//        isMouseDragging = false;
//        chosenObj = null;
//    }

//    if (isMouseDragging && chosenObj != null) // Перетаскивание
//    {
//        Vector3 newPosition = mousePosition + (Vector2)offset;
//        ApplyConstraints(ref newPosition);
//        chosenObj.transform.position = newPosition;
//    }


//}

//void ApplyConstraints(ref Vector3 position)
//{
//    if (lockX) position.x = chosenObj.transform.position.x; // Фиксация X
//    if (lockY) position.y = chosenObj.transform.position.y; // Фиксация Y
//    position.z = chosenObj.transform.position.z; // Сохраняем Z

//    if (useMovementBounds) // Ограничение зоны перемещения
//    {
//        position.x = Mathf.Clamp(position.x, movementBounds.xMin, movementBounds.xMax);
//        position.y = Mathf.Clamp(position.y, movementBounds.yMin, movementBounds.yMax);
//    }
//}



//private void chooseObject() 
//{
//    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//    RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

//    if (hit.collider != null)
//    {
//        selectedObject = hit.collider.gameObject;
//        offset = selectedObject.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);

//        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//        //RaycastHit2D hit2D;
//        //if (hit2D = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, selectableLayer))
//        //{
//        //    HandleSelection(hit2D.collider.gameObject);
//        //}
//        //else if (!isMultiSelectMode) ResetSelection();

//    }

//private void HandleSelection(GameObject newSelection) 
//{
//    //if (!isMultiSelectMode && chosenObj != null)
//    if (chosenObj != null)
//    {
//        ResetSelection();
//    }
//    chosenObj = newSelection;
//    HighlightObject(chosenObj, true);
//}

//private void HighlightObject(GameObject obj, bool highlight)
//{
//    Renderer renderer = obj.GetComponent<Renderer>();
//    if (renderer == null) return;

//    if (highlight == true)
//    {
//        originMaterial = renderer.material;
//        renderer.material.color = selectedColor;
//    }
//    else
//    {
//        renderer.material = originMaterial;
//    }
//}

//private void ResetSelection()
//{
//    if (chosenObj != null)
//    {
//        HighlightObject(chosenObj, false);
//        chosenObj = null;
//    }
//}




//private void OnMouseDown()
//{
//    //Collider2D chObj = chosenObj.GetComponent<Collider2D>();
//    //if (chObj != null)
//    //{
//    //    chosenObj.SetActive(true);
//    //    //spriteRenderer.color = selectedColor;
//    //}

//}


