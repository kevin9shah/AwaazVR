using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ImageLoader : MonoBehaviour
{
    public IEnumerator LoadImages(string[] urls, ProgramManager manager)
    {
        manager.slideSprites = new Sprite[urls.Length]; // Allocate array

        for (int i = 0; i < urls.Length; i++)
        {
            string url = urls[i];
            if (string.IsNullOrEmpty(url))
            {
                Debug.LogWarning($"⚠️ Empty URL for slide {i + 1}");
                continue;
            }

            using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
            {
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError($"❌ Failed to load image {i + 1}: {www.error}");
                }
                else
                {
                    Texture2D texture = DownloadHandlerTexture.GetContent(www);

                    // Convert to Sprite
                    Sprite sprite = Sprite.Create(
                        texture,
                        new Rect(0, 0, texture.width, texture.height),
                        new Vector2(0.5f, 0.5f)
                    );

                    manager.slideSprites[i] = sprite; // ✅ Store in ProgramManager
                    Debug.Log($"✅ Loaded slide {i + 1} from {url}");
                }
            }
        }

        Debug.Log("🎉 All slides loaded!");

        // 🔹 Notify PresentationManager to show the first slide
        PresentationManager pm = FindObjectOfType<PresentationManager>();
        if (pm != null)
        {
            pm.ShowSlide(0); // automatically show first slide
        }
    }
}
