using System;

public class BaseEvent {
    private event Action Changed;

    public void Invoke() => Changed?.Invoke();
    public void Subscribe(Action handler) => Changed += handler;
    public void Unsubscribe(Action handler) => Changed -= handler;
}

public class BaseEvent<T> {
    private event Action<T> Changed;
    public void Invoke(T value) => Changed?.Invoke(value);
    public void Subscribe(Action<T> handler) => Changed += handler;
    public void Unsubscribe(Action<T> handler) => Changed -= handler;
}