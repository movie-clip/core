﻿using System;
using System.Collections.Generic;

namespace Core.Dispatcher
{
    public class Dispatcher : IDispatcher
    {
        public Dictionary<string, List<Action>> _map = new Dictionary<string, List<Action>>();

        public void AddListener(string eventId, Action callback)
        {
            if (!_map.ContainsKey(eventId))
            {
                _map.Add(eventId, new List<Action>());
            }

            _map[eventId].Add(callback);
        }
        
        public void RemoveListener(string eventId, Action callback)
        {
            if (!_map.ContainsKey(eventId))
            {
                return;
            }

            _map[eventId].Remove(callback);
        }

        public void Dispatch(string eventId)
        {
            if (!_map.ContainsKey(eventId))
            {
                return;
            }

            var actions = _map[eventId];
            for (int i = 0; i < actions.Count; i++)
            {
                if(actions[i] != null)
                {
                    actions[i]();
                }
            }
        }
    }
}
