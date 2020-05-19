using UnityEngine;

public class Camera : MonoBehaviour
{

    // Игрок за которым надо следовать.
    public GameObject player;

    // Коефициент для Lerp.
    public float koef = 0.1f;

    // Расстояние на которое может камера отклониться от
    // player.
    public float delta = 0;

    // Умер ли игрок.
    public bool IsDie = false;

    // Смещение камеры по Z относительно player.
    private Vector3 t = new Vector3(0, 0, -10);



    private void Start()
    {
        // Установка начального положения камеры.
        gameObject.transform.position = player.transform.position + t;
    }

    void Update()
    {
        // Если игрок жив.
        if (!IsDie)
        {
            // Если игрок выбежал за максимальное отклонение.
            if (Mathf.Abs(Vector3.Distance(player.transform.position + t, gameObject.transform.position)) > delta)
            {
                // Перемещаем камеру к игроку.
                transform.position = Vector3.Lerp(gameObject.transform.position, player.transform.position + t, koef);
            }
        }
    }
}
