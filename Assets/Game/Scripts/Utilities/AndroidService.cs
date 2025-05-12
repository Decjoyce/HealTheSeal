using System.Collections;
//using Unity.Notifications.Android;
using UnityEngine;

public class AndroidService : MonoBehaviour
{


    public static AndroidService instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of ANDROIDSERVICE found");
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
/*
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

    public void QueueNotification_Seal(Seal seal, notif_types notification_type, string delay)
    {
        var notification = new AndroidNotification();

        if (notification_type == notif_types.heal)
            notification.Title = ConvertNotification_Title(notification_type);
        else
            notification.Title = seal.seal_name + ":";

        notification.Text = ConvertNotification_Text(notification_type);
        notification.SmallIcon = ConvertNotification_Icon(notification_type);
        notification.FireTime = System.DateTime.Parse(delay);
        notification.Color = ConvertNotification_Color(notification_type);

        AndroidNotificationCenter.SendNotification(notification, "channel_id");
    }

    public void QueueNotification_BigPicture(Seal seal, notif_types notification_type, float delay)
    {
        var notification = new AndroidNotification();
        notification.Title = seal.seal_name + ":";
        notification.Text = ConvertNotification_Text(notification_type);
        notification.BigPicture = new BigPictureStyle()
        {
            Picture = ConvertNotification_Icon(notification_type),
        };
        notification.FireTime = System.DateTime.Now.AddMinutes(delay);
        notification.Color = ConvertNotification_Color(notification_type);

        AndroidNotificationCenter.SendNotification(notification, "channel_id");
    }

    string ConvertNotification_Title(notif_types notification_type)
    {
        switch (notification_type)
        {
            case notif_types.feed:
                return ":";
            case notif_types.heal:
                return ":";
            case notif_types.rescue:
                return "ALERT:";
            case notif_types.release:
                return ":";
            default:
                return "INVALID NOTIFICATION TYPE: notify Declan";
        }
    }

    string ConvertNotification_Text(notif_types notification_type)
    {
        switch (notification_type)
        {
            case notif_types.feed:
                return "I'm hungry!";
            case notif_types.heal:
                return "I'm ready for my medicine!";
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
    }*/
}

public enum notif_types
{
    feed,
    heal,
    rescue,
    release,
}