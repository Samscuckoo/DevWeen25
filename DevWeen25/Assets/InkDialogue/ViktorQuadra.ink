VAR ViktorQuadraQuestId = "ViktorQuadraQuest"
VAR ViktorQuadraQuestState = "REQUIREMENTS_NOT_MET"
=== ViktorQuadra ===
{ViktorQuadraQuestState :
    - "REQUIREMENTS_NOT_MET": -> requirementsNotMet
    - "CAN_START": -> comeca
    - "FINISHED": -> end
    - else -> requirementsNotMet
}
=requirementsNotMet
Viktor está distraido demais com a quadra para notar você.#speaker:Narrador #portrait:Viktor_normal
-> END
=comeca
~ StartQuest(ViktorQuadraQuestId)
"Elias. Vejo que conheceu a Decalya." #speaker: Viktor Shadowfang #portrait:Viktor_normal
*["Sim, e não posso dizer que fui com a cara dela"]
    "Ah, não leve para o coração, Decalya tem seu jeitinho particular de ser, mas é um doce quando quer" #speaker: Viktor Shadowfang #portrait:Viktor_normal
    ->followUp
*["Sim, e acabei esbarrando com ela e acho que está chateada comigo"]
    "Hahaha. Ela não está chateada. Apenas não sabe ser carinhosa. Aposto que apreciou suas desculpas" #speaker: Viktor Shadowfang #portrait:Viktor_shy
    ->followUp

=followUp
"Na hora do intervalo, sempre vamos para a quadra jogar Ghoulball, quer vir com a gente?" #speaker: Viktor Shadowfang #portrait:Viktor_normal
*[Sim]
    "Sabia que iria topar!"
    ->hurryUp


=hurryUp
"Ótimo! Venha!"
-> END
~ FinishQuest(ViktorQuadraQuestId)
=end
"Acho que já levaram a bola para a quadra, vamos!"
->end