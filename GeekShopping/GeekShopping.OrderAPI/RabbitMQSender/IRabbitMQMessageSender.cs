﻿using GeekShoppingMessageBus;

namespace GeekShopping.OrderAPI.RabbitMQSender
{
    public interface IRabbitMQMessageSender
    {
       void SendMessage(BaseMessage baseMessage, string queueName);
    }
}
