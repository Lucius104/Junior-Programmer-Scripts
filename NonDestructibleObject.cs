using UnityEngine;


public class NonDestructibleObject : MonoBehaviour
{
    public NonDestructibleObject Instance { get; private set; }
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
    }
}
