using UnityEngine;

public class GunAmmo : MonoBehaviour
{
    // Скорость снаряда.
    public float speed;

    // Время до уничтожения.    
    public float destroyTime;

    void Start()
    {
        // Уничтожить объект через destroyTime.
        Invoke("DestroyAmmo", destroyTime);
    }

    void Update()
    {
        // Движение.
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    /// <summary>
    /// Уничтожение объекта.
    /// </summary>
    void DestroyAmmo()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Обработка касания с игроком.
    /// </summary>
    /// <param name="col">
    /// Коллайдер на который сработал тригер.
    /// </param>
    private void OnTriggerEnter2D(Collider2D col)
    {
        // Проверка на принадлежность тригера игроку.
        PlayerController pl = col.GetComponent<PlayerController>();

        if (pl != null)
        {
            // Нанесение урона и уничтожения снаряда.
            GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<HealthSystem>().GetDamage();
            Destroy(gameObject);
        }
    }
}
