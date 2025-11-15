VAR decalyaMeetingQuestId = "decalyaMeetingQuest"
VAR decalyaMeetingQuestState = "REQUIREMENTS_NOT_MET"

=== decalyaMeeting ===
{decalyaMeetingQuestState :
    - "REQUIREMENTS_NOT_MET": -> requirementsNotMet
    - "CAN_START": -> comeca
    - "FINISHED": -> end
    - else -> requirementsNotMet
}
=requirementsNotMet
-> END
=comeca
~ StartQuest(decalyaMeetingQuestId)
"Seu cérebro desacostumou com as aulas de Edgewood High nos anos que passou fora, te deixando quase zonzo no final da aula. Ao sair da sala, esbarrou com alguém que estava logo em frente a porta." #speaker: Narrador
"Ai.. Aí, presta um pouco mais de atenção, seu sem cérebro!" #speaker: Decalya Gorey #portrait:Decalya_angry
*["Me desculpa, não te vi aí. Você está bem?" ]
    "Hnf. Estou bem. Mas vê não esbarra mais em mim assim, boboca! (+15 de Prestígio com Decalya)" #speaker: Decalya Gorey #portrait:Decalya_shy
    ~ FinishQuest(decalyaMeetingQuestId)
    ->END
*["Você que está barrando o caminho!"]
    "O-o que?! Você tá dizendo que a culpa de VOCÊ esbarrar em MIM é MINHA?! Idiota.(-15 de Prestígio com Decalya)" #speaker: Decalya Gorey #portrait:Decalya_angry
    ~ FinishQuest(decalyaMeetingQuestId)
    -> END

=end
"Não fica me encarando, bobão! Hnf."#speaker:Decalya Gorey #portrait:Decalya_shocked
->END