
VAR BasicDialogueQuestId = "BasicDialogue"

VAR BasicDialogueQuestState = "REQUIREMENTS_NOT_MET"

=== shelly1 ===
{VisitPillarsQuestState :
    - "CAN_START": -> canStart
    - else -> END
}
=canStart
"Elias!Você por aqui! Finalmente decidiu voltar?"#speaker:Shelly Coralite #portrait: Shelly_normal
* [Decidir não né... voltei por conveniência]
    "Ah, deixa de bobeira! Eu sei que você tava com saudade."#portrait:Shelly_angry 
    -> followUp
* [Demorou, mas eu vim. A saudade estava me matando.]
    "Eu também estava morrendo de saudade!"#portrait:Shelly_shy
    ->followUp

=followUp
"Você está animado de voltar para o colégio Edgewood High de novo? Tenho certeza que vão amar te rever!"#portrait:Shelly_normal
* ["Na verdade não. Preferia ter ficado no meu outro colégio."]
    "Ah... É uma pena então. Eu estava bem animada para a sua volta."#portrait:Shelly_cry
    ->hurryUp
* ["Estou, gostaria de conhecer as novas pessoas que estão aqui"]
    "Bem, eu posso dizer que bastante coisa mudou enquanto você esteve fora. Estou feliz que voltou!(+10 Prestígio com Shelly)"#rewardShelly:10
    ->hurryUp
* ["Estou, principalmente feliz por rever você, Shelly."]
    "Eu também estou muito feliz! Senti sua falta enquanto estava fora.(+15 Prestígio com Shelly)"#rewardShelly:15
    ->hurryUp
    
=hurryUp
"Vamos, não podemos nos atrasar logo para o seu primeiro dia de aula! E ainda temos que pegar o anúncio do baile de início de ano"#portrait:Shelly_shocked
~ FinishQuest(BasicDialogueQuestId)


->END