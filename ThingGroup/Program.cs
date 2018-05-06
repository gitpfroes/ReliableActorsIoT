using System;
using System.Diagnostics;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors.Runtime;

namespace ThingGroup
{
    internal static class Program
    {
        /// <summary>
        /// Este é o ponto de entrada do processo de host de serviço.
        /// </summary>
        private static void Main()
        {
            try
            {
                // Esta linha registra um Serviço de Ator para hospedar sua classe de ator com o tempo de execução do Service Fabric.
                // Os conteúdos dos seus arquivos ServiceManifest.xml e ApplicationManifest.xml
                // são populados automaticamente ao compilar este projeto.
                // Para obter mais informações, consulte https://aka.ms/servicefabricactorsplatform

                ActorRuntime.RegisterActorAsync<ThingGroup>(
                   (context, actorType) => new ActorService(context, actorType)).GetAwaiter().GetResult();

                Thread.Sleep(Timeout.Infinite);
            }
            catch (Exception e)
            {
                ActorEventSource.Current.ActorHostInitializationFailed(e.ToString());
                throw;
            }
        }
    }
}
