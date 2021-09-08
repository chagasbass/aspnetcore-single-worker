using Amazon.SQS;
using Amazon.SQS.Model;
using Aspnetcore.SingleWorker.CrossCutting.Configurations;
using Aspnetcore.SingleWorker.Domain.Entities;
using Aspnetcore.SingleWorker.Domain.Services;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Aspnetcore.SingleWorker.Infrastructure.Messagings.Services
{
    public class OrderQueueService : IOrderQueueService
    {
        //cliente da amazon para acesso a fila
        private readonly AmazonSQSClient _queueClient;
        private readonly QueueConfigOptions _queueConfigOptions;

        public OrderQueueService(IOptionsMonitor<QueueConfigOptions> options)
        {
            //Criando o cliente e passando a região da fila
            _queueClient = new AmazonSQSClient(Amazon.RegionEndpoint.APSouth1);
            _queueConfigOptions = options.CurrentValue;
        }

        public async Task DeleteMessageAsync(object message)
        {
            var deletedMessage = (Message)message;

            await _queueClient.DeleteMessageAsync(_queueConfigOptions.QueueUrl, deletedMessage.ReceiptHandle);
        }

        public async Task<Order> ReadQueueAsync()
        {
            var receiveMessageRequest = new ReceiveMessageRequest { QueueUrl = _queueConfigOptions.QueueUrl };

            var receiveMessageResponse = await _queueClient.ReceiveMessageAsync(receiveMessageRequest);

            if (receiveMessageResponse.HttpStatusCode == HttpStatusCode.OK)
            {
                var message = receiveMessageResponse.Messages[0];

                var order = JsonSerializer.Deserialize<Order>(message.Body);
                return order;
            }

            return default;
        }

        #region Implementações de teste da fila

        /// <summary>
        /// Efetua a criação da fila caso não exista
        /// </summary>
        /// <param name="amazonSQSClient"></param>
        /// <param name="queueName"></param>
        /// <param name="visibilityTimeout"></param>
        /// <returns></returns>
        private async Task<string> CreateQueueAsync(AmazonSQSClient amazonSQSClient, string queueName, string visibilityTimeout)
        {
            var queueConfigurations = new Dictionary<string, string>();
            queueConfigurations.Add(QueueAttributeName.VisibilityTimeout, visibilityTimeout);

            var createQueueRequest = new CreateQueueRequest
            {
                QueueName = queueName,
                Attributes = queueConfigurations
            };

            var createQueueResponse = await amazonSQSClient.CreateQueueAsync(createQueueRequest);

            return createQueueResponse.QueueUrl;
        }

        /// <summary>
        /// Escreve na fila
        /// </summary>
        /// <param name="orderMessage"></param>
        /// <param name="queueUrl"></param>
        /// <returns></returns>
        private async Task<Order> SendMessageAsync(Order orderMessage, string queueUrl)
        {
            var message = JsonSerializer.Serialize(orderMessage);

            var messageRequest = new SendMessageRequest
            {
                QueueUrl = queueUrl,
                MessageBody = message
            };

            var sendMessageResponse = await _queueClient.SendMessageAsync(messageRequest);

            var response = sendMessageResponse.MD5OfMessageBody;

            return JsonSerializer.Deserialize<Order>(response);
        }

        /// <summary>
        /// Recupera o numero de mensagens na fila
        /// </summary>
        /// <param name="queueUrl"></param>
        /// <returns></returns>
        private async Task<int> GetNumberOfMessagesInQueueAsync(string queueUrl)
        {
            var queueAttributesRequest = new GetQueueAttributesRequest();
            queueAttributesRequest.QueueUrl = queueUrl;
            queueAttributesRequest.AttributeNames.Add("ApproximateNumberOfMessages");

            var response = await _queueClient.GetQueueAttributesAsync(queueAttributesRequest);

            //response.ApproximateNumberOfMessages = numero aproximado
            //response.ApproximateNumberOfMessagesNotVisible = mensagens não visiveis
            //response.ApproximateNumberOfMessagesDelayed = mensagens atrasadas

            return response.ApproximateNumberOfMessages;
        }

        /// <summary>
        /// Efetua a  exclusão de uma mensagem da fila
        /// </summary>
        /// <param name="queueUrl">url da fila</param>
        /// <param name="message">objeto que representa a mensagem da fila</param>
        /// <returns></returns>
        private async Task DeleteMessagesAsync(string queueUrl, Message message) => await _queueClient.DeleteMessageAsync(queueUrl, message.ReceiptHandle);

        #endregion
    }
}
