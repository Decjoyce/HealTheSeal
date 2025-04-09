using System;
using System.Collections;
using Unity.Notifications.Android;
using UnityEngine;

public class AndroidService : MonoBehaviour
{
    //TEMPP

    WaitForSecondsRealtime debug_delay = new WaitForSecondsRealtime(10f);

    IEnumerator DEBUGGINGNOTIFICATIONS()
    {
        bool tom = true;
        int ranNotif;
        while (tom)
        {
            yield return debug_delay;
            ranNotif = UnityEngine.Random.Range(0, 5);
            QueueBasicNotification(ConvertIndex_Notification(ranNotif), 0.1f);
        }
    }
    //TEMPPPPPPP

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(RequestNotificationPermission());
        var group = new AndroidNotificationChannelGroup()
        {
            Id = "Main",
            Name = "Main notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannelGroup(group);
        var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Default Channel",
            Importance = Importance.Default,
            Description = "Generic notifications",
            Group = "Main",  // must be same as Id of previously registered group
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);

        StartCoroutine(DEBUGGINGNOTIFICATIONS());
    }

    IEnumerator RequestNotificationPermission()
    {
        var request = new PermissionRequest();
        while (request.Status == PermissionStatus.RequestPending)
            yield return null;
        // here use request.Status to determine user's response
        Debug.Log("Permission: " + request.Status);
    }

    public void QueueBasicNotification(notif_types notification_type, float delay)
    {
        var notification = new AndroidNotification();
        notification.Title = ConvertNotification_Title(notification_type);
        notification.Text = ConvertNotification_Text(notification_type);
        notification.SmallIcon = ConvertNotification_Icon(notification_type);
        notification.FireTime = System.DateTime.Now.AddMinutes(delay);
        notification.Color = ConvertNotification_Color(notification_type);

        AndroidNotificationCenter.SendNotification(notification, "channel_id");
    }

    public void QueueNotification_Seal(Seal seal, notif_types notification_type, float delay)
    {
        var notification = new AndroidNotification();
        notification.Title = seal.seal_name;
        notification.Text = ConvertNotification_Text(notification_type);
        notification.SmallIcon = ConvertNotification_Icon(notification_type);
        notification.FireTime = System.DateTime.Now.AddMinutes(delay);
        notification.Color = ConvertNotification_Color(notification_type);

        AndroidNotificationCenter.SendNotification(notification, "channel_id");
    }


    string ConvertNotification_Title(notif_types notification_type)
    {
        switch (notification_type)
        {
            case notif_types.feed:
                return "Feed Seal";
            case notif_types.heal:
                return "Heal Seal";
            case notif_types.rescue:
                return "ALERT!";
            case notif_types.release:
                return "Me";
            default:
                return "INVALID NOTIFICATION TYPE: notify Declan";
        }
    }

    string ConvertNotification_Text(notif_types notification_type)
    {
        switch (notification_type)
        {
            case notif_types.feed:
                return "I'm hungry! :<{";
            case notif_types.heal:
                return "I'm ready for my treatment! :<{";
            case notif_types.rescue:
                return "A seal needs rescuing!!";
            case notif_types.release:
                return "I'm ready to go home.";
            default:
                return "INVALID NOTIFICATION TYPE: notify Declan";
        }
    }

    string ConvertNotification_Icon(notif_types notification_type)
    {
        switch (notification_type)
        {
            case notif_types.feed:
                return "icon_feed";
            case notif_types.heal:
                return "icon_heal";
            case notif_types.rescue:
                return "icon_rescue";
            case notif_types.release:
                return "icon_release";
            default:
                return "INVALID NOTIFICATION TYPE: notify Declan";
        }
    }

    Color ConvertNotification_Color(notif_types notification_type)
    {
        switch (notification_type)
        {
            case notif_types.feed:
                return Color.green;
            case notif_types.heal:
                return Color.red;
            case notif_types.rescue:
                return Color.red;
            case notif_types.release:
                return Color.blue;
            default:
                Debug.LogError("NOTIFICATION COLOUR NOT FOUND");
                return Color.black;
        }
    }

    notif_types ConvertIndex_Notification(int index)
    {
        switch (index)
        {
            case 0:
                return notif_types.feed;
            case 1:
                return notif_types.heal;
            case 2:
                return notif_types.rescue;
            case 3:
                return notif_types.release;
            default:
                return notif_types.release;
        }
    }
}

public enum notif_types
{
    feed,
    heal,
    rescue,
    release,
}