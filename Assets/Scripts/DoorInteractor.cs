using System;
using UnityEngine;

public class DoorInteractor : MonoBehaviour
{
    [SerializeField] private LayerMask _doorLayerMask;
    [SerializeField] private float _distance;
    
    private bool _hasLastDoor;
    private Door _lastDoor;

    private void Awake()
    {
        _hasLastDoor = false;
    }

    private void Update()
    {
        var ray = new Ray(transform.position + Vector3.up, transform.forward);
        if (!Physics.Raycast(ray, out var hit, _distance, _doorLayerMask))
        {
            DeactivateLastDoorHint();
            return;
        }

        if (!hit.transform.TryGetComponent<Door>(out var doorComponent))
        {
            DeactivateLastDoorHint();
            return;
        }
        doorComponent.DisplayHints(true);
        if (_hasLastDoor)
        {
            if (_lastDoor != doorComponent)
            {
                DeactivateLastDoorHint();
                _hasLastDoor = true;
                _lastDoor = doorComponent;
            }
        }
        else
        {
            _hasLastDoor = true;
            _lastDoor = doorComponent;
        }
        
        if (!Input.GetKeyDown(KeyCode.E)) return;
        doorComponent.Interact();
    }

    private void DeactivateLastDoorHint()
    {
        if (!_hasLastDoor) return;
        _lastDoor.DisplayHints(false);
        _hasLastDoor = false;
        _lastDoor = null;
    }
}
