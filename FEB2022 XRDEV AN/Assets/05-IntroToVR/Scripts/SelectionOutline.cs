using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionOutline : MonoBehaviour
{
    [SerializeField]
    private Renderer rendererToBeHighlighted;

    private float highlighted = 0.0f;
    private MaterialPropertyBlock block;
    private int highlightID;

    void Start()
    {
        // get the renderer
        if (rendererToBeHighlighted == null)
        {
            rendererToBeHighlighted = GetComponent<Renderer>();
        }

        // get the shader property to adjust
        highlightID = Shader.PropertyToID("HighlightActive");

        // set the shader property
        block = new MaterialPropertyBlock();
        block.SetFloat(highlightID, highlighted);
        rendererToBeHighlighted.SetPropertyBlock(block);

    }

    public void Highlight()
    {
        highlighted = 1.0f;

        // get shader property block
        rendererToBeHighlighted.GetPropertyBlock(block);

        // set shader property block
        block.SetFloat(highlightID, highlighted);
        rendererToBeHighlighted.SetPropertyBlock(block);
    }

    public void UnHighlight()
    {
        highlighted = 0.0f;

        // get shader property block
        rendererToBeHighlighted.GetPropertyBlock(block);

        // set shader property block
        block.SetFloat(highlightID, highlighted);
        rendererToBeHighlighted.SetPropertyBlock(block);
    }
}