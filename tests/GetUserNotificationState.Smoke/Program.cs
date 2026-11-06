using System;
using System.Runtime.CompilerServices;
using SevenKeyboard;

string[] names =
[
    "QUNS_NOT_PRESENT",
    "QUNS_BUSY",
    "QUNS_RUNNING_D3D_FULL_SCREEN",
    "QUNS_PRESENTATION_MODE",
    "QUNS_ACCEPTS_NOTIFICATIONS",
    "QUNS_QUIET_TIME",
    "QUNS_APP"
];

if (Enum.GetUnderlyingType(typeof(UserNotificationState)) != typeof(int)
    || Unsafe.SizeOf<UserNotificationState>() != 4
    || Enum.GetValues<UserNotificationState>().Length != names.Length)
    throw new InvalidOperationException("Unexpected native enum layout.");

for (int i = 0; i < names.Length; i++)
    if (((UserNotificationState)(i + 1)).ToString() != names[i])
        throw new InvalidOperationException("Unexpected native enum value or name.");

Console.WriteLine($"Enum layout and names passed; process: {IntPtr.Size * 8}-bit.");
if (!GetUserNotificationState.TryGet(out UserNotificationState state))
{
    if ((int)state != 0)
        throw new InvalidOperationException("Failed query retained an output value.");

    Console.Error.WriteLine("SHQueryUserNotificationState failed; no state is available.");
    return 1;
}

Console.WriteLine($"State: {(int)state} ({state})");
return 0;
