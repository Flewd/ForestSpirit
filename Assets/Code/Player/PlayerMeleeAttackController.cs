using System;
using System.Collections;
using UnityEngine;

namespace Game.Code.Player
{
    public class PlayerMeleeAttackController : MonoBehaviour, IOnTriggerListener
    {
        [SerializeField] private Animator _animationController;
        
        private string _ANIMATION_TRIGGER_MELEE = "melee";

        private bool IsAttacking = false;
        
        // Called from unity input system
        public void OnMelee()
        {
            _animationController.SetTrigger(_ANIMATION_TRIGGER_MELEE);
            StartCoroutine(SetIsAttacking());
        }

        IEnumerator SetIsAttacking()
        {
            IsAttacking = true;
            yield return new WaitForSeconds(0.4f);
            IsAttacking = false;
        }
        
        // Weapon triggers
        void IOnTriggerListener.OnTriggerStay(Collider other)
        {
            if (!IsAttacking)
            {
                return;
            }

            if (other.tag == "Spell" || other.tag == "Player")
            {
                IHitByMelee meleeListener = other.gameObject.GetComponentWithInterface<IHitByMelee>();
                meleeListener?.HitByMelee(gameObject);
                Debug.Log("Trigger Enter: " + other.tag);
            }
        }

        void IOnTriggerListener.OnTriggerEnter(Collider other)
        {
            // don't need
        }

        void IOnTriggerListener.OnTriggerExit(Collider other)
        {
            // don't need
        }
    }
}