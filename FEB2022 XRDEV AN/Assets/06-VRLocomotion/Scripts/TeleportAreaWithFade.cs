using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class TeleportAreaWithFade : TeleportationArea
{
    public ScreenFader screenFader;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if(teleportTrigger == TeleportTrigger.OnSelectEntered)
        {
            StartCoroutine(FadeSequence(base.OnSelectEntered, args));
        }
    }
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        if (teleportTrigger == TeleportTrigger.OnSelectExited)
        {
            StartCoroutine(FadeSequence(base.OnSelectExited, args));
        }
    }
    protected override void OnActivated(ActivateEventArgs args)
    {
        if (teleportTrigger == TeleportTrigger.OnActivated)
        {
            StartCoroutine(FadeSequence(base.OnActivated, args));
        }
    }
    protected override void OnDeactivated(DeactivateEventArgs args)
    {
        if (teleportTrigger == TeleportTrigger.OnDeactivated)
        {
            StartCoroutine(FadeSequence(base.OnDeactivated, args));
        }
    }

    private IEnumerator FadeSequence<T>(UnityAction<T> action, T args)
        where T: BaseInteractionEventArgs
    {
        // fade to black
        float duration = screenFader.FadeIn();
        yield return new WaitForSeconds(duration);
        action.Invoke(args);

        // fade to clear
        screenFader.FadeOut();
    }
}
