using Amazeit.Utilities;

namespace LudumDare52.Systems
{
    public class ResourceSystem : Singleton<ResourceSystem>
    {
        protected override void Awake()
        {
            base.Awake();
            AssemblyResources();
        }
        
        private void AssemblyResources()
        {
            //lade scriptableobjects 
            // ItemList = Resources.LoadAll<ItemList>("ItemList").First();
        }
    }
}