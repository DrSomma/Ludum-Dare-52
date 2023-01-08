using System;
using Amazeit.Utilities.Singleton;

namespace LudumDare52.Storage.Money
{
    public class MoneyManager : Singleton<MoneyManager>
    {
        private int _money;
        public int Monay => _money;

        public Action<int, int> OnUpdateMoney; //value to add, new sum
        
        public void AddMoney(int add)
        {
            _money += add;
            OnUpdateMoney?.Invoke(add, _money);
        }
    }
}