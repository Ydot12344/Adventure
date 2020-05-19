using UnityEngine;
public class HealthImproving : MonoBehaviour
{
    // Степени заполнения полоски прокачки.
    public Sprite[] bars;

    // Цены на прокачку.
    private int[] costs;
    void Start()
    {
        // Инициализация цен.
        costs = Info.costs;

        // Установка текущего уровня прокачки.
        GetComponent<SpriteRenderer>().sprite = bars[Info.numberOfLifes - 2];
    }

    /// <summary>
    /// Улучшение.
    /// </summary>
    public void Improve()
    {
        // Улучшить можно только если есть что улучшать.
        if (Info.numberOfLifes != 5)
        {
            // Если хватает денег.
            if (Info.total_coins >= costs[Info.numberOfLifes - 2])
            {
                // Вычитаем плату.
                Info.total_coins -= costs[Info.numberOfLifes - 2];

                // Увеличиваем максимальное число жизней.
                Info.numberOfLifes++;

                // Меняем спрайт уровня прокачки.
                GetComponent<SpriteRenderer>().sprite = bars[Info.numberOfLifes - 2];
            }

            // Сохраняем игру.
            SaveLoadManager sv = new SaveLoadManager();
            sv.SaveGame();
        }
    }
}
