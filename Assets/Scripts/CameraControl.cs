using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using Doublsb.Dialog;

public class CameraMouseFollow : MonoBehaviour
{
    public float velocity = 1f;
    public float clampedLeft, clampedRight, clampedUp, clampedDown;
    public bool cameraFollowMouse = true;
    public ScriptableMousePointer mousePointers;
    public LayerMask interactionLayerMask;
    Vector3 target;

    void Start()
    {
        target = transform.position;
    }

    void Update()
    {
        if (!InventoryUI.isOpen)
        {
            if (DialogManager.Instance.state == State.Deactivate) CameraMovement();
            if (Input.GetButtonDown("Interact")) CastRayInteraction();
        }
    }

    private void CameraMovement()
    {
        // Get Normalized Mouse Position
        Vector3 normalizedMousePosition = GetNormalizedScreenPosition(Input.mousePosition);

        // Move Camera
        transform.position = Vector3.Slerp(transform.position, target, Time.deltaTime * velocity);

        if (cameraFollowMouse && Mathf.Abs(normalizedMousePosition.x) > 0.7f || Mathf.Abs(normalizedMousePosition.y) > 0.7f)
        {
            // Save target position
            target = transform.position + normalizedMousePosition;

            // Clamp Camera Position
            target = new Vector3(Mathf.Clamp(target.x, clampedLeft, clampedRight), Mathf.Clamp(target.y, clampedDown, clampedUp), transform.position.z);
        }
    }

    void CastRayInteraction()
    {
        RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
        if (hit.transform != null && hit.transform.TryGetComponent<IMouseClickable>(out var clickable))
        {
            clickable.OnMouseClick();
        }
    }

    /// <summary>
    /// Normalizes pixel screen coordinates, being Vector2.zero for the center position
    /// </summary>
    Vector2 GetNormalizedScreenPosition(Vector2 screenPosition)
    {
        return new Vector2(((screenPosition.x / Screen.width) - 0.5f) * 2, ((screenPosition.y / Screen.height) - 0.5f) * 2);
    }

    [Button()]
    void ClampLeft()
    {
        clampedLeft = transform.position.x;
    }

    [Button()]
    void ClampDown()
    {
        clampedDown = transform.position.y;
    }

    [Button()]
    void ClampUp()
    {
        clampedUp = transform.position.y;
    }

    [Button()]
    void ClampRight()
    {
        clampedRight = transform.position.x;
    }

    [Button()]
    void MirrorClamp()
    {
        clampedUp = clampedDown * -1;
        clampedRight = clampedLeft * -1;
    }

}
