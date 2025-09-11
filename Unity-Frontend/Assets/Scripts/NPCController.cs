using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCController : MonoBehaviour
{
    [Header("UI")]
    public GameObject canvas;         // Canvas on head
    public TextMeshProUGUI questionText;
    public Button mainButton;

    [Header("Question Data")]
    public string assignedQuestion;

    private enum State { Idle, WaitingForAsk, QuestionAsked, Recording }
    private State currentState = State.Idle;

    private void Start()
    {
        canvas.SetActive(false);
        mainButton.onClick.AddListener(OnMainButtonClicked);
    }

    public void TriggerQuestion(string question)
    {
        assignedQuestion = question;
        canvas.SetActive(true);
        currentState = State.WaitingForAsk;
        questionText.text = "Press 'Ask Question' to hear your question";
        mainButton.GetComponentInChildren<TextMeshProUGUI>().text = "Ask Question";
    }

    private void OnMainButtonClicked()
    {
        switch (currentState)
        {
            case State.WaitingForAsk:
                questionText.text = assignedQuestion;
                mainButton.GetComponentInChildren<TextMeshProUGUI>().text = "Record Answer";
                currentState = State.QuestionAsked;
                break;

            case State.QuestionAsked:
                // Start recording
                questionText.text = "Recording... Press 'Stop' when done.";
                mainButton.GetComponentInChildren<TextMeshProUGUI>().text = "Stop";
                currentState = State.Recording;
                // TODO: Start audio/text recording here
                break;

            case State.Recording:
                // Stop recording
                currentState = State.Idle;
                canvas.SetActive(false);
                mainButton.GetComponentInChildren<TextMeshProUGUI>().text = "Ask Question"; // Reset
                // TODO: Convert audio → text & send to backend
                break;
        }
    }
}
