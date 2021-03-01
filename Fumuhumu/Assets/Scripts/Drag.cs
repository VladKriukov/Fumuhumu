using UnityEngine;

public class Drag : MonoBehaviour
{

    [SerializeField] Material movableMaterial;
    [SerializeField] Material normalMaterial;

    float mZCoord;
    Camera mainCamera;
    Vector3 mOffset;
    Vector3 mousePoint;

    void OnEnable()
    {
        GameManager.OnStartLevel += StartLevel;
    }

    void OnDisable()
    {
        GameManager.OnStartLevel -= StartLevel;
    }

    void Start()
    {
        if (movableMaterial && GetComponent<MeshRenderer>())
        {
            GetComponent<MeshRenderer>().material = movableMaterial;
        }
    }

    void StartLevel()
    {
        if (normalMaterial && GetComponent<MeshRenderer>())
        {
            GetComponent<MeshRenderer>().material = normalMaterial;
        }
    }

    void Awake()
    {
        mainCamera = Camera.main;
    }

    void OnMouseDown()
    {
        mZCoord = mainCamera.WorldToScreenPoint(gameObject.transform.position).z;
        Debug.Log("Mouse z: " + mZCoord);
        mOffset = gameObject.transform.position - GetMouseAsWorldPoint();
    }

    private Vector3 GetMouseAsWorldPoint()
    {
        mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return mainCamera.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        if (!GameManager.inGame && GameManager.canMoveStuff)
        {
            transform.position = GetMouseAsWorldPoint() + mOffset;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
        //rb.AddForce((GetMouseAsWorldPoint() + mOffset) * force);
    }
}
