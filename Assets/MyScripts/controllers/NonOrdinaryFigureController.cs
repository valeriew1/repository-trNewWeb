using UnityEngine;

public class NonOrdinaryFigureController : MonoBehaviour
{
    //скрипт для балки-вращалки

    [SerializeField] private GameObject th;

    [SerializeField] private LayerMask selectableLayer;


    [SerializeField] private bool isMoving = false;
    [SerializeField] private bool smoothMovement = true;
    [SerializeField] private float dragSpeed = 5.0f;

    [SerializeField] private bool isRotating = false;
    [SerializeField] private float rotationSpeed = 10f;
    //[SerializeField] private float rotationDamping = 5f; //замедление
    //[SerializeField] private float rotationSensitivity = 1f;
    //[SerializeField] private float smoothFactor = 1f;

    [SerializeField] private bool invertRotation = false;

    //[SerializeField] private bool clampRotation = false;
    [SerializeField] private float minAngle = -45f;
    [SerializeField] private float maxAngle = 45f;

    [SerializeField] private bool keepFullObjectInView = true;

    private bool ShiftPressed;

    //private float currentAngle;
    //private float initialAngle;
    //private float targetRotation;

    //private Vector2 mouseStartPosition; //initial
    //private Vector2 originalPosition;
    //private Vector2 pivotPoint; //центр прямоугольника
    private Vector3 center;
    private Vector3 mouseWorldPos;

    //private float currentRotationVelocity;

    //private Vector3 offset;
    private Vector3 originalPosition;
    private Vector3 mouseStartPosition;



    private Bounds objectBounds;

    private Color selectedColor = Color.darkGreen;
    //private Color selectedColor = new Color(10f, 107f, 67f, 0.5f);
    private Color RotationColor = Color.greenYellow; //почему ты не меняешь цвет на какой я хочу... почему цвет настроения - черный?....... UPD:спс, друг, что начал меняться)
    private Material originMaterial;
    private Color originColor;
    

    void Start()
    {
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
                center = renderers[i].bounds.center;
            }            
            
        }
        else
        {
            // Если нет рендереров, используем коллайдер
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
            //if (ShiftPressed == false) 
            //{ 
            ////перемещение
            
            
            
            //isRotating = false;

            //Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
           //mouseWorldPos.z = transform.position.z;
            //originalPosition = mouseWorldPos + offset;

            mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = transform.position.z;
            originalPosition = mouseWorldPos;

            // Ограничиваем позицию в пределах видимости камеры
            originalPosition = ClampPositionToCameraView(originalPosition);

            if (smoothMovement)
                transform.position = Vector3.Lerp(transform.position, originalPosition, dragSpeed * Time.deltaTime);
            else
                transform.position = originalPosition;
            //}
            

            //if (ShiftPressed == true)
            //{
            //    StartRotation();

            //}
            //if (isRotating == true)
            //{
            //    FigureRotation();
            //    ApplyRotation();
            //}


            //if (ShiftPressed == true)
            //{//вращение
            //    StartRotation();

            //    //isRotating = true;
            //    //initialAngle = transform.eulerAngles.y;
            //    //RotateFigure();

            //}
            //if (isRotating == true)
            //{
            //    if (ShiftPressed == true)
            //        RotateFigure();
            //    else if (ShiftPressed == false) { ApplyRotation(); }
            //}



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

            ////Вектор от центра к начальной точке касания
            //Vector3 toInitial = mouseStartPosition - center;

            //// Вектор от центра к текущей позиции мыши
            //Vector3 toCurrent = currentMouseRotationPosition - center;

            //// Угол между векторами
            //float angle = Vector2.SignedAngle(toInitial, toCurrent) * rotationSpeed * Time.deltaTime;
            //float angleAm = invertRotation ? -angle : angle;

            //transform.Rotate(0, 0, angleAm);
            //transform.Rotate(0, 0, deltaZ);
            //transform.Rotate(0, 0, angle);
        }
    }
    
   




    //чтобы за пределы камеры не улетало:
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



//private void StartRotation()
//{
//    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
//    if (hit.collider != null && hit.collider.gameObject == th)
//    {
//        isRotating = true;
//        mouseStartPosition = Input.mousePosition;
//        currentAngle = transform.eulerAngles.z;
//    }

//}

//private void RotateFigure()
//{
//    if (isRotating == true)
//    {
//        Vector3 mouseDelta = Input.mousePosition - mouseStartPosition;
//        targetRotation = currentAngle - (mouseDelta.x + mouseDelta.y) * rotationSpeed * Time.deltaTime;

//        if (clampRotation)
//        {
//            targetRotation = Mathf.Clamp(targetRotation, minAngle, maxAngle);
//        }
//        //currentAngle += mouseDelta.x * rotationSpeed;
//        //currentAngle = Mathf.Clamp(currentAngle,minAngle,maxAngle);
//        //targetRotation = initialAngle - mouseDelta.x

