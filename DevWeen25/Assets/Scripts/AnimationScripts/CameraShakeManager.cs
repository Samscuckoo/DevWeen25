using Unity.Cinemachine;
using UnityEngine;

public class CameraShakeManager : MonoBehaviour
{
    public static CameraShakeManager instanceShake;
    private CinemachineImpulseSource impulseSource;

    [SerializeField] float forca = 0.5f;
    void Awake()
    {
        if(instanceShake == null)
        {
            instanceShake = this;
        }


    }

    private void Start()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();

    }
    // Update is called once per frame
    public void CameraShake()
    {
        impulseSource.GenerateImpulseWithForce(forca);
    }
}
