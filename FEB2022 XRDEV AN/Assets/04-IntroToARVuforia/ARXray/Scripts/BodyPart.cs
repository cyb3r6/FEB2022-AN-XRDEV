using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyPart : MonoBehaviour
{
    public Image bladderIcon;
    public Image brainIcon;
    public Image heartIcon;
    public Image leftKidney;
    public Image rightKidney;
    public Image leftLung;
    public Image rightLung;
    public Image skeleton;
    public Image stomach;

    public List<Image> bodyParts = new List<Image>();

    public void OnBladderFound()
    {
        bladderIcon.color = new Color(1,1,1,1);
    }
    public void OnBrainFound()
    {
        brainIcon.color = new Color(1, 1, 1, 1);
    }
    public void OnHeartFound()
    {
        heartIcon.color = new Color(1, 1, 1, 1);
    }
    public void OnLeftKidneyFound()
    {
        leftKidney.color = new Color(1, 1, 1, 1);
    }
    public void OnRightKidneyFound()
    {
        rightKidney.color = new Color(1, 1, 1, 1);
    }
    public void OnLeftLungFound()
    {
        leftLung.color = new Color(1, 1, 1, 1);
    }
    public void OnRightLungFound()
    {
        rightLung.color = new Color(1, 1, 1, 1);
    }
    public void OnSkeletonFound()
    {
        skeleton.color = new Color(1, 1, 1, 1);
    }
    public void OnStomachFound()
    {
        stomach.color = new Color(1, 1, 1, 1);
    }

    public void OnBodyPartFound(int index)
    {
        bodyParts[index].color = new Color(1, 1, 1, 1);
    }
}
