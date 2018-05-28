using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EfinnestParty.Models;

namespace EfinnestParty.Models
{
    public class Repository
    {
        public static List<Invitees> invites = new List<Invitees>();
        public static IEnumerable<Invitees> Responses
        {
            get
            {
                return invites;
            }
        }
        public static void AddInvites(Invitees invite)
        {
            invites.Add(invite);
        }
    }
}
