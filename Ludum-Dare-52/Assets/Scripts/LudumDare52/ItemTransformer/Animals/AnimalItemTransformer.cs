using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazeit.Utilities.Random;
using LudumDare52.Animals;
using LudumDare52.Storage;
using UnityEngine;

namespace LudumDare52.ItemTransformer.Animals
{
    public class AnimalItemTransformer : ItemTransformer
    {
        [SerializeField]
        private List<AnimalBehavior> animals;

        [SerializeField]
        private StorageDisplay display;

        protected override IEnumerator Produce()
        {
            while (!animals.Any(x => x.CanProcessing))
            {
                yield return new WaitForSeconds(0.3f);
            }
            
            AnimalBehavior animal = animals.Where(x => x.CanProcessing).RandomElement();

            Vector2 foodPos = display.GetEnitiyPosInWorld(input);
            Task task = animal.Eat(foodPos);
            yield return new WaitUntil(() => task.IsCompleted);
            containerInput.Storage.TryRemoveFromStorage(input);
            yield return new WaitForSeconds(timeInSeconds);
            animal.ProcessingDone();

            WorldEntiySpawner.Instance.Spawn(output, animal.transform.position);
        }
    }
}