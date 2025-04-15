using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Represent a fire whirlwild effect that can set objects on fire.
/// </summary>
public class FireWhirlwind : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private float timer = 0.4f; // Duration of the whirlwind effect

    private bool isActive = true; // Controls whether the whirlwind applies the fire status

    private void Start()
    {
        _particleSystem.Play();
        GetComponent<CircleCollider2D>().enabled = true;
        StartCoroutine(DisableAfterTime());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isActive) return; // If the whirlwind is inactive, ignore collisions

        if (collision.gameObject.CompareTag("Destructible"))
        {
            try
            {
                Destructible destructible = collision.gameObject.GetComponent<Destructible>();
                if (destructible != null)
                {
                    destructible.Burn();
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
    /// Disables the fire whirlwind effect after a set duration.
    /// </summary>
    private IEnumerator DisableAfterTime()
    {
        yield return new WaitForSeconds(timer);

        // Disable fire status effect
        isActive = false;
        GetComponent<CircleCollider2D>().enabled = false;

        _particleSystem.Stop();

        // Wait until all particles have disappeared
        yield return new WaitUntil(() => !_particleSystem.IsAlive());

        Destroy(gameObject);
    }
}
