using System;
using Unity.Notifications.Android;

namespace _Scripts
{
    public class NotificationManager
        // only supports for android devices
    {
        private AndroidNotificationChannel androidNotificationChannel;

        public NotificationManager()
        {
            // creates a notification channel to send notifications
            androidNotificationChannel = new AndroidNotificationChannel()
            {
                Id = "channel_01",
                Name = "Default Channel",
                Importance = Importance.High,
                Description = "Generic notifications",
            };

            //registers the notification chanel to Android NotificationCenter
            AndroidNotificationCenter.RegisterNotificationChannel(androidNotificationChannel);
            ScheduleNotifications();
        }

        private void ScheduleNotifications()
        {
            //creates a notification for Daily Rewards
            AndroidNotification dailyRewardNotification = new AndroidNotification
            {
                Title = "Hey!!! Daily Rewards Are Now Available",
                Text = "collect daily rewards",
                FireTime = DateTime.Today.AddDays(1), // 1 day
            };

            //creates a notification for Play Reminder
            AndroidNotification playReminderNotification = new AndroidNotification
            {
                Title = "Come back!!!",
                Text = "Space journey awaits for you, come lets explore",
                FireTime = DateTime.Now.AddHours(2), // 2 hours
                RepeatInterval = new TimeSpan(2, 0, 0)
                //todo improvement - trigger according to the playtime
            };

            //schedule the notification to send
            int rewardNotificationId =
                AndroidNotificationCenter.SendNotification(dailyRewardNotification, "channel_01");
            int playNotificationId = AndroidNotificationCenter.SendNotification(dailyRewardNotification, "channel_01");

            if ((AndroidNotificationCenter.CheckScheduledNotificationStatus(rewardNotificationId) ==
                 NotificationStatus.Scheduled) &&
                (AndroidNotificationCenter.CheckScheduledNotificationStatus(playNotificationId) ==
                 NotificationStatus.Scheduled))
            {
                AndroidNotificationCenter.CancelAllNotifications();
                AndroidNotificationCenter.SendNotification(dailyRewardNotification, "channel_01");
                AndroidNotificationCenter.SendNotification(playReminderNotification, "channel_01");
            }
        }
    }
}