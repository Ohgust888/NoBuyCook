using UnityEngine;

public class EarthEffect : MonoBehaviour
{
    public float upSpeed = 5.0f;
    Rigidbody2D rigidbody2d;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        AnimalController animal = other.gameObject.GetComponent<AnimalController>();

        if (animal != null)
        {
            Vector2 velocity = animal.rigidbody2d.linearVelocity;
            velocity.y = upSpeed;
            animal. rigidbody2d.linearVelocity = velocity;
        }
    }

}
