using Aspnetcore.SingleWorker.Shared.BaseCommands;
using System;

namespace Aspnetcore.SingleWorker.Domain.Commands
{
    public class ResultCommand : IResult
    {
        public string Status { get; set; }
        public DateTime Timestamp { get; set; }

        public ResultCommand()
        {
            Timestamp = DateTime.Now;
        }
    }
}
