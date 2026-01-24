using StreamingApp.Core.Queries.Alerts.Interfaces;
using StreamingApp.Domain.Entities.Dtos.Twitch;

namespace StreamingApp.Core.Queries.Alerts;

public class SubAlertLoong : ISubAlertLoong
{
    public async Task<AlertDto> Execute(string userName, int Length, int rotation, int saturation, bool directionltr)
    {
        string src = "assets/Stream/";
        string body = "";

        int width = (56 * Length) + (2 * 56);
        int bottom = new Random().Next(-10, 10);
        int animationDuration = 15;

        //Image Colour Change: https://stackoverflow.com/questions/7415872/change-color-of-png-image-via-css

        string directionLogic = "";
        string image = "pyxtriSnakewalk";

        string head = $"<img class=\"{userName}bildmed\" src=\"{src}{image}.png\" alt=\"Head\">";
        for (int t = 0; t < Length; t++)
        {
            body += $"<img class=\"{userName}bildmed\" src=\"{src}{image}.png\" alt=\"Body\">";
        }
        string tail = $"<img class=\"{userName}bildmed\" src=\"{src}{image}.png\" alt=\"Tail1\">";//$"<img class=\"{userName}bildmed\" src=\"{src}{image}.png\" alt=\"Tail2\">";

        if (directionltr)
        {
            directionLogic = $"#{userName}" + " { width:" + $"{width}px" + $"; bottom: {bottom}px; position: relative; animation: linear infinite; animation-name: " + $"{userName}" + $"; animation-duration: {animationDuration}s;"+" animation-iteration-count: 1 } @keyframes " + $"{userName}" + " { 0% {left: " + $"-{width}px" + ";transform: rotateY(180deg); } 100% { left: calc(100% + 600px); transform: rotateY(180deg); }}";
        }
        else
        {
            directionLogic = "#target { width: " + $"{width}px;" + " bottom: 0;  position: relative;  animation: linear infinite;  animation-name: run;  animation-duration: " + animationDuration + "s;  animation-iteration-count: 1}@keyframes run {  0% { left: 100%; } 48% { transform: rotateY(180deg); } 100% { left: -100%; transform: rotateY(180deg); }}";
        }

        var sub = "<html lang=\"en\"> <body>" +
            "<div id=\""+$"{userName}"+"\">" +
                "<div>" +
                   $"{head}" +
                   $"{body}" +
                   $"{tail}" +
                "</div>" +
            "</div>" +
            "<style>" +
                 $".{userName}bildmed"+"{  filter: " + $"hue-rotate({rotation}deg) saturate({saturation}%)" + "; height: 50px; }" +
                 $"{directionLogic}" +
            "</body>" +
            "</style>";

        return new AlertDto() { Html = sub, Duration = animationDuration };
    }
}
