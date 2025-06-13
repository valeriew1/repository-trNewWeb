using UnityEngine;
using UnityEngine.UI;

public class QuitAppScript : MonoBehaviour
{
    [SerializeField] private Button QuitBut;
    void Start()
    {
        QuitBut.onClick.AddListener(QuitButton);
    }
    public void QuitButton() 
    {
        Application.Quit();
    }
}
