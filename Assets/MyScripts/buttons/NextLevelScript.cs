using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevelScript : MonoBehaviour
{

    [SerializeField] private Button nextLevelButton;
    [SerializeField] private int NextLevelNumber;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nextLevelButton.onClick.AddListener(OnNextLevelButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnNextLevelButtonClick() 
    {
        SceneManager.LoadScene(NextLevelNumber);
    }

}
