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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextLevel() 
    { 
        //newLevelBut.onClick.AddListener(() => { SceneManager.LoadScene("Level1"); });


    }

    public void OnNextLevelButClick() 
    { SceneManager.LoadScene(NextSceneOrder); }
}
