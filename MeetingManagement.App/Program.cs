using MeetingManagement.App.Clients;
using MeetingManagement.App.Common;
using MeetingManagement.App.Controllers;
using MeetingManagement.Domain.Interfaces.Clients;
using MeetingManagement.Domain.Interfaces.Common;
using MeetingManagement.Domain.Interfaces.Services;
using MeetingManagement.Services.Services;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddSingleton<IMeetingService, MeetingService>();
services.AddScoped<IMeetingAdditionClient, MeetingAdditionClient>();
services.AddScoped<IMeetingUpdateClient, MeetingUpdateClient>();
services.AddScoped<IFillData, FillData>();

var serviceProvider = services.BuildServiceProvider();

var meetingController = new MeetingController(serviceProvider.GetService<IMeetingService>(), serviceProvider.GetService<IMeetingAdditionClient>(), serviceProvider.GetService<IMeetingUpdateClient>());

while (true)
{
    Console.WriteLine("Добро пожаловать в планировщик встреч!");
    Console.WriteLine("Нажмите 'S' чтобы начать.");
    Console.WriteLine("Нажмите 'Escape' чтобы завершить работу.");

    switch (Console.ReadKey(true).Key)
    {
        
        case ConsoleKey.S:
            meetingController.StartApp();
            break;
        case ConsoleKey.Escape:
            ConsoleOutput.ConsoleWriteLineWithColor("Работа завершена!", ConsoleColor.Green);
            return;
        default:
            ConsoleOutput.ConsoleWriteLineWithColor("Неверная клавиша.", ConsoleColor.Red);
            break;
    }

}
