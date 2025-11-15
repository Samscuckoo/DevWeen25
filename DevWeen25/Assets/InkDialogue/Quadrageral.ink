=== Quadrageral ===
{QuadrageralQuestState :
    - "REQUIREMENTS_NOT_MET": -> requirementsNotMet
    - "CAN_START": -> comeca
    - "FINISHED": -> end
    - else -> requirementsNotMet
}
=requirementsNotMet
-> END
=comeca
~ StartQuest(QuadrageralQuestId)
"Você e Viktor se movimentam até que chegassem na quadra, onde Ankhesara, Decalya e Shelly discutiam por algo." #speaker: Narrador
"Não sei por que vocês ainda se dão ao trabalho de jogar, sabem que eu sempre ganho!" #speaker: Ankhesara Sakhmet #portrait: Ankhesara_angry
"Jogamos justamente para tentar te derrotar, Ankhesara" #speaker: Decalya Gorey #portrait: Decalya_angry
"Nunca vão conseguir, desistam! Eu sempre serei a melhor daqui" #speaker: Ankhesara Sakhmet #portrait: Ankhesara_angry
"Deixa de ser tão convencida." #speaker: Shelly Coralite #portrait: Shelly_angry
"Ah, o ar do intervalo, como sempre sendo muito amigável." #speaker: Viktor Shadowfang #portrait: Viktor_shocked
 "Acredito que você ainda não saiba jogar Ghoulball. Fica aqui do lado e assiste uma partida para aprender!"#speaker: Viktor Shadowfang #portrait: Viktor_normal
 "Você se posiciona próximo da arquibancada mais baixa, me escorando contra o corrimão. Logo, o jogo se iniciou, mas pareciam que algumas pessoas buscavam a vitória com sangue nos olhos. Ankhesara era forte com seus ataques, Decalya e Shelly eram rápidas em conseguir contra atacar e Viktor era estratégico, sabia jogar o jogo." #speaker:Narrador
 "Elias, cuidado!" #speaker: Shelly #portrait: Shelly_shocked
 ->end
-> END
~ FinishQuest(QuadrageralQuestId)
=end
->end