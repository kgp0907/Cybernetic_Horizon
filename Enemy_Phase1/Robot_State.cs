public interface Robot_State<T>
{
    void OnEnter(T sender);
    void OnExit(T sender);
    void OnFixedUpdate(T sender);
    void OnUpdate(T sender);
}

public interface Robot_State2<T>
{
    void OnEnter(T sender);
    void OnExit(T sender);
    void OnFixedUpdate(T sender);
    void OnUpdate(T sender);
}


