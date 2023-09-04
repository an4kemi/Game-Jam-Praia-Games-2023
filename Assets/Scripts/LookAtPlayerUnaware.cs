using UnityEngine;

public class LookAtPlayerUnaware : MonoBehaviour
{ 
    public Transform lookAtTarget;
    private Camera _playerCamera;

    
    private void Start()
    {
        _playerCamera = Camera.main;
    }
    
    private void Update()
    {
        var screenPos = _playerCamera.WorldToScreenPoint(lookAtTarget.position);
        var isVisibleOnScreen = screenPos.x > -100 && screenPos.x < Screen.width &&
                                screenPos.y > -100 && screenPos.y < Screen.height && screenPos.z > -100;

        if (!isVisibleOnScreen)
        {
            var lookDir = _playerCamera.transform.position - lookAtTarget.position;
            lookDir.y = 0;
            lookAtTarget.rotation = Quaternion.LookRotation(lookDir);
        }
    }
}
