using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoseLevelButton : MonoBehaviour
{
    public void LevelNumber(string levelNumber) => SceneManager.LoadScene(levelNumber);
}
