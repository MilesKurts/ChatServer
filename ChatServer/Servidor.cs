using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

            if (statusHandler != null)
            {
                //invoca o delegate
                statusHandler(null,e);
            }
        }
        //envia msg
        public static void EnviaMensagemAdmin(string Mensagem)
        {
            StreamWriter swSenderSender;


            //Exibe primeiro na aplicação
            e = new StatusChangedEventArgs("ADM: " + Mensagem);
            OnStatusChanged(e);

            //CRIAR UM ARRAY DE CLIENTES TCPs DO TAMANHO DO NUMERO DE CLIENTES
            TcpClient[] tcpClients = new TcpClient[Servidor.htUsuarios.Count];
            //copia os objetos tcpCliente no array
            Servidor.htUsuarios.Values.CopyTo(tcpClients, 0);

            //percorre a lista de clientes TCP
            for (int i = 0; i < tcpClients.Length; i++)
            {
                //tenta mandar msg para cada cliente
                try
                {
                    //se a msg estiver em branco ou conexao nula.. sai..
                    if (Mensagem.Trim() == "" || tcpClients[i] == null)
                    {
                        continue;
                    }
                    //envia msg
                    swSenderSender = new StreamWriter(tcpClients[i].GetStream());
                    swSenderSender.WriteLine("Administrador: "+Mensagem);
                    swSenderSender.Flush();
                    swSenderSender = null;

                }
                catch
                {
                    //se houver problema, o usuario nao existe entao deve ser removido
                    RemoveUsuario(tcpClients[i]);


                }
            }
        }
        //envia msg para todos usuarios
        public static void EnviaMensagem(string Origem, string Mensagem)
        {
            StreamWriter swSenderSender;

            //Primeiro exibe a msg na aaplicação
            e = new StatusChangedEventArgs(Origem + " disse..: "+Mensagem);
            OnStatusChanged(e);

            //cria array de clientes TCPs DO TAMANHO DO NUMERO DE CLIENTES EXISTENTES
            TcpClient[] tcpClients = new TcpClient[Servidor.htUsuarios.Count];
            
            //copia os objetos TCPcliente no array
            Servidor.htUsuarios.Values.CopyTo (tcpClients, 0);

            //Percorre a lista de clientes TCP
            for (int i = 0; i < tcpClients.Length; i++)
            {
                //manda msg para cada cliente
                //tenta mandar msg para cada cliente
                try
                {
                    //se a msg estiver em branco ou conexao nula.. sai..
                    if (Mensagem.Trim() == "" || tcpClients[i] == null)
                    {
                        continue;
                    }
                    //envia msg
                    swSenderSender = new StreamWriter(tcpClients[i].GetStream());
                    swSenderSender.WriteLine("Administrador: " + Mensagem);
                    swSenderSender.Flush();
                    swSenderSender = null;

                }
                catch
                {
                    //se houver problema, o usuario nao existe entao deve ser removido
                    RemoveUsuario(tcpClients[i]);
                }
            }

        }

        public void IniciaAtendimento()
        {
            try
            {
                //pega IP
                IPAddress ipaLocal = enderecoIP;
                int portaLocal = portaHost;

                //clia um objeto TCP listener usando o IP do servidor e porta definidas
                tlsCliente = new TcpListener(ipaLocal, portaLocal);

                //Inicia o TCP listener e escuta as conexoes
                tlsCliente.Start();

                //o laço while verifica se o servidor esta rodando antes de checar as conexoes
                ServRodando = true;

                //Inicial uma nova tread que hospeda o listener
                thrListener = new Thread(MantemAtendimento);
                thrListener.IsBackground = true;
                thrListener.Start();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private void MantemAtendimento()
        {
            //enquanto o servidor estiver rodando 
            while (true)
            {
                //aceita uma conexao pendente 
                tcpCliente = tlsCliente.AcceptTcpClient();
                //cria uma nova instancia da conexao
                Conexao newConnection = new Conexao(tcpCliente);
            }
        }
    }
}
