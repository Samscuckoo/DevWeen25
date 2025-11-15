VAR ViktorMeetingQuestId = "ViktorMeetingQuest"

VAR ViktorMeetingQuestState = "REQUIREMENTS_NOT_MET"

=== victorMeeting ===
{ViktorMeetingQuestState :
    - "REQUIREMENTS_NOT_MET": -> requirementsNotMet
    - "CAN_START": -> comeca
    - "FINISHED": -> end
    - else -> requirementsNotMet
}
=requirementsNotMet
Um jovem pálido confere anotações em seu caderno. Ele está focado e parece não querer ser interrompido. #speaker:Narrador#portrait:Viktor_normal
-> END

=comeca
~ StartQuest(ViktorMeetingQuestId)
"Bom dia, Shelly. Você terminou de preencher a ficha do baile que a professora Ossilda pediu?"#speaker:Viktor Shadowfang #portrait:Viktor_normal

"Ah?! Tinha isso? Eu esqueci totalmente...."#speaker:Shelly Coralite #portrait:Shelly_shocked

"Está tudo bem. Ela disse que pode entregar ate hoje a tarde.. Melhor se apressar" #speaker:Viktor Shadowfang #portrait:Viktor_normal

"Vou aproveitar a aula para preencher. Além disso, Viktor, esse é meu amigo Elias Moonward, ele voltou para estudar com a gente nesse ano!" #speaker:Shelly Coralite #portrait:Shelly_normal

"É um prazer conhecê-lo, Elias. Viktor Shadowfang." - Ele se inclinou para beijar sua mão como um galã de novela #speaker:Viktor Shadowfang #portrait:Viktor_shy

* [Recuar]
    "Oh. Isso deve ser estranho para você, sinto muito" #speaker: Viktor Shadowfang #portrait:Viktor_normal
    ->followUp
* [Aceitar]
    "Encantado." (+15 Prestígio com Viktor) (-5 Prestígio com Shelly) #speaker: Viktor Shadowfang #portrait:Viktor_shy #rewardShelly:-5 #rewardViktor:15
    ->followUp
    
=followUp
"Eu acho que deveríamos ir, Elias... A aula já vai começar" #speaker:Shelly Coralite #portrait:Shelly_normal
~ FinishQuest(ViktorMeetingQuestId)
    -> END 
=end
"O sinal acabou de tocar!" #speaker:Viktor Shadowfang #portrait:Viktor_normal
    ->END
