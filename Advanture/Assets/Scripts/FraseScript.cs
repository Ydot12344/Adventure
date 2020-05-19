using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FraseScript : MonoBehaviour
{
    // Source фразы.
    AudioSource source;
    private void Start()
    {
        // Инициализация source
        source = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Проигрывает фразу.
    /// </summary>
    public void Play()
    {
        source.Play();
    }
}
