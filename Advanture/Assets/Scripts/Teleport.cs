using UnityEngine;

public class Teleport : MonoBehaviour
{
    // Точка куда телепортируются
    public Floor finishPoint;

    // Точка откуда телепортируются.
    public Floor startPoint;

    /// <summary>
    /// Обработка соприкосновения с игроком.
    /// </summary>
    /// <param name="col">
    /// Коллайдер на который сработал тригер.
    /// </param>
    private void OnTriggerEnter2D(Collider2D col)
    {
        // Проверка на принадлежность коллайдера игроку.
        PlayerController pl = col.GetComponent<PlayerController>();

        if(pl != null)
        {
            // Меняем новую конечную позицию игрока на конечную точку.
            pl.new_position += (Vector2)(finishPoint.transform.position - startPoint.transform.position);

            // Перемещаем игрока на конечню точку.
            pl.transform.position += (finishPoint.transform.position - startPoint.transform.position);
        }
    }
}
