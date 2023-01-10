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

        private void Start()
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
                    _currentLoop = StartCoroutine(MoveToTaget());
                    break;
                }
                case AnimalGoal.Processing:
                {
                    // SetGoal(AnimalGoal.Idle);
                    Debug.Log("Processing");
                    break;
                }
                default: throw new ArgumentOutOfRangeException(paramName: nameof(newGoal), actualValue: newGoal, message: null);
            }
        }

        private IEnumerator MoveToTaget()
        {
            animalMovement.StopMovement();
            while (true)
            {
                yield return new WaitUntil(() => animalMovement.MoveToPosition().IsCompleted);
                SetGoal(AnimalGoal.Processing);
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