using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection_Animation : MonoBehaviour
{
    [SerializeField] private Animator animator = null;
    private static readonly int hashChosen = Animator.StringToHash ("charChosen");
    private static readonly int hashEsc = Animator.StringToHash ("esc");
    private static readonly int hashMatchBegin = Animator.StringToHash ("matchBegin");

    public void select()
	{
        animator.SetTrigger (hashChosen);
	}

    public void deselect ()
    {
        animator.SetTrigger (hashEsc);
    }

    public void beginMatch()
	{
        animator.SetTrigger (hashMatchBegin);
	}
}
