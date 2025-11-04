VAR VisitPillarsQuestId = "VisitPillarsQuest"

VAR VisitPillarsQuestState = "REQUIREMENTS_NOT_MET"

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
"Olá, você! Seja bem vindo à Edgewood High! Ainda estamos arrumando as coisas para o baile de boas vindas, então você vai ter que esperar para poder jogar... #speaker:Ankhesara Sakhmet #portrait:Ankhesara_normal
"Mas se quiser, posso te dar um exemplo do que está por vir."#portrait:Ankhesara_shy
* [Sim]
    ~ StartQuest(VisitPillarsQuestId)
    "Ótimo! Dê uma volta por aí, procure a lixeira e o arbusto."#portrait:Ankhesara_normal
* [Não]
    "Então não desperdice meu tempo! Volte aqui se quiser tentar de novo" #portrait:Ankhesara_cry
- -> END

= inProgress
Já foi lá? To esperando...#speaker:Ankhesara Sakhmet #portrait:Ankhesara_angry
-> END

= canFinish
~ FinishQuest(VisitPillarsQuestId)
"Muito Bem! Sweet Halloween: Edgewood High é um dating simulator de gelar a espinha, e você poderá jogar em breve. Fique atento para novas versões!"#speaker:Ankhesara Sakhmet #portrait:Ankhesara_normal
-> END

= finished
"V-você voltou! Tá afim de mim, é?"#portrait:Ankhesara_shocked
-> END