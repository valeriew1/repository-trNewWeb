using UnityEngine;
using UnityEngine.UI;

public class CloseSettingsButtonScript : MonoBehaviour
{
    [SerializeField] private Button CloseSetBut;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CloseSetBut.onClick.AddListener(onClickCloseSetBut);
    }

    // Update is called once per frame
    void Update()
    {
        
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
