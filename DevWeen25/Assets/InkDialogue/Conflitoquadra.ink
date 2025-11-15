VAR ConflitoquadraQuestId = "ConflitoquadraQuest"
VAR ConflitoquadraQuestState = "REQUIREMENTS_NOT_MET"

=== Conflitoquadra ===

{ConflitoquadraQuestState :
    - "REQUIREMENTS_NOT_MET": -> requirementsNotMet
    - "CAN_START": -> comeca
    - "FINISHED": -> END
    - else -> requirementsNotMet
}
=requirementsNotMet
-> END
=comeca
~ StartQuest(ConflitoquadraQuestId)

"Não teve tempo para processar o chamado de Shelly. Uma bola veio em sua direção, lhe acertando na cabeça com uma força surreal e lhe fazendo apagar por um momento. Quando recobrou a consciência, conseguiu enxergar 4 silhuetas ao seu redor"#speaker:Narrador
 "Está tudo bem?! - Todos perguntaram, estendendo a mão em sua direção" #speaker:Narrador
    "Sim, estou bem. - Seguro a mão de..."#speaker:Elias Moonward
+[Ankhesara]
    ->Ankhesara
+[Viktor]
    ->Viktor
+[Shelly]
    ->Shelly
+[Decalya]
    ->Decalya
        
===Ankhesara===
"Elias segura nos dedos enfaixados de Ankhesara, se equilibrando para levantar."(-10 de Prestígio com Decalya, Shelly e Viktor) #speaker: Narrador #rewardViktor:-10 #rewardShelly:-10 #rewardDecalya:-10
"Por que diabos você tinha que ficar bem no caminho da bola? Iria ser um ponto perfeito!" #speaker:Ankhesara Sakhmet #portrait:Ankhesara_angry
    *["Não foi minha culpa, me atingiram sem eu querer."]
        "Poderia ter desviado se fosse menos lerdo!"(+10 de Prestígio com Ankhesara)#portrait:Ankhesara_angry #rewardAnkhesara:+10
        ->segmentoankhe1
    *["Não seria um ponto perfeito, eu estava observando."]
        "Como ousa falar assim de algo que EU fiz? É claro que seria um ponto perfeito."(+5 de Prestígio com Ankhesara)#portrait:Ankhesara_angry #rewardAnkhesara:+5
        ->segmentoankhe1
    *["Eu sinto muito, minha cabeça ficou na frente da bola."]
        "É melhor que se desculpe mesmo!"(+15 de Prestígio com Ankhesara)#portrait:Ankhesara_angry #rewardAnkhesara:+15
        ->segmentoankhe1
            
    ===segmentoankhe1===
    "É melhor que se sente. Não quero ter que te levantar caso caia morto aqui na minha frente"#speaker:Ankhesara Sakhmet #portrait:Ankhesara_normal
    "E... E eu conheço você. É o Elias que estudou comigo alguns anos atrás, não é? Eu sabia que te reconhecia de algum lugar! Estava mais cedo andando com a esquisitinha da Shelly."#speaker:Ankhesara Sakhmet #portrait:Ankhesara_normal
        *["Ela não é esquisitinha, não fale assim da minha melhor amiga."]
            "Ah. Que estranho. Você poderia andar com gente melhor, como eu. Mas não posso julgar as escolhas de amizade..." (-10 de Prestígio com Ankhesara) #portrait:Ankhesara_shocked #rewardAnkhesara:-10
            ->segmentoankhe2
        *["É, a gente ainda anda junto, mas nada demais."]
            "Está na hora de renovar essas amizade(+10 de Prestígio com Ankhesara)#portrait:Ankhesara_shy #rewardAnkhesara:+10
            ->segmentoankhe2
    
    ===segmentoankhe2===
    "Eu ainda não consigo acreditar que perdi por sua causa. Um simples "foi sem querer" não vai consertar meu placar!"#speaker:Ankhesara Sakhmet #portrait:Ankhesara_angry
        *["Posso compensar te comprando algo para comer. Você decide, e eu pago."]
            "Hmph... Bom, talvez você tenha alguma utilidade fora do campo. Aceito."(+15 de Prestígio com Ankhesara)#portrait:Ankhesara_shy #rewardAnkhesara:+15
            ->END
        *["Mesmo perdendo, suas jogadas ainda foram impressionantes."]
        "Impressionante? Claro. Eu sempre sou. Mas... pode continuar dizendo isso, talvez eu perdoe seu desastre. Vamos, te ajudo a colocar um gelo para não deixar roxo"(+10 de Prestígio com Ankhesara)#portrait:Ankhesara_shy #rewardAnkhesara:+10
        ->END
        *["Olha, você também atrapalhou minha visão do campo com a bolada."]
        "Tome cuidado, que da próxima vez eu te transformo em uma múmia também, para aprender a não me atrapalhar" - A garota sai andando.(-10 de Prestígio com Ankhesara)#portrait:Ankhesara_angry #rewardAnkhesara:-10
        ->END
        
