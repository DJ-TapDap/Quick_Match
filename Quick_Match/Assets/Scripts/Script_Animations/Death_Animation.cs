using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_Animation : MonoBehaviour
{
    [SerializeField] private Animator animator = null;
    private static readonly int hashDeath = Animator.StringToHash ("playerDied");

    public void doDeath()
	{
        animator.SetTrigger (hashDeath);
    }

    public void animationDone()
	{
        gameObject.SetActive (false);
	}
}
