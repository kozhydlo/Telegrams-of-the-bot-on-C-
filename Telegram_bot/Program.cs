using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram_Bot
{
    class Program
    {
        private static string Token { get; set; } = "Ваш токен тут";
        private static TelegramBotClient client;

        static void Main(string[] args)
        {
            client = new TelegramBotClient(Token);
            client.StartReceiving();
            client.OnMessage += OnMessageHandler;
            Console.ReadLine();
            client.StopReceiving();
        }

        private static async void OnMessageHandler(object sender, MessageEventArgs e)
        {
            var msg = e.Message;
            if (msg.Text != null)
            {
                Console.WriteLine($"Прийшло оповішення з текстом: {msg.Text}");
                switch (msg.Text)
                {
                    case "Стикер":
                        await client.SendStickerAsync(
                            chatId: msg.Chat.Id,
                            sticker: "Силка на стікери",
                            replyToMessageId: msg.MessageId,
                            replyMarkup: GetButtons());
                        break;
                    case "Картинка":
                        await client.SendPhotoAsync(
                            chatId: msg.Chat.Id,
                            photo: "Силка на стікери",
                            replyMarkup: GetButtons());
                        break;

                    default:
                        await client.SendTextMessageAsync(msg.Chat.Id, "Виберіть команду: ", replyMarkup: GetButtons());
                        break;
                }
            }
        }

        private static IReplyMarkup GetButtons()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{ new KeyboardButton { Text = "Стікер"}, new KeyboardButton { Text = "Картинка"} },
                    new List<KeyboardButton>{ new KeyboardButton { Text = "123"}, new KeyboardButton { Text = "456"} }
                }
            };
        }
    }
}