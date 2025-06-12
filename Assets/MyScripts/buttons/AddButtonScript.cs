using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class AddButtonScript : MonoBehaviour
{
    [SerializeField] private GameObject[] AddObjects;
    [SerializeField] private GameObject centralObject;
    [SerializeField] private Button AddButt;

    private GameObject chosenObj;
    private int currentAddIndex = 0;
    private bool activator = false;
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
