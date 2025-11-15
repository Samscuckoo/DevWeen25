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
    ->END
*["Você que está barrando o caminho!"]
    "O-o que?! Você tá dizendo que a culpa de VOCÊ esbarrar em MIM?! Idiota.(-15 de Prestígio com Decalya)" #speaker: Decalya Gorey #portrait:Decalya_angry
    -> END
~ FinishQuest(decalyaMeetingQuestId)
=end
"Não fica me encarando, bobão! Hnf."
->end