using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public enum InputType
{
    ExceptEsc,
    All,
    MovementAndUse,
    Movement,
    Use,
    Tab,
    Esc
}

public struct ControlData
{
    public InputType inputType;
    public bool enable;

    //fix for scene change
    public bool resetValue;

    public ControlData(bool enable)
    {
        this.inputType = InputType.ExceptEsc;
        this.enable = enable;
        this.resetValue = true;
    }
    public ControlData(InputType inputType, bool enable)
    {
        this.inputType = inputType;
        this.enable = enable;
        this.resetValue = true;
    }
    public ControlData(InputType inputType, bool enable, bool resetValue)
    {
        this.inputType = inputType;
        this.enable = enable;
        this.resetValue = resetValue;
    }
}

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    [SerializeField] private Rigidbody2D rigidBody;

    Vector2 movement;

    Dictionary<InputType, bool> controlDict = default;


    [Header("Input Events")]
    [SerializeField] private VoidEventChannelSO OnUseClick;
    [SerializeField] private VoidEventChannelSO OnTabClick;
    [SerializeField] private VoidEventChannelSO OnEscClick;

    [Header("Events Listen")]
    [SerializeField] private ControlDataEventChannelSO inputControlChannel;


    void OnEnable()
    {
		if (inputControlChannel != null)
			inputControlChannel.OnEventRaised += InputControl;
    }
    void OnDisable()
    {
        if (inputControlChannel != null)
			inputControlChannel.OnEventRaised -= InputControl;
    }


    void Awake()
    {
        InitInput();
    }


    void InitInput()
    {
        controlDict = new Dictionary<InputType, bool>();
        foreach (InputType inputType in System.Enum.GetValues(typeof(InputType)))
        {
            controlDict[inputType] = true;
        }
    }

    void ResetMovement()
    {
        movement.x = 0f;
        movement.y = 0f;
    }


    void FixedUpdate()
    {
        // Перемещение игрока
        rigidBody.MovePosition(rigidBody.position + movement * moveSpeed * Time.fixedDeltaTime);
    }


    public void InputControl(ControlData controlData)
    {
        switch (controlData.inputType)
        {
            case InputType.All:
                ControlALLExcept(controlData.enable, InputType.All);
                break;
            case InputType.ExceptEsc:
                ControlALLExcept(controlData.enable, InputType.Esc);
                break;
            case InputType.MovementAndUse:
                controlDict[InputType.Use] = controlData.enable;
                controlDict[InputType.Movement] = controlData.enable;
                break;
            case InputType.Movement:
            case InputType.Use:
            case InputType.Tab:
            case InputType.Esc:
                Debug.Log($"{controlData.inputType} {controlData.enable}");
                controlDict[controlData.inputType] = controlData.enable;
                break;
            default:
                Debug.Log($"Неизвестный тип {controlData.inputType}");
                break;
        }
        if(controlData.resetValue == true) ResetMovement();
    }
    void ControlALLExcept(bool enable, InputType exceptType)
    {
        Dictionary<InputType, bool> copyDict = new Dictionary<InputType, bool>(controlDict);

        //line.Key and line.Value
        foreach(KeyValuePair<InputType, bool> line in copyDict)
        {
            if(line.Key == exceptType) continue; // got to next iteration and skip other instructions
            controlDict[line.Key] = enable;
        }
    }

    // new input system
    void OnMove(InputValue inputValue)
	{
        if(!controlDict[InputType.Movement]) return;
        movement = inputValue.Get<Vector2>();

        //Debug.Log("Move click");
	}
    void OnUse(InputValue inputValue)
	{
        if(!controlDict[InputType.Use]) return;
        OnUseClick.RaiseEvent();

        //Debug.Log("Use click");
	}
    void OnTab(InputValue inputValue)
	{
        if(!controlDict[InputType.Tab]) return;
        OnTabClick.RaiseEvent();

        //Debug.Log("Tab click");
	}
    void OnEsc(InputValue inputValue)
	{
        if(!controlDict[InputType.Esc]) return;
        OnEscClick.RaiseEvent();

        //Debug.Log("Esc click");
	}


}