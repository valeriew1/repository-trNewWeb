using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CloseLevelScript : MonoBehaviour
{

    [SerializeField] private Button closeBut;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        closeBut.onClick.AddListener(OnCloseButClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCloseButClick() 
    {
        SceneManager.LoadScene(0);
    }

}
