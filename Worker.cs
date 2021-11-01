using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PokeApiNet;
using Telegram.Bot;
using Telegram.Bot.Args;
using PokemonBot.Services;
using Microsoft.Extensions.Configuration;

namespace PokemonBot
{
    public class Worker : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<Worker> _logger;
        private readonly TelegramBotClient _bot;
        private readonly PokeApiClient _pokeApi;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = logger;
            _bot = new TelegramBotClient(_configuration["TelegramBot:Token"]);
            _pokeApi = new PokeApiClient();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _bot.StartReceiving();
            _bot.OnMessage += MessageEventHandler;
        }

        private async void MessageEventHandler(object sender, MessageEventArgs e)
        {
            _logger.LogInformation(DateTime.Now + " : " + e.Message.Text + " : " + e.Message.Chat.FirstName);

            try
            {                
                PokeService pokeData = new PokeService(await _pokeApi.GetResourceAsync<Pokemon>(e.Message.Text));

                await _bot.SendPhotoAsync(e.Message.Chat.Id, pokeData.Image);
                await _bot.SendTextMessageAsync(e.Message.Chat.Id, pokeData.GetPokemonInfo(),parseMode:Telegram.Bot.Types.Enums.ParseMode.Markdown);
            }
            catch (System.Exception)
            {
                await _bot.SendTextMessageAsync(e.Message.Chat.Id, "Pokemon no encontrado. "+char.ConvertFromUtf32(0x1F635));
            }
        }
    }
}
