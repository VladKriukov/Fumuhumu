using UnityEngine;

public class SpeedPipe : MonoBehaviour
{

    [SerializeField] float speed = 1;
    Vector3 direction;
    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Rigidbody>())
        {
            direction = transform.TransformDirection(Vector3.forward);
            other.gameObject.GetComponent<Rigidbody>().velocity = direction * speed;
            audioSource.Play();
        }
    }

}
