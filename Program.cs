// Program.cs
// Zulaikha Zakiullah
// This program controls the Discord bot that's based on past profs.

using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace ProfBot
{
    class Program
    {
        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;

        public async Task MainAsync()
        {

            _client = new DiscordSocketClient();

            _client.Log += Log;
            _client.MessageReceived += MessageReceived;
            

            // Token is stored in environment variables for security reasons
            await _client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("DiscordToken", EnvironmentVariableTarget.User));
            await _client.StartAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private async Task MessageReceived(SocketMessage message)
        {
            string content = message.Content.ToLower();

            if (content == "!ping")
            {
                await message.Channel.SendMessageAsync("Pong!");
            }
            else if (content.Contains("cry") || content.Contains("cri"))
            {
                await message.AddReactionAsync(Emote.Parse("<:cry:674774877550411786>"));
            }
            else if (content.Contains("cool") || content.Contains("kool"))
            {
                await message.AddReactionAsync(Emote.Parse("<:coolio:625922857997959178>"));
                await message.AddReactionAsync(Emote.Parse("<:cool_guy:625924283272527882>"));
            }
        }

        public async Task ReactWithEmoteAsync(SocketUserMessage userMsg, string escapedEmote)
        {
            if (Emote.TryParse(escapedEmote, out var emote))
            {
                await userMsg.AddReactionAsync(emote);
            }
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
