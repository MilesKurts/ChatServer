using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatServer
{
    //este delegate é necessário para especificar os parametros que estamos passando com o nosso evento
    public delegate void StatusChangedEventHandler(object sender, StatusChangedEventArgs e);
        
    class Servidor
    {
        //Esta hash table armazena os usuarios e as conexões(acessado/consultado por usuario)
        public static Hashtable htUsuarios = new Hashtable(30);//30 usuarios é o limite definido
        //este hash table armazena os usuarios e as conexoes(acessad/consultada por conexão)
        public static Hashtable htConexoes = new Hashtable(30);
        //armazena o ip passado

        private IPAddress enderecoIP;
        private int portaHost;
        private TcpClient tcpCliente;

        public static event StatusChangedEventHandler StatusChanged;
        public static StatusChangedEventArgs e;

        public Servidor (IPAddress endereco, int porta)
        {
            enderecoIP = endereco;
            portaHost = porta;

        }
        //A thread que ira tratar o escutador de conexoes
        private Thread thrListener;

        //o objeto TCP object que escuta as conexoes
        private TcpListener tlsCliente;

        //Ira dizer ao laço while para manter monitoramento das conexoes
        bool ServRodando=false;

        public static void IncluirUsuario(TcpClient tcpUsuario, string strUsername)
        {
            //Primeiro inclui o nome e conexão associada para ambas as hash tables
            Servidor.htUsuarios.Add(strUsername, tcpUsuario);
            Servidor.htConexoes.Add(tcpUsuario,strUsername);

            //INforma a nova conexao para todos os usuarios
            EnviaMensagemAdmin(htConexoes[tcpUsuario] + " Entrou...");

        }
        //Remove o usuario
        public static void RemoveUsuario(TcpClient tcpUsuario)
        {
            //Se o usuario Existir
            if (htConexoes[tcpUsuario] != null)
            {
                EnviaMensagemAdmin(htConexoes[tcpUsuario]+" Saiu...");

                //Remove o usuario da hash table
                Servidor.htUsuarios.Remove(Servidor.htConexoes[tcpUsuario]);
                Servidor.htConexoes.Remove(tcpUsuario);
            } ;
        }

        //Evento chamado para disparar o evento StatusChanged
        public static void OnStatusChanged(StatusChangedEventArgs e)
        {
            StatusChangedEventHandler statusHandler =StatusChanged;
        }
        public static void EnviaMensagemAdmin(string Mensagem)
        {

        }
    }
}
