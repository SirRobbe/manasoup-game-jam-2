using UnityEngine;

public abstract class ACardManager : MonoBehaviour
{
    public abstract void Invoke();
    public abstract float GetCooldown();
}