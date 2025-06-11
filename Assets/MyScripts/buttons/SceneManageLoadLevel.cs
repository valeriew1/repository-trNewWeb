using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManageLoadLevel : MonoBehaviour
{
    [SerializeField] private Button newLevelBut;
    [SerializeField] private int NextSceneOrder;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        newLevelBut.onClick.AddListener(OnNextLevelButClick);
    }
    public void OnNextLevelButClick() 
    { 
        SceneManager.LoadScene(NextSceneOrder); 
    }

    public void OnNextLevelButClick() 
    { SceneManager.LoadScene(NextSceneOrder); }
}
