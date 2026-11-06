//==============================================================
// GetUserNotificationState — SHQueryUserNotificationState wrapper + enum name helper
//
// GitHub: https://github.com/SevenKeyboard/get-user-notification-state
// Author: SevenKeyboard Ltd. (2026)
// License: The Unlicense
//==============================================================

using System.Runtime.InteropServices;

namespace SevenKeyboard;

// Native QUERY_USER_NOTIFICATION_STATE is a 32-bit enum on both x86 and x64.
// https://learn.microsoft.com/en-us/windows/win32/api/shellapi/ne-shellapi-query_user_notification_state
public enum UserNotificationState : int
{
    QUNS_NOT_PRESENT = 1,
    QUNS_BUSY = 2,
    QUNS_RUNNING_D3D_FULL_SCREEN = 3,
    QUNS_PRESENTATION_MODE = 4,
    QUNS_ACCEPTS_NOTIFICATIONS = 5,
    QUNS_QUIET_TIME = 6,
    QUNS_APP = 7
}

public static partial class GetUserNotificationState
{
    /// <summary>Reads the current user's notification state. On failure, state is zero and must be ignored.</summary>
    public static bool TryGet(out UserNotificationState state)
    {
        // This API documents S_OK (0) as success; HRESULT is a signed 32-bit value.
        // https://learn.microsoft.com/en-us/windows/win32/api/shellapi/nf-shellapi-shqueryusernotificationstate
        int result = SHQueryUserNotificationState(out state);
        if (result == 0)
            return true;

        state = default;
        return false;
    }

    [LibraryImport("shell32.dll")]
    private static partial int SHQueryUserNotificationState(out UserNotificationState state);
}
