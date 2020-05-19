using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class SaveLoadManager : MonoBehaviour
{
    // Путь к файлу сохранения.
    private string filePath = "save.gamesave";

    private void Start()
    {
        // Если нет файла сохранения, то сохраняем игру.
        if (!File.Exists("save.game"))
        {
            SaveGame();
        }
    }

    /// <summary>
    /// Сохранение игры.
    /// </summary>
    public void SaveGame()
    {
        // Создание форматераю.
        BinaryFormatter bf = new BinaryFormatter();

        try
        {
            // Открываем поток для записи файла.
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                // Создаем объект сохранения.
                Save save = new Save(Info.total_coins, Info.best_record, Info.numberOfLifes, Info.costs, Info.magneteLevel);
            
            // Сериализуем его.
            bf.Serialize(fs, save);
            }
        }
        catch(Exception ex)
        {
            print(ex.Message);
        }
    }

    /// <summary>
    /// Загрузка игры.
    /// </summary>
    public void LoadGame()
    {
        // Если файла нет, то сохраняем игру и выходим.
        if (!File.Exists(filePath))
        {
            SaveGame();
            return;
        }

        // Создание форматера.
        BinaryFormatter bf = new BinaryFormatter();

        try
        {
            // Открываем поток для чтения файла.
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                // Десериализуем объект Save.
                Save save = (Save)bf.Deserialize(fs);

                // Устанавливаем его значения в Info.
                Info.best_record = save.best_record;
                Info.total_coins = save.total_coins;
                Info.numberOfLifes = save.numOfLifes;
                Info.costs = save.costs;
                Info.magneteLevel = save.magneteLevel;
            }
        }
        catch(Exception ex)
        {
            print(ex.Message);
        }
    }
}

// Объект сохранения.

[System.Serializable]
public class Save
{
    // Конструктор.
    public Save(int tc, int br, int numOfLifes, int[] costs, float magneteLevel)
    {
        total_coins = tc;
        best_record = br;
        this.numOfLifes = numOfLifes;
        this.costs = costs;
        this.magneteLevel = magneteLevel;

    }

    // Уровень магнита.
    public float magneteLevel;

    // Стоимость прокачек.
    public int[] costs;

    // Кол-во накопленных монет.
    public int total_coins;

    // Рекорд.
    public int best_record;

    // Максимальное кол-во жизней.
    public int numOfLifes;
}