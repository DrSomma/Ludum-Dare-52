using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazeit.Utilities.Random;
using LudumDare52.Animals;
using UnityEngine;

namespace LudumDare52.ItemTransformer.Animals
{
    public class AnimalItemTransformer : ItemTransformer
    {
        [SerializeField]
        private List<AnimalBehavior> animals;

        protected override IEnumerator Produce()
        {
            while (!animals.Any(x => x.CanProcessing))
            {
                yield return new WaitForSeconds(0.3f);
            }

            AnimalBehavior animal = animals.Where(x => x.CanProcessing).RandomElement();

            

            Vector2 foodPos = transform.position;
            Task task = animal.Eat(foodPos);
            yield return new WaitUntil(() => task.IsCompleted);
            containerInput.Storage.TryRemoveFromStorage(input);
            animal.ProgressDisplay.StartAnimation(timeInSeconds);
            yield return new WaitForSeconds(timeInSeconds);
            animal.ProcessingDone();

            WorldEntiySpawner.Instance.Spawn(item: output, position: animal.transform.position);
        }
    }
}