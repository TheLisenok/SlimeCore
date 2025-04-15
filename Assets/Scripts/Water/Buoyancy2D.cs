using UnityEngine;

public class Buoyancy2D : MonoBehaviour
{
    [Header("Water Settings")]
    [Tooltip("Water density (the higher, the stronger the lifting force)")]
    public float density = 1f;

    [Tooltip("Lift coefficient")]
    public float buoyancyCoefficient = 1f;

    [Tooltip("Linear resistance of water")]
    public float drag = 1f;

    [Tooltip("Angular resistance of water")]
    public float angularDrag = 1f;

    // Ссылка на коллайдер воды (EdgeCollider2D)
    private Collider2D waterCollider;

    void Start()
    {
        waterCollider = GetComponent<Collider2D>();
        if (waterCollider == null)
            Debug.LogError("Collider не найден на объекте воды!");
    }

    void OnTriggerStay2D(Collider2D other)
    {
        Rigidbody2D rb = other.attachedRigidbody;
        if (rb == null)
            return;

        // Определяем уровень поверхности воды по верхней границе коллайдера
        float waterSurface = waterCollider.bounds.max.y;

        // Границы объекта
        float objectBottom = other.bounds.min.y;
        float objectTop = other.bounds.max.y;
        float objectHeight = objectTop - objectBottom;

        // Рассчитываем, какая часть объекта погружена в воду
        float submergedHeight = waterSurface - objectBottom;
        submergedHeight = Mathf.Clamp(submergedHeight, 0, objectHeight);
        float submergedFraction = submergedHeight / objectHeight;

        // Расчёт подъёмной силы (формула похожа на закон Архимеда)
        float upwardForce = density * submergedFraction * buoyancyCoefficient * rb.mass * Physics2D.gravity.magnitude;

        // Применяем силу вверх
        rb.AddForce(Vector2.up * upwardForce);

        // Добавляем сопротивление для имитации вязкости воды
        rb.drag = drag;
        rb.angularDrag = angularDrag;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Сброс значений сопротивления, когда объект выходит из воды
        Rigidbody2D rb = other.attachedRigidbody;
        if (rb != null)
        {
            rb.drag = 0f;
            rb.angularDrag = 0f;
        }
    }
}
