using UnityEngine;

public class Obstacle : MonoBehaviour
{
    /// <summary>
    /// Взаимодействие с игроком.
    /// </summary>
    /// <param name="col">
    /// Коллайдер на который сработал тригер.
    /// </param>
    private void OnTriggerEnter2D(Collider2D col)
    {
        // Проверка на принадлежность коллайдера игроку.
        PlayerController pl = col.GetComponent<PlayerController>();

        if (pl != null)
        {
            // Возврат игрока на начальную точку.
            GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerController>().GoToStart();
        }
    }
}
