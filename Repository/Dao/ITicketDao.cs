﻿
using Ticket.Model;
using Ticket.Repository.Utils;

namespace Ticket.Repository.Dao;

public interface ITicketDao: ICommand<Tickets>, IQuery<Tickets>
{
    Show FindByShowId(int showId);

    List<Show> FindByShowName(string nome);
}
