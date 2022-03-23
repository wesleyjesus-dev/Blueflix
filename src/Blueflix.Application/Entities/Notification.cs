namespace Blueflix.Application.Entities
{
    public class Notification
    {

        public readonly IList<string> Notifications;

        public Notification()
        {
            Notifications = new List<string>();
        }
        public bool HasErros() => Notifications.Any();
    }
}