===Viktor===
"Elias se firma na mão gélida de Viktor, se equilibrando para levantar."(-10 de Prestígio com Decalya, Shelly e Ankhesara) #speaker: Narrador #rewardAnkhesara:-10 #rewardShelly:-10 #rewardDecalya:-10
"Levantar-se com ajuda é sempre mais fácil. Deixe-me te ajudar"#speaker:Viktor Shadowfang #portrait:Viktor_shy
    *["Aceito, mas prometa que não vai pedir meu sangue como pagamento!"]
        "Ah, que falta de fé em mim, Elias. Nem todo convite meu envolve mordidas.. Pelo menos não por agora."(+10 de Prestígio com Viktor)#portrait:Viktor_normal #rewardViktor:+10
        ->segmentovik1
    *["Eu agradeço, Viktor. Foi mais ágil do que eu esperava."]
        "Velocidade é o mínimo que se espera de alguém que já viveu séculos, mas aprecio o elogio"(+15 de Prestígio com Viktor)#portrait:Viktor_shy #rewardViktor:+15
        ->segmentovik1
    *["Eu consigo sozinho, mas valeu"]
        "Orgulho ferido... compreensível"#portrait:Viktor_normal #rewardViktor:+5
        ->segmentovik1
        
===segmentovik1===
"Acabei de ouvir Ankhesara reclamando do "drama" todo... Ela tem um talento admirável, mas uma precisão letal." - Viktor diz tirando a poeira dos ombros de Elias #speaker:Viktor Shadowfang #portrait:Viktor_normal
"Mas me diga, está tudo bem? Ainda tem pique para jogar uma partida daqui um pouco? Ou prefere evitar outro ataque com a fúria de mil faraós?" #speaker:Viktor Shadowfang #portrait:Viktor_normal
    *["Eu irei jogar, não quero parecer fraco."]
        "Ah, coragem... ou teimosia? Difícil distinguir às vezes"(-10 de Prestígio com Viktor)#portrait:Viktor_shocked #rewardViktor:-10
        ->segmentovik2
    *["Acho que vou descansar um pouco. E gostei da conversa que estamos tendo"]
        "Sábia decisão. Alguns jogos valem mais quando jogados fora da quadra"(+10 de Prestígio com Viktor)#portrait:Viktor_shy #rewardViktor:+10
        ->segmentovik2
        
