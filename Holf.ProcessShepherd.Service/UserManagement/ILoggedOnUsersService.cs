using System.Collections.Generic;

namespace Holf.ProcessShepherd.Service
{
	public interface ILoggedOnUsersService
    {
        List<UsernameAndSessionId> GetUsernamesAndSessionIds();
    }
}
