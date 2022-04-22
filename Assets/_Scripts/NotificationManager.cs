using System;
using Unity.Notifications.Android;
public class NotificationManager
// only supports for android devices
{
    public NotificationManager()
    {
        // creates a notification channel to send notifications
        AndroidNotificationChannel androidNotificationChannel = new AndroidNotificationChannel()
        {
            Id = "channel_01",
            Name = "Default Channel",
            Importance = Importance.High,
            Description = "Generic notifications",
        };
        
        //registers the notification chanel to Android NotificationCenter
        AndroidNotificationCenter.RegisterNotificationChannel(androidNotificationChannel);
        
        //creates a notification for Daily Rewards
        AndroidNotification dailyRewardNotification = new AndroidNotification
        {
            Title = "Hey!!! Daily Rewards Are Now Available",
            Text = "collect daily rewards",
            // FireTime = System.DateTime.Today.AddDays(1), // 1 day
            FireTime = System.DateTime.Now.AddMinutes(1),
            RepeatInterval = new TimeSpan(0, 1, 0)
        };
        
        //creates a notification for Play Reminder
        AndroidNotification playReminderNotification = new AndroidNotification
        {
            Title = "Come back!!!",
            Text = "Your Text",
            FireTime = System.DateTime.Now.AddHours(2) // 2 hours
        };
        
        //schedule the notification to send
        var notificationId = AndroidNotificationCenter.SendNotification(dailyRewardNotification, "channel_01");

        if (AndroidNotificationCenter.CheckScheduledNotificationStatus(notificationId) == NotificationStatus.Scheduled)
        {
            AndroidNotificationCenter.CancelAllNotifications();
            AndroidNotificationCenter.SendNotification(dailyRewardNotification, "channel_01"); 
        }
    }
}
