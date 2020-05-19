using UnityEngine;
using UnityEngine.UI;

public class player_score : MonoBehaviour
{
    // Очки в текущем забеге.
    static public int current_score = 0;

    // Счетчик очков.
    public Text TextScore;

    void Start()
    {
        // Обнуление очков.
        current_score = 0;
    }
}
