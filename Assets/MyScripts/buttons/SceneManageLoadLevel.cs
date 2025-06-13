using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneManageLoadLevel : MonoBehaviour
{
    [SerializeField] private Button newLevelBut;
    [SerializeField] private int NextSceneOrder;
    void Start()
    {
        newLevelBut.onClick.AddListener(OnNextLevelButClick);
    }
    public void OnNextLevelButClick() 
    {
        SceneManager.LoadScene(NextSceneOrder); 
    }
}
