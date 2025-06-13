using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class CloseLevelScript : MonoBehaviour
{
    [SerializeField] private Button closeBut;
    void Start()
    {
        closeBut.onClick.AddListener(OnCloseButClick);
    }
    public void OnCloseButClick() 
    {
        SceneManager.LoadScene(0);
    }
}
