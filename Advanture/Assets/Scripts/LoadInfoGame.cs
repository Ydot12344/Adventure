using UnityEngine;

public class LoadInfoGame : MonoBehaviour
{
    void Start()
    {
        // Загрузка игры.   
        SaveLoadManager sv = new SaveLoadManager();
        sv.LoadGame();
    }
}
