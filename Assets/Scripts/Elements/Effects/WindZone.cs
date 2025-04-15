using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a wind zone that applies an upward force to objects.
/// </summary>
public class WindZone : MonoBehaviour
{
    [SerializeField] private Vector2 windForce = new Vector2(0f, 5f); // Wind force (X - sideways, Y - upwards)
    [SerializeField] private float maxHeight = 5f; // Maximum height an object can be lifted
    [SerializeField] private float lifeTime = 3f; // Lifetime before destruction (if not permanent)
    [SerializeField] private bool isPermanent = false; // Determines whether the wind zone disappears after a while

    [SerializeField] private ParticleSystem particleSystem;

    private HashSet<Rigidbody2D> affectedObjects = new HashSet<Rigidbody2D>(); // Objects currently affected by the wind


    private void Start()
    {
        particleSystem.Play();

        if (!isPermanent)
            Destroy(gameObject, lifeTime); // Destroy the wind zone after its lifetime if it's not permanent
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            affectedObjects.Add(rb);
            StartCoroutine(ApplyWind(rb));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb != null && affectedObjects.Contains(rb))
        {
            affectedObjects.Remove(rb);
        }
    }

    /// <summary>
    /// Continuously applies wind force to objects inside the wind zone.
    /// </summary>
    private IEnumerator ApplyWind(Rigidbody2D rb)
    {
        while (affectedObjects.Contains(rb))
        {
            if (rb.transform.position.y < (maxHeight + this.gameObject.transform.position.y)) // Calculate max height considering global pos
            {
                rb.AddForce(windForce * Time.deltaTime);
            }
            yield return null;
        }
    }
}
