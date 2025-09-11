using UnityEngine;
using UnityEngine.UI;

public class PresentationManager : MonoBehaviour
{
    [Header("References")]
    public ProgramManager programManager;   // Drag ProgramManager from hierarchy
    public Image slideImage;                // UI Image where slides are shown

    [Header("Slide Control")]
    public int currentSlide = 0;

    private void Start()
    {
        // Try to load the first slide when everything is ready
        if (programManager != null && programManager.slideSprites != null && programManager.slideSprites.Length > 0)
        {
            ShowSlide(0);
        }
        else
        {
            Debug.LogWarning("⚠️ No slides available to show at Start.");
        }
    }

    public void ShowSlide(int index)
    {
        if (programManager == null || programManager.slideSprites == null) return;

        if (index >= 0 && index < programManager.slideSprites.Length)
        {
            currentSlide = index;
            slideImage.sprite = programManager.slideSprites[currentSlide];
            Debug.Log($"📖 Showing slide {currentSlide + 1}");
        }
        else
        {
            Debug.LogWarning($"⚠️ Slide index {index} is out of range.");
        }
    }

    public void NextSlide()
    {
        if (programManager == null || programManager.slideSprites == null) return;

        if (currentSlide < programManager.slideSprites.Length - 1)
        {
            ShowSlide(currentSlide + 1);
        }
        else
        {
            Debug.Log("✅ Already at last slide.");
        }
    }

    public void PreviousSlide()
    {
        if (programManager == null || programManager.slideSprites == null) return;

        if (currentSlide > 0)
        {
            ShowSlide(currentSlide - 1);
        }
        else
        {
            Debug.Log("✅ Already at first slide.");
        }
    }
}
