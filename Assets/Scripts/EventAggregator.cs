using UnityEngine;
using UnityEngine.Events;

public class EventWithInt : UnityEvent<int> { }
public class EventWithVector : UnityEvent<Vector3> { }

public class EventAggregator : MonoBehaviour
{
    public static EventWithInt SwapWeapon = new EventWithInt();
    public static UnityEvent AddBullets = new UnityEvent();
    public static UnityEvent HealPlayer = new UnityEvent();
    public static EventWithVector MovePlayer = new EventWithVector();
    public static UnityEvent StorPlayer = new UnityEvent();
    public static UnityEvent Fire = new UnityEvent();
    public static EventWithInt SetWeapon = new EventWithInt();

}
