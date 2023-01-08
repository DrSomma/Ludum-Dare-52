using System.Linq;
using Amazeit.Utilities.Singleton;
using LudumDare52.Crops.ScriptableObject;
using LudumDare52.Systems;

namespace LudumDare52.Crops
{
    public class CropSelector : Singleton<CropSelector>
    {
        private Crop _selectedCrop;

        private void Start()
        {
            //Todo: select!!!!
            _selectedCrop = ResourceSystem.Instance.CropsList.First();
        }

        public Crop GetCrop()
        {
            return _selectedCrop;
        }
    }
}