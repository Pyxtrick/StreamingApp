using StreamingApp.Domain.Entities.Dtos.Twitch;

namespace StreamingApp.Core.Queries.Alerts;

public class SubAlertLoong : ISubAlertLoong
{
    public async Task<AlertDto> Execute(string userName, int Length)
    {
        int rot = new Random().Next(1, 360);
        int sat = new Random().Next(1, 400);

        string body = "";

        int width = (56 * Length) + (2 * 56);

        //Image Colour Change: https://stackoverflow.com/questions/7415872/change-color-of-png-image-via-css

        string cssltr = $"#{userName}"+" {\r\n\t  width:" + $"{width}px" + ";\r\n\t  bottom: 0;\r\n\t  position: relative;\r\n\t  animation: linear infinite;\r\n\t  animation-name: "+$"{userName}"+";\r\n\t  animation-duration: 15s;\r\n\t  animation-iteration-count: 1\r\n\t}\r\n\t@keyframes " + $"{userName}"+" {\r\n\t  0% {\r\n\t\tleft: " + $"-{width}px" + ";\r\n\t\ttransform: rotateY(180deg);\r\n\t  }\r\n\t  100% { \r\n\t\tleft: calc(100% + 600px);\r\n\t\ttransform: rotateY(180deg);\r\n\t  }\r\n\t}";
        string cssrtl = "#target {\r\n width: " + $"{width}px;" + " bottom: 0;\r\n  position: relative;\r\n  animation: linear infinite;\r\n  animation-name: run;\r\n  animation-duration: 15s;\r\n  animation-iteration-count: 1\r\n}\r\n@keyframes run {\r\n  0% {\r\n    left: 100%;\r\n  }\r\n  48% {\r\n    transform: rotateY(180deg); \r\n  }\r\n  100% {\r\n    left: -100%;    \r\n     transform: rotateY(180deg);\r\n  }\r\n}";

        for (int t = 0; t < Length; t++)
        {
            body += "<img class=\"bildmed\" src=\"assets/images/the8bitS2.gif\" alt=\"body\">";
        }

        var sub = "<html lang=\"en\">\r\n <body>" +
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
