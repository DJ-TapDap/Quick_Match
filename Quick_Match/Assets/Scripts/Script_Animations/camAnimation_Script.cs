using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camAnimation_Script : MonoBehaviour
{
    [SerializeField] private Animator animator = null;
    private static readonly int hashChosen = Animator.StringToHash ("cameraMove");

    public void moveCam()
	{
        animator.SetTrigger (hashChosen);
	}
}
