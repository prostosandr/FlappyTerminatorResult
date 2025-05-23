using System;

public interface IItem<TItem> where TItem : class
{
    public event Action<TItem> Deactivated;
}
