# get-user-notification-state
Windows notification state query for .NET 10.

```csharp
if (SevenKeyboard.GetUserNotificationState.TryGet(out var state))
    Console.WriteLine(state);
```

Use the output only when the query succeeds. `state.ToString()` returns the native enum name.
