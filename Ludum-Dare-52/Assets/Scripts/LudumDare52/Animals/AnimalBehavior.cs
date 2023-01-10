using System;
using System.Collections;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace LudumDare52.Animals
{
    public class AnimalBehavior : MonoBehaviour
    {
        [SerializeField]
        private AnimalMovement animalMovement;

        private Coroutine _currentLoop;

        private AnimalGoal _goal;

        public bool CanProcessing => _goal == AnimalGoal.Idle;

        private void Start()
        {
            SetGoal(AnimalGoal.Idle);
        }

        public async Task Eat(Vector2 pos)
        {
            SetGoal(AnimalGoal.Eat);
            await animalMovement.MoveToPosition(pos);
            SetGoal(AnimalGoal.Processing);
        }
        
        public void ProcessingDone()
        {
            SetGoal(AnimalGoal.Idle);
        }

        private void SetGoal(AnimalGoal newGoal)
        {
            if (_goal == newGoal)
            {
                return;
            }

            _goal = newGoal;
            OnUpdateGoal(_goal);
        }

        private void OnUpdateGoal(AnimalGoal newGoal)
        {
            if (_currentLoop != null)
            {
                StopCoroutine(_currentLoop);
            }

            switch (newGoal)
            {
                case AnimalGoal.Idle:
                {
                    _currentLoop = StartCoroutine(IdleLoop());
                    break;
                }
                case AnimalGoal.Eat:
                {
                    break;
                }
                case AnimalGoal.Processing:
                {
                    Debug.Log("Processing");
                    _currentLoop = StartCoroutine(IdleLoop());
                    break;
                }
                default: throw new ArgumentOutOfRangeException(paramName: nameof(newGoal), actualValue: newGoal, message: null);
            }
        }
        

        private IEnumerator IdleLoop()
        {
            animalMovement.StopMovement();
            while (true)
            {
                Task task = animalMovement.MoveToNextWaypoint();
                yield return new WaitUntil(() => task.IsCompleted);
            }
        }

        private enum AnimalGoal
        {
            NoGoal,
            Idle,
            Eat,
            Processing
        }
    }
}