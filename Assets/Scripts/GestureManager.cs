using UnityEngine;

public class GestureManager : MonoBehaviour
{
    static public GestureManager Instance { get; private set; }
    [SerializeField] float touchTolerance = 25;
    [SerializeField] float holdDuration = 0.5f;
    Vector2 startPosition = Vector2.zero;
    Touch touch;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else if (Instance)
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        if (Input.touchCount == 1)
        {
            touch = Input.GetTouch(0);
        }
    }

    public bool OnTap()
    {
        if (touch.phase == TouchPhase.Began)
        {
            startPosition = touch.position;
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            float distance = (touch.position - startPosition).magnitude;

            if (distance <= touchTolerance)
            {
                Debug.Log("Tap!");
                return true;
            }
        }

        return false;
    }

    public bool OnStrictTap()
    {
        float timer = 0f;
        if (touch.phase == TouchPhase.Began)
        {
            timer += Time.deltaTime;
            startPosition = touch.position;
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            float distance = (touch.position - startPosition).magnitude;

            // Check timer & swipe
            if (distance <= touchTolerance && timer <= holdDuration)
            {
                Debug.Log("Tap!");
                return true;
            }
        }

        return false;
    }

    public bool OnHold()
    {
        float timer = 0f;
        if (touch.phase == TouchPhase.Began)
        {
            timer += Time.deltaTime;
            startPosition = touch.position;
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            float distance = (touch.position - startPosition).magnitude;

            // Check timer & swipe
            if (distance <= touchTolerance && timer >= holdDuration)
            {
                Debug.Log("Hold");
                return true;
            }
        }

        return false;
    }
}
