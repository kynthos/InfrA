using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    LinkedList<KeyCode> reservedKeyCode = new LinkedList<KeyCode>();

    Dictionary<KeyCode, Action<KeyCode>> upEvent = new Dictionary<KeyCode, Action<KeyCode>>();
    Dictionary<KeyCode, Action<KeyCode>> downEvent = new Dictionary<KeyCode, Action<KeyCode>>();

    public bool inputCheckEnable;

    private void Update()
    {
        if (!inputCheckEnable)
            return;

        for (var node = reservedKeyCode.First; node != null;)
        {
            var nextNode = node.Next;

            KeyCode keyCode = node.Value;

            if (Input.GetKeyDown(keyCode))
                upEvent[keyCode]?.Invoke(keyCode);

            else if (Input.GetKeyUp(keyCode))
                downEvent[keyCode]?.Invoke(keyCode);

            node = nextNode;
        }
    }

    public void AddUpEvent(KeyCode keycode, Action<KeyCode> action)
    {
        reservedKeyCode.AddLast(keycode);
    }

    public void RemoveUpEvent(KeyCode keyCode, Action<KeyCode> action)
    {
        reservedKeyCode.Remove(keyCode);
    }

    public void AddDownEvent(KeyCode keycode, Action<KeyCode> action)
    {
        reservedKeyCode.AddLast(keycode);
        downEvent.Add(keycode, action);
    }
}
