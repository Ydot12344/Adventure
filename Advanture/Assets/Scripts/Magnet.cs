using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{ 
    // Монеты попавшие в поле.
    private List<GameObject> CoinsInField  = new List<GameObject>();

    // Игрок.
    private GameObject player;

    // Мощность притягивания.
    public float power;

    // Коллайдер магнита.
    public BoxCollider2D bc;

    private void Start()
    {
        // Инициализация игрока и радиуса магнита
        player = GameObject.FindGameObjectWithTag("Player");
        bc.edgeRadius = Info.magneteLevel;
    }

    void Update()
    {
        // Перемещаем магнит за игроком.
        transform.position = player.transform.position;

        // Прохожим по списку монет в поле.
        for (int i = 0; i < CoinsInField.Count; i++)
        {
            // Некоторые монеты достигнут игрока и уничтожатся
            // до того как мы до них дойдем. Поэтому удалим их
            // из списка.
            if (CoinsInField[i] == null)
            {
                CoinsInField.RemoveAt(i);
            }
            else
            {
                // Остальные монеты притянем к игроку.
                CoinsInField[i].transform.position = Vector3.Lerp(CoinsInField[i].transform.position, player.transform.position, power);
            }
        }
    }

    /// <summary>
    /// Добавление монеты в поле.
    /// </summary>
    /// <param name="obj">
    /// Объект монеты.
    /// </param>
    public void AddToField(GameObject obj)
    {
        CoinsInField.Add(obj);
    }
}
