﻿using OpenAI.Completions;

namespace AIAssist.Brokers.GraphApis
{
    public partial interface IOpenAIBroker
    {
        public Task<CompletionResult> CreateCompletionAsync(string prompt, string model);

        public Task StreamCompletionAsync(string prompt, string model);
    }
}