//        //transform.rotation = Quaternion.AngleAxis(currentAngle,Vector3.forward);
//    }

//}
//private void ApplyRotation()
//{
//    if (smoothMovement)
//    {
//        float currentZ = transform.eulerAngles.z;
//        float newZ = Mathf.LerpAngle(currentZ, targetRotation, smoothFactor * Time.deltaTime);
//        transform.rotation = Quaternion.Euler(0, 0, newZ);
//    }
//    else if (isRotating)
//    {
//        transform.rotation = Quaternion.Euler(0, 0, targetRotation);
//    }


//}


//private void StartRotation()
//{ // Start rotation only when both mouse button and shift are pressed
//    if (Input.GetMouseButtonDown(0) && ShiftPressed)
//    {
//        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

//        if (hit.collider != null && hit.collider.gameObject == gameObject)
//        {
//            isRotating = true;
//            mouseStartPosition = mouseWorldPos;
//            //pivotPoint = transform.position; //центр прямоугольника

//            //currentAngle = transform.eulerAngles.z;
//        }
//    }

//}

////void FigureRotation()
////{
////    // Continue rotation only while both mouse button and shift are held
////    if (isRotating)
////    {

////        if (isRotating && Input.GetMouseButton(0))
////        {
////            Vector2 currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

////            // Вектор от центра к начальной точке касания
////            Vector2 toInitial = mouseStartPosition - center;

////            // Вектор от центра к текущей позиции мыши
////            Vector2 toCurrent = currentMousePos - center;

////            // Угол между векторами
////            float angle = Vector2.SignedAngle(toInitial, toCurrent);

////            // Ограничиваем скорость вращения
////            currentRotationVelocity = Mathf.Clamp(angle * rotationSensitivity, -rotationSpeed, rotationSpeed);
////        }
////        else
////        {
////            isRotating = false;
////            // Плавное замедление
////            currentRotationVelocity = Mathf.Lerp(currentRotationVelocity, 0, rotationDamping * Time.deltaTime);
////        }
////        }
////}


//void RotateFigure()
//{

//    if (Input.GetMouseButton(0) && ShiftPressed)
//    {
//        Vector2 mouseDelta = Input.mousePosition - mouseStartPosition;
//        targetRotation = currentAngle + (mouseDelta.x - mouseDelta.y) * rotationSpeed;

//        if (clampRotation)
//            targetRotation = Mathf.Clamp(targetRotation, minAngle, maxAngle);
//    }
//    else
//    {
//        isRotating = false;
//    }
//}


//void ApplyRotation()
//{
//    //if (Mathf.Abs(currentRotationVelocity) > 0.1f)
//    //{
//    //    // Вращаем вокруг центра
//    //    transform.RotateAround(center, Vector3.forward, currentRotationVelocity * Time.deltaTime);
//    //}

//    if (smoothMovement)
//    {
//        float currentZ = Mathf.LerpAngle(
//            transform.eulerAngles.z,
//            targetRotation,
//            smoothFactor * Time.deltaTime);

//        transform.rotation = Quaternion.Euler(0, 0, currentZ);
//    }
//    else if (isRotating)
//    {
//        transform.rotation = Quaternion.Euler(0, 0, targetRotation);
//    }

//}

//public void ResetRotation()
//    {
//        targetRotation = 0f;
//        transform.rotation = Quaternion.identity;
//    }














//private void StartRotation()
//{
//    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
//    if (hit.collider != null && hit.collider.gameObject == th)
//    {
//        isRotating = true;
//        mouseStartPosition = Input.mousePosition;
//        //currentAngle = transform.eulerAngles.z;
//    }

//}

//private void RotateFigure()
//{
//    if (isRotating == true)
//    {
//        Vector3 mouseDelta = Input.mousePosition - mouseStartPosition;
//        targetRotation = currentAngle - (mouseDelta.x + mouseDelta.y) * rotationSpeed * Time.deltaTime;

//        if (clampRotation)
//        {
//            targetRotation = Mathf.Clamp(targetRotation, minAngle, maxAngle);
//        }
//        currentAngle += mouseDelta.x * rotationSpeed;
//        currentAngle = Mathf.Clamp(currentAngle, minAngle, maxAngle);
//        targetRotation = initialAngle - mouseDelta.x;

//        //transform.rotation = Quaternion.AngleAxis(currentAngle,Vector3.forward);
//        transform.rotation = Quaternion.AngleAxis(currentAngle, );
//    }

//}
//private void ApplyRotation()
//{
//    //if (smoothMovement)
//    //{
//    //    //float currentZ = transform.eulerAngles.z;
//    //    //float newZ = Mathf.LerpAngle(currentZ, targetRotation, smoothFactor * Time.deltaTime);
//    //    //transform.rotation = Quaternion.Euler(0, 0, newZ);
//    //}
//    //else if (isRotating)
//    //{
//    //    transform.rotation = Quaternion.Euler(0, 0, targetRotation);
//    //}


//}
