using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace MFellow_Bot
{
    public class Display
    {
        public static async Task DisplayAMovie(ITelegramBotClient botClient,
                         string neededId, CancellationToken cancellationToken, D movie)
        {
            await botClient.SendPhotoAsync(
                    chatId: neededId,
                    photo: movie.i.imageUrl,
                    cancellationToken: cancellationToken);

            await botClient.SendTextMessageAsync(
                chatId: neededId,
                text: $"Name: {movie.l}\n" +
                      $"Main Actors: {movie.s}\n" +
                      $"Rank: {movie.rank}\n" +
                      $"Was shown in: {movie.y}",
                cancellationToken: cancellationToken);
        }
    }
}
