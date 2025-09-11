// Program.cs
// Purpose: Defines the data structures (schemas) to hold the presentation data fetched from the API.

using System.Collections.Generic;

/// <summary>
/// This attribute makes the class fields visible and serializable in Unity's inspector
/// and for JSON deserialization.
/// </summary>
[System.Serializable]
public class PresentationData
{
    public string code;
    public string title;
    public int slideCount;
    public bool hasImages;
    public Dictionary<string, string> slideUrls;
    public Dictionary<string, string> slideTexts;

    // In your backend, 'questions' is an object where each value is an array with one question.
    // So we map it to a dictionary with a list of strings.
    public Dictionary<string, List<string>> questions;

    public Dictionary<string, string> speechContent;
    public string createdAt;
}

[System.Serializable]
public class ApiResponse
{
    public bool success;
    public PresentationData presentation;
}