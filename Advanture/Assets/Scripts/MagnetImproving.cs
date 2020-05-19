using UnityEngine;

public class MagnetImproving : MonoBehaviour
{
    // Степени прокачки магнита.
    public Sprite[] bars;

    // Стоимости прокачки.
    private int[] costs;
    void Start()
    {
        // Инициализация стоимостей.
        costs = Info.costs;

        // Установка текущего уровня прокачки.
        GetComponent<SpriteRenderer>().sprite = bars[(int)Info.magneteLevel];
    }

    /// <summary>
    /// Улучшение.
    /// </summary>
    public void Improve()
    {
        // Если можем улучшить.
        if (Info.magneteLevel != 3)
        {
            // Если хватает денег.
            if (Info.total_coins >= costs[(int)Info.magneteLevel])
            {
                // Вычитаем плату.
                Info.total_coins -= costs[(int)Info.magneteLevel];

                // Улучшаем.
                Info.magneteLevel++;

                // Меняем степень прокачки.
                GetComponent<SpriteRenderer>().sprite = bars[(int)Info.magneteLevel];
            }

            // Сохраняем игру.
            SaveLoadManager sv = new SaveLoadManager();
            sv.SaveGame();
        }
    }
}

