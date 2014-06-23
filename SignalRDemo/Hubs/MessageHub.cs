using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Web.Script.Serialization;

namespace SignalRDemo.Hubs
{
    public class MessageHub : Hub
    {
        public void Hello(string msg)
        {
            Clients.All.hello(msg);
        }

        public void Post(string user)
        {
            var o = new Ticket();
            o.Name = user;
            o.Description = "This is a new ticket";
            o.TicketDate = DateTime.Now;
            o.TicketNumber = Guid.NewGuid().ToString();

            Clients.All.NewTicket(o);
        }

        public void Send(string o)
        {
            var serializer = new JavaScriptSerializer();
            dynamic item = serializer.Deserialize<object>(o);

            var s = "A new ticket has been created for " + item["name"];

            Clients.All.hello(s);
        }
    }

    public class Ticket
    {
        public string Name { get; set; }
        public string TicketNumber { get; set; }
        public string Description { get; set; }
        public DateTime TicketDate { get; set; }
    }
}