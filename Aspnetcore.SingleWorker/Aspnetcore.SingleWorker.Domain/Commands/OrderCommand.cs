using MediatR;

namespace Aspnetcore.SingleWorker.Domain.Commands
{
    public class OrderCommand : IRequest<string>
    {
        public string Status { get; set; }
    }
}
