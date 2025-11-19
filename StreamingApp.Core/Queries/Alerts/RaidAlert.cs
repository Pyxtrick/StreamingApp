using Microsoft.IdentityModel.Tokens;
using StreamingApp.Core.Queries.Alerts.Interfaces;
using StreamingApp.Domain.Entities.Dtos.Twitch;

namespace StreamingApp.Core.Queries.Alerts;

public class RaidAlert : IRaidAlert
{
    public async Task<AlertDto> Execute(int count, string image)
    {
        bool isEmpty = image.IsNullOrEmpty();

        var animationDuration = 10;

        var raid = "<div class=\"raid\">";
        var style = "";

        for (int i = 0; i < count; i++)
        {
            if (isEmpty)
            {
                string[] strings = { "pyxtriSnekWiggle.gif", "peak.png" };
                image = strings[new Random().Next(strings.Length)];
            }
            var delay = new Random().Next(0, 5);
            var duration = new Random().Next(5, animationDuration);
            var height = new Random().Next(100, 300);
            var position = new Random().Next(0, 95);

            style += $".gif{i} {{ animation-delay: {delay}s; animation: gif{i} {duration}s linear 1; height: {height}px; margin-left: {position}%; margin-top: 100vh;}}" +
                     $"@keyframes gif{i} {{ 0% {{ top: 0px; }} 50% {{ top: -{height}px; }} 100% {{ top: 0px; }} }}";


            var img = $"<img class=\"gif gif{i}\" src=\"assets/Stream/{image}\">";

            raid += img;
        }

        raid += $"<style>{style} .gif {{ display: inline-block; font-size: 20px; position: fixed;}}</style</div>";

        return new AlertDto() { Html = raid, Duration = animationDuration };
    }
}
