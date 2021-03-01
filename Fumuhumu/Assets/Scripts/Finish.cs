using UnityEngine;

public class Finish : MonoBehaviour
{

    public delegate void Collected();
    public static Collected OnCollected;

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Objective")
        {
            other.gameObject.tag = "Untagged";
            Debug.Log("Success!!!");
            OnCollected?.Invoke();
            Destroy(other.gameObject, 2.0f);
            audioSource.Play();
        }
    }

}
