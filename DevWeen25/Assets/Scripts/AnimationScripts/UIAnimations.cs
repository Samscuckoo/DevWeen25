using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIAnimations : MonoBehaviour
{
    private Animator animator;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();

       // StartCoroutine(Teste());

        
    }

    // Update is called once per frame
    public void PlayAnimJump()
    {
        animator.Play("Image_Jump");
    }

    public void PlayAnimShake()
    {
        animator.Play("Image_Shake");
    }



    /*
    IEnumerator Teste()
    {
       
        yield return new WaitForSeconds(3f);
        
        PlayAnimJump();
        Debug.Log("deveria ter pulado");

        yield return new WaitForSeconds(2f);
        PlayAnimShake();

        CameraShakeManager.instanceShake.CameraShake();
    }
  */
}