===segmentovik2===
"Percebi como elas olham para você. E acredito que não seja só porque acabou de voltar para a escola. Costuma atrair esse tipo de atenção normalmente?"#speaker:Viktor Shadowfang #portrait:Viktor_normal
    *["Só das pessoas mais interessantes, Viktor."]
        "Então admito estar em boa companhia"(+15 de Prestígio com Viktor)#portrait:Viktor_shy #rewardViktor:+15
        ->b4end1
    *["Aparentemente sim. Acho que sou um imã pra confusão!" ]
        "Confusão pode ser... incrível, se conduzida com elegância"(+10 de Prestígio com Viktor)#portrait:Viktor_shy #rewardViktor:+10
        ->b4end1
    *["Não é minha culpa que todos querem me acertar"]
        "Culpa talvez não, mas deve ter algo em você que intriga os outros... Algo deve ter."(-10 de Prestígio com Viktor)#portrait:Viktor_angry #rewardViktor:-10
        ->b4end1

===b4end1===
"Venha, vou te ajudar a se recuperar do ataque da Ankhesara"
->END

===Shelly===
"Elias se firma nos dedos escorregadios de Shelly, se equilibrando para levantar."(-10 de Prestígio com Decalya, Viktor e Ankhesara) #speaker: Narrador #rewardAnkhesara:-10 #rewardViktor:-10 #rewardDecalya:-10
"Elias! Ai meu tridente, você tá bem?! Eu juro que ela não mirou em você... acho."#speaker:Shelly Coralite #portrait:Shelly_shocked
    *["Valeu, Shelly. Só você pra me salvar de morrer de vergonha"]
        "Hehe! Tá vendo? Eu ainda sou boa em alguma coisa! Mesmo que seja te tirar de apuros, como sempre!"(+15 de Prestígio com Shelly)#portrait:Shelly_normal #rewardShelly:+15
        ->segmentoshelly1
    *["Tô bem, Shelly. Só preciso de um minuto pra lembrar meu nome"]
        "Ah, então seu nome ainda é Elias! Ufa. Achei que eu ia ter que te batizar de novo"(+10 de Prestígio com Shelly) #portrait:Shelly_normal #rewardShelly:+10
        ->segmentoshelly1
    *["Acho que preferia desmaiar do que ser visto assim"]
        "Mas daí eu teria que te carregar, e olha... eu mal consigo com meus livros"(+5 de Prestígio com Shelly)#portrait:Shelly_normal #rewardShelly:+5
        ->segmentoshelly1
        
===segmentoshelly1===
"Sabe, eu ainda lembro de quando você levava boladas por mim. Agora parece que a maré virou, né? É meio estranho... te ver aqui de novo. Como se eu tivesse sonhado que você tinha sumido."#speaker: Shelly Coralite #portrait:Shelly_cry
    *["Acho que o sonho acabou, e eu voltei pra encher o saco."]
        "Hehehe! Melhor pesadelo que eu já tive!"(+10 de Prestígio com Shelly) #portrait:Shelly_shy #rewardShelly:+10
        ->segmentoshelly2
    *["Eu nunca sumi, só estava ocupado com outras coisas"]
        "Eu entendo! Só posso dizer que senti sua falta nas aulas chatas que tivemos nesses anos..."(-10 de Prestígio com Shelly)#portrait:Shelly_angry #rewardShelly:-10
        ->segmentoshelly2
        
===segmentoshelly2===
"Sabia que eu fiquei treinando pra te impressionar? Pensei: Quando o Elias voltar, eu vou ser tipo... a monstrinha mais incrível do colégio! Mas, uh... ainda tô trabalhando nisso"#speaker: Shelly Coralite #portrait:Shelly_shy
    *["Você já é incrível, Shelly. Eu devia ter voltado antes"]
        "Awn... agora quem vai desmaiar sou eu! Mas... obrigada, de verdade"(+15 de Prestígio com Shelly) #portrait:Shelly_shy #rewardShelly:+15
        ->b4end2
    *["Hahaha, você já tem muitas qualidades, não tinha que pensar isso"]
        "Eu agradeço, mas eu ainda tenho que te mostrar muita coisa que eu consigo fazer!"(+10 de Prestígio com Shelly)#portrait:Shelly_shy #rewardShelly:+10
        ->b4end2
    *["Bom, ainda tem chão pela frente" ]
        "Isso foi... um elogio?"(-10 de Prestígio com Shelly)#portrait:Shelly_angry #rewardShelly:-10
        ->b4end2
        
