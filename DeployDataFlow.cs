using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using LCU.State.API.NapkinIDE.DataFlowManager.Models;
using LCU.State.API.NapkinIDE.DataFlowManager.Services;

namespace LCU.State.API.NapkinIDE.DataFlowManager
{
    [Serializable]
    [DataContract]
    public class DeployDataFlowRequest
    {
        [DataMember]
        public virtual string DataFlowLookup { get; set; }
    }

    public static class DeployDataFlow
    {
        [FunctionName("DeployDataFlow")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Admin, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            return await req.Manage<DeployDataFlowRequest, DataFlowManagerState, DataFlowManagerStateHarness>(log, async (mgr, reqData) =>
            {
                log.LogInformation($"Deploying Data Flow: {reqData.DataFlowLookup}");

                return await mgr.DeployDataFlow(reqData.DataFlowLookup);
            });
        }
    }
}
