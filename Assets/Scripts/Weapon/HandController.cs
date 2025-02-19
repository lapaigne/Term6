using UnityEngine;
public class HandController : MonoBehaviour
{
    public FloatReference speed;
    public FloatReference lifeTime;

    private Vector2 destination;
    private float distance;
    private Vector2 currentDirection;
    private float _timer;

    private void Awake()
    {
        _timer = -1;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision);
        Explode();
    }

    public void Launch(Vector2 initialDirection)
    {
        currentDirection = initialDirection.normalized;
        _timer = 0;
    }

    private void Update()
    {
        Debug.Log(currentDirection);
        if (_timer < 0) { return; }

        if (_timer >= .3f)
        {
            destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentDirection = destination - (Vector2)transform.position;
            distance = currentDirection.magnitude;

            if (distance < .1f) { Explode(); }

            currentDirection.Normalize();
        }

        transform.Translate(currentDirection * Time.deltaTime * speed);

        _timer += Time.deltaTime;
        if (_timer >= lifeTime) { Explode(); }
    }

    private void Explode()
    {
        Debug.Log("KABOOM!");
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
