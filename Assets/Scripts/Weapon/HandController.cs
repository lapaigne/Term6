using UnityEngine;
public class HandController : MonoBehaviour
{
    public FloatReference speed;
    public FloatReference lifeTime;
    public Rigidbody rb;

    private bool launched = false;
    private float distance;
    private Vector2 destination;
    private Vector2 currentDirection = Vector2.zero;

    private float _timer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _timer = -1;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision);
        if (launched)
        {
            Explode();
        }
    }

    public void Launch(Vector2 initialDirection)
    {
        launched = true;
        currentDirection = initialDirection.normalized;
        _timer = 0;
    }

    private void Update()
    {
        if (_timer < 0) { return; }

        if (_timer >= .3f)
        {
            destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentDirection = destination - (Vector2)transform.position;
            distance = currentDirection.magnitude;

            if (distance < .1f) { Explode(); }

            currentDirection.Normalize();
        }

        rb.velocity = currentDirection * speed;

        _timer += Time.deltaTime;
        if (_timer >= lifeTime) { Explode(); }
    }

    private void Explode()
    {
        Debug.Log("KABOOM!");
        Destroy(gameObject);
    }
}
