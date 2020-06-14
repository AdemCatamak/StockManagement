﻿using System;

 namespace StockManagement.Api.Contracts.Responses
{
    public class ErrorHttpResponse
    {
        public ErrorHttpResponse(string friendlyMessage, string exceptionType = null)
        {
            FriendlyMessage = friendlyMessage ?? throw new ArgumentNullException(nameof(friendlyMessage));
            ExceptionType = exceptionType;
        }

        public string FriendlyMessage { get; }
        public string ExceptionType { get; }
    }
}