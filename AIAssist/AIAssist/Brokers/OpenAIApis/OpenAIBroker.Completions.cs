﻿using OpenAI.Completions;
using OpenAI.Models;

namespace AIAssist.Brokers.GraphApis
{
    public partial class OpenAIBroker : IOpenAIBroker
    {
        public async Task<CompletionResult> CreateCompletionAsync(string prompt, string model)
        {
            var completionResult = await this.openAIClient.CompletionsEndpoint.CreateCompletionAsync(prompt: prompt, model: new Model(model));
            return completionResult;
        }

        public async Task StreamCompletionAsync(string prompt, string model)
        {
            await this.openAIClient.CompletionsEndpoint.StreamCompletionAsync(result =>
            {
                foreach (var choice in result.Completions)
                {
                    Console.Write(choice);
                }
            }, prompt: prompt, maxTokens: 200, temperature: 0.5, presencePenalty: 0.1, frequencyPenalty: 0.1, model: new Model(model));

            Console.WriteLine();
        }
    }
}
