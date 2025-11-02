using System;

public class PlayerEvents
{
    // Movement
    public event Action onDisablePlayerMovement;
    public void DisablePlayerMovement()
    {
        onDisablePlayerMovement?.Invoke();
    }

    public event Action onEnablePlayerMovement;
    public void EnablePlayerMovement()
    {
        onEnablePlayerMovement?.Invoke();
    }

    // Prestígio: notifica quando os pontos de um personagem mudam
    // CharacterType deve existir em outro arquivo (enum com os 4 personagens)
    public event Action<CharacterType, int> onPrestigePointsChange;
    public void PlayerPrestigePointsChange(CharacterType character, int points)
    {
        onPrestigePointsChange?.Invoke(character, points);
    }

    
    public event Action onPrestigeUpdated;
    public void PrestigeUpdated()
    {
        onPrestigeUpdated?.Invoke();
    }
}
