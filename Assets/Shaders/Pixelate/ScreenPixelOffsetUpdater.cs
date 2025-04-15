using UnityEngine;

public class ScreenPixelOffsetUpdater : MonoBehaviour
{
    public Material pixelateMaterial;
    public Camera mainCamera;

    private void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    void Update()
    {
        // �������� �������� ������� ������ ������� � ��������
        Vector3 screenPos = mainCamera.WorldToScreenPoint(transform.position);

        // �����������: ����� ������� �����, ����� �������� �������� [0, 1)
        Vector2 offset = new Vector2(screenPos.x % 1f, screenPos.y % 1f);

        // �������� �������� � ������
        pixelateMaterial.SetVector("_ScreenPixelOffset", new Vector4(offset.x, offset.y, 0, 0));
    }
}
