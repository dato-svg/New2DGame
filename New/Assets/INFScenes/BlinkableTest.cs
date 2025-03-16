using BaseScripts.BugSystem;
using UnityEngine;

public class BlinkableTest : MonoBehaviour, IBlinkable
{
    public void Activate()
    {
        // Хочу пельменей
    }

    public void Deactivate()
    {
        Destroy(gameObject);
    }
}
