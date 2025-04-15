using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    #region Singletone
    public static CameraManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Удаляем дубликат, если он есть
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Чтобы объект не уничтожался при смене сцен


        defaultZoom = _virtualCam.m_Lens.OrthographicSize;
    }
    #endregion

    private void Start()
    {
        if (defaultFollow == null)
            Debug.LogError("No object to default camera follow!");
        if (_virtualCam == null)
        {
            Debug.LogError("No virtual camera!");
        }
    }

    public CinemachineVirtualCamera _virtualCam;

    [SerializeField] private Transform defaultFollow;
    [SerializeField] private float zoomDuration = 0.5f;
    private float defaultZoom;

    public void SetFollowTarget(Transform target)
    {
        _virtualCam.Follow = target;
    }

    public void ResetFollowTarget()
    {
        _virtualCam.Follow = defaultFollow;
    }

    public void SetZoom(float requiredSize)
    {
        StartCoroutine(ChangeZoomSmooth(requiredSize, zoomDuration));
        
        //_virtualCam.m_Lens.OrthographicSize = requiredSize;
    }

    public void ResetZoom()
    {
        StartCoroutine(ChangeZoomSmooth(defaultZoom, zoomDuration));
        
        //_virtualCam.m_Lens.OrthographicSize = defaultZoom;
    }

    private IEnumerator ChangeZoomSmooth(float targetSize, float duration)
    {
        float startSize = _virtualCam.m_Lens.OrthographicSize;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            _virtualCam.m_Lens.OrthographicSize = Mathf.Lerp(startSize, targetSize, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _virtualCam.m_Lens.OrthographicSize = targetSize; // Final size
    }
}
