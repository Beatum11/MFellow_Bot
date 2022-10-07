using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace MFellow_Bot
{
    public static class Buttons
    {
        public static IReplyMarkup? GetButtons()
        {
            ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
            {
                new KeyboardButton[] { "/start", "/MovieInfo" },
            })
            {
                ResizeKeyboard = true
            };

            return replyKeyboardMarkup;
        }
    }
}
