﻿using System;
using Raven.Imports.Newtonsoft.Json.Linq;
using Raven.Imports.SignalR.Client.Infrastructure;

namespace Raven.Imports.SignalR.Client.Hubs
{
    /// <summary>
    /// <see cref="T:System.IObservable{object[]}"/> implementation of a hub event.
    /// </summary>
    public class Hubservable : IObservable<JToken[]>
    {
        private readonly string _eventName;
        private readonly IHubProxy _proxy;

        public Hubservable(IHubProxy proxy, string eventName)
        {
            _proxy = proxy;
            _eventName = eventName;
        }

        public IDisposable Subscribe(IObserver<JToken[]> observer)
        {
            var subscription = _proxy.Subscribe(_eventName);
            subscription.Data += observer.OnNext;

            return new DisposableAction(() =>
            {
                subscription.Data -= observer.OnNext;
            });
        }
    }
}
