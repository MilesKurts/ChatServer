using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    //trata os argumentos para o evendo statusChanged
    public class StatusChangedEventArgs : EventArgs 
    {
        //Estamos interessados na mensagem descrevendo o evento
        private string EventMsg;

        // Propiedade para retornar e definir uma mensagem do evento
        public string EventMessage
        {
            get { return EventMsg; }
            set { EventMsg = value; }
        }
        //consultor para definir a mensagem do evento
        public StatusChangedEventArgs(string strEventMsg)
        {
            EventMsg = strEventMsg;


        }
    }
}