===b4end2===
"De qualquer forma... Eu vou buscar um gelo para você! Espera aí"
->END

===Decalya===
"Elias se firma na mão fraca e sem motivação de Decalya, se equilibrando para levantar.(-10 de Prestígio com Shelly, Viktor e Ankhesara) #speaker: Narrador #rewardAnkhesara:-10 #rewardViktor:-10 #rewardShelly:-10
"Mas que idiota fica parado no meio da quadra durante uma partida de Ghoulball?! Vem, levanta logo antes que eu me arrependa de ajudar!"#speaker:Decalya Gorey #portrait:Decalya_angry
    *["Você sempre foi tão gentil assim com as pessoas desmaiadas?"]
        "Tsc... é isso que dá tentar ser boazinha. Não me acostuma mal, hein"(+15 de Prestígio com Decalya) #portrait:Decalya_shy #rewardDecalya:+15
        -> segmentodec1
    *["Valeu... eu devia saber que você apareceria pra me zoar" ]
        "Não é zoar! É... só garantir que não vá quebrar mais nada. Já basta o ego"(+10 de Prestígio com Decalya)#portrait:Decalya_normal #rewardDecalya:+10
        ->segmentodec1
    *["Nem precisava ajudar, eu tava bem" ]
        "Ah, claro. Parecia ótimo ali no chão. Tá, da próxima vez deixo te desintegrar sozinho"#portrait:Decalya_angry #rewardDecalya:+5
        ->segmentodec1

===segmentodec1===
"Decalya cruza os braços, olhando pra ele com expressão irritada mas o olhar denuncia um pouco de preocupação"#speaker:Narrador #portrait:Decalya_shy
"Da próxima vez, presta atenção, tá? Não posso ficar colando as partes de todo mundo"#speaker:Decalya Gorey #portrait:Decalya_angry
    *["Então quer dizer que você se importa, hein?"]
        "N-Não é isso! Só... não quero que morra e vire mais um morto-vivo pra cuidar, entendeu?!"(+10 de Prestígio com Decalya)#portrait:Decalya_shy #rewardDecalya:+10
        ->segmentodec2
    *["Eu sabia que no fundo você só queria me ver passando vergonha"]
        "Credo, não seja tão convencido. Eu nem teria vindo se não fosse por... enfim, deixa pra lá"(-10 de Prestígio com Decalya)#portrait:Decalya_angry #rewardDecalya:-10
        ->segmentodec2
        
===segmentodec2===
"Ela tira um curativo de uma bolsa próxima à arquibancada e entrega para Elias"#speaker:Narrador
"Toma. Não quero que vá por aí com essa marca roxa assustando os outros alunos bem no dia do baile"#speaker:Decalya Gorey #portrait:Decalya_shy
    *["Então é assim que você mostra que se importa? Fazendo piada?"]
        "Heh... é o único jeito que eu conheço. Se quiser carinho, procura outra pessoa"(+10 de Prestígio com Decalya)#portrait:Decalya_shy #rewardDecalya:+10
        ->b4end3
    *["Você até que é fofa quando tenta parecer durona."]  
        "F-Fofa?! Me respeita! Eu sou aterrorizante! Tipo... 7 de 10 no mínimo"(+15 de Prestígio com Decalya)#portrait:Decalya_shy #rewardDecalya:+15
        ->b4end3
    *["Nem precisava, eu aguento sozinho"]
        "Tá bom, Sr. Independente. Da próxima vez, morre quieto então"(-10 de Prestígio com Decalya)#portrait:Decalya_shy #rewardDecalya:-10
        ->b4end3

===b4end3===
"Você parece pálido, vou buscar uma água para você"
    
-> END
