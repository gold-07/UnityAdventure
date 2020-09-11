using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControler : MonoBehaviour
{

        /// <summary>
        /// Max horizontal speed of the player.
        /// </summary>
        public float maxSpeed = 7;
        public Vector3 characterVelocity { get; set; }
        public AudioSource audioSource;
        CharacterController controller;
        internal Animator animator;
        public bool isMoving = false;
        public bool isAttacking = false;
        public AttackState attackState = AttackState.Not;
        
        //public Health health;
        public bool controlEnabled = true;


    // Start is called before the first frame update
    void Start()
    {

        //health = GetComponent<Health>();

        audioSource = GetComponent<AudioSource>();

        controller = GetComponent<CharacterController>();

        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (controlEnabled){
            

            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            characterVelocity = Vector3.ClampMagnitude(move, 1);
            characterVelocity *= maxSpeed;
            controller.Move(characterVelocity * Time.deltaTime);

            if (characterVelocity.magnitude >= .1f) // Input given?
            {
                isMoving = true;
                transform.rotation = Quaternion.LookRotation(new Vector3(characterVelocity.x, 0f, characterVelocity.z)); // Rotate the Player to the moving direction
            }
            else
            {
                isMoving = false;
            }

            if(Input.GetButtonDown("Attack")){
                isAttacking = true;
            }
            else
                isAttacking = false;

            // if(Input.GetButton("Attack") && attackState == AttackState.Not){
            //     attackState = AttackState.PrepareToAtt;
            // }
            // else if(Input.GetButton("Attack") && AttackState == AttackState.Attack1){
            //     attackState = AttackState.PrepareToAtt2;
            // }
            // UpdateAttackState();
        }

        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isAttacking", isAttacking);
    }

    void UpdateAttackState()
        {
            
            switch (attackState)
            {
                case AttackState.PrepareToAtt:
                    attackState = AttackState.Attack1;
                    break;
                case AttackState.Attack1:
                    attackState = AttackState.Finished;
                    break;
                case AttackState.PrepareToAtt2:
                    attackState = AttackState.Attack2;
                    break;
                case AttackState.Attack2:
                    attackState = AttackState.Finished;
                    break;
                case AttackState.Finished:
                    attackState = AttackState.Not;
                    break;
            }
        }

    public enum AttackState
        {
            Not,
            PrepareToAtt,
            Attack1,
            PrepareToAtt2,
            Attack2,
            Finished
        }
}
