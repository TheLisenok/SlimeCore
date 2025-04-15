using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZone : Interactable
{

    private BoxCollider2D m_Collider;

    private void Awake()
    {
        m_Collider = GetComponent<BoxCollider2D>();

        if (m_Collider == null)
            Debug.LogError("Missing collider in CameraZone " + this.gameObject.name);
    }

    public override void OnInteract()
    {
        base.OnInteract();

        // Camera target
        CameraManager.Instance.SetFollowTarget(this.gameObject.transform);

        // Camera zoom
        Bounds bounds = m_Collider.bounds;
        float colliderWidth = bounds.size.x;
        float colliderHeight = bounds.size.y;

        float screenAspect = (float)Screen.width / Screen.height;
        float requiredSize = colliderHeight / 2f;

        float horizontalSize = (colliderWidth / 2f) / screenAspect;
        if (horizontalSize > requiredSize)
        {
            requiredSize = horizontalSize;
        }

        CameraManager.Instance.SetZoom(requiredSize);
    }

    public override void OnDeactivate()
    {
        base.OnDeactivate();

        CameraManager.Instance.ResetFollowTarget();
        CameraManager.Instance.ResetZoom();
    }
}
