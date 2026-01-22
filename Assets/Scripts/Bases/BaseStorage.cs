using System;

namespace Bases
{
    public class BaseStorage
    {
        private int _count;

        public event Action<int> CountChanged;
        
        public void ChangeCount()
        {
            _count++;
            CountChanged?.Invoke(_count);
        }
    }
}