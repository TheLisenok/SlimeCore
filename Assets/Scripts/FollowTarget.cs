using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target; // Цель, за которой следуем
    public Vector3 offset = new Vector3(0, 5, -10); // Смещение
    public float smoothSpeed = 5f; // Скорость следования
    public bool useFixedUpdate = false; // Использовать FixedUpdate для физики

    void LateUpdate()
    {
        if (!useFixedUpdate)
        {
            Follow();
        }
    }

    void FixedUpdate()
    {
        if (useFixedUpdate)
        {
            Follow();
        }
    }

    void Follow()
    {
        if (target == null)
        {
            Debug.LogWarning("FollowTarget: Цель не назначена!");
            return;
        }

        Vector3 desiredPosition = target.position + offset; // Позиция с учетом offset
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}
