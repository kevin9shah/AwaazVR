// ProgramManager.cs
// Purpose: Manages API communication, data processing, and UI state updates.

using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Collections.Generic;
using System.Threading.Tasks;
//using Newtonsoft.Json; // <-- Important: Using Newtonsoft for dictionary support

public class ProgramManager : MonoBehaviour
{
    [Header("API Configuration")]
    [Tooltip("The base URL of your backend server. E.g., http://localhost:3000")]
    public string backendUrl = "http://localhost:3000";

    [Header("UI References")]
    [Tooltip("The TextMeshPro UI element to display loading/success/error status.")]
    public TextMeshProUGUI statusText;
    [Tooltip("The parent GameObject of the hashcode input UI that will be disabled on success.")]
    public GameObject hashcodeUI;

    // Public properties to hold the final, processed data for other scripts to access
    public static Dictionary<int, string> SlideQuestions { get; private set; }
    public static Dictionary<int, string> SlideImageUrls { get; private set; }
    public static Dictionary<int, string> SpeechContents { get; private set; }
    public static int SlideCount { get; private set; }
    public static string PresentationTitle { get; private set; }

    // Properties set by HashcodeManager
    [HideInInspector]
    public string presentationCode;
    [HideInInspector]
    public TextMeshProUGUI codeText;

    private void Start()
    {
        // Initialize dictionaries to avoid null reference errors
        SlideQuestions = new Dictionary<int, string>();
        SlideImageUrls = new Dictionary<int, string>();
        SpeechContents = new Dictionary<int, string>();

        if (statusText != null)
        {
            statusText.text = "Please enter the presentation code.";
        }
    }

    /// <summary>
    /// Fetches and processes the presentation data from the backend.
    /// This is an async Task method to allow for non-blocking web requests.
    /// </summary>
    public async Task LoadPresentation(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            statusText.text = "Error: Code cannot be empty.";
            return;
        }

        presentationCode = code;
        string apiUrl = $"{backendUrl}/api/presentation/{presentationCode}";
        statusText.text = $"Connecting...";

        // Using UnityWebRequest for the API call
        using (UnityWebRequest webRequest = UnityWebRequest.Get(apiUrl))
        {
            // Send the request and wait for the response asynchronously
            var operation = webRequest.SendWebRequest();
            while (!operation.isDone)
            {
                await Task.Yield(); // Wait for the next frame
            }

            // Check for network or HTTP errors
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"API Error: {webRequest.error}");
                statusText.text = $"Error: Could not find presentation. Please check the code and try again.";
                // Clear the hashcode input field on failure
                if (codeText != null)
                {
                    codeText.text = "";
                }
            }
            else
            {
                // Request was successful
                statusText.text = "Processing presentation...";
                string jsonResponse = webRequest.downloadHandler.text;

                try
                {
                    // Deserialize the JSON response using Newtonsoft
                    //ApiResponse response = JsonConvert.DeserializeObject<ApiResponse>(jsonResponse);

                    if (response != null && response.success)
                    {
                        ProcessData(response.presentation);

                        // --- UI SUCCESS LOGIC ---
                        statusText.text = $"Success! Loaded '{PresentationTitle}'.";
                        if (hashcodeUI != null)
                        {
                            hashcodeUI.SetActive(false); // Disable the hashcode input UI
                        }
                    }
                    else
                    {
                        throw new System.Exception("API returned success: false or invalid format.");
                    }
                }
                catch (System.Exception ex)
                {
                    Debug.LogError($"JSON Parsing Error: {ex.Message}");
                    statusText.text = "Error: Failed to process data from server.";
                    if (codeText != null) codeText.text = ""; // Clear input
                }
            }
        }
    }

    /// <summary>
    /// Processes the raw data from the API response and populates the clean, static dictionaries.
    /// </summary>
    private void ProcessData(PresentationData data)
    {
        // Clear any old data
        SlideQuestions.Clear();
        SlideImageUrls.Clear();
        SpeechContents.Clear();

        PresentationTitle = data.title;
        SlideCount = data.slideCount;

        // Populate Image URLs
        if (data.slideUrls != null)
        {
            foreach (var pair in data.slideUrls)
            {
                SlideImageUrls[int.Parse(pair.Key)] = pair.Value;
            }
        }

        // Populate Questions (extracting the single question from the list)
        if (data.questions != null)
        {
            foreach (var pair in data.questions)
            {
                if (pair.Value != null && pair.Value.Count > 0)
                {
                    SlideQuestions[int.Parse(pair.Key)] = pair.Value[0];
                }
            }
        }

        // Populate Speech Content
        if (data.speechContent != null)
        {
            foreach (var pair in data.speechContent)
            {
                SpeechContents[int.Parse(pair.Key)] = pair.Value;
            }
        }

        // You can now access the data from any other script, for example:
        Debug.Log($"Loaded Presentation: {PresentationTitle} with {SlideCount} slides.");
        Debug.Log($"Question for slide 1: {SlideQuestions[1]}");
    }
}