using UnityEngine;

public class Floor : MonoBehaviour
{
    private void Start()
    {
        // Позиция элемента пола по Z = Y.
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
    }

}
