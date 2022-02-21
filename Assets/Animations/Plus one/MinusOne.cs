using UnityEngine;
using UnityEngine.Events;

[SelectionBase]
public class MinusOne : MonoBehaviour
{
    
    public UnityEvent onCollect;

    public void Collectt()
    {
        onCollect?.Invoke();

    }
}
