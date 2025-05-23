using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTriggerHandler : MonoBehaviour
{
    [SerializeField] private LayerMask _waterMask;
    [SerializeField] private GameObject _splashParticles;

    private EdgeCollider2D _edgeCollider;

    private InteractableWater _interactableWater;

    private void Awake()
    {
        _edgeCollider = GetComponent<EdgeCollider2D>();
        _interactableWater = GetComponent<InteractableWater>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If our collision gameObject is within the waterMask LayerMask
        if ((_waterMask.value & (1 << collision.gameObject.layer)) > 0)
        {
            Rigidbody2D rb = collision.GetComponentInParent<Rigidbody2D>();

            if (rb != null)
            {
                // Spawn splash particles
                Vector2 localPos = gameObject.transform.localPosition;
                Vector2 hitObjectPos = collision.transform.position;
                Bounds hitObjectBounds = collision.bounds;

                Vector3 spawnPos = Vector3.zero;
                if (collision.transform.position.y >= _edgeCollider.points[1].y + _edgeCollider.offset.y + localPos.y)
                {
                    // Hit from above
                    spawnPos = hitObjectPos - new Vector2(0f, hitObjectBounds.extents.y);
                }
                else
                {
                    // Hit from below
                    spawnPos = hitObjectPos + new Vector2(0f, hitObjectBounds.extents.y);
                }

                GameObject splash = Instantiate(_splashParticles, spawnPos, Quaternion.identity);
                StartCoroutine(DestroyAfterParticles(splash));

                // Clamp splash point to a MAX velocity

                int multiplier = 1;

                if (rb.velocity.y < 0)
                {
                    multiplier = -1;
                }
                else
                {
                    multiplier = 1;
                }

                float vel = rb.velocity.y * _interactableWater.ForceMultiplier;
                vel = Mathf.Clamp(Mathf.Abs(vel), 0f, _interactableWater.MaxForce);
                vel *= multiplier;

                _interactableWater.Splash(collision, vel);
            }
        }
    }

    private IEnumerator DestroyAfterParticles(GameObject particleObject)
    {
        ParticleSystem ps = particleObject.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            yield return new WaitUntil(() => !ps.IsAlive());
        }
        Destroy(particleObject);
    }
}
