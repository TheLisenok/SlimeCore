using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Represents an explosion effect that can destroy objects with the "Destructible" tag.
/// </summary>
public class Caboom : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private float timer = 0.4f; // Duration of the explosion effect

    private bool isActive = true; // Controls whether the explosion can apply destruction effects

    private void Start()
    {
        _particleSystem.Play();
        GetComponent<CircleCollider2D>().enabled = true;
        StartCoroutine(DisableAfterTime());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isActive) return; // If explosion is inactive, ignore collisions

        if (collision.gameObject.CompareTag("Destructible"))
        {
            try
            {
                // Attempt to get the Destructible component
                BreakableObject destructible = collision.gameObject.GetComponent<BreakableObject>();
                if (destructible != null)
                {
                    destructible.Break();
                }
                else
                {
                    throw new NullReferenceException("Destructible component is missing on the object.");
                }
            }
            catch (NullReferenceException e)
            {
                Debug.LogError($"Error: {e.Message} Object: {collision.gameObject.name}");
            }
        }
    }

    /// <summary>
    /// Disables the explosion effect after a set duration.
    /// </summary>
    private IEnumerator DisableAfterTime()
    {
        yield return new WaitForSeconds(timer);

        // Disable destruction effect
        isActive = false;
        GetComponent<CircleCollider2D>().enabled = false;

        _particleSystem.Stop();

        // Wait until all particles have disappeared
        yield return new WaitUntil(() => !_particleSystem.IsAlive());

        Destroy(gameObject);
    }
}
