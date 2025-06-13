using System;
using UnityEngine;
using UnityEngine.UI;
public class ResetButtonScript : MonoBehaviour
{
    [SerializeField] private GameObject[] resetObjects;
    [SerializeField] private GameObject[] stObjects;
    [SerializeField] private Button resetButt;
    private GameObject resetObj;
    private GameObject stObj;
    private int currentResetIndex = 0;
    private bool activator = false;
    private void Start()
    {
        resetButt.onClick.AddListener(OnResetButtonClick);
    }
    private void Update()
    {
        if (activator == true)
        { 
            if (resetObjects.Length > 0)
            {
                ResetObject();
                activator = false;
            } 
        }        
    }
    private void ResetObject()
    {
        for (int i = 0; i < resetObjects.Length; i++)
        {
            currentResetIndex = i;
            resetObj = resetObjects[currentResetIndex];
            stObj = stObjects[currentResetIndex];
            if (resetObj != null)
            {
                resetObj.transform.position = stObj.transform.position;
            }
            if (currentResetIndex == resetObjects.Length - 1)
            {
                currentResetIndex = 0;
                break;
            }
        }
    }
    private void OnResetButtonClick()
    {
        activator = true;
    }
}
