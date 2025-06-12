using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ChooseObjectToMoveScript : MonoBehaviour
{
    [SerializeField] private GameObject[] AddObjects;
    [SerializeField] private GameObject centralObject;
    [SerializeField] private Button AddButt;
    private bool activator = false;
    private int currentAddIndex = 0;
    private GameObject chosenObj;
    private Color selectedColor = Color.darkGreen;
    private Material originMaterial;
    private Color originColor;
}
