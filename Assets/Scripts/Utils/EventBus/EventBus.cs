using System;
using System.Collections.Generic;

public static class EventBus
{
    private static Dictionary<Type, List<Delegate>> eventListeners = new Dictionary<Type, List<Delegate>>();

    public static void Subscribe<T>(Action<T> listners)
    {
        if (!eventListeners.ContainsKey(typeof(T)))
        {
            eventListeners[typeof(T)] = new List<Delegate>();
        }

        eventListeners[typeof(T)].Add(listners);
    }

    public static void Unsubscribe<T>(Action<T> listners) 
    {
        if (eventListeners.ContainsKey(typeof(T)))
        {
            eventListeners[typeof(T)].Remove(listners);
        }
    }

    public static void Publish<T>(T publishedEvent)
    {
        if (eventListeners.ContainsKey(publishedEvent.GetType()))
        {
            foreach (var listner in eventListeners[publishedEvent.GetType()])
            {
                ((Action<T>)listner)(publishedEvent);
            }
        }
    }
}