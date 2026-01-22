using System.Collections.Generic;

namespace Resources
{
    public class ResourceHandler
    {
        private readonly Queue<Resource> _freeResources = new();
        private readonly HashSet<Resource> _allResources = new();
    
        public void Add(Resource resource)
        {
            if (resource == null)
                return;

            if (_allResources.Add(resource))
                _freeResources.Enqueue(resource);
        }
    
        public bool TryGetFree(out Resource resource)
        {
            while (_freeResources.Count > 0)
            {
                resource = _freeResources.Dequeue();

                if (resource != null)
                    return true;
            }

            resource = null;
        
            return false;
        }
    
        public void Release(Resource resource)
        {
            if (resource == null)
                return;

            if (_allResources.Contains(resource))
                _freeResources.Enqueue(resource);
        }
    }
}