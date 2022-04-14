using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocomotionSwitcher : MonoBehaviour
{
    public LocomotionManager locomotionManager;
    public Toggle continuousMoveToggle;
    public Toggle continuousTurnToggle;

    
    void Start()
    {
        SubscribeToContinuousMove(continuousMoveToggle);
        SubscribeToContinuousTurn(continuousTurnToggle);
    }

    private void SubscribeToContinuousMove(Toggle toggle)
    {
        var continuousMove = locomotionManager.MoveScheme == MoveScheme.Continuous;
        toggle.isOn = continuousMove;
        toggle.onValueChanged.AddListener(OnContinousMoveToggleChanged);
    }
    private void SubscribeToContinuousTurn(Toggle toggle)
    {
        var continuousTurn = locomotionManager.TurnStyle == TurnStyle.Continuous;
        toggle.isOn = continuousTurn;
        toggle.onValueChanged.AddListener(OnContinuousTurnToggleChanged);
    }

    private void OnContinousMoveToggleChanged(bool value)
    {
        locomotionManager.MoveScheme = value ? MoveScheme.Continuous : MoveScheme.Noncontinuous;
    }

    private void OnContinuousTurnToggleChanged(bool value)
    {
        locomotionManager.TurnStyle = value ? TurnStyle.Continuous : TurnStyle.Snap;
    }
}
