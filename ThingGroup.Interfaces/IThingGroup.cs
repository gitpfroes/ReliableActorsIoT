using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using DevicesIoT;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Remoting.FabricTransport;
using Microsoft.ServiceFabric.Services.Remoting;

[assembly: FabricTransportActorRemotingProvider(RemotingListener = RemotingListener.V2Listener, RemotingClient = RemotingClient.V2Client)]
namespace ThingGroup.Interfaces
{
    public interface IThingGroup : IActor
    {
        Task RegisterDevice(ThingInfo deviceInfo);
        Task UnregisterDevice(ThingInfo deviceInfo);
        Task SendTelemetryAsync(ThingTelemetry telemetry);
    }

    [DataContract]
    class ThingGroupState
    {
        [DataMember]
        public List<ThingInfo> _devices;
        [DataMember]
        public Dictionary<string, int> _faultsPerRegion;
        [DataMember]
        public List<ThingInfo> _faultyDevices;
    }
}
