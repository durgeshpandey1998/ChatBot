// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.18.1

using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System;

namespace ChatBot2.Bots
{
    public class EchoBot : ActivityHandler
    {
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var replyText = $"Echo: {turnContext.Activity.Text}";
           replyText = await ReadJsonFile(turnContext.Activity.Text);
            await turnContext.SendActivityAsync(MessageFactory.Text(replyText, replyText), cancellationToken);
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            var welcomeText = "Hello and welcome!";
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText, welcomeText), cancellationToken);
                }
            }
        }

        public async Task<string> ReadJsonFile(string question)
        {
              var json = File.ReadAllText(@"D:\GenericRepositoryPattern\ChatBoatProject\ChatBot2\wwwroot\JsonFile\Chatbot.json");
            var jObject = JObject.Parse(json);
            string returnMessage = null;
            if (question.ToLower()=="hello" || question.ToLower() == "how can you help me? " || question.ToLower() == "what can you do?" || question.ToLower() == "hi")
            {  
                returnMessage = jObject["welcome"].ToString();
            }
           else if (question.ToLower() == "what is your name ?")
            {
                returnMessage = "Chat bot";
            }
           else if (question.ToLower() == "what is my name ?")
            {
                returnMessage = "Xyz";
            }
           else if (question.ToLower() =="are you a robot?")
            {
                returnMessage = jObject["areyourobot"].ToString();
            }
            else if (question.ToLower() == "who is your father ?" || question.ToLower() == "who invent you?" || question.ToLower() == "how old are you?" || question.ToLower() == "what’s your age?")
            {
                returnMessage = jObject["createdby"].ToString() + DateTime.Now.ToString();
            }
            else if (question.ToLower() == "how are you?" || question.ToLower() == "how are you doing?" || question.ToLower() == "how are you doing going?" || question.ToLower() == "what’s up?")
            {
                returnMessage = jObject["iamgood"].ToString();
            }
            else if (question.ToLower() == "tell me something" || question.ToLower() == "tell me something about yourself")
            {
                returnMessage = jObject["tellesomething"].ToString();
            }
            else if (question.ToLower() == "thank you" || question.ToLower() == "goodbye")
            {
                returnMessage = jObject["thankyou"].ToString();
            }
            else if (question.ToLower() == "will you marry me?" || question.ToLower() == "are you single?")
            {
                returnMessage = jObject["marry"].ToString();
            }
            else if(question.ToLower() == "what day is it today?")
            {
                returnMessage = DateTime.Now.ToString("dddd");
            }
            else
            {
                returnMessage = jObject["messageforemail"].ToString();
            }
            return returnMessage;
        }
    }
}
