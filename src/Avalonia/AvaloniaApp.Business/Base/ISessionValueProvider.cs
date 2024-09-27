using System;

namespace AvaloniaApp.Business;

// Let presentation tier implement this and manage its session data how it's best suited to.
// Meant to work in conjunction with ISessionValueResolver so that the session doesn't need to care about data access or hold anything in the heap.
public interface ISessionValueProvider
{
    int? GetUserID();
    void SetUserID(int id);
}
