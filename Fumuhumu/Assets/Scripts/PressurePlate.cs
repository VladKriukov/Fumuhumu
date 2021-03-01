using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] GameObject target;

    void OnTriggerEnter(Collider other)
    {
        if (target.GetComponent<Spawner>())
        {
            target.GetComponent<Spawner>().Spawn();
        }
    }

}
