using UnityEngine;
using UnityEngine.UI;

public class QuitAppScript : MonoBehaviour
{
    [SerializeField] private Button QuitBut;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        QuitBut.onClick.AddListener(QuitButton);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitButton() 
    {
        Application.Quit();
    }
}
