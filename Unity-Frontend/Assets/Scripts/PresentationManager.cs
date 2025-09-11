using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PresentationManager : MonoBehaviour
{
    [Header("Projector Settings")]
    [Tooltip("UI Image that displays the slides (e.g., the projector canvas Image)")]
    public Image projectionImage;

    [Tooltip("List of slide textures (set in Inspector or at runtime)")]
    public List<Texture2D> slideTextures = new List<Texture2D>();

    private int currentIndex = 0;

    void Start()
    {
        if (slideTextures.Count > 0)
        {
            ShowSlide(currentIndex);
        }
    }

    /// <summary>
    /// Go to the next slide if available.
    /// </summary>
    public void NextSlide()
    {
        if (slideTextures.Count == 0) return;

        currentIndex = Mathf.Min(currentIndex + 1, slideTextures.Count - 1);
        ShowSlide(currentIndex);
    }

    /// <summary>
    /// Go to the previous slide if available.
    /// </summary>
    public void PrevSlide()
    {
        if (slideTextures.Count == 0) return;

        currentIndex = Mathf.Max(currentIndex - 1, 0);
        ShowSlide(currentIndex);
    }

    /// <summary>
    /// Display the slide at the given index.
    /// </summary>
    private void ShowSlide(int index)
    {
        if (index < 0 || index >= slideTextures.Count || projectionImage == null) return;

        Texture2D tex = slideTextures[index];
        if (tex == null) return;

        // Convert Texture2D into a Sprite for the UI Image
        Sprite sprite = Sprite.Create(
            tex,
            new Rect(0, 0, tex.width, tex.height),
            new Vector2(0.5f, 0.5f)
        );

        projectionImage.sprite = sprite;
    }
}
