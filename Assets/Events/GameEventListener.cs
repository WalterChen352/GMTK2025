using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Sourced from https://www.youtube.com/watch?v=7_dyDmF0Ktw

[System.Serializable]
public class CustomGameEvent : UnityEvent<Component, object> {}

public class GameEventListener : MonoBehaviour
{

    [Tooltip("Event to register with.")]
    public GameEvent gameEvent;

    [Tooltip("Response to invoke when Event with GameData is raised.")]
    public CustomGameEvent response;

    private void OnEnable() {
        if (gameEvent != null) gameEvent.RegisterListener(this);
    }

    private void OnDisable() {
        if (gameEvent != null) gameEvent.UnregisterListener(this);
    }

    public void OnEventRaised(Component sender, object data) {
        response.Invoke(sender, data);
    }

}
