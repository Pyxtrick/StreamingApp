using StreamingApp.Domain.Entities.Dtos.Twitch;

namespace StreamingApp.Core.Queries.Alerts;

public class SubAlertLoong : ISubAlertLoong
{
    public async Task<AlertDto> Execute(string userName, int Length)
    {
        int rot = new Random().Next(1, 360);
        int sat = new Random().Next(1, 1000);

        string body = "";

        int width = (56 * Length) + (2 * 56);

        //Image Colour Change: https://stackoverflow.com/questions/7415872/change-color-of-png-image-via-css

        string cssltr = $"#{userName}"+" { width:" + $"{width}px" + "; bottom: 0; position: relative; animation: linear infinite; animation-name: "+$"{userName}"+"; animation-duration: 15s; animation-iteration-count: 1 } @keyframes " + $"{userName}"+" { 0% {left: " + $"-{width}px" + ";transform: rotateY(180deg); } 100% { left: calc(100% + 600px); transform: rotateY(180deg);}}";
        string cssrtl = "#target { width: " + $"{width}px;" + " bottom: 0;  position: relative;  animation: linear infinite;  animation-name: run;  animation-duration: 15s;  animation-iteration-count: 1}@keyframes run {  0% {    left: 100%;  }  48% {    transform: rotateY(180deg);   }  100% {    left: -100%;         transform: rotateY(180deg);  }}";

        for (int t = 0; t < Length; t++)
        {
            body += "<img class=\"bildmed\" src=\"assets/images/the8bitS2.gif\" alt=\"body\">";
        }

        var sub = "<html lang=\"en\"> <body>" +
            "<div id=\""+$"{userName}"+"\">" +
                "<div>" +
                   "<img class=\"bildmed\" src=\"assets/images/the8bitS1.gif\" alt=\"Head\">" +
                   $"{body}" +
                   "<img class=\"bildmed\" src=\"assets/images/the8bitS3.gif\" alt=\"Tail\">" +
                "</div>" +
            "</div>" +
            "<style>" +
                ".bildmed { filter: " + $"hue-rotate({rot}deg) saturate({sat}%)" + "; }" +
                 $"{cssltr}" +
            "</body>" +
            "</style>";

        return new AlertDto() { Html = sub };
    }
}
