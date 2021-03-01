using UnityEngine;
using UnityEngine.Events;

public class Activatable : MonoBehaviour
{
    [SerializeField] bool initiallyActivated;
    public UnityEvent activate;
    public UnityEvent deActivate;

    bool activated;

    void OnEnable()
    {
        activated = initiallyActivated;
        Activate();
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && GameManager.inGame)
        {
            //Debug.Log("Mouse clicked on " + gameObject.name);
            activated = !activated;
            Activate();
        }
    }

    void Activate()
    {
        if (activated)
        {
            activate.Invoke();
        }
        else
        {
            deActivate.Invoke();
        }
    }

}
