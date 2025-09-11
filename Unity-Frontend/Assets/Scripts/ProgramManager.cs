    using UnityEngine;
    using UnityEngine.Networking;
    using TMPro;
    using System.Collections;
    using SimpleJSON;

    public class ProgramManager : MonoBehaviour
    {
        [Header("Input")]
        public string presentationCode;

        [Header("UI References")]
        public TextMeshProUGUI statusText;
        public TextMeshProUGUI codeText;
        public GameObject hashcodeCanvas;

        [Header("Slide UI Canvases")]
        public GameObject speechCanvas;        // Canvas for scrollable speech
        public TextMeshProUGUI speechText;     // Scrollable TMP text in speech canvas
        public GameObject slideControlCanvas;  // Canvas with Next/Prev buttons

        [Header("Fetched Presentation Data")]
        public string title;
        public int slideCount;
        public bool hasImages;
        public string createdAt;

        [Header("Slide Data")]
        public string[] slideTexts;
        public string[] slideImageUrls;
        public string[] speechContents;
        public string[] questions;

        [Header("Slides as Sprites (Loaded)")]
        public Sprite[] slideSprites;

        private string baseUrl = "http://localhost:3000/api/presentation/";

        public IEnumerator LoadPresentation(string code)
        {
            string url = baseUrl + code;
            using (UnityWebRequest www = UnityWebRequest.Get(url))
            {
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError("Request Error: " + www.error);
                    if (statusText != null) statusText.text = "Failed";
                    if (codeText != null) codeText.text = "";
                    yield break;
                }

                var json = JSON.Parse(www.downloadHandler.text);

                if (json == null || !json["success"].AsBool)
                {
                    Debug.LogError("Invalid response: " + www.downloadHandler.text);
                    if (statusText != null) statusText.text = "Failed";
                    if (codeText != null) codeText.text = "";
                    yield break;
                }

                var pres = json["presentation"];
                title = pres["title"];
                slideCount = pres["slideCount"].AsInt;
                hasImages = pres["hasImages"].AsBool;
                createdAt = pres["createdAt"];

                // --- Slide URLs ---
                var slideUrls = pres["slideUrls"];
                if (slideUrls != null && slideUrls.Count > 0)
                {
                    slideImageUrls = new string[slideUrls.Count];
                    foreach (var kvp in slideUrls)
                    {
                        string key = kvp.Key;
                        JSONNode value = kvp.Value;

                        if (int.TryParse(key, out int index))
                        {
                            index -= 1;
                            if (index >= 0 && index < slideImageUrls.Length)
                                slideImageUrls[index] = value.Value;
                        }
                    }
                }

                // --- Slide Texts ---
                var texts = pres["slideTexts"];
                if (texts != null && texts.Count > 0)
                {
                    slideTexts = new string[texts.Count];
                    foreach (var kvp in texts)
                    {
                        string key = kvp.Key;
                        JSONNode value = kvp.Value;

                        if (int.TryParse(key, out int index))
                        {
                            index -= 1;
                            if (index >= 0 && index < slideTexts.Length)
                                slideTexts[index] = value.Value;
                        }
                    }
                }

                // --- Questions ---
                var qns = pres["questions"];
                if (qns != null && qns.Count > 0)
                {
                    questions = new string[qns.Count];
                    for (int i = 0; i < qns.Count; i++)
                    {
                        questions[i] = qns[i].Value;
                    }
                }

                // --- Speech Content ---
                var speech = pres["speechContent"];
                if (speech != null && speech.Count > 0)
                {
                    speechContents = new string[speech.Count];
                    for (int i = 0; i < speech.Count; i++)
                    {
                        speechContents[i] = speech[i].Value;
                    }

                    // Combine all speech into a single string for display
                    if (speechText != null)
                    {
                        string combinedSpeech = "";
                        for (int i = 0; i < speechContents.Length; i++)
                        {
                            combinedSpeech += $"Slide {i + 1}: {speechContents[i]}\n\n";
                        }
                        speechText.text = combinedSpeech;
                    }
                }

                // ✅ Show success
                Debug.Log("✅ Presentation Loaded Successfully");
                if (statusText != null) statusText.text = "Passed";
                if (codeText != null) codeText.text = ""; // clear input

                // 🔴 Enable required UI
                if (hashcodeCanvas != null) hashcodeCanvas.SetActive(false);
                if (speechCanvas != null) speechCanvas.SetActive(true);
                if (slideControlCanvas != null) slideControlCanvas.SetActive(true);

                // 🔽 Trigger slide image loading
                ImageLoader imageLoader = FindObjectOfType<ImageLoader>();
                if (imageLoader != null && slideImageUrls != null && slideImageUrls.Length > 0)
                {
                    StartCoroutine(imageLoader.LoadImages(slideImageUrls, this));
                }
                else
                {
                    Debug.LogWarning("⚠️ No ImageLoader found or no slide URLs to load.");
                }

                // 🔹 Trigger NPC questions automatically
                NPCManager npcManager = FindObjectOfType<NPCManager>();
                if (npcManager != null && questions != null && questions.Length > 0)
                {
                    npcManager.StartQuestions(questions); // pass questions in order
                }
            }
        }
    }
