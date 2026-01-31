using System.Collections;
using LuduArtsCase.Runtime.Core;
using UnityEngine;
using UnityEngine.Events;

namespace LuduArtsCase.Runtime.Interactables
{
    public class Switch : InteractableBase
    {
        #region Fields
        [Header("Switch Settings")]
        [SerializeField] private bool m_StartOn = false;
        [SerializeField] private float m_OpenAngle = 90f;
        [SerializeField] private float m_animationDuration = 0.2f;
        [SerializeField] private string m_SwitchOnPromptText = "Turn Off";
        [SerializeField] private string m_SwitchOffPromptText = "Turn on";
        
        [Header("References")]
        [SerializeField] private Transform m_LeverTransform;
        
        private bool m_IsOn=false;
        private Quaternion m_ClosedRotation;
        private Quaternion m_OpenedRotation;
        public UnityEvent OnSwitchOn;
        public UnityEvent OnSwitchOff;
        #endregion
        
        #region Unity Methods
        protected override void Awake()
        {
            base.Awake();
            m_IsOn = m_StartOn;
            
            m_ClosedRotation = m_LeverTransform.localRotation;
            m_OpenedRotation = m_ClosedRotation * Quaternion.Euler(0, 0, m_OpenAngle);

            if (m_IsOn)
            {
                m_LeverTransform.localRotation = m_OpenedRotation;
                SetPromptText(m_SwitchOnPromptText);
            }
            else
            {
                m_LeverTransform.localRotation = m_ClosedRotation;
                SetPromptText(m_SwitchOffPromptText);
            }
        }
        #endregion
        
        #region Public Methods
        public override bool Interact(GameObject interactor)
        {
            ToggleSwitch();
            return true;
        }
        #endregion
        
        #region Private Methods
        
        private void ToggleSwitch()
        {
            m_IsOn = !m_IsOn;

            StopAllCoroutines();
            StartCoroutine(PlaySwitchAnimation(m_IsOn));

            if (m_IsOn)
            {
                Debug.Log("Switch ON Triggered");
                OnSwitchOn?.Invoke();
                SetPromptText(m_SwitchOnPromptText);
                InteractionUI.UpdatePromptText(m_SwitchOnPromptText);
            }
            else
            {
                Debug.Log("Switch OFF Triggered");
                OnSwitchOff?.Invoke();
                SetPromptText(m_SwitchOffPromptText);
                InteractionUI.UpdatePromptText(m_SwitchOffPromptText);
            }
        }
        
        IEnumerator PlaySwitchAnimation(bool turnOn)
        {
            float elapsedTime = 0f;
            Quaternion startRotation = m_LeverTransform.localRotation;
            Quaternion endRotation = turnOn ? m_OpenedRotation : m_ClosedRotation;

            while (elapsedTime < m_animationDuration)
            {
                m_LeverTransform.localRotation = Quaternion.Slerp(startRotation, endRotation, (elapsedTime / m_animationDuration));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            m_LeverTransform.localRotation = endRotation;
        }
        #endregion
    }
}
