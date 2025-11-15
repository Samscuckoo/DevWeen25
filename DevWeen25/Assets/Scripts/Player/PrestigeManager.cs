using UnityEngine;

public enum CharacterType { Shelly = 0, Viktor = 1, Ankhesara = 2, Decalya = 3 }

public class PrestigeManager : MonoBehaviour
{
    public static PrestigeManager instance { get; private set; }

    [Header("Configuration")]
    [SerializeField] private int startingPrestigePoints = 0;

    // ordem: 0 = Shelly, 1 = Viktor, 2 = Ankhesara, 3 = Decalya
    private int[] prestigePoints = new int[4];

    private const string PREF_POINTS = "PrestigePoints_{0}";

    private void Awake()
    {
        // Singleton: se já existe outro PrestigeManager, destrói este
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // Persiste entre cenas

        // inicializa pontos a partir do PlayerPrefs
        for (int i = 0; i < prestigePoints.Length; i++)
            prestigePoints[i] = PlayerPrefs.GetInt(string.Format(PREF_POINTS, i), startingPrestigePoints);
    }

    private void OnEnable()
    {
        GameEventsManager.instance.playerEvents.onPrestigePointsChange += PlayerPrestigePointsChange;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.playerEvents.onPrestigePointsChange -= PlayerPrestigePointsChange;
    }

    private void PlayerPrestigePointsChange(CharacterType character, int delta)
    {
        switch (character)
        {
            case CharacterType.Shelly:
                AddPrestigePoints(delta, 0, 0, 0);
                break;
            case CharacterType.Viktor:
                AddPrestigePoints(0, delta, 0, 0);
                break;
            case CharacterType.Ankhesara:
                AddPrestigePoints(0, 0, delta, 0);
                break;
            case CharacterType.Decalya:
                AddPrestigePoints(0, 0, 0, delta);
                break;
        }
    }

    private void Start()
    {
        // notifica UI/others do estado inicial
        for (int i = 0; i < prestigePoints.Length; i++)
        {
            GameEventsManager.instance?.playerEvents?.PlayerPrestigePointsChange((CharacterType)i, prestigePoints[i]);
        }
    }

    public void AddPrestigePoints(int shellyDelta, int viktorDelta, int ankhesaraDelta, int decalyaDelta)
    {
        if (shellyDelta != 0) ChangePointsAtIndex(0, shellyDelta);
        if (viktorDelta != 0) ChangePointsAtIndex(1, viktorDelta);
        if (ankhesaraDelta != 0) ChangePointsAtIndex(2, ankhesaraDelta);
        if (decalyaDelta != 0) ChangePointsAtIndex(3, decalyaDelta);
    }

    private void ChangePointsAtIndex(int idx, int delta)
    {
        int newValue = prestigePoints[idx] + delta;
        if (newValue < 0) newValue = 0;
        if (newValue == prestigePoints[idx]) return;

        prestigePoints[idx] = newValue;
        SaveIndex(idx); // DESCOMENTE PARA SALVAR
    }

    private void SaveIndex(int idx)
    {
        PlayerPrefs.SetInt(string.Format(PREF_POINTS, idx), prestigePoints[idx]);
        PlayerPrefs.Save();
    }

    // Método público para obter pontos de um personagem (útil para verificar em QuestManager)
    public int GetPrestigePoints(CharacterType character)
    {
        return prestigePoints[(int)character];
    }
}
