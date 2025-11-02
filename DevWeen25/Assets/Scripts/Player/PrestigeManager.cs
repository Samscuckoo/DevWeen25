using UnityEngine;

public class PrestigeManager : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private int startingPrestigePoints = 0;

    // ordem: 0 = Shelly, 1 = Viktor, 2 = Ankhesara, 3 = Decalya
    private int[] prestigePoints = new int[4];

    private const string PREF_POINTS = "PrestigePoints_{0}";

    private void Awake()
    {
        // inicializa pontos
        for (int i = 0; i < prestigePoints.Length; i++)
            prestigePoints[i] = PlayerPrefs.GetInt(string.Format(PREF_POINTS, i), startingPrestigePoints);
    }

    private void OnEnable()
    {
        // se você tiver um evento que dispara ganhos de prestígio, conecte aqui
        // Exemplo (adapte ao seu GameEventsManager se existir):
        // GameEventsManager.instance.playerEvents.onPrestigeGained += PrestigeGained;
    }

    private void OnDisable()
    {
        // desconectar o evento equivalente, se conectado
        // GameEventsManager.instance.playerEvents.onPrestigeGained -= PrestigeGained;
    }

    private void Start()
    {
        // notifica UI/others do estado inicial (adapte nomes dos eventos ao seu GameEventsManager)
        for (int i = 0; i < prestigePoints.Length; i++)
        {
            GameEventsManager.instance?.playerEvents?.PlayerPrestigePointsChange((CharacterType)i, prestigePoints[i]);
        }
    }

   
    public void AddPrestigePoints(int shelly, int viktor, int ankhesara, int decalya)
    {
        if (shelly != 0) AddToIndex(0, shelly);
        if (viktor != 0) AddToIndex(1, viktor);
        if (ankhesara != 0) AddToIndex(2, ankhesara);
        if (decalya != 0) AddToIndex(3, decalya);
    }

    // handler no estilo do seu ExperienceGained (caso queira ligar a um evento com a mesma assinatura)
    private void PrestigeGained(int shelly, int viktor, int ankhesara, int decalya)
    {
        AddPrestigePoints(shelly, viktor, ankhesara, decalya);
    }

    private void AddToIndex(int idx, int amount)
    {
        if (amount <= 0) return;
        prestigePoints[idx] += amount;
        SaveIndex(idx);

        // notifica via eventos (adapte os nomes aos seus eventos reais)
        GameEventsManager.instance?.playerEvents?.PlayerPrestigePointsChange((CharacterType)idx, prestigePoints[idx]);
    }

    private void SaveIndex(int idx)
    {
        PlayerPrefs.SetInt(string.Format(PREF_POINTS, idx), prestigePoints[idx]);
        PlayerPrefs.Save();
    }
}
