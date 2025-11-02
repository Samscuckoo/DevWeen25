using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menuCanvas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToggleMenu()
    {
        menuCanvas.SetActive(!menuCanvas.activeSelf);
    }
}
