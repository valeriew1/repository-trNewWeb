using UnityEngine;
using UnityEngine.UI;

public class OpenSettingsScript : MonoBehaviour
{
    [SerializeField] private Button SettingsButton;
    void Start()
    {
        SettingsButton.onClick.AddListener(OnSettingsButScript);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSettingsButScript() 
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
                canvas.gameObject.SetActive(true);
            }
        }

    }

    

}
