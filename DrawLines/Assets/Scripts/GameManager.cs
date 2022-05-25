using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton class: GameManager
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    #endregion

    private Camera cam;

    public Ball Ball;
    public Trajectory Trajectory;

    [SerializeField] private float pushForce = 4f;

    private bool isDragging = false;

    private Vector2 startPoint;
    private Vector2 endPoint;
    private Vector2 direction;
    private Vector2 force;
    private float distance;

    private void Start()
    {
        cam = Camera.main;
        Ball.DesactivateRb();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            OnDragStart();
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            OnDragEnd();
        }

        if (isDragging)
        {
            OnDrag();
        }
    }

    public void OnDragStart()
    {
        Ball.DesactivateRb();
        startPoint = cam.ScreenToWorldPoint(Input.mousePosition);

        Trajectory.Show();
    }

    public void OnDrag()
    {
        endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        distance = Vector2.Distance(startPoint, endPoint);
        direction = (startPoint - endPoint).normalized;
        force = direction * distance * pushForce;

        Debug.DrawLine(startPoint, endPoint);

        Trajectory.UpdateDots(Ball.position, force);
    }

    public void OnDragEnd()
    {
        Ball.ActivateRb();
        Ball.Push(force);

        Trajectory.Hide();
    }
}
