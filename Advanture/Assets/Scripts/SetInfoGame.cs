using UnityEngine.UI;
using UnityEngine;

public class SetInfoGame : MonoBehaviour
{
    // UI элемент текста рекорда.
    public Text record;
    void Start()
    {
        // Установка текущего рекорда.
        record.text = Info.best_record.ToString();
    }
}
