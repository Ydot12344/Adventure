using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    // Префабы кусков дороги.
    public GameObject[] Roads;
    
    // Стартовый кусок дороги.
    public GameObject StartBlock;

    // Координаты точки в которой создается следующий префаб.
    float X, Y;

    // Размер буфера.
    int ColSpawn = 6;

    // Координата Y последнего элемента дороги.
    float Ylast;

    // Уничтожен ли стартовый блок.
    private bool WasStartBlockDeleted = true;

    // Игрок.
    public Transform PlayerTransf;

    // Буфер префабов
    List<GameObject> CurrentBlocks = new List<GameObject>();


    void Start()
    {
        // Инициализация координат.
        X = StartBlock.transform.position.x;
        Y = StartBlock.transform.position.y;

        // Заполнение буфера.
        for (int i = 0; i < ColSpawn; i++)
        {
            SpawnBlock();
        }
    }
    void Update()
    {
        CheckForSpawn();
    }

    /// <summary>
    /// Проверка на возможность спавна нового блока и
    /// уничтожение последнего блока буфера.
    /// </summary>
    void CheckForSpawn()
    {
        if (PlayerTransf != null && PlayerTransf.position.y > Ylast + 25)
        {
            // Уничтожение стартого блока, если надо
            if (WasStartBlockDeleted)
            {
                Destroy(StartBlock.gameObject);
                WasStartBlockDeleted = false;
            }

            // Спавн нового и удаление последнего.
            SpawnBlock();
            DestroyBlock();
        }
    }

    /// <summary>
    /// Спавн нового блока буфера.
    /// </summary>
    void SpawnBlock()
    {
        // Создание нового блока.
        GameObject block = Instantiate(Roads[Random.Range(0, Roads.Length)], transform);

        // Изменение координат для нового спавна.
        if (CurrentBlocks.Count == 0)
        {
            X += StartBlock.GetComponent<BlockInfo>().X;
            Y += StartBlock.GetComponent<BlockInfo>().Y;
        }
        else
        {
            X += CurrentBlocks[CurrentBlocks.Count - 1].GetComponent<BlockInfo>().X;
            Y += CurrentBlocks[CurrentBlocks.Count - 1].GetComponent<BlockInfo>().Y;
        }
        
        // Установка координат созданного блока.
        block.transform.position = new Vector3(X, Y, 2);

        // Добавление нового блока в буфер.
        CurrentBlocks.Add(block);

        // Обновление координат последнего блока.
        Ylast = CurrentBlocks[0].gameObject.transform.position.y;
    }

    /// <summary>
    /// Уничтожение последнего блока.
    /// </summary>
    void DestroyBlock()
    {
        Destroy(CurrentBlocks[0].gameObject);
        CurrentBlocks.RemoveAt(0);
    }
}
