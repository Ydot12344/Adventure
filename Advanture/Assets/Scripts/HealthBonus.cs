using UnityEngine;

public class HealthBonus : MonoBehaviour
{
    // Жизни игрока.
    private HealthSystem player_hp;

    void Start()
    {
        // Инициализация.
        player_hp = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSystem>();
    }

    /// <summary>
    /// Обработка касания игрока.
    /// </summary>
    /// <param name="collision">
    /// Коллайдер на который сработал тригер.
    /// </param>
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (player_hp == col.GetComponent<HealthSystem>())
        {
            player_hp.AddHP();
            Destroy(gameObject);
        }
    }
}
