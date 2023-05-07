using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimate : MonoBehaviour {


    public Animator animator;
    public GameObject spriteGameObject;
    [SerializeField] private string animationName;


    void Start() {

        animator = spriteGameObject.GetComponent<Animator>();
        animator.Play(animationName);

    }
}
