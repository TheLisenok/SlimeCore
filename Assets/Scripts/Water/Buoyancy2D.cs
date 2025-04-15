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

    // ������ �� ��������� ���� (EdgeCollider2D)
    private Collider2D waterCollider;

    void Start()
    {
        waterCollider = GetComponent<Collider2D>();
        if (waterCollider == null)
            Debug.LogError("Collider �� ������ �� ������� ����!");
    }

    void OnTriggerStay2D(Collider2D other)
    {
        Rigidbody2D rb = other.attachedRigidbody;
        if (rb == null)
            return;

        // ���������� ������� ����������� ���� �� ������� ������� ����������
        float waterSurface = waterCollider.bounds.max.y;

        // ������� �������
        float objectBottom = other.bounds.min.y;
        float objectTop = other.bounds.max.y;
        float objectHeight = objectTop - objectBottom;

        // ������������, ����� ����� ������� ��������� � ����
        float submergedHeight = waterSurface - objectBottom;
        submergedHeight = Mathf.Clamp(submergedHeight, 0, objectHeight);
        float submergedFraction = submergedHeight / objectHeight;

        // ������ ��������� ���� (������� ������ �� ����� ��������)
        float upwardForce = density * submergedFraction * buoyancyCoefficient * rb.mass * Physics2D.gravity.magnitude;

        // ��������� ���� �����
        rb.AddForce(Vector2.up * upwardForce);

        // ��������� ������������� ��� �������� �������� ����
        rb.drag = drag;
        rb.angularDrag = angularDrag;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // ����� �������� �������������, ����� ������ ������� �� ����
        Rigidbody2D rb = other.attachedRigidbody;
        if (rb != null)
        {
            rb.drag = 0f;
            rb.angularDrag = 0f;
        }
    }
}
