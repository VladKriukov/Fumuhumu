using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    public static bool inGame { get; private set; }
    public static bool canMoveStuff { get; private set; }

    bool finishedSpawning;
    public UnityEvent onLevelComplete;
    public delegate void StartLevel();
    public static StartLevel OnStartLevel;

    [SerializeField] Animator canvasAnimator;

    Rigidbody[] rbs;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void SetRigidbodiesKinematic(bool b)
    {
        rbs = FindObjectsOfType<Rigidbody>();
        foreach (var item in rbs)
        {
            item.isKinematic = b;
        }
    }

    public void GameReady()
    {
        canvasAnimator.SetTrigger("Start");
        canMoveStuff = true;
    }

    public void StartGame()
    {
        inGame = true;
        OnStartLevel?.Invoke();
        SetRigidbodiesKinematic(false);
    }

    public void StopGame()
    {
        inGame = false;
        canMoveStuff = false;
    }

    void OnDisable()
    {
        Finish.OnCollected -= OnCollected;
        CameraCinematic.OnFinishedCinematic -= GameReady;
    }

    void OnEnable()
    {
        Finish.OnCollected += OnCollected;
        CameraCinematic.OnFinishedCinematic += GameReady;
    }

    void OnCollected()
    {
        if (finishedSpawning)
        {
            Invoke(nameof(CheckForWin), 2.2f);
        }
    }

    void CheckForWin()
    {
        if (GameObject.FindGameObjectsWithTag("Objective").Length == 0)
        {
            onLevelComplete.Invoke();
            canMoveStuff = false;
        }
    }

    public void OnFinishedSpawning()
    {
        finishedSpawning = true;
    }

}
