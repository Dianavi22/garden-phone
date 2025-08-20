using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }
    private readonly Dictionary<Type, Delegate> _eventDictionary = new();
    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else
            Instance = this;
    }

    public void Subscribe<T>(EventHandler<T> callback) where T : EventArgs
    {
        Type type = typeof(T);

        if (_eventDictionary.TryGetValue(type, out Delegate callbacks))
        {
            _eventDictionary[type] = Delegate.Combine(callbacks, callback);
        }
        else
        {
            _eventDictionary.Add(type, callback);
        }
    }

    public void Unsubscribe<T>(EventHandler<T> callback) where T : EventArgs
    {
        Type type = typeof(T);

        if (_eventDictionary.TryGetValue(type, out Delegate callbacks) )
        {
            _eventDictionary[type] = Delegate.Remove(callbacks, callback);
            if(callbacks == null)
            {
                _eventDictionary.Remove(type);

            }
        }
    }

    public void RaiseEvent<T>(T @event) where T : EventArgs
    {
        if (_eventDictionary.TryGetValue(typeof(T), out Delegate callbacks))
        {
            (callbacks as EventHandler<T>)?.Invoke(this, @event);
        }
    }

    public void SelectTile(Tile tile)
    {
        RaiseEvent(new OnTileSelected() { tileSelected = tile });
    }
}

public class OnTileSelected: EventArgs
{
    public bool tileSelected;
}
