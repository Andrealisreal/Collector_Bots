using Generics.Objects;

namespace Units
{
    public class UnitPool : ObjectsPool<Unit>
    {
        protected override void OnObjectReleased(Unit item) =>
            item.Released -= OnObjectReleased;
    }
}
