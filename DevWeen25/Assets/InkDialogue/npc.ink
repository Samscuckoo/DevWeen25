=== npc ===
{VisitPillarsQuestState :
    - "REQUIREMENTS_NOT_MET": -> requirementsNotMet
    - "CAN_START": -> canStart
    - "IN_PROGRESS": -> inProgress
    - "CAN_FINISH": -> canFinish
    - "FINISHED": -> finished
    - else -> END
}

= requirementsNotMet
-> END

= canStart
CADE VC ANISAAAA #speaker:Ank #portrait:mumia_feliz
Vc ta vendo a transmissão?
* [Sim]
    ~ StartQuest(VisitPillarsQuestId)
    Então vai perto dos pilares
* [Não]
    Quando tiver, avisa. #portrait:mumia_triste
- -> END

= inProgress
Já foi lá? To esperando...
-> END

= canFinish
~ FinishQuest(VisitPillarsQuestId)
Beleza, aprendeu a andar, agora SOME.
-> END

= finished
SOME DAQUI MANO
-> END