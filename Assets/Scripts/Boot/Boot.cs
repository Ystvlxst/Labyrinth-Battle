using UnityEngine;

public class Boot : MonoBehaviour
{
    private void Start()
    {
        Singleton<LevelLoader>.Instance.LoadSavedLevel();
    }
}
