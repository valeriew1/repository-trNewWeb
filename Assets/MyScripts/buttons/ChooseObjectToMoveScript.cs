using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ChooseObjectToMoveScript : MonoBehaviour
{ //��� ������� ���������� ������ �� ����, ����� �� ����� ���������� � �� �������

    [Header("��������� ������")]
    [SerializeField] private GameObject[] AddObjects;
    [SerializeField] private GameObject centralObject;
    [SerializeField] private Button AddButt;
    //[SerializeField] private Camera MainCamera;

    private bool activator = false;
    private int currentAddIndex = 0;
    //private KeyCode selectionKey = KeyCode.Tab;
    //private KeyCode addingKey = KeyCode.KeypadEnter;

    private GameObject chosenObj;
    private Color selectedColor = Color.darkGreen;
    private Material originMaterial;
    private Color originColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
