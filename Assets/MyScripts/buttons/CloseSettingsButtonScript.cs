using UnityEngine;
using UnityEngine.UI;

public class CloseSettingsButtonScript : MonoBehaviour
{
    [SerializeField] private Button CloseSetBut;
    void Start()
    {
        CloseSetBut.onClick.AddListener(onClickCloseSetBut);
    }
    
    public void onClickCloseSetBut() 
    {
        Canvas[] allCanvases = Resources.FindObjectsOfTypeAll<Canvas>();
        foreach (Canvas canvas in allCanvases)
        {
            if (canvas.name == "CanvasMenu")
            {
                canvas.gameObject.SetActive(true);
            }
            if (canvas.name == "SettingsCanvas")
            {
                canvas.gameObject.SetActive(false);
            }
        }
    }
}