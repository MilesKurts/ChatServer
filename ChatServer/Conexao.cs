using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatServer
{
    //Classe trata as conexoes. serao tantas quanto as instancias do usuarios conectados
    class Conexao
    {
        TcpClient tcpClient;

        //a thread que ira enviar a informação para o cliente

        private Thread thrSender;
        private StreamReader srReceptor;
        private StreamWriter swEnviador;
        private string usuarioAtual;
        private string strResposta;


        //o construtor da classe que toma conexao TCP
        public Conexao(TcpClient tcpCon)
        {
            tcpClient = tcpCon;
            //a thread que aceita o cliente e espera mensagem

            thrSender = new Thread(AceitaCliente);
            thrSender.IsBackground = true;
            //a thread chama o método AceitaCliente
            thrSender.Start();
        }

        private void FechaConexao()
        {
            //fecha os objetos abertos
            tcpClient.Close();
            srReceptor.Close();
            swEnviador.Close();
        }
        private void AceitaCliente()
        {
            srReceptor = new StreamReader(tcpClient.GetStream());
            swEnviador = new StreamWriter(tcpClient.GetStream());

            //le as informações da conta do cliente
            usuarioAtual = srReceptor.ReadLine();

            //temos uma resposta do cliente
            if (usuarioAtual != "")
            {
                //armazena o nome do usuario na hash table
                if (Servidor.htUsuarios.Contains(usuarioAtual))
                {
                    //0=> significa não conectado
                    swEnviador.WriteLine("0| Este usuario ja existe.");
                    swEnviador.Flush();
                    FechaConexao(); ;
                    return;
                }else if(usuarioAtual == "Administrador")
                {
                    //0 => não conectado
                    swEnviador.WriteLine("0|Este nome de usuário é reservado.");
                    swEnviador.Flush();
                    FechaConexao(); return;
                }
                else
                {
                    //1=> conectou com sucesso
                    swEnviador.WriteLine("1");
                    swEnviador.Flush();

                    //inclui o usuário na hash table e inicia a escuta de suas mensagens
                    Servidor.IncluirUsuario(tcpClient,usuarioAtual);

                }
            }
            else
            {
                FechaConexao();
                return;
            }
            try
            {
                //continua aguardando msg do usuário
                while ((strResposta = srReceptor.ReadLine()) != "") ;
                {
                    //se for invalido remove-o
                    if(strResposta ==null){
                        Servidor.RemoveUsuario(tcpClient);
                    
                }
                    else
                    {
                        //envia msg para todos os outros usuarios
                        Servidor.EnviaMensagem(usuarioAtual, strResposta);
                    }
                }
            }
            catch (Exception ex)
            {
                //se houve um problema com esse usuário desconecta-o
                Servidor.RemoveUsuario(tcpClient);

            }
        }
    }
}
