using UnityEngine;
using UnityEngine.UI;

public class player_money : MonoBehaviour
{
    // Деньги заработанные в текущем забеге.
    static public int coin = 0;

    // Счетчик монет.
    public Text TextMoney;

    void Start()
    {
        // Начальная инициализация.
        coin = 0;
    }
}
