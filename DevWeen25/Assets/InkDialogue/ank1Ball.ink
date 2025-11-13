

=== ank1Ball ===
{BasicDialogueQuestState :
    - "REQUIREMENTS_NOT_MET": -> requirementsNotMet
    - "CAN_START": -> canStart
    - else -> requirementsNotMet
}
=requirementsNotMet
"Eita porra, algo aconteceu"
-> END

=canStart
"CAPIROTOOO"
->END
