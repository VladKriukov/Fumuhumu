using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{

    [SerializeField] bool spawnedByPlayer = true;
    [SerializeField] GameObject objectToSpawn;
    [SerializeField] bool isInfinate;
    public float countAvailable = 1;
    [SerializeField] float cooldown;

    [SerializeField] Material ready, coolingDown;

    public UnityEvent finishedSpawning;

    bool canSpawn = true;
    float timer;
    TMP_Text counter;
    MeshRenderer meshRenderer;

    AudioSource audioSource;

    void OnEnable()
    {
        GameManager.OnStartLevel += StartLevel;
    }

    void OnDisable()
    {
        GameManager.OnStartLevel -= StartLevel;
    }

    void StartLevel()
    {
        if (transform.GetChild(0).childCount > 1)
        {
            transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        }
    }

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = ready;
        counter = transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        counter.text = "" + countAvailable;
        if (isInfinate)
        {
            counter.gameObject.SetActive(false);
        }
        audioSource = GetComponent<AudioSource>();
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && spawnedByPlayer)
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        if (countAvailable > 0 && canSpawn && GameManager.inGame)
        {
            Instantiate(objectToSpawn, transform.position, Quaternion.identity);
            if (!isInfinate)
            {
                canSpawn = false;
                countAvailable--;
                counter.text = "" + countAvailable;
                if (countAvailable == 0)
                {
                    finishedSpawning.Invoke();
                }
                meshRenderer.material = coolingDown;
                audioSource.Play();
            }
            timer = cooldown;
        }
    }

    void Update()
    {
        if (canSpawn == false)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                canSpawn = true;
                meshRenderer.material = ready;
            }
        }
    }

}
