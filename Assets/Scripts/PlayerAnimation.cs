using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim;
    public PlayerController playerController;
    public SpriteRenderer spriteRenderer;

    private void LateUpdate()
    {
        if (playerController.xAxis != 0)
        {
            SetRun();
        }
        else 
        {
            SetIdle();
        }

        if (playerController.xAxis > 0)
        {
            spriteRenderer.flipX = true;
        }
        if (playerController.xAxis < 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    public void SetIdle()
    {
        anim.SetBool("Run", false);
    }

    public void SetRun()
    {
        anim.SetBool("Run", true);
    }
}
