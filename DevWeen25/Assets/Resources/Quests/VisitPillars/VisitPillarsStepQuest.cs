using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class VisitPillarsStepQuest : QuestStep
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FinishQuestStep();
        }
    }
    protected override void SetQuestStepState(string state)
    {
    
    }
}
