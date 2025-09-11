using UnityEngine;
using TMPro;

public class HashcodeManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI codeText;       // drag the TMP text (digit display) here
    public ProgramManager programManager;  // drag the ProgramManager object here

    void Start()
    {
        codeText.text = "";
    }

    public void AddNum(string num)
    {
        codeText.text += num;
    }

    public void ClearCode()
    {
        codeText.text = "";
    }

    public async void SubmitCode()
    {
        if (programManager != null)
        {
            programManager.presentationCode = codeText.text;
            programManager.codeText = codeText; // ensure reference
            await programManager.LoadPresentation(codeText.text);
        }
    }
}
