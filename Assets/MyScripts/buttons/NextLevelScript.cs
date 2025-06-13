using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevelScript : MonoBehaviour
{
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private int NextLevelNumber;
    
    void Start()
    {
        nextLevelButton.onClick.AddListener(OnNextLevelButtonClick);
    }

    public void OnNextLevelButtonClick() 
    {
        SceneManager.LoadScene(NextLevelNumber);
    }
}
