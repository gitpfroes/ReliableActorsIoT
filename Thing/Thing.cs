using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Actors.Client;
using Thing.Interfaces;
using DevicesIoT;
using ThingGroup.Interfaces;

namespace Thing
{
    [StatePersistence(StatePersistence.Persisted)]
    internal class Thing : Actor, IThing
    {
        public Thing(ActorService actorService, ActorId actorId)
            : base(actorService, actorId)
        {
        }

        protected override Task OnActivateAsync()
        {
            State._telemetry = new List<ThingTelemetry>();
            State._deviceGroupId = -1; // not activated
            return base.OnActivateAsync();
        }

        public Task SendTelemetryAsync(ThingTelemetry telemetry)
        {
            State._telemetry.Add(telemetry); // saving data at the device level
            if (State._deviceGroupId != -1)
            {
                var deviceGroup = ActorProxy.Create<IThingGroup>(new ActorId(State._deviceGroupId));
                return deviceGroup.SendTelemetryAsync(telemetry); // sending telemetry data for aggregation
            }
            return Task.FromResult(true);
        }

        public Task ActivateMe(string region, int version)
        {
            State._deviceInfo = new ThingInfo()
            {
                DeviceId = this.GetPrimaryKeyLong(),
                Region = region,
                Version = version
            };

            // based on the info, assign a group... for demonstration we are assigning a random group
            State._deviceGroupId = new Random().Next(10, 12);

            var deviceGroup = ActorProxy.Create<IThingGroup>(new ActorId(State._deviceGroupId));
            return deviceGroup.RegisterDevice(State._deviceInfo);
        }

        private int GetPrimaryKeyLong()
        {
            //recupera ator?
            return this.StateManager.GetStateAsync<int>("ThingState").Id;
//            throw new NotImplementedException();
        }
    }
}
