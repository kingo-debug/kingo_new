using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    //  [RequireComponent(typeof(Rigidbody))]
   // [RequireComponent(typeof(CapsuleCollider))]
 //   [RequireComponent(typeof(Animator))]
    public class ThirdPersonCharacter : MonoBehaviour
    {
        [SerializeField] float m_MovingTurnSpeed = 360;
        [SerializeField] float m_StationaryTurnSpeed = 180;
        [SerializeField] float m_JumpPower = 12f;
        [Range(1f, 4f)] [SerializeField] float m_GravityMultiplier = 2f;
        [SerializeField] float m_RunCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others
        [SerializeField] float m_MoveSpeedMultiplier = 1f;
        [SerializeField] float m_AnimSpeedMultiplier = 1f;
        [SerializeField] float m_GroundCheckDistance = 0.1f;

        //   Rigidbody m_Rigidbody;
        Animator m_Animator;
        bool m_IsGrounded;
        float m_OrigGroundCheckDistance;
        const float k_Half = 0.5f;
        float m_TurnAmount;
        float m_ForwardAmount;
        Vector3 m_GroundNormal;
        float m_CapsuleHeight;
        Vector3 m_CapsuleCenter;
        CapsuleCollider m_Capsule;
        bool m_Crouching;


        //  void Start()
        // {
        //   m_Animator = GetComponent<Animator>();
        //   m_Rigidbody = GetComponent<Rigidbody>();
        //   m_Capsule = GetComponent<CapsuleCollider>();
        //   m_CapsuleHeight = m_Capsule.height;
        //   m_CapsuleCenter = m_Capsule.center;

        //   m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        //  m_OrigGroundCheckDistance = m_GroundCheckDistance;
        // }


        public void Move(Vector3 move, bool crouch, bool jump)
        {

            // convert the world relative moveInput vector into a local-relative
            // turn amount and forward amount required to head in the desired
            // direction.
            if (move.magnitude > 1f) move.Normalize();
            move = transform.InverseTransformDirection(move);

            move = Vector3.ProjectOnPlane(move, m_GroundNormal);
            m_TurnAmount = Mathf.Atan2(move.x, move.z);
            m_ForwardAmount = move.z;

            ApplyExtraTurnRotation();

            // control and velocity handling is different when grounded and airborne:
            //  if (m_IsGrounded)
            {
                //      HandleGroundedMovement(crouch, jump);
            }
            //   else
            {

                //    }

                //   ScaleCapsuleForCrouching(crouch);
                //   
                // send input and other state parameters to the animator

            }


            //   void ScaleCapsuleForCrouching(bool crouch)
            // {
            //  if (m_IsGrounded && crouch)
            //   {
            // if (m_Crouching) return;
            //   m_Capsule.height = m_Capsule.height / 2f;
            //   m_Capsule.center = m_Capsule.center / 2f;
            ////     m_Crouching = true;
            // }
            // else
            //   {
            //   Ray crouchRay = new Ray(m_Rigidbody.position + Vector3.up * m_Capsule.radius * k_Half, Vector3.up);
            //  float crouchRayLength = m_CapsuleHeight - m_Capsule.radius * k_Half;
            //  if (Physics.SphereCast(crouchRay, m_Capsule.radius * k_Half, crouchRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore))
            // {
            //    m_Crouching = true;
            //     return;
            //     }
            //     m_Capsule.height = m_CapsuleHeight;
            //     m_Capsule.center = m_CapsuleCenter;
            //         m_Crouching = false;
            //   }
            //    }







            //  void HandleGroundedMovement(bool crouch, bool jump)
            //   {
            // check whether conditions are right to allow a jump:
            //    if (jump && !crouch && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
            {
                // jump!
                //   m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, m_JumpPower, m_Rigidbody.velocity.z);
                //     m_IsGrounded = false;
                //     m_Animator.applyRootMotion = false;
                //     m_GroundCheckDistance = 0.1f;
                //  }
            }

            void ApplyExtraTurnRotation()
            {
                // help the character turn faster (this is in addition to root rotation in the animation)
                float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
                transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
            }


        }



    }

}