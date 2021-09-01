using MediatR;
using System;

namespace Aspnetcore.SingleWorker.Domain.Events
{
    public class OrderEvent : INotification
    {
        public DateTime Timestamp { get; private set; }
        public string Status { get; private set; }

        public OrderEvent(string status)
        {
            Timestamp = DateTime.Now;
            Status = status;
        }
    }
}
