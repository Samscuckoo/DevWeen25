using UnityEngine;

[CreateAssetMenu(fileName = "QuestInfoSO", menuName = "ScriptableObjects/QuestInfoSO", order = 1)]
public class QuestInfoSO : ScriptableObject
{
    [field: SerializeField] public string id { get; private set; }

    [Header("Quest Details")]
    public string displayName;

    [Header("Requirements")]
    public int shellyPoints;
    public int viktorPoints;
    public int ankhesaraPoints;
    public int decalyaPoints;
    public QuestInfoSO[] questPrerequisites;

    [Header("Steps")]
    public GameObject[] questStepPrefabs;

    [Header("Rewards")]
    public int prestigePointsShelly;
    public int prestigePointsViktor;
    public int prestigePointsAnkhesara;
    public int prestiegePointsDecalya;


    private void OnValidate()
    {
        #if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }
   
}
