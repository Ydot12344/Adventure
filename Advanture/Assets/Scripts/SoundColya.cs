using UnityEngine;

public class SoundColya : MonoBehaviour
{
    // Пол на котором находится звук.
    public GameObject pref;
    void Start()
    {
        // Компенсация сдвига пола.
        transform.position += new Vector3(0, 0, -pref.transform.position.z);
    }

}
