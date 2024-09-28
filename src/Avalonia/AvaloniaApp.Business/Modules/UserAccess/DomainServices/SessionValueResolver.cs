using DotNet.Business.Modules.UserAccess.DTOs;
using DotNet.Data;
using DotNet.Data.Entities;

namespace DotNet.Business.Modules.UserAccess.DomainServices;

// Works in conjunction with ISessionValueProvider so that session data doesn't need to hold on to everything in the heap.
public class SessionValueResolver
{
    private readonly IDataAccess _dataAccess;
    private readonly ISessionValueProvider _sessionValueProvider;
    private readonly UserAccessMapper _userAccessMapper = new();

    public SessionValueResolver(IDataAccess dataAccess, ISessionValueProvider sessionValueProvider)
    {
        _dataAccess = dataAccess;
        _sessionValueProvider = sessionValueProvider;
    }

    public EndUserDto? GetSessionUser(params string[] relationships)
    {
        int? endUserId = _sessionValueProvider.GetUserID();
        if (endUserId == null)
        {
            return null;
        }

        EndUser? endUser = _dataAccess.ReadFiltered<EndUser>(eu => eu.EndUserId == endUserId, relationships).SingleOrDefault();
        if (endUser == null)
        {
            return null;
        }

        return _userAccessMapper.MapToDto<EndUserDto>(endUser);
    }

    public void SetSessionUserID(int userId)
    {
        _sessionValueProvider.SetUserID(userId);
    }
}
