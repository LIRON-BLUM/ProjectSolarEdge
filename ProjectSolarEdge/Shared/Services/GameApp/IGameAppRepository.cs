using ProjectSolarEdge.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSolarEdge.Shared.Services.GameApp
{
    public interface IGameAppRepository
    {
        IEnumerable<UsersGameRecord> GetAllUsersGameRecordByGameID(int ID);

        IEnumerable<UsersGameRecord> GetAllUsersGameRecord();

    }
}
