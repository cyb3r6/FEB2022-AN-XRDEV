using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[System.Serializable]
public enum MoveScheme
{
    Continuous,
    Noncontinuous
}

[System.Serializable]
public enum TurnStyle
{
    Snap,
    Continuous
}

public class LocomotionManager : MonoBehaviour
{
    [SerializeField]
    private List<InputActionAsset> actionAssets;
    public List<string> actionMaps;
    public string baseControlScheme;
    public string nonContinuousControlScheme;
    public string continousControlScheme;
    public ContinuousMoveProviderBase continuousMoveProvider;
    public ContinuousTurnProviderBase continuousTurnProvider;
    public SnapTurnProviderBase snapTurnProvider;
    public List<InputActionReference> actions;

    [SerializeField]
    private MoveScheme _moveScheme;
    public MoveScheme MoveScheme
    {
        get => _moveScheme;
        set
        {
            SetMoveScheme(value);
            _moveScheme = value;
        }
    }
    [SerializeField]
    private TurnStyle _turnStyle;
    public TurnStyle TurnStyle
    {
        get => _turnStyle;
        set
        {
            SetTurnStyle(value);
            _turnStyle = value;
        }
    }

    void Start()
    {
        SetMoveScheme(MoveScheme);
        SetTurnStyle(TurnStyle);
    }


    private void SetMoveScheme(MoveScheme scheme)
    {
        switch (scheme)
        {
            case MoveScheme.Noncontinuous:
                // set the binding masks for the noncontinuous control scheme
                SetBindingMasks(nonContinuousControlScheme);
                if (continuousMoveProvider != null)
                {
                    continuousMoveProvider.enabled = false;
                }
                break;
            case MoveScheme.Continuous:
                // set the binding masks for the continuous control scheme
                SetBindingMasks(continousControlScheme);
                if(continuousMoveProvider != null)
                {
                    continuousMoveProvider.enabled = true;
                }
                break;
            default:
                throw new NotImplementedException();
        }
    }
    private void SetTurnStyle(TurnStyle style)
    {
        switch (style)
        {
            case TurnStyle.Snap:
                if (continuousTurnProvider != null)
                {
                    continuousTurnProvider.enabled = false;
                }
                if(snapTurnProvider != null)
                {
                    snapTurnProvider.enabled = false;
                    snapTurnProvider.enabled = true;
                    snapTurnProvider.enableTurnLeftRight = true;
                }
                break;
            case TurnStyle.Continuous:
                if(snapTurnProvider != null)
                {
                    snapTurnProvider.enableTurnLeftRight = false;
                }
                if (continuousTurnProvider != null)
                {
                    continuousTurnProvider.enabled = true;
                }
                break;
            default:
                throw new NotImplementedException();
        }
    }

    void SetBindingMasks(string controlSchemeName)
    {
        foreach (var actionReference in actions)
        {
            if (actionReference == null)
                continue;

            var action = actionReference.action;
            if (action == null)
            {
                Debug.LogError($"Cannot set binding mask on {actionReference} since the action could not be found.", this);
                continue;
            }

            // Get the (optional) base control scheme and the control scheme to apply on top of base
            var baseInputControlScheme = FindControlScheme(baseControlScheme, actionReference);
            var inputControlScheme = FindControlScheme(controlSchemeName, actionReference);

            action.bindingMask = GetBindingMask(baseInputControlScheme, inputControlScheme);
        }

        if (actionMaps.Count > 0 && actionAssets.Count == 0)
        {
            Debug.LogError($"Cannot set binding mask on action maps since no input action asset references have been set.", this);
        }

        foreach (var actionAsset in actionAssets)
        {
            if (actionAsset == null)
                continue;

            // Get the (optional) base control scheme and the control scheme to apply on top of base
            var baseInputControlScheme = FindControlScheme(baseControlScheme, actionAsset);
            var inputControlScheme = FindControlScheme(controlSchemeName, actionAsset);

            if (actionMaps.Count == 0)
            {
                actionAsset.bindingMask = GetBindingMask(baseInputControlScheme, inputControlScheme);
                continue;
            }

            foreach (var mapName in actionMaps)
            {
                var actionMap = actionAsset.FindActionMap(mapName);
                if (actionMap == null)
                {
                    Debug.LogError($"Cannot set binding mask on \"{mapName}\" since the action map not be found in '{actionAsset}'.", this);
                    continue;
                }

                actionMap.bindingMask = GetBindingMask(baseInputControlScheme, inputControlScheme);
            }
        }
    }

    void ClearBindingMasks()
    {
        SetBindingMasks(string.Empty);
    }

    InputControlScheme? FindControlScheme(string controlSchemeName, InputActionReference action)
    {
        if (action == null)
            throw new ArgumentNullException(nameof(action));

        if (string.IsNullOrEmpty(controlSchemeName))
            return null;

        var asset = action.asset;
        if (asset == null)
        {
            Debug.LogError($"Cannot find control scheme \"{controlSchemeName}\" for '{action}' since it does not belong to an {nameof(InputActionAsset)}.", this);
            return null;
        }

        return FindControlScheme(controlSchemeName, asset);
    }

    InputControlScheme? FindControlScheme(string controlSchemeName, InputActionAsset asset)
    {
        if (asset == null)
            throw new ArgumentNullException(nameof(asset));

        if (string.IsNullOrEmpty(controlSchemeName))
            return null;

        var scheme = asset.FindControlScheme(controlSchemeName);
        if (scheme == null)
        {
            Debug.LogError($"Cannot find control scheme \"{controlSchemeName}\" in '{asset}'.", this);
            return null;
        }

        return scheme;
    }

    static InputBinding? GetBindingMask(InputControlScheme? baseInputControlScheme, InputControlScheme? inputControlScheme)
    {
        if (inputControlScheme.HasValue)
        {
            return baseInputControlScheme.HasValue
                ? InputBinding.MaskByGroups(baseInputControlScheme.Value.bindingGroup, inputControlScheme.Value.bindingGroup)
                : InputBinding.MaskByGroup(inputControlScheme.Value.bindingGroup);
        }

        return baseInputControlScheme.HasValue
            ? InputBinding.MaskByGroup(baseInputControlScheme.Value.bindingGroup)
            : (InputBinding?)null;
    }
}
