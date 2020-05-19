using UnityEngine;
using UnityEngine.Audio;
public class Deth : MonoBehaviour
{
    // Игрок.
    public PlayerController player;
    // Камера.
    public GameObject camera;

    // Миксер Master(родитель всех остальных миксеров).
    public AudioMixerGroup master;

    // Скорость.
    public float speed;
    // Буферное расстояние между объектом и игроком.
    public float delay;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        camera = GameObject.FindGameObjectWithTag("MainCamera");

        // Установка задержки по y.
        transform.position = player.transform.position - new Vector3(0, delay, 0);
    }

    void Update()
    {
        // При приближении на 7 блоков начинаем изменять аудио фильтр LowPas
        // для эффекта погружения подводу.
        if (Vector2.Distance(player.transform.position, transform.position) < 7)
            master.audioMixer.SetFloat("MasterLowPassFreq", Mathf.Lerp(300, 5000, (Vector2.Distance(player.transform.position, transform.position) - 3) / 4f));
        else
            master.audioMixer.SetFloat("MasterLowPassFreq", 5000);

        // Если игрок жив.
        if (player != null)
        {
            // Если игрок отбежал от объекта больше чем на delay.
            if ((player.transform.position.y - transform.position.y) > delay)
            {
                // Телепортируем объект ближе к игроку.
                transform.position = player.transform.position - new Vector3(0, delay, 0); 
            }

            // Установка объекта по центру камеры.
            transform.position = new Vector3(camera.transform.position.x, transform.position.y, transform.position.z);
            // Движение объекта.
            transform.position += new Vector3(0, speed, 0) * Time.deltaTime;
        }
    }


    /// <summary>
    /// Обработка соприкосновения.
    /// </summary>
    /// <param name="collision">
    /// Коллайдер который задействовал тригер.
    /// </param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Проверка коллайдера на принадлежность игроку.
        PlayerController pl = collision.GetComponent<PlayerController>();

        if (pl != null)
        {
            // Смерть игрока.
            player.Die(0);
        }
    }
}
