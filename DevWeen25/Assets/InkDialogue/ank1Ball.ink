VAR AnkBallQuestId = "AnkBallQuest"

VAR AnkBallQuestState = "REQUIREMENTS_NOT_MET"

=== ank1Ball ===
{AnkBallQuestState :
    - "REQUIREMENTS_NOT_MET": -> requirementsNotMet
    - "CAN_START": -> canStart
    -"FINISHED": ->impaciencia
    - else -> requirementsNotMet
}
=requirementsNotMet
Você vê uma figura que parece estar em suas memórias, mas você não tem certeza... Ela não nota sua presença.#speaker:Narrador #portrait:Ankhesara_normal
-> END

=impaciencia
"Ta olhando o que? Perdeu alguma coisa?"#speaker:Ankhesara Sakhmet#portrait:Ankhesara_angry
->END
=canStart
 ~ StartQuest(AnkBallQuestId)
Enquanto caminhavam escola adentro, alguns alunos mais novos jogavam bola em frente aos portões. Subitamente, um deles perde o controle e a bola passa por vocês em alta velocidade e atinge outra estudante. #speaker:Narrador
"Ei! Toma cuidado com isso aí que estão jogando! Você sabe quantos séculos essa roupa demorou para ser costurada?"#speaker:Ankhesara Sakhmet#portrait:Ankhesara_angry
Ouvindo ela falar dessa forma, você instantâneamente faz a conexão: você conhece essa garota. Ela estudava com você e com Shelly.#speaker: Narrador
"Ankhesara não mudou nada. Continua com o mesmo temperamento egocêntrico de sempre. Você se lembra dela, não é?"#speaker:Shelly Coralite#portrait:Shelly_normal
*["Sim, me lembro. Gostava dela."]
    "Você... Gostava? Ah, sei lá, eu não gosto dela, eu já achava ela chata antes, agora parece que piorou!"#portrait:Shelly_angry
        ->carryOn

*["Sim, me lembro. Não gostava dela."]
    "Nem eu... Acredita que agora ela conseguiu se tornar ainda mais chata?!(+10 Prestígio com Shelly)"#portrait:Shelly_shocked#rewardShelly:10
        ->carryOn

*["Não me lembro."]
    "Sua memória parece ter piorado desde que saiu da cidade, hein? Ela sempre desprezou os outros monstros por não termos tanto dinheiro quanto ela, afinal o pai dela era o faraó!"#portrait:Shelly_shocked
    ->carryOn
    
=carryOn
"Vamos esquecer ela. Quero te mostrar o que mudou na escola e o restante dos nossos colegas!"#portrait:Shelly_normal
~ FinishQuest(AnkBallQuestId)

->END
