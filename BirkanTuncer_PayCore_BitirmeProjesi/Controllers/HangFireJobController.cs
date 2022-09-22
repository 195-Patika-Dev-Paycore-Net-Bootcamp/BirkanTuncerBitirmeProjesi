using Hangfire;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BirkanTuncer_PayCore_BitirmeProjesi.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class HangFireJobController : ControllerBase
    {
        /// <summary>
        /// The standard hangfire controller is not visible from swagger because it is marked with nonaction
        /// </summary>
        public HangFireJobController()
        {

        }

        [NonAction]
        [HttpGet]
        [Route("FireAndForget")]
        public string FireAndForgetJob()
        {
            var jobId = BackgroundJob.Enqueue(() => JobFireAndForget.Run());
            return jobId;
        }

        [NonAction]
        [HttpGet]
        [Route("Delayed")]
        public string DelayedJob()
        {
            var id = BackgroundJob.Schedule(() => JobDelayed.Run("Patika"), TimeSpan.FromSeconds(25));
            return id;
        }

        [NonAction]
        [HttpGet]
        [Route("Recurring")]
        public string Recurring(string cron)
        {
            RecurringJob.AddOrUpdate(() => JobRecurring.Run(), cron);
            return "PXT1";
        }

        [NonAction]
        [HttpGet]
        [Route("Continuations")]
        public string ContinuationsJob()
        {
            var job1 = BackgroundJob.Schedule(() => JobDelayed.Run(), TimeSpan.FromSeconds(85));
            var resultId = BackgroundJob.ContinueJobWith(job1, () => JobContinuations.Run());
            var resultId2 = BackgroundJob.ContinueJobWith(resultId, () => JobDelayed.Run());
            return resultId2;
        }

    }
}
