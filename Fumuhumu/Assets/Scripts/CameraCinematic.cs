using UnityEngine;

public class CameraCinematic : MonoBehaviour
{

    public delegate void FinishedCinematic();
    public static FinishedCinematic OnFinishedCinematic;
    Camera mainCam;
    int loopChild;

    bool doCinematic;

    void Awake()
    {
        mainCam = Camera.main;
        Invoke(nameof(StartCinematic), 0.5f);
    }

    void StartCinematic()
    {
        doCinematic = true;
    }

    void Update()
    {
        if (doCinematic)
        {
            DoCinematic();
        }
    }

    void DoCinematic()
    {
        if (loopChild < transform.GetChild(0).childCount)
        {
            if (Vector3.Distance(mainCam.transform.position, transform.GetChild(0).GetChild(loopChild).position) < 1)
            {
                loopChild++;
                Invoke(nameof(EnableNext), 0.4f);
            }
        }
        else
        {
            transform.GetChild(1).gameObject.SetActive(true);
            if (Vector3.Distance(mainCam.transform.position, transform.GetChild(1).position) < 1)
            {
                Debug.Log("End");
                OnFinishedCinematic?.Invoke();
                enabled = false;
            }
            
        }
    }

    void EnableNext()
    {
        if (loopChild < transform.GetChild(0).childCount)
        {
            transform.GetChild(0).GetChild(loopChild).gameObject.SetActive(true);
        }
    }

}
