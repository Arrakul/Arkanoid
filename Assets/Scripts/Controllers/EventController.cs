using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class EventController : MonoBehaviour
    {
        public static EventController Instance;
        [SerializeField] EventObject objectEvent;
        [SerializeField] private int percentageDrop = 10;
        [SerializeField] private const int MaxPercent = 100;

        [SerializeField] public List<Events> Eventses;
    
        [Serializable]
        public struct Events
        {
            public int PercentMin;
            public int PercentMax;
            public EventGame EventGame;
        }
    
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }
    
        void Start()
        {
            OnSubscription();
        }

        private void OnSubscription()
        {
            BallController.Instance.onDamageBlock += CreateEventObject;
        } 

        private void OnDestroy()
        {
            BallController.Instance.onDamageBlock -= CreateEventObject;
        }

        private void CreateEventObject()
        {
            int percent = Random.Range(0, MaxPercent);

            if (percent >= percentageDrop)
            {
                Instantiate(objectEvent);
            }
        }

        public void EventGeneration()
        {
            int percent = Random.Range(0, MaxPercent);

            foreach (var _event in Eventses)
            {
                if (_event.PercentMin >= percent && _event.PercentMax >= percent)
                {
                    _event.EventGame.Play();
                }
            }
        }
    }
}