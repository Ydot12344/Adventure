using System;
using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Снарпяд.
    public GameObject ammo;

    // Направление стрельбы.
    public Transform shotDir;

    // Таймер.
    private float timer;

    // Игрок.
    private Transform Player;

    void Start()
    {
        // Инициализация таймера.
        timer = 2;

        // Инициализация игрока.
        Player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
    }

    void Update()
    {
        // Если игрок находится недалеко от пушки она начинает стрелять.
        if (Mathf.Abs(Player.position.y - gameObject.transform.position.y ) < 10)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;           
            }
            else
            {
                timer += 2;
                Shoot();
                timer -= Time.deltaTime;
            }
        }
    }

    /// <summary>
    /// Выстрел
    /// </summary>
    void Shoot()
    {
        // Создание объекта снаряда.
        Instantiate(ammo, shotDir.position, shotDir.rotation);
    }
}
