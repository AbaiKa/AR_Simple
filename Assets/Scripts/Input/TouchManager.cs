using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class TouchManager : MonoBehaviour
{
    public static TouchManager Instance { get; private set; }

    public event Action<Vector2> onClick;
    public event Action<Vector3> onClickToTarget;

    [SerializeField] private ARRaycastManager raycastManager;
    [SerializeField] private InputActionManager playerInput;

    private InputAction clickPositionAction;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        Instance = this;
        clickPositionAction = playerInput.actionAssets[0]["Point"];
    }

    private void Start()
    {
        clickPositionAction.performed += InputClick;
    }
    private void OnDisable()
    {
        clickPositionAction.performed -= InputClick;
    }


    private void InputClick(InputAction.CallbackContext context)
    {
        Vector2 position = context.ReadValue<Vector2>();

        onClick?.Invoke(position);

        if (raycastManager.Raycast(position, hits, TrackableType.PlaneWithinPolygon))
        {
            onClickToTarget?.Invoke(hits[0].pose.position);
        }
    }
}
