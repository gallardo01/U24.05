using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileJoystick : Singleton<MobileJoystick>
{
    [Header(" Elements ")]
    [SerializeField] private RectTransform joystickOutline;
    [SerializeField] private RectTransform joystickKnob;

    [Header(" Settings ")]
    [SerializeField] private float moveFactor;
    private Vector3 clickedPosition;
    private Vector3 move;
    private bool canControl;
    private float canvasScale;

    private static MobileJoystick instance; public static MobileJoystick Instance { get { return instance; } }

    void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(this.gameObject);

        canvasScale = GetComponentInParent<Canvas>().GetComponent<RectTransform>().localScale.x;
        HideJoystick();
    }

    private void OnDisable()
    {
        HideJoystick();
    }

    void Update()
    {
        if(canControl) ControlJoystick();
    }

    public void ClickedOnJoystickZoneCallback()
    {
        clickedPosition = Input.mousePosition;
        joystickOutline.position = clickedPosition;

        ShowJoystick();
    }

    private void ShowJoystick()
    {
        joystickOutline.gameObject.SetActive(true);
        canControl = true;
    }

    private void HideJoystick()
    {
        joystickOutline.gameObject.SetActive(false);
        canControl = false;

        move = Vector3.zero;
    }

    private void ControlJoystick()
    {
        Vector3 currentPosition = Input.mousePosition;
        Vector3 direction = currentPosition - clickedPosition;

        float moveMagnitude = direction.magnitude * moveFactor * canvasScale;
        float absoluteWidth = joystickOutline.rect.width / 2;
        float realWidth = absoluteWidth * canvasScale;

        moveMagnitude = Mathf.Min(moveMagnitude, realWidth);
        move = direction.normalized * moveMagnitude;
        
        Vector3 targetPosition = clickedPosition + move;

        joystickKnob.position = targetPosition;

        if (Input.GetMouseButtonUp(0)) HideJoystick();
    }

    public Vector3 GetMoveVector() => move / canvasScale;
}
