using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [Header("NPCs in Scene")]
    public List<NPCController> npcs;

    [Header("Presentation Questions")]
    public string[] questions; // questions in presentation order

    private int currentQuestionIndex = 0;

    /// <summary>
    /// Called by ProgramManager after API call succeeds
    /// </summary>
    public void StartQuestions(string[] orderedQuestions)
    {
        if (orderedQuestions.Length > 0 && npcs.Count > 0)
        {
            questions = orderedQuestions;
            currentQuestionIndex = 0;
            TriggerNextNPC();
        }
    }

    /// <summary>
    /// Triggers the next NPC in sequence
    /// </summary>
    public void TriggerNextNPC()
    {
        if (currentQuestionIndex >= questions.Length) return;

        int npcIndex = currentQuestionIndex % npcs.Count; // rotate NPCs if fewer NPCs than questions
        npcs[npcIndex].TriggerQuestion(questions[currentQuestionIndex]);

        currentQuestionIndex++;
    }
}
