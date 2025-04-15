using UnityEngine;
using UnityEngine.U2D;

public class SlimeShapeController : MonoBehaviour
{
    public Transform[] points; // Внешние точки
    public SpriteShapeController spriteShape; // Sprite Shape Controller
    public float spineOffset = 0.1f; // Смещение для сплайна

    private CircleCollider2D[] colliders;

    private void Start()
    {
        colliders = new CircleCollider2D[points.Length];
        for (int i = 0; i < points.Length; i++)
        {
            colliders[i] = points[i].gameObject.GetComponent<CircleCollider2D>();
        }
    }

    private void Update()
    {
        UpdateVertices();
    }

    private void UpdateVertices()
    {
        if (points == null || spriteShape == null || spriteShape.spline == null)
        {
            Debug.LogWarning("Points or SpriteShape is not assigned.");
            return;
        }

        for (int i = 0; i < points.Length; i++)
        {
            if (points[i] == null || colliders[i] == null)
            {
                Debug.LogWarning($"Point or collider at index {i} is not assigned.");
                continue;
            }

            Vector2 _vertex = points[i].localPosition;
            Vector2 _towardsCenter = -_vertex.normalized;

            float _colliderRadius = colliders[i].radius;
            float _radiusOffset = _colliderRadius + spineOffset;

            Vector2 targetPosition = _vertex - _towardsCenter * _radiusOffset;
            Vector2 currentPosition = spriteShape.spline.GetPosition(i);

            // Плавное смещение
            spriteShape.spline.SetPosition(i, Vector2.Lerp(currentPosition, targetPosition, 0.1f));

            UpdateTangents(i, _towardsCenter);
        }
    }

    private void UpdateTangents(int index, Vector2 towardsCenter)
    {
        Vector2 _lt = spriteShape.spline.GetLeftTangent(index);
        Vector2 _newRt = new Vector2(-towardsCenter.y, towardsCenter.x) * _lt.magnitude;
        Vector2 _newLt = -_newRt;

        spriteShape.spline.SetRightTangent(index, _newRt);
        spriteShape.spline.SetLeftTangent(index, _newLt);
    }
}