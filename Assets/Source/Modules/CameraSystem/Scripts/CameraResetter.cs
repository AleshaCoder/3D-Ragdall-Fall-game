using Assets.Source.CameraSystem.Scripts;
using System.Collections;
using UnityEngine;

public class CameraResetter : MonoBehaviour
{
    [SerializeField] private Vector3 defaultPosition;
    [SerializeField] private Vector3 defaultLocalPosition;
    [SerializeField] private Vector3 localEulerAngles;
    [SerializeField] private Vector3 localEulerAngles2;
    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private Transform _camera;
    [SerializeField] private CameraMover _mover;

    private bool isInitialized = false;

    private void OnValidate()
    {
        defaultPosition = transform.position;
        defaultLocalPosition = transform.localPosition;
        localEulerAngles = _cameraPivot.localEulerAngles;
        localEulerAngles2 = _camera.localEulerAngles;
    }

    private void Awake()
    {
        if (!isInitialized)
        {
            defaultLocalPosition = transform.localPosition;
            defaultPosition = transform.position;
            localEulerAngles = _cameraPivot.localEulerAngles;
            localEulerAngles2 = _camera.localEulerAngles;
            isInitialized = true;
            Debug.Log("Cam init " + defaultPosition);
        }
    }

    void OnEnable()
    {
        // ResetCamera();
    }

    public void ResetCamera()
    {
        _mover.enabled = false;
        transform.localPosition = defaultLocalPosition;
        transform.position = defaultPosition;
        _cameraPivot.localEulerAngles = localEulerAngles;
        _camera.localEulerAngles = localEulerAngles2;
        _mover.enabled = true;
        _mover.gameObject.SetActive(true);
        StartCoroutine(ActivateMover());
    }

    IEnumerator ActivateMover()
    {
        yield return null;

    }
}
