using System;

public interface INotifyDestroy
{
    event Action<INotifyDestroy> WillBeDestroyed;
}