using MFellow_Bot;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Reflection;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

string key = "TOKEN";

//This is a new movie object
var movie = new D();

#region Basic Set Up
var botClient = new TelegramBotClient(key);

using var cts = new CancellationTokenSource();

// StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
var receiverOptions = new ReceiverOptions
{
    AllowedUpdates = Array.Empty<UpdateType>() // receive all update types
};
botClient.StartReceiving(
    updateHandler: HandleUpdateAsync,
    pollingErrorHandler: HandlePollingErrorAsync,
    receiverOptions: receiverOptions,
    cancellationToken: cts.Token
);

var me = await botClient.GetMeAsync();

Console.WriteLine($"Start listening for @{me.Username}");
Console.ReadLine();

// Send cancellation request to stop bot
cts.Cancel();
#endregion



async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{

    var message = update.Message;
    Console.WriteLine($"Received a '{message.Text}' message in chat {message.Chat.Id}.");

    if (message?.Text == "/start")
    {
        await botClient.SendTextMessageAsync(
           chatId: message.Chat.Id,
           text: WelcomeMsg.msg,
           replyMarkup: Buttons.GetButtons(),
           cancellationToken: cancellationToken);
    }

    if (message?.Text == "/MovieInfo")
    {
        await botClient.SendTextMessageAsync(
          chatId: message.Chat.Id,
          text: "Write your movie:",
          cancellationToken: cancellationToken);
    }

    if(message?.Text != null && message?.Text != "/start" && message?.Text != "/MovieInfo")
    {
        //Initializing of a movie class (D).
        movie = await FindMovie.MovieInform(message.Text);

        await Display.DisplayAMovie(botClient, 
            message.Chat.Id.ToString(), cancellationToken, movie);
    }
}




Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
{
    var ErrorMessage = exception switch
    {
        ApiRequestException apiRequestException
            => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
        _ => exception.ToString()
    };

    Console.WriteLine(ErrorMessage);
    return Task.CompletedTask;
}

