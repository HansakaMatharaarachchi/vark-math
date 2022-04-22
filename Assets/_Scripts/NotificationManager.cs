using Unity.Notifications.Android;
public class NotificationManager
{
    public NotificationManager()
    {
        // creates a notification channel
        AndroidNotificationChannel androidNotificationChannel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Default Channel",
            Importance = Importance.Default,
            Description = "Generic notifications",
        };
        
        //registers the notification chanel to Android NotificationCenter
        AndroidNotificationCenter.RegisterNotificationChannel(androidNotificationChannel);
        
        //creates a notification
        AndroidNotification notification = new AndroidNotification
        {
            Title = "Your Title",
            Text = "Your Text",
            FireTime = System.DateTime.Now.AddMinutes(1)
        };
        
        //schedules the notification to send
        var notificationId = AndroidNotificationCenter.SendNotification(notification, "channel_id");

        if (AndroidNotificationCenter.CheckScheduledNotificationStatus(notificationId) == NotificationStatus.Scheduled)
        {
            AndroidNotificationCenter.CancelAllNotifications();
            AndroidNotificationCenter.SendNotification(notification, "channel_id"); 
        }
    }
}